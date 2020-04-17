using System;
using System.Collections.Generic;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}