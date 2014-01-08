using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrioritizerService.Model;

namespace  PrioritizerService
{
    public static class Utils
    {
        public static Dictionary<Guid, DateTime> connectedUsers = new Dictionary<Guid, DateTime>();
        public static Dictionary<Guid, Users> _usersDict;
    }
}