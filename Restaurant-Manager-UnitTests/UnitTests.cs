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
            Database.ConnectionString = "db1.db";
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
        }

        [TestMethod]
        public void TestPasswordValidation() {
            // Align
            File.Delete("db2.db");
            Database.ConnectionString = "db2.db";
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();

            // Act
            Database.InsertUsersTable(1, "johndoe", "password123", "John", "Doe", "USER");

            // Assert
            Assert.IsFalse(LoginLogic.VerifyPassword("johndoe", "password"));
            Assert.IsTrue(LoginLogic.VerifyPassword("johndoe", "password123"));
        }
    }
}
