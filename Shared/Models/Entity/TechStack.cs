using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.Entity
{
    public class TechStack
    {
        [Key]
        public Guid Id {  get; set; }
        [Required]
        public string TechStackName { get; set; }
        //public List<ProjectTechStack> ProjectTechStacks { get; set; }

        public Guid ProjectEntityId { get; set; }
        public ProjectEntity ProjectEntity { get; set; }
    }
}
