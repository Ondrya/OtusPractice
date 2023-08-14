using System;
using Dapper;
using System.Threading.Tasks;
using OtusPractice.Domain.Models;

namespace OtusPractice.WebApiCore.Services
{
    public class UserService : IUserService
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;
        private readonly Generator _generator;

        public UserService(SqlConnectionFactory sqlConnectionFactory, Generator generator)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _generator = generator;
        }


        public async Task<string> Register(User user)
        {
            using var connection = _sqlConnectionFactory.Create();

            user.Id = Guid.NewGuid().ToString();
            if (string.IsNullOrWhiteSpace(user.Login))
                user.Login = _generator.Login();
            if (string.IsNullOrWhiteSpace(user.Password))
                user.Password = _generator.Password();

            const string sql =
@"
    INSERT INTO Users(
          Id
        , FirstName
        , SecondName
        , Age
        , Birthdate
        , Biography
        , City
        , Sex
        , Login
        , Password)
    VALUES (
          @Id
        , @FirstName
        , @SecondName
        , @Age
        , @Birthdate
        , @Biography
        , @City
        , @Sex
        , @Login
        , @Password);
";
            try
            {
                await connection.ExecuteAsync(sql, user);
                return user.Id;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                throw;
            }
        }


        public async Task<User> GetById(string id)
        {
            using var connection = _sqlConnectionFactory.Create();

            const string sql = @"select * from Users where id = @UserId";

            var user = await connection.QuerySingleOrDefaultAsync<User>(
                sql,
            new { UserId = id });

            return user;
        }


        public async Task<User> GetByLoginAsync(string userLogin)
        {
            using var connection = _sqlConnectionFactory.Create();

            const string sql = @"select * from Users where login = @login";

            var user = await connection.QuerySingleOrDefaultAsync<User>(
                sql,
            new { login = userLogin });

            return user;
        }
    }
}
