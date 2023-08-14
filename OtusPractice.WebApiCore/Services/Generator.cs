using Bogus;

namespace OtusPractice.WebApiCore.Services
{
    public class Generator
    {
        private readonly Faker _faker;


        public Generator()
        {
            _faker = new Faker();
        }

        public string Password() => _faker.Internet.Password();
        public string Login() => _faker.Internet.UserName();


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
