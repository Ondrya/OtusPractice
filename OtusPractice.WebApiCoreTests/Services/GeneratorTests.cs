using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace OtusPractice.WebApiCore.Services.Tests
{
    [TestClass()]
    public class GeneratorTests
    {
        private Generator generator;

        [TestInitialize()]
        public void Init()
        {
            generator = new Generator();
        }


        [TestMethod()]
        public async Task CreateUsersTest()
        {
            var count = 10;

            var users = await generator.CreateUsers(count);
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count == count);
        }
    }
}