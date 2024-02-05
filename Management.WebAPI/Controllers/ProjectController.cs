using AutoMapper;
using Management.Common.Enum;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Management.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        public readonly IProjectService projectService;
        public ILogger Logger;
        public IMapper mapper;
        private readonly IWebHostEnvironment webHost;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger, IMapper mapper, IWebHostEnvironment webHost)
        {
            this.projectService = projectService;
            Logger = logger;
            this.mapper = mapper;
            this.webHost = webHost;
        }

        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromForm] ProjectDto projectDtoo)
        {
            try
            {
                var created = await projectService.AddAsync(projectDtoo);
                Logger.LogError("Reached Webapi response");
                return Ok(new BaseResponse { Success = true });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllProjects")]
        public async Task<List<ProjectDto>> GetAllProjects()
        {
            try
            {
                var allProject = await projectService.GetAllProjects();
                var projectList= mapper.Map<List<ProjectDto>>(allProject);
                return projectList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetTechStackNames")]
        public async Task<List<TechStackDto>> GetTechStackNames()
        {
            var technologies = projectService.GetTechStackNames();
            return technologies;
        }
        [HttpPost("DeleteProject")]
        public async Task<BaseResponse> DeleteProject(DeleteObj deleteObj)
        {
            try
            {
                var projDelete = projectService.DeleteProject(deleteObj.id);
                return new BaseResponse{Success = true,};
            }
            catch (Exception ex)
            {
                return new BaseResponse{Success = false,};
            }
        }

        [HttpPost("UpdateProject")]
        public async Task<ProjectDto> UpdateProject(UpdateObj updateObj)
        {
            var project = await projectService.GetProjectById(updateObj.id);
            var projectDto=mapper.Map<ProjectDto>(project);
            return projectDto;
        }

        [HttpPost("UpdateProjectPost")]
        public async Task<IActionResult> UpdateProjectPost(ProjectDto existingProjectDto)
        {
            var result = await projectService.Update(existingProjectDto);
            return Ok(result);
        }

        [HttpPost("SearchByName")]
        public async Task<List<ProjectDto>> SearchByName([FromBody] string name)
        {
            if (name != null)
            {
                var result = await projectService.SearchByName(name);
                var list = mapper.Map<List<ProjectDto>>(result);
                return list;
            }
            return [];
        }

    }

    public class DeleteObj
    {
        public string id { get; set; }
    }
    public class UpdateObj
    {
        public string id { get; set; }
    }
}
