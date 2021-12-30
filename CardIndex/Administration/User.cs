using Microsoft.AspNetCore.Identity;

namespace Administration
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
