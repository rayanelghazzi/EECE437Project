using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.DataContracts
{
    public class RefreshToken
    {
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public string Value { get; set; }
        public string ExpiryTime { get; set; }
    }
}
