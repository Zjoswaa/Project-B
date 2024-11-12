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
            Database.InsertUsersTable(1, "johndoe@gmail.com", "password123", "John", "Doe", "USER");

            // Assert
            Assert.AreEqual(Database.GetUserByEmail("johndoe@gmail.com")?.Email, "johndoe@gmail.com");
            Assert.AreEqual(Database.GetUserByEmail("johndoe@gmail.com")?.FirstName, "John");
            Assert.AreEqual(Database.GetUserByEmail("johndoe@gmail.com")?.LastName, "Doe");
            Assert.AreEqual(Database.GetUserByEmail("johndoe@gmail.com")?.Role, "USER");
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
            Database.InsertUsersTable(1, "johndoe@gmail.com", "password123", "John", "Doe", "USER");

            // Assert
            Assert.IsFalse(LoginLogic.VerifyPassword("johndoe@gmail.com", "password"));
            Assert.IsTrue(LoginLogic.VerifyPassword("johndoe@gmail.com", "password123"));
        }
    }
}
