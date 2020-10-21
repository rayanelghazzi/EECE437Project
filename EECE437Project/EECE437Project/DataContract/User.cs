using System;
using System.Collections.Generic;
using System.Text;

namespace EECE437Project.DataContract
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Usernane { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<Location> Locations { get; set; }
    }
}
