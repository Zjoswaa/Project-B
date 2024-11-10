using System.Text.RegularExpressions;

static class RegisterLogic {
    public static bool EmailValid(string Email) {
        if (string.IsNullOrEmpty(Email)) {
            return true;
        }
        if (string.IsNullOrWhiteSpace(Email)) {
            return false;
        }

        return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").IsMatch(Email);
    }

    public static bool PasswordValid(string Password) {
        if (string.IsNullOrEmpty(Password)) {
            return true;
        }
        if (string.IsNullOrWhiteSpace(Password)) {
            return false;
        }
        
        return new Regex(@".{8,}").IsMatch(Password);
    }
}
