using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.Entity
{
    public class ProjectTechStack
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ProjectEntityId { get; set; }
        public Guid TechStackId { get; set; }

        [ForeignKey("ProjectEntityId")]
        public virtual ProjectEntity ProjectEntity { get; set; }

        [ForeignKey("TechStackId")]
        public virtual TechStack TechStack { get; set; }
    }
}
