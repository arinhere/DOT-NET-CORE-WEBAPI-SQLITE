using System.ComponentModel.DataAnnotations;

namespace DOT_NET_CORE_WEBAPI_SQLITE.DTO.users
{
    public class userDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }

        public string name { get; set; }
    }
}