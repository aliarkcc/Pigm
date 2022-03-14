using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models
{
    public class MultiModel
    {
        public FollowList FollowList { get; set; }
        public List<FollowList> FollowLists { get; set; }
    }
}
