using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserStorageServices.Tests
{
    [TestClass]
    public class UserStorageServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullAsUserArgument_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();

            // Act
            userStorageService.Add(null);

            // Assert - [ExpectedException]
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_UserFirstNameIsNull_ExceptionThrown()
        {
            // Arrange
            var userStorageService = new UserStorageService();
            
            // Act
            userStorageService.Add(new User
            {
                FirstName = null
            });

            // Assert - [ExpectedException]
        }

        [TestMethod]
        public void Remove_WithoutArguments_NothingHappen()
        {
        }
    }
}
