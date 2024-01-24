using Management.Common;
using Management.Common.Models;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Data.AppDbContext;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Management.Common.Enum;
using Microsoft.EntityFrameworkCore;
using Management.Common.Models.ApiResponse;
using System.Security.Cryptography.X509Certificates;

namespace Management.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        public readonly ApplicationDbContext appDbContext;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;

        public ApplicationDbContext applicationDbContext { get; }
        public ProjectService(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment, ApplicationDbContext appDbContext, ILogger<ProjectService> logger, IConfiguration configuration)
        {
            this.applicationDbContext = applicationDbContext;
            this.webHost = webHostEnvironment;
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task<string> AddAsync(ProjectDto projectDto)
        {
            if (projectDto == null)
                return string.Empty;

            ProjectEntity projectEntity = new ProjectEntity
            {

                Name = projectDto.Name,
                ProductionUrl = projectDto.ProductionUrl,
                Id = projectDto.ProjectId,
                Description = projectDto.Description,
                TechStackUsed = projectDto.TechStackUsed.Select(x => new ProjectTechStack { TechStackId = x }).ToList(),
                StageName = projectDto.StageName,
                StageUrl = projectDto.StageUrl,
                DevelopmentName = projectDto.DevelopmentName,
                DevelopmentUrl = projectDto.DevelopmentUrl,
                StartDate = projectDto.StartDate,
                Status = projectDto.Status,
                ProductionName = projectDto.ProductionName,
            };
            var filePath = configuration["FilePath"];

            var localPath = Path.Combine(webHost.WebRootPath, "Images");

            foreach (var doc in projectDto.SnapShoots)
            {
                await SaveFileAsync(doc, localPath, projectEntity, DocumentType.SnapShoots);
            }
            await SaveFileAsync(projectDto.Logo, localPath, projectEntity, DocumentType.Logo);
            await SaveFileAsync(projectDto.Documentation, localPath, projectEntity, DocumentType.Documentation);

            appDbContext.Projects.Add(projectEntity);
            await appDbContext.SaveChangesAsync();
            logger.LogError("Reached After Save");

            return Constants.Created;
        }

        private async Task SaveFileAsync(IFormFile file, string localPath, ProjectEntity project, DocumentType docType)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(localPath, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(fileStream);
                }

                project.Documents.Add(new() { FilePath = "https://localhost:7111/Images/" + fileName, FileName = fileName, DocumentType = docType, ProjectEntityId = project.Id });
            }
        }

        public List<TechStackDto> GetTechStackNames()
        {
            return appDbContext.TechStack.Select(x => new TechStackDto { Name = x.TechStackName, Id = x.Id }).ToList();
        }

        public List<ProjectEntity> GetAllProjects()
        {
            return appDbContext.Projects.Include(p => p.Documents)
            .Include(p => p.TechStackUsed)
                .ThenInclude(pt => pt.TechStack)
            .ToList();
        }

        public bool DeleteProject(string id)
        {
            var project = appDbContext.Projects.SingleOrDefault(x => x.Id == new Guid(id));

            if (project != null)
            {
                appDbContext.Projects.Remove(project);
                appDbContext.SaveChanges();
                return false;
            }
            return false;
        }
        public async Task<ProjectEntity> GetProjectById(string id)
        {
            var project = await appDbContext.Projects.Include(p => p.Documents)
            .Include(p => p.TechStackUsed)
                .ThenInclude(pt => pt.TechStack)
            .SingleOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return project;
        }
        public async Task<BaseResponse> Update(ProjectDto entity)
        {
            var project = await appDbContext.Projects.Include(p => p.Documents)
            .Include(p => p.TechStackUsed)
                .ThenInclude(pt => pt.TechStack)
            .SingleOrDefaultAsync(x => x.Id == entity.ProjectId);


            if (project != null)
            {
                project.Name = entity.Name;
                project.StartDate = entity.StartDate;
                project.Status = entity.Status;
                project.Description = entity.Description;
                project.TechStackUsed = entity.TechStackUsed
                .Select(id => new ProjectTechStack
                {
                    TechStackId = id,
                })
                .ToList();
                project.DevelopmentName = entity.DevelopmentName;
                project.DevelopmentUrl = entity.DevelopmentUrl;
                project.StageName = entity.StageName;
                project.StageUrl = entity.StageUrl;
                project.ProductionName = entity.ProductionName;
                project.ProductionUrl = entity.ProductionUrl;

                var localPath = Path.Combine(webHost.WebRootPath, "Images");

                if (entity.SnapShoots != null)
                {
                    foreach (var doc in entity.SnapShoots)
                    {
                        await SaveFileAsync(doc, localPath, project, DocumentType.SnapShoots);
                    }
                }
                if (entity.Logo != null)
                    await SaveFileAsync(entity.Logo, localPath, project, DocumentType.Logo);
                if (entity.Documentation != null)
                    await SaveFileAsync(entity.Documentation, localPath, project, DocumentType.Documentation);

                appDbContext.Projects.Update(project);
                await appDbContext.SaveChangesAsync();
                return new BaseResponse { Success = true };
            }

            return new BaseResponse { Success = false };

        }

        //public async Task<List<ProjectEntity>> SearchByName(string name)
        //{
        //    return appDbContext.Projects.Where(x => x.Name.Contains(name) || x.StageName.Contains(name) || x.DevelopmentName.Contains(name) || x.ProductionName.Contains(name)).ToList();
        //}

        public async Task<List<ProjectEntity>> SearchByName(string name)
        {
            return appDbContext.Projects
                .Include(x => x.Documents)
                .Include(x => x.TechStackUsed)
                .ThenInclude(x=>x.TechStack)
                .Where(x =>
                    x.Name.Contains(name) ||
                    x.StageName.Contains(name) ||
                    x.DevelopmentName.Contains(name) ||
                    x.ProductionName.Contains(name) ||
                    x.DevelopmentUrl.Contains(name) ||
                    x.StageUrl.Contains(name) ||
                    x.ProductionUrl.Contains(name) ||
                    x.Documents.Any(doc => doc.FileName.Contains(name)) ||
                    x.TechStackUsed.Any(ts => ts.TechStack.TechStackName.Contains(name)) 
                )
                .ToList();
        }

    }
}
