using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class Application
    {
        [Key]
        public long ApplicationID { get; set; }
        public long SchoolID { get; set; }
        public long ApplicationConvoID { get; set; }
        public string Subject { get; set; }
    }
}
