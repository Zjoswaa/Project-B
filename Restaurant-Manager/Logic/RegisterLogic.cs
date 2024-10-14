static class RegisterLogic {
    public static bool UsernameValid(string Username) {
        if (string.IsNullOrWhiteSpace(Username)) {
            return false;
        }
        return Username.Length > 2;
    }

    public static bool PasswordValid(string Password) {
        if (string.IsNullOrWhiteSpace(Password)) {
            return false;
        }
        return Password.Length > 2;
    }
}
