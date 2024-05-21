using Dapper;
using ModelLayer.Exceptions;
using ModelLayer.UserRegistration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserServices: UserInterface
    {
        private readonly DapperContext Context;
        private readonly IAuthServiceRL _authService;
        public UserServices(DapperContext context, IAuthServiceRL authService)
        {
            Context = context;
            _authService = authService;
        }
        public bool IsValidEmail(string email)
        {

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        public async Task<bool> RegisterUser(UserRegistrationModel userRegModel)
        {

            var parametersToCheckEmailIsValid = new DynamicParameters();
            parametersToCheckEmailIsValid.Add("Email", userRegModel.Email, DbType.String);

            var querytoCheckEmailIsNotDuplicate = @"SELECT COUNT(*)FROM Users WHERE Email = @Email;";



            var query = @"INSERT INTO Users (Name, Email, Password,MobileNumber)VALUES (@Name, @Email, @Password,@MobileNumber);";


            var parameters = new DynamicParameters();
            parameters.Add("Name", userRegModel.Name, DbType.String);
            parameters.Add("MobileNumber", userRegModel.MobileNumber, DbType.Int64);



            if (!IsValidEmail(userRegModel.Email))
            {
                throw new InvalidEmailFormatException("Invalid email format");
            }

            parameters.Add("Email", userRegModel.Email, DbType.String);


            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegModel.Password);

            parameters.Add("Password", hashedPassword, DbType.String);

            using (var connection = Context.CreateConnection())
            {


                bool tableExists = await connection.QueryFirstOrDefaultAsync<bool>(
                    @"
                    SELECT COUNT(*)
                    FROM INFORMATION_SCHEMA.TABLES
                    WHERE TABLE_NAME = 'Users';
                     "
                );


                if (!tableExists)
                {
                    await connection.ExecuteAsync(
                                                    @" CREATE TABLE Users (
                                                             UserId INT IDENTITY(1, 1) PRIMARY KEY,
                                                             FirstName NVARCHAR(100) NOT NULL,
                                                             LastName NVARCHAR(100) NOT NULL,
                                                             Email NVARCHAR(100) UNIQUE NOT NULL,
                                                             Password NVARCHAR(100) UNIQUE NOT NULL )"
                                                 );
                }


                bool emailExists = await connection.QueryFirstOrDefaultAsync<bool>(querytoCheckEmailIsNotDuplicate, parametersToCheckEmailIsValid);

                if (emailExists)
                {
                    throw new DuplicateEmailExceptions("Email address is already in use");
                }


                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }
        public async Task<string> UserLogin(UserLoginModel userLogin)
        {

            var parameters = new DynamicParameters();
            parameters.Add("Email", userLogin.Email);



            string query = @"SELECT * FROM Users WHERE Email = @Email;";


            using (var connection = Context.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<UserEntity>(query, parameters);

                if (user == null)
                {
                    throw new NotFoundException($"User with email '{userLogin.Email}' not found.");
                }

                if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
                {
                    throw new InvalidPasswordException($"User with Password '{userLogin.Password}' not Found.");
                }

                //if password enterd from user and password in db match then generate Token 
                var token = _authService.GenerateJwtToken(user);
                return token;
            }
        }
    }
}
