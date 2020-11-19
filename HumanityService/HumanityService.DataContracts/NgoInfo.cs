using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts
{
    public class NgoInfo
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
