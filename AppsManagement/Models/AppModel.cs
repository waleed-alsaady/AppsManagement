using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AppsManagement.Models
{
    [DataContract]
    public class AppModel
    {
        [Key]
        [DataMember(Name = "id")]
        public string AppId { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }


        [DataMember(Name = "organization_id")]
        public string OrganizationId { get; set; }
        [DataMember(Name = "chrome_web_origin")]
        public string ChromeWebOrigin { get; set; }
    }

    public class AppModelJsonList
    {
        public List<AppModel> Apps { get; set; }
    }
}