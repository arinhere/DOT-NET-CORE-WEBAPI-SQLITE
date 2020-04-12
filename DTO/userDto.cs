using System.ComponentModel.DataAnnotations;

namespace DotNetCore.API.DTO
{
    public class userDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        [MaxLength(8)]
        public string password { get; set; }
    }
}