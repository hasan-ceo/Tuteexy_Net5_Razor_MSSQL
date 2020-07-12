using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class ApplicationThread
    {
        [Key]
        public long ApplicationThreadID { get; set; }
        public long ApplicationID { get; set; }
        public long SenderID { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
