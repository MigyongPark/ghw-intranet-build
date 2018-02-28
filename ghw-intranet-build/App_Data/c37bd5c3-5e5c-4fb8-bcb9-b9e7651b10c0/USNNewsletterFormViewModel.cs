using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace USNStarterKit.USNModels
{

    /// <summary>
    /// Summary description for USNNewsletterFormViewModel
    /// </summary>
    public class USNNewsletterFormViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string Email { get; set; }

        public int CurrentNodeID { get; set; }

        public int ActualPageID { get; set; }
    }

}