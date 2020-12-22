using System;
using System.Collections.Generic;
using System.Text;

namespace HumanityService.Client.DataContracts.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
