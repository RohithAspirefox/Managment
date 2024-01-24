using Management.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.Entity
{
    public class ProjectFilter
    {
        public ProjectFilter() { }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<string> TechStackUsed { get; set; }
    }
}
