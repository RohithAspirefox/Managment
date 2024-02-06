using Management.Common;
using Management.Common.Models;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Data.AppDbContext;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Management.Common.Enum;
using Microsoft.EntityFrameworkCore;
using Management.Common.Models.ApiResponse;

namespace Management.Services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IWebHostEnvironment webHost;
        private readonly IHttpContextAccessor httpContextAccessor;
        public readonly ApplicationDbContext appDbContext;
        private readonly ILogger logger;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext applicationDbContext { get; }
        public ProjectService(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment, ApplicationDbContext appDbContext, ILogger<ProjectService> logger, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.applicationDbContext = applicationDbContext;
            this.webHost = webHostEnvironment;
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> AddAsync(ProjectDto projectDto)
        {
            if (projectDto == null)
                return string.Empty;

            ProjectEntity projectEntity = new ProjectEntity
            {
                Name = projectDto.Name,
                ProductionUrl = projectDto.ProductionUrl,
                Id = Guid.NewGuid(),
                Description = projectDto.Description,
                TechStackUsed = projectDto.TechStackUsed.First().Split(',').Select(x => new TechStack { TechStackName = x }).ToList(),
                StageName = projectDto.StageName,
                StageUrl = projectDto.StageUrl,
                DevelopmentName = projectDto.DevelopmentName,
                DevelopmentUrl = projectDto.DevelopmentUrl,
                StartDate = projectDto.StartDate,
                Status = projectDto.Status,
                ProductionName = projectDto.ProductionName,
                UserProject = projectDto.Developers.First().Split(',').Select(x => new UserProject { UserId = x }).ToList(),
            };

            var localPath = Path.Combine(webHost.WebRootPath, "Images");

            foreach (var doc in projectDto.SnapShoots)
            {
                await SaveFileAsync(doc, localPath, projectEntity, DocumentType.SnapShoots);
            }
            await SaveFileAsync(projectDto.Logo, localPath, projectEntity, DocumentType.Logo);
            await SaveFileAsync(projectDto.Documentation, localPath, projectEntity, DocumentType.Documentation);

            appDbContext.Projects.Add(projectEntity);
            await appDbContext.SaveChangesAsync();

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

                project.Documents.Add(new() { FilePath = $"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}{_httpContextAccessor?.HttpContext?.Request.PathBase}/Images/" + fileName, FileName = fileName, DocumentType = docType, ProjectEntityId = project.Id });
            }
        }

        private async Task UpdateFileAsync(IFormFile file, string localPath, ProjectEntity project, DocumentType docType)
        {
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(localPath, fileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await file.CopyToAsync(fileStream);
                }

                string urlPath = null;
                if (_httpContextAccessor != null)
                {
                    urlPath = $"{_httpContextAccessor?.HttpContext?.Request.Scheme}://{_httpContextAccessor?.HttpContext?.Request.Host}{_httpContextAccessor?.HttpContext?.Request.PathBase}/Images/{fileName}";
                }
                project.Documents.Add(new Document { FilePath = urlPath, DocumentType = docType ,FileName=fileName});
                                          

            }
        }

        public List<TechStackDto> GetTechStackNames()
        {
            return appDbContext.TechStack.Select(x => new TechStackDto { Name = x.TechStackName, Id = x.Id }).ToList();
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            return await appDbContext.Projects.Include(p => p.Documents).Include(p => p.UserProject).ThenInclude(u => u.User)
            .Include(p => p.TechStackUsed).Include(p => p.UserProject).ThenInclude(u => u.User)
            .ToListAsync();
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
            var project = await appDbContext.Projects.Include(p => p.Documents).Include(p => p.UserProject).ThenInclude(u => u.User)
            .Include(p => p.TechStackUsed).SingleOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return project;
        }
        public async Task<BaseResponse> Update(ProjectDto entity)
        {
            var project = await appDbContext.Projects.Include(p => p.Documents).Include(p => p.UserProject).ThenInclude(u => u.User)
            .Include(p => p.TechStackUsed).SingleOrDefaultAsync(x => x.Id == entity.ProjectId);


            if (project != null)
            {
                project.Name = entity.Name;
                project.StartDate = entity.StartDate;
                project.Status = entity.Status;
                project.Description = entity.Description;
                project.TechStackUsed = entity.TechStackUsed.Select(x => new TechStack { TechStackName = x }).ToList();
                project.DevelopmentName = entity.DevelopmentName;
                project.DevelopmentUrl = entity.DevelopmentUrl;
                project.StageName = entity.StageName;
                project.StageUrl = entity.StageUrl;
                project.ProductionName = entity.ProductionName;
                project.ProductionUrl = entity.ProductionUrl;
                project.UserProject = entity.Developers.First().Split(',').Select(x => new UserProject { UserId = x }).ToList();

                var localPath = Path.Combine(webHost.WebRootPath, "Images");

                if (entity.SnapShoots != null)
                {
                    foreach (var doc in entity.SnapShoots)
                    {
                        await UpdateFileAsync(doc, localPath, project, DocumentType.SnapShoots);
                    }
                }
                if (entity?.DeletedSnapShoots?.Any() ?? false)
                {
                    foreach (var item in entity.DeletedSnapShoots)
                    {
                        project.Documents.RemoveAll(x => string.Equals(x.FilePath, item));
                    }
                }
                if (entity?.DeletedDocuments?.Any() ?? false)
                {
                    foreach (var item in entity.DeletedDocuments)
                    {
                        project.Documents.RemoveAll(x => string.Equals(x.FilePath, item));
                    }
                }

                if (entity.Logo != null)
                    await UpdateFileAsync(entity.Logo, localPath, project, DocumentType.Logo);
                if (entity.Documentation != null)
                    await UpdateFileAsync(entity.Documentation, localPath, project, DocumentType.Documentation);
                try
                {
                    appDbContext.Projects.Update(project);
                    await appDbContext.SaveChangesAsync();
                    return new BaseResponse { Success = true };
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return new BaseResponse { Success = false };
        }

        public async Task<List<ProjectEntity>> SearchByName(string name)
        {
            return appDbContext.Projects
                .Include(x => x.Documents).Include(p => p.UserProject).ThenInclude(u => u.User)
                .Include(x => x.TechStackUsed)
                .Where(x =>
                    x.Name.Contains(name) ||
                    x.Description.Contains(name) ||
                    x.StageName.Contains(name) ||
                    x.DevelopmentName.Contains(name) ||
                    x.ProductionName.Contains(name) ||
                    x.DevelopmentUrl.Contains(name) ||
                    x.StageUrl.Contains(name) ||
                    x.ProductionUrl.Contains(name) ||
                    x.Documents.Any(doc => doc.FileName.Contains(name)) ||
                    x.TechStackUsed.Any(ts => ts.TechStackName.Contains(name)) ||
                    x.UserProject.Any(x => x.User.FirstName.Contains(name))
                )
                .ToList();
        }

    }
}
