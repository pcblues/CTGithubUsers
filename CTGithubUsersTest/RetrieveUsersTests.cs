using CTGithubUsers.Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace CTGithubUsersTest
{
    public class RetrieveUsersTests
    {

        // valid users: pcblues, microsoft, trebonian, blaze , string
        // invalid users: mxxxxx1




        [Fact]
        public void TestNoUser()
        {
            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> {  };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 0);
        }

        [Fact]
        public void TestOneUserValid()
        {
            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "pcblues" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 1);
        }

        [Fact]
        public void TestOneUserInvalid()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "mxxxxx1" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 0);
        }

        [Fact]
        public void TestMultiUserValid()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "pcblues", "microsoft", "trebonian", "blaze", "string" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 5);
        }

        [Fact]
        public void TestMultiUserOneInvalid()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "pcblues", "microsoft", "trebonian", "blaze", "string","mxxxxx1" };


            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 5);
        }

        [Fact]
        public void TestMultiUserAllInvalid()
        {
            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "mxxxxx1","yxxxxx1" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 0);
        }

        [Fact]
        public void TestMultiUserAlphaOrder()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "pcblues", "microsoft", "trebonian", "blaze", "string" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            var sortUsers = users.OrderBy(x => x.Name).ToList();
            bool same = true;
            int cnt = sortUsers.Count();
            for (int i = 0; i < cnt; i++)
            {
                if (users[i].Name != sortUsers[i].Name) { same = false;break; }

            }

            Assert.True(same == true);
        }

        [Fact]
        public void TestUserAverageZeroReps()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "string" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users[0].NumberRepositories == 0);
            Assert.True(users[0].AverageFollowersPerRepository == 0);
        }

        [Fact]
        public void TestUserDuplicateNameHandling()
        {

            // Arrange
            var logger = Mock.Of<ILogger<GithubUserController>>();
            var ghuController = new GithubUserController(logger);

            List<string> userNames = new List<string> { "pcblues","pcblues","pcblues" };

            // Act
            var users = ghuController.GetGithubUsers(userNames);

            // Assert
            Assert.True(users.Count == 1);
        }
}
}