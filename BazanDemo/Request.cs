using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazanDemo
{
    public class Request
    {
        public int Id { get; set; }
        public Equipment Equipment { get; set; }
        public Status Status { get; set; }
        public string Fio { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
