using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferenceTracker.Entities
{
    public class Speaker
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public bool IsStaff { get; set; }
    }
}
