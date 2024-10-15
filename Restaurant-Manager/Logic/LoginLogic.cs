public static class LoginLogic {
    public static bool VerifyPassword(string Username, string Password) {
        if (!Database.UsersTableContainsUser(Username)) {
            return false;
        }
        //Console.WriteLine(Password == Encryptor.Decrypt(Database.GetEncryptedPassword(Username)!));
        return Password == Encryptor.Decrypt(Database.GetEncryptedPassword(Username)!);
    }
}
