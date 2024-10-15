using System.Data.Entity;

namespace Restaurant_Manager_UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestInsertUser()
        {
            // Align
            File.Delete("db1.db");
            Database.Connect("db1.db");
            Database.OpenConnection();
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();

            // Act
            Database.InsertUsersTable(1, "johndoe", "password123", "John", "Doe", "USER");

            // Assert
            Assert.AreEqual(Database.GetUserByUsername("johndoe")?.Username, "johndoe");
            Assert.AreEqual(Database.GetUserByUsername("johndoe")?.FirstName, "John");
            Assert.AreEqual(Database.GetUserByUsername("johndoe")?.LastName, "Doe");
            Assert.AreEqual(Database.GetUserByUsername("johndoe")?.Role, "USER");

            // Finalize
            Database.CloseConnection();
            Database.DisposeConnection();
        }

        [TestMethod]
        public void TestPasswordValidation() {
            // Align
            File.Delete("db2.db");
            Database.Connect("db2.db");
            Database.OpenConnection();
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();

            // Act
            Database.InsertUsersTable(1, "johndoe", "password123", "John", "Doe", "USER");

            // Assert
            Assert.IsFalse(LoginLogic.VerifyPassword("johndoe", "password"));
            Assert.IsTrue(LoginLogic.VerifyPassword("johndoe", "password123"));

            // Finalize
            Database.CloseConnection();
            Database.DisposeConnection();
        }
    }
}
