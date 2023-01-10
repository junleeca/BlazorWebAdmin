﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Permissions
{
    public partial class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public object? Payload { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public DateTime CreatedTime { get; set; }
        public UserInfo()
        {
            UserId = string.Empty;
            Payload = null;
            Roles = Enumerable.Empty<string>();
            CreatedTime = DateTime.Now;
        }
    }
}
