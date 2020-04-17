using System;
using System.Collections.Generic;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.products;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.DTO.users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public ICollection<ProductResponseDto> Products { get; set; }
    }
}