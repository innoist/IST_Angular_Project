using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;

namespace IST.WebApi2.Models
{
    public class DashboardModel
    {
        public int TotalStudents { get; set; }
        public int StudentsPresent { get; set; }
        public int StudentsAbsent { get; set; }

    }

}

