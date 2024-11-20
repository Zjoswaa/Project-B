using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod]
        public void TestInsertDish()
        {
            // Align
            File.Delete("db3.db");
            Database.ConnectionString = "db3.db";
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();

            // Act
            Database.InsertDishesTable("Spaghetti Bolognese", "12,99", false, true, false, false);

            // Assert
            List<Dish> dishes = Database.GetAllDishes();
            Assert.IsNotNull(dishes);
            Assert.IsTrue(dishes.Count() == 1);
            Assert.AreEqual(dishes[0].Name, "Spaghetti Bolognese");

            // Act
            Database.InsertDishesTable("Pizza Margherita", "9.95", false, true, true, false);

            // Assert
            dishes = Database.GetAllDishes();
            Assert.IsTrue(dishes.Count() == 2);
            Assert.IsTrue(dishes[1].IsVegan == false);
        }

        [TestMethod]

        public void TestDeleteDish()
        {
            // Align
            File.Delete("db4.db");
            Database.ConnectionString = "db4.db";
            Database.CreateUsersTable();
            Database.CreateDishesTable();
            Database.CreateLocationsTable();
            Database.CreateReservationsTable();

            // Act
            Database.InsertDishesTable("Pizza Margherita", "9.95", false, true, true, false);
            List<Dish> dishes = Database.GetAllDishes();
            Assert.IsTrue(dishes.Count() == 1);
            Database.DeleteDishesTable("Pizza Margherita");
            dishes = Database.GetAllDishes();
            Assert.IsTrue(dishes.Count() == 0);
        }
    }
}
