using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContentAPI.Models
{
    public class CommentFormViewModel
    {
        [Required]
        [StringLength(60)]
        public string Summary { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        public string Postedby { get; set; }

        public string Ward { get; set; }

        public string Site { get; set; }

        //[RegularExpression("[0-9 ]+", ErrorMessage = "Only Numbers")]

        [Phone]
        public string PhoneNo { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

    }
}