using System;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CloudinaryId { get; set; }
        public string CloudinaryUrl { get; set; }
        public User User {get; set;}
        public int UserId {get; set;}
    }
}