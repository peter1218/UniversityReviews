using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityReview.Models
{
    public class University
    {
        public int Id { get; set; }
        public string Name{get;set;}
        public string City{get;set;}
        public virtual ICollection<UniversityReviews> Reviews { get; set; }
    }
}