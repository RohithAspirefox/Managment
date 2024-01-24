using Management.Common.Enum;
using Management.Common.Models.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Common.Models.DTO
{
    public class ProjectDto
    {
        public Guid ProjectId { get; set; }

        public IFormFile? Logo { get; set; }
        public string? LogoUrl { get; set; }

        [Required(ErrorMessage ="Name is Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        public DateTime StartDate { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public List<Guid> TechStackUsed { get; set; }
        public List<TechStackDto>? TechStackUsedObj { get; set; }
        public IFormFile? Documentation { get; set; }
        public string? DocumentationUrl { get; set; }
        public List<IFormFile>? SnapShoots { get; set; }

        public List<string>? SnapShootsUrl { get; set; }

        public string? DevelopmentName { get; set; }
   
        public string? DevelopmentUrl { get; set; }
    
        public string? StageName { get; set; }
     
        public string? StageUrl { get; set; }
       
        public string? ProductionName { get; set; }
        
        public string? ProductionUrl { get; set; }
    }

    public class TechStackDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

}

