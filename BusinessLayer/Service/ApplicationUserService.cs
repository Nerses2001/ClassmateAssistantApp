using AutoMapper;
using BusinessLayer.IService;
using DataLayer.IRepository;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService
            (IApplicationUserRepository repository, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._configuration = configuration;
            this._userManager = userManager;
        }

        public async Task CreateUserAsync(ApplicationUserDTO user)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            await _repository.CreateUserAsync(appUser);
        }

        public async Task DeleteUserAsync(string userId)
        {
            await _repository.DeleteUserAsync(userId);
        }

        public void DeleteUserByEamil(string email)
        {
            _repository.DeleteUserByEamil(email);
        }

        public async Task DeleteUserByEamilAsync(string email)
        {
            await _repository.DeleteUserByEamilAsync(email);
        }

        public void DeleteUserById(string userId)
        {
            _repository.DeleteUserById(userId);
        }

        public async Task<ICollection<ApplicationUserDTO>> GetAllUsersAsync()
        {
            var appUsers = await _repository.GetAllUsersAsync();
            return _mapper.Map<ICollection<ApplicationUserDTO>>(appUsers);
        }

        public ApplicationUserDTO GetUserByEmail(string email)
        {
            var appUser = _repository.GetUserByEmail(email);
            return _mapper.Map<ApplicationUserDTO>(appUser);
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string email)
        {
            var appUser = await _repository.GetUserByEmailAsync(email);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public ApplicationUserDTO GetUserById(string userId)
        {
            var appUser = _repository.GetUserById(userId);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public async Task<ApplicationUserDTO> GetUserByIdAsync(string userId)
        {
            var appUser = await _repository.GetUserByIdAsync(userId);
            return _mapper.Map<ApplicationUserDTO>(appUser);

        }

        public async Task UpdateUserAsync(ApplicationUserDTO user)
        {
            var appUser = _mapper.Map<ApplicationUser>(user);
            await _repository.UpdateUserAsync(appUser);
        }

        private string generateJwtToken(ApplicationUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim("id", user.Id), 
                    new Claim("access", user.AccessToken!), 
                    new Claim(ClaimTypes.Email, user.Email) 
                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public async Task<UserManagerResponse> LoginUserAsync(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !user.EmailConfirmed)
                return new UserManagerResponse
                {
                    Message = user == null ? "InvalidUser" : "ActivateEmail",
                    IsSuccess = false
                };

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return new UserManagerResponse
                {
                    Message = "PasswordInvalid",
                    IsSuccess = false
                };


            #region NotNeed

            //var jwt = _jwtHandler.Create(user.UserName);
            //var refreshToken = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
            //    .Replace("+", string.Empty)
            //    .Replace("=", string.Empty)
            //    .Replace("/", string.Empty);
            //jwt.RefreshToken = refreshToken;
            //_refreshTokens.Add(new RefreshToken { Username = username, Token = refreshToken });

            //var claims = new[]
            //{
            //    new Claim("Email", model.Email),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id),
            //};

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //var token = new JwtSecurityToken(
            //    issuer: _configuration["Jwt:Issuer"],
            //    audience: _configuration["Jwt:Issuer"],
            //    claims: claims,
            //    expires: DateTime.Now.AddDays(30),
            //    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            //string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            #endregion

            string token = generateJwtToken(user);

            return new UserManagerResponse
            {
                Message = token,
                IsSuccess = true,
            };

        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterModel model)
        {
            if(model == null)
                throw new NullReferenceException("NotFound");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "PasswordMissMatch",
                    IsSuccess = false
                };

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                AccessToken = Guid.NewGuid().ToString()
                                            .Replace("+", string.Empty)
                                            .Replace("=", string.Empty)
                                            .Replace("/", string.Empty),
                PhoneNumber = model.PhoneNumber,
                DateOfBirth = model.DateOfBirth,
                Address = model.Address,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                sendEmail(model.Email, "Test", "User created successfully");
                return new UserManagerResponse
                {
                    Message = "User created successfully!",
                    IsSuccess = true,
                };

            }
            return new UserManagerResponse
            {
                Message = "SomethingWrong",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };


        }

        private void sendEmail(string toAddress, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("dotnet.test2001@gmail.com", "DotnetTest$$$"),
                    EnableSsl = true,
                };

                MailAddress fromAddress = new("dotnet.test2001@gmail.com", "dotnet");
                MailAddress toAddressObj = new(toAddress);

                MailMessage mailMessage = new(fromAddress, toAddressObj)
                {
                    Subject = subject,
                    Body = body,
                };

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }

        public ApplicationUser GetById(string userId)
        {
            return _userManager.Users.Include(i => i.UserName).FirstOrDefault(x => x.Id == userId);
        }
    }
}
