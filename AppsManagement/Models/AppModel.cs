using System.ComponentModel.DataAnnotations;

namespace AppsManagement.Models
{
    public class AppModel
    {
        [Key]
        public string AppId { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Name")]
        public string Name { get; set; }


        public string OrganizationId { get; set; }
        public string ChromeWebOrigin { get; set; }
    }
}