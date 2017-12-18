using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class DateInTheFutureAttribute : ValidationAttribute
    {
        // Vaildation for future date
        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            string futureDate = Convert.ToDateTime(value).ToString("yyyy-MM-dd");
            DateTime validate = DateTime.ParseExact(futureDate, "yyyy-MM-dd",null);
            if (futureDate != null)
            {
                if (validate < DateTime.Now.Date)
                {
                    return new ValidationResult(ErrorMessageString);
                }
                else{
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessageString);
            
        }
    }
    public class Dashboard
    {
        public List<Wedding> Weddings {get;set;}
        public User User {get;set;}
    }
    
    public class WeddingView
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Wedder One")]
        public string Wedder1 { get; set; }
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Wedder Two")]
        public string Wedder2 { get; set; }

        [Required]
        [DateInTheFuture(ErrorMessage = "Date of wedding must be in the future")]
        [DataType(DataType.Date, ErrorMessage = "Wedding Date must be a valid date.")]
        [Display(Name = "Date of Wedding")]
        public string WeddingDate { get; set; }

        [Required]
        [MinLength(10)]
        [Display(Name = "Wedding Address")]
        public string WeddingAddress { get; set; }

    }
}