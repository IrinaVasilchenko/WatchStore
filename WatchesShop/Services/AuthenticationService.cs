namespace WatchesShop.Services
{
    public class AuthenticationService
    {
        public bool Authenticate(string username, string password)
        {
            return username == "Admin" && password == "123admin";
        }
    }
}
