﻿using MDbEntity.Attributes;
using Project.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Permissions
{
    [TableName("ROLE")]
    [Table(Name = "ROLE")]
    public class Role
    {
        [PrimaryKey]
        [ColumnDefinition("角色编号")]
        [ColumnName("ROLE_ID")]
        [Column(Name = "ROLE_ID", PrimaryKey = true)]
        public string RoleId { get; set; }
        [ColumnDefinition("角色名称")]
        [ColumnName("ROLE_NAME")]
        [Column(Name = "ROLE_NAME", PrimaryKey = true)]
        public string RoleName { get; set; }
    }
}
