using Dapper;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OtusPractice.WebApiCore.Services
{
    public class InitDbService : IHostedService
    {
        private readonly SqlConnectionFactory _sqlConnectionFactory;


        public InitDbService(SqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // create tables if they don't exist
            using var connection = _sqlConnectionFactory.Create();
            await _initUsers();


            async Task _initUsers()
            {
                var sql =
    @"
        CREATE TABLE IF NOT EXISTS `Users` (
          `Id` varchar(36) COLLATE utf8mb4_unicode_ci NOT NULL,
          `FirstName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
          `SecondName` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
          `Age` int(11) DEFAULT NULL,
          `Birthdate` date DEFAULT NULL,
          `Biography` text COLLATE utf8mb4_unicode_ci,
          `City` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
          `Sex` int(11) DEFAULT NULL,
          `login` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
          `password` text COLLATE utf8mb4_unicode_ci NOT NULL
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

        ALTER TABLE `Users`
          ADD UNIQUE KEY `login` (`login`);
        COMMIT;
    ";


                try
                {
                    await connection.ExecuteAsync(sql);
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.ToString());
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
