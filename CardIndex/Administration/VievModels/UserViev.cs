using System.Collections.Generic;

namespace Administration.VievModels
{
    public class UserViev
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public List<string> Roles { get; set; }
    }
}
