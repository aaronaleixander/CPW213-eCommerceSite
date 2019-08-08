using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual website user.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// The first and last name of the member. (ex. J. Doe)
        /// </summary>
        [StringLength(60)]
        [Required]
        [Display(Name = "Full name")] // having display so it displays the same on all the views.
        public string FullName { get; set; }
        
        /// <summary>
        /// users  Email Address
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// The user name the member chooses.
        /// </summary>
        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[\d\w]+$"
                            , ErrorMessage = "Usernames can only contain a-z , 0-9, and underscores.")]
        public string Username { get; set; }

        /// <summary>
        /// Password of new user/member
        /// </summary>
        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        /// <summary>
        /// Date of Birth of the member. Time is ignored.
        /// </summary>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        // Custom attributes to make DateTime dynamic
        //[Range(typeof(DateTime), DateTime.Today.AddYears(-120).ToShortDateString(), DateTime.Today.ToShortDateString())]
        // [Required] - Already required becasue DateTime is a structure (Value type)
        public DateTime DateOfBirth { get; set; }



    }
}
