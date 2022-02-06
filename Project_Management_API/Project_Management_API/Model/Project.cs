using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string CreatedOn { get; set; }
    }
}
