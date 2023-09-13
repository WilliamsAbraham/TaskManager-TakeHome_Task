using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Presentation.DTOs
{
    public class UserDto
    {
        [MinLength(8,ErrorMessage ="UserName can not be less than 8 characters")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class UserViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
       
        [EmailAddress]
        public string Email { get; set; }
      
    }
}
