using System;

namespace DOT_NET_CORE_WEBAPI_SQLITE.DTO.products
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}