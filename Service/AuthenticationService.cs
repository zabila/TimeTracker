using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

public sealed class AuthenticationService : IAuthenticationService {
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;

    private User? _user;

    public AuthenticationService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration) {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;

        _jwtSettings = new JwtSettings();
        _configuration.Bind(nameof(JwtSettings), _jwtSettings);
    }

    public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration) {
        var user = _mapper.Map<User>(userForRegistration);
        var result = await _userManager.CreateAsync(user, userForRegistration.Password!);

        if (result.Succeeded) {
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles!);
        }

        return result;
    }

    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth) {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName!);
        var result = _user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password!);
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong password.");
        return result;
    }

    public async Task<string> CreateToken() {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(await signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private Task<SigningCredentials> GetSigningCredentials() {
        var key = Encoding.UTF8.GetBytes(_jwtSettings.AccessTokenSecret!);
        var secret = new SymmetricSecurityKey(key);
        return Task.FromResult(new SigningCredentials(secret, SecurityAlgorithms.HmacSha256));
    }

    private async Task<List<Claim>> GetClaims() {
        Debug.Assert(_user!.UserName != null, "_user.UserName != null");
        var claims = new List<Claim> {
            new(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims) {
        var tokenOptions = new JwtSecurityToken
        (
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.AccessTokenExpirationMinutes!)),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}