static class RegisterLogic {
    public static bool UsernameValid(string Username) {
        if (string.IsNullOrEmpty(Username)) {
            return true;
        }
        if (string.IsNullOrWhiteSpace(Username)) {
            return false;
        }
        return Username.Length > 3;
    }

    public static bool PasswordValid(string Password) {
        if (string.IsNullOrEmpty(Password)) {
            return true;
        }
        if (string.IsNullOrWhiteSpace(Password)) {
            return false;
        }
        return Password.Length > 7;
    }
}
