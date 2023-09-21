using StripeDemoApp.Models;

namespace StripeDemoApp.Services
{
    public class UsersService
    {

        public UserDemo GetCurrentUser()
        {
            return new UserDemo()
            {
                Email = "moa@gmail.com",
                Id = 1,
                Username = "moamen soroor"
            };
        }

        public UserDemo GetUser(int id)
        {
            return new UserDemo()
            {
                Email = "moa@gmail.com",
                Id = 1,
                Username = "moamen soroor"
            };
        }




    }




}