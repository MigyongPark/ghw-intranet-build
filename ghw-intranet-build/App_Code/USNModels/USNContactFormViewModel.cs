using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace USNStarterKit.USNModels
{

    /// <summary>
    /// Summary description for USNContactFormViewModel
    /// </summary>
    public class USNContactFormViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string Email { get; set; }

        public string Telephone { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public Boolean NewsletterSignup { get; set; }

        public int CurrentNodeID { get; set; }
    }

}