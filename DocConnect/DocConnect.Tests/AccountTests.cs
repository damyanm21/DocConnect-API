namespace DocConnect.Tests.ServicesTests
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;
    using DocConnect.Data;
    using DocConnect.Data.Models.Domains;

    [TestFixture]
    public class AccountTests
    {
        private ApplicationDbContext _context;
        private DbContextOptions<ApplicationDbContext> _options
            = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase("DocConnectTest")
           .Options;
        private UserStore<ApplicationUser> _userStore;
        private UserManager<ApplicationUser> _userManager;


        [SetUp]
        public async Task Setup()
        {
            _context = new ApplicationDbContext(_options);
            await _context.Database.EnsureCreatedAsync();
            _userStore = new UserStore<ApplicationUser>(_context);
            _userManager = new UserManager<ApplicationUser>(_userStore, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task AddUserToDatabase_ValidUser_UserSavedSuccessfully()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
            };

            // Act
            await _userManager.CreateAsync(user);

            // Assert
            var savedUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.FirstName == "John");

            Assert.IsNotNull(savedUser);
            Assert.AreEqual("Doe", savedUser.LastName);
            Assert.AreEqual("john.doe@gmail.com", savedUser.Email);
        }
    }
}
