using System.ComponentModel.DataAnnotations;

namespace TaskManager.Presentation.Dtos
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

    }
}
