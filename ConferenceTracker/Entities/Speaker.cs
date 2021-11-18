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
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(maximumLength: 500, MinimumLength = 10)]
        public string Description { get; set; }
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public bool IsStaff { get; set; }
    }
}
