using System;
using System.ComponentModel.DataAnnotations;

namespace Tuteexy.Models
{
    public class ApplicationThread : EntryInfo
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
