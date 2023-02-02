namespace AuthHandler.Services
{
    public class UsersStore
    {
        public Dictionary<string, User> Users;

        public UsersStore()
        {
            Users = new Dictionary<string, User>();
        }
    }

    public class User   
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
