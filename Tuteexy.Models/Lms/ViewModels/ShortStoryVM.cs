using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Tuteexy.Models.ViewModels
{
    public class ShortStoryVM
    {
        public ShortStory ShortStory { get; set; }

        public IEnumerable<ShortStoryThread> ShortStoryThread { get; set; }

        public string UserId { get; set; }

    }
}
