namespace Restaurant_Manager_UnitTests;

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

        // Assert
        Assert.IsTrue(dishes.Count() == 1);

        // Act
        Database.DeleteDishesTable("Pizza Margherita");
        dishes = Database.GetAllDishes();

        // Assert
        Assert.IsTrue(dishes.Count() == 0);
    }

    [TestMethod]
    public void TestInsertReservation() {
        // Align
        File.Delete("db5.db");
        Database.ConnectionString = "db5.db";
        Database.CreateUsersTable();
        Database.CreateDishesTable();
        Database.CreateLocationsTable();
        Database.CreateReservationsTable();

        // Act
        Database.InsertReservationsTable(null, 1, 1, "12:00", DateOnly.FromDateTime(DateTime.Now), 6, 1);
        List<Reservation> Reservations = Database.GetAllReservations();

        // Assert
        Assert.IsTrue(Reservations.Count() == 1);
    }

    [TestMethod]
    public void TestRemoveReservation() {
        // Align
        File.Delete("db7.db");
        Database.ConnectionString = "db7.db";
        Database.CreateUsersTable();
        Database.CreateDishesTable();
        Database.CreateLocationsTable();
        Database.CreateReservationsTable();
        
        // Act
        Database.InsertReservationsTable(null, 1, 1, "12:00", DateOnly.FromDateTime(DateTime.Now), 6, 1);
        Assert.AreEqual(Database.GetAllReservations().Count, 1);
        Database.DeleteReservationsTable(1);
        Assert.AreEqual(Database.GetAllReservations().Count, 0);
    }

    [TestMethod]
    public void TestEditReservation() {
        File.Delete("db8.db");
        Database.ConnectionString = "db8.db";
        Database.CreateUsersTable();
        Database.CreateDishesTable();
        Database.CreateLocationsTable();
        Database.CreateReservationsTable();
            
        Database.InsertReservationsTable(null, 1, 1, "12:00", DateOnly.FromDateTime(DateTime.Now), 6, 1);
        Reservation newReservation = new(1, 1, 1, "15:00", DateOnly.FromDateTime(DateTime.Now.AddDays(3)), 3, 1);
        Database.UpdateReservation(newReservation);
            
        Reservation updatedReservation = Database.GetAllReservations()[0];
        Assert.AreEqual(updatedReservation.GroupSize, newReservation.GroupSize);
        Assert.AreEqual(updatedReservation.Date, newReservation.Date);
        Assert.AreEqual(updatedReservation.Timeslot, newReservation.Timeslot);
    }
    
    [TestMethod]
    public void TestHiddenCodeReservation()
    {
        File.Delete("db9.db");
        Database.ConnectionString = "db9.db";
        Database.CreateUsersTable();
        Database.CreateDishesTable();
        Database.CreateLocationsTable();
        Database.CreateReservationsTable();
            
        HiddenDiscount.RemoveCodeFromMenu();
        State.LoggedInUser = new(1, "test@mail.com", "Test", "Test", "User");
        HiddenDiscount.AddCodeToReservations();
            
        Reservation hiddenCodeReservation = Database.GetAllReservations()[0];
        Assert.IsTrue(HiddenDiscount.HiddenCodes.Contains(hiddenCodeReservation.Timeslot));
    }
}
