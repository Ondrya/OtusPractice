using Bogus;
using Bogus.DataSets;
using Newtonsoft.Json;
using OtusPractice.Domain.Models;
using OtusPractice.WebApiCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace OtusPractice.WebApiCore.Services
{
    public class Generator
    {
        private readonly Faker _faker;
        private readonly string _locale = "ru";


        public Generator()
        {
            _faker = new Faker(_locale);
        }

        public string Password() => _faker.Internet.Password();
        public string Login() => _faker.Internet.UserName();


        /// <summary>
        /// Сгенерировать пакет тестовых пользователей
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<User>> CreateUsers(int count)
        {
            var testUsers = new Faker<User>(_locale)

                .RuleFor(u => u.Sex, f => (int)f.PickRandom<Gender>())
                //.RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
                .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName((Gender)u.Sex))
                .RuleFor(u => u.SecondName, (f, u) => f.Name.LastName((Gender)u.Sex))
                .RuleFor(u => u.Login, (f, u) => f.Internet.UserName())   //f.Internet.Email(u.FirstName, u.SecondName))
                .RuleFor(u => u.Password, (f, u) => f.Internet.Password())
                .RuleFor(u => u.City, (f) => f.Address.City())

                .FinishWith((f, u) =>
                {
                    //Console.WriteLine($"{u.Id} User {u.Login} => {u.FirstName} {u.SecondName} created Credentials: {u.Login} {u.Password}");

                    Console.WriteLine(JsonConvert.SerializeObject(u, Formatting.Indented));
                });

            var users = testUsers.Generate(count);


            return users;
        }


        //var testUsers = new Faker<User>()
        ////Optional: Call for objects that have complex initialization
        //.CustomInstantiator(f => new User())

        ////Use an enum outside scope.
        //.RuleFor(u => u.Gender, f => f.PickRandom<Gender>())

        ////Basic rules using built-in generators
        //.RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(u.Gender))
        //.RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender))
        //.RuleFor(u => u.Avatar, f => f.Internet.Avatar())
        //.RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
        //.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
        //.RuleFor(u => u.SomethingUnique, f => $"Value {f.UniqueIndex}")

        ////Use a method outside scope.
        //.RuleFor(u => u.CartId, f => Guid.NewGuid())
        ////Compound property with context, use the first/last name properties
        //.RuleFor(u => u.FullName, (f, u) => u.FirstName + " " + u.LastName)
        ////And composability of a complex collection.
        //.RuleFor(u => u.Orders, f => testOrders.Generate(3).ToList())
        ////Optional: After all rules are applied finish with the following action
        //.FinishWith((f, u) =>
        //{
        //    Console.WriteLine("User Created! Id={0}", u.Id);
        //});

        //var user = testUsers.Generate();
    }
}
