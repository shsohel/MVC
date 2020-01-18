using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CrudMvcImageUpload.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentPhoto { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoPath { get; set; }
    }
}