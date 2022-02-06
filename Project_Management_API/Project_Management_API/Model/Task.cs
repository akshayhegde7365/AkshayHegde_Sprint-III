using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management_API.Model
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int Status { get; set; }
        public int AssignedToUserId { get; set; }
        public string Detail { get; set; }
        public string CreatedOn { get; set; }
    }
}
