using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
