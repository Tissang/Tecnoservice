using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazanDemo
{
    public class Job
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public Status Status { get; set; }
        public string Name { get; set; }
    }
}
