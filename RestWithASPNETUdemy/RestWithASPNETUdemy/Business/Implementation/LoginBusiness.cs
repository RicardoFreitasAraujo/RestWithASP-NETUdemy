using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Security.Configuration;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly IUserRepository _repository;
        private SigningConfigurations _siginingConfigurations;
        private TokenConfiguration _tokenConfigurations;

        public LoginBusiness(IUserRepository repository, SigningConfigurations siginingConfigurations, TokenConfiguration tokenConfigurations)
        {
            this._repository = repository;
            this._tokenConfigurations = tokenConfigurations;
            this._siginingConfigurations = siginingConfigurations;
        }

        public BinaryReader JwtRegisteredClaimsNames { get; private set; }

        public User FindByLogin(string login)
        {
            return this._repository.FindByLogin(login);
        }

        public object FindByLogin(User user)
        {
            bool credentialsIsValid = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = this._repository.FindByLogin(user.Login);
                credentialsIsValid = (baseUser != null
                                      && user.Login == baseUser.Login
                                      && user.AcessKey == baseUser.AcessKey);
            }

            if (credentialsIsValid)
            {
                #region Geração de Claims
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                    new []
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                );
                #endregion

                DateTime createDate = System.DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate,token);
            } else
            {
                return ExceptionObject();
            }
            
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor() {
                Issuer  = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = this._siginingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
