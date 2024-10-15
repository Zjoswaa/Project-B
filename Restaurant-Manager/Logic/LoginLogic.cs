using System.Data.SQLite;

static class LoginLogic {
    public static bool VerifyPassword(string Username, string Password) {
        if (!Database.UsersTableContainsUser(Username)) {
            return false;
        }

        return Password == Encryptor.Decrypt(Database.GetEncryptedPassword(Username)!);
    }
}
