using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferenceTracker.Entities
{
    public class Speaker : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            if (EmailAddress != null)
            {
                if (EmailAddress.EndsWith("TechnologyLiveConference.com", StringComparison.OrdinalIgnoreCase))
                {
                    validationResults.Add(new ValidationResult("Technology Live Conference staff should not use their conference email addresses."));
                }
            }

            return validationResults;
        }
    }
}
