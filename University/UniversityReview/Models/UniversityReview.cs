using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityReview.Models
{
    public class UniversityReviews
    {
        public int Id { get; set; }
        [Range(1,100)]
        [Required]
        public int Rating {get;set;}
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        [Display(Name="User Name")]
        [DisplayFormat(NullDisplayText="anonymous")]
        public string ReviewerName { get; set; }
        public int UniversityId { get; set; }
    }
}