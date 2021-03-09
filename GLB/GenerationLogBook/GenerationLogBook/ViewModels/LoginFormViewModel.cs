using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GenerationLogBook.ViewModels
{
    public class LoginFormViewModel
    {
        [Required]
        [EmailAddress]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}