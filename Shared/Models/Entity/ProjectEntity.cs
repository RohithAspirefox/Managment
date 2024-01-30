using Management.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.Entity
{
    public class ProjectEntity
    {
        [Key]
        public Guid Id { get; set; }

        public List<Document> Documents { get; set; } = new List<Document>();

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public Status Status { get; set; }

        public string Description { get; set; }

        //public IEnumerable<TechStack> TechStackUsed { get; set; }
        //public List<ProjectTechStack> TechStackUsed { get; set; }
        public List<TechStack> TechStackUsed { get; set; }
        public string? DevelopmentName { get; set; }

        public string? DevelopmentUrl { get; set; }

        public string? StageName { get; set; }
 
        public string? StageUrl { get; set; }

        public string? ProductionName { get; set; }

        public string? ProductionUrl { get; set; }
    }
}
