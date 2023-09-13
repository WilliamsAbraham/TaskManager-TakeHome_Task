using Domain.StaticObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.StaticObjects.PriorityEnums;

namespace Domain.Entities
{
    public class MyTask
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityType Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DueDate { get; set; }
        public  MyProject Project { get; set; }
        public User User { get; set; }
    }
}
