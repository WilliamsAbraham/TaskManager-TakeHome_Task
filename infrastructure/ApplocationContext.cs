using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure
{
    public class ApplocationContext:DbContext
    {
        public ApplocationContext(DbContextOptions<ApplocationContext> optons):base(optons)
        {
            
        }
    }
}
