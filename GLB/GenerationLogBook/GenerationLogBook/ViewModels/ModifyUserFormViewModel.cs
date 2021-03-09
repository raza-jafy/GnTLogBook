using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GenerationLogBook.ViewModels
{
    public class ModifyUserFormViewModel
    {
        [Display(Name ="User ID")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserId { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string UserStatusId { get; set; }
        [Display(Name = "Created On")]
        public string CreatedOn { get; set; }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Changed On")]
        public string ChangedOn { get; set; }
        [Display(Name = "Changed By")]
        public string ChangedBy { get; set; }
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        public IEnumerable<UserStatus> LOVUserStatus { get; set; }
    }
}