using EECE437Project.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EECE437Project.DataContract
{
    public class Ngo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string RegistrationNumber { get; set; }
        public string WebsiteAddress { get; set; }
        public long PhoneNumber { get; set; }
        public List<Location> Locations { get; set; }
        public string Description { get; set; }
    }
}
