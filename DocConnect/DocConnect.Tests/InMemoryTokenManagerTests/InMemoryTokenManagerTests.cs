using System.Diagnostics;
using AutoFixture;
using DocConnect.Business.Abstraction.TokenManager;
using DocConnect.Business.Models.Token;
using DocConnect.Business.TokenManager;
using FluentAssertions;

namespace DocConnect.Tests.TokenMangementTests
{
    [TestFixture]
    public class InMemoryTokenManagerTests
    {
        private const int TokenExpireTimeInHours = 1;

        private const int CleanUpInterval = 5;

        private ITokenManager _tokenManager;

        private Fixture fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            // In the constructor we set up the in-memory clean up interval.
            _tokenManager = new InMemoryTokenManager(TimeSpan.FromSeconds(CleanUpInterval));
        }

        [Test]
        public void AddToken_TokenAdded_Successfully()
        {
            // Assert
            var token = fixture.Build<InMemoryToken>()
                              .With(t => t.Expiry, DateTime.UtcNow.AddHours(TokenExpireTimeInHours))
                              .Create();
            bool expectedResult = true;

            // Act
            _tokenManager.AddToken(token);

            // Arrange
            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void IsActiveTokenExisting_WithActiveAndExistingToken_ReturnsTrue()
        {
            // Assert
            var token = fixture.Build<InMemoryToken>()
                               .With(t => t.Expiry, DateTime.UtcNow.AddHours(TokenExpireTimeInHours))
                               .Create();
                
            _tokenManager.AddToken(token);
            bool expectedResult = true;

            // Act
            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);

            // Arrange
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void IsActiveTokenExisting_WithNonExistingToken_ReturnsFalse()
        {
            // Assert
            var token = fixture.Create<InMemoryToken>();
            bool expectedResult = false;

            // Act
            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);

            // Arrange
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void IsActiveTokenExisting_WithExistingButNonActiveToken_ReturnsFalse()
        {
            // Assert
            int addHours = -1;
            var token = fixture.Build<InMemoryToken>()
                             .With(t => t.Expiry, DateTime.UtcNow.AddHours(addHours))
                             .Create();
            bool expectedResult = false;

            // Act
            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);

            // Arrange
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 3)]
        public void IsActiveTokenExisting_WithExistingButCleanedUpToken_ReturnsFalse(int expiryTimeInSeconds, int sleepDurationInSeconds)
        {
            // Assert
            var token = fixture.Build<InMemoryToken>()
                             .With(t => t.Expiry, DateTime.UtcNow.AddSeconds(expiryTimeInSeconds))
                             .Create();
            _tokenManager.AddToken(token);
            bool expectedResult = false;
            TimeSpan sleepDuration = TimeSpan.FromSeconds(sleepDurationInSeconds);
            var stopwatch = Stopwatch.StartNew();

            // Act
            while (stopwatch.Elapsed < sleepDuration)
            {
                // we are waiting the private method to trigger clean up the expired tokens.
            }

            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);

            // Arrange
            actualResult.Should().Be(expectedResult);
        }

        [Test]
        public void DeleteToken_WithValidUserIdAndToken_DeletesToken()
        {
            // Assert
            var userId = Guid.NewGuid().ToString();
            var tokenValue = Guid.NewGuid().ToString();
            var token = fixture.Build<InMemoryToken>()
                              .With(t => t.Expiry, DateTime.UtcNow.AddHours(TokenExpireTimeInHours))
                              .With(t => t.UserId , userId)
                              .With(t => t.Value, tokenValue)
                              .Create();
            bool expectedResult = false;

            // Act
            _tokenManager.AddToken(token);
            _tokenManager.DeleteToken(userId, tokenValue);

            // Arrange
            bool actualResult = _tokenManager.IsActiveTokenExisting(token.UserId, token.Type);
            actualResult.Should().Be(expectedResult);
        }
    }
}

