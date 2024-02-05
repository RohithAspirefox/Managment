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
        public string Name { get; set; }

        public List<string> Developers { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        public DateTime StartDate { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public List<string> TechStackUsed { get; set; }
        public List<TechStackDto>? TechStackUsedObj { get; set; }
        public IFormFile? Documentation { get; set; }
        public string? DocumentationUrl { get; set; }
        public List<IFormFile>? SnapShoots { get; set; }
        public List<string>? SnapShootsUrl { get; set; }
        [Required(ErrorMessage = "DevelopmentName is Required")]
        public string? DevelopmentName { get; set; }
        [Required(ErrorMessage = "DevelopmentUrl is Required")]
        public string? DevelopmentUrl { get; set; }
        [Required(ErrorMessage = "StageName is Required")]
        public string? StageName { get; set; }
        [Required(ErrorMessage = "StageUrl is Required")]
        public string? StageUrl { get; set; }
        [Required(ErrorMessage = "ProductionName is Required")]
        public string? ProductionName { get; set; }
        [Required(ErrorMessage = "ProductionUrl is Required")]
        public string? ProductionUrl { get; set; }
        public List<string>? DeletedSnapShoots { get; set; }
        public List<string>? DeletedDocuments { get; set; }
    }

    public class TechStackDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

}

