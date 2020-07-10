using System;
using System.Collections.Generic;
using System.Text;

namespace Titan.Models.ViewModels
{
    public class PageVM
    {
        public IEnumerable<Page> Pages { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
