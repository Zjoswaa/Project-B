class User {
    public long ID { get; }
    public string Username { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string Role { get; } // "ADMIN" or "USER"

    public static long NextID { get; set; } = 0;

    public User(long ID, string Username, string? FirstName, string? LastName, string Role) {
        this.ID = ID;
        this.Username = Username;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Role = Role;
    }

    public User(string Username, string? FirstName, string? LastName, string Role) {
        this.ID = -1;
        this.Username = Username;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Role = Role;
    }

    public string GetFullName() {
        if (FirstName is null && LastName is null) {
            return Username;
        }
        if (FirstName is null) {
            return $"Mr/Ms {LastName}";
        }
        if (LastName is null) {
            return FirstName;
        }
        return $"{FirstName} {LastName}";
    }

    public override string ToString() {
        return $"User [{ID}]: {GetFullName()} - {Role}";
    }
}
