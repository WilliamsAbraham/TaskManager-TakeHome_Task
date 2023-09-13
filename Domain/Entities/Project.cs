using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MyProject
    {
        [Key]
        public Guid Id { get; set; }
        public  Guid UserId { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }
        public ICollection<MyTask> MyTasks { get; set; }
    }
}
