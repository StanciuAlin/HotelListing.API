using AutoMapper;
using HotelListing.Api.Common;
using HotelListing.Api.Contracts;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Users;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.Api.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        public async Task<string> CreateRefreshToken()
        {
            // Removes existing refresh token in Identity table
            await _userManager.RemoveAuthenticationTokenAsync(_user,
                IdentitySettings.LoginProvider,
                JwtUtils.RefreshToken);

            var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user,
                IdentitySettings.LoginProvider,
                JwtUtils.RefreshToken);

            var result = await _userManager.SetAuthenticationTokenAsync(_user,
                IdentitySettings.LoginProvider,
                JwtUtils.RefreshToken,
                newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            // Set the global user variable
            _user = await _userManager.FindByEmailAsync(loginDto.Email);
            var isValidUser = await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            
            if (_user == null || !isValidUser)
            {
                return null;
            }

            var token = await GenerateToken();
            return new AuthResponseDto
            {
                Token = token,
                UserId = _user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            _user = _mapper.Map<ApiUser>(userDto);
            userDto.IsAdmin = false; // For future use
            _user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            // If the user is created successfully, add the user to the User role
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, UserRoles.User);
            }

            // If the user is created successfully, the result.Errors will be empty
            // If the user is not created successfully, the result.Errors will contain the error messages
            return result.Errors;
        }

        public async Task<IEnumerable<IdentityError>> RegisterAdmin(ApiUserDto userDto)
        {
            _user = _mapper.Map<ApiUser>(userDto);
            userDto.IsAdmin = true; // For future use
            _user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(_user, userDto.Password);

            // If the user is created successfully, add the user to the User role
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(_user, UserRoles.Administrator);
            }

            // If the user is created successfully, the result.Errors will be empty
            // If the user is not created successfully, the result.Errors will contain the error messages
            return result.Errors;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
            var userName = tokenContent.Claims.ToList().FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await _userManager.FindByEmailAsync(userName);

            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }

            var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user,
                IdentitySettings.LoginProvider,
                JwtUtils.RefreshToken,
                request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            // Regenerating a security stamp will sign out any saved login for the user
            await _userManager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(_user);
            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            // If we store the user claim, get it. In our case, we don't store the user claim
            var userClaims = await _userManager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email), // Subject, is the person to whom the token is issued
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Used to prevent playback attacks
                new Claim(JwtRegisteredClaimNames.Email, _user.Email),
                new Claim("uid", _user.Id) // Not recommeded to store the user id in a string, use a claim type!
            }
            .Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
