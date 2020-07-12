using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Titan.Models
{
    public class NoticeBoard
    {
        [Key]
        public long NoticBoardId { get; set; }

        [Required]
        [MaxLength(50)]
        public string NoticHead { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Noticebody { get; set; }

        public long classid { get; set; }
    }
}
