using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.DataContracts.CompositeDesignPattern
{
    public class Utils
    {
        static public long UnixTimeSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        static public string CreateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
