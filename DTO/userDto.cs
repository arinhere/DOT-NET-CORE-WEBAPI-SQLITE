using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.DTO
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