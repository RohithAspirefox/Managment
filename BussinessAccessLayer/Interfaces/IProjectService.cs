using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Services.Interfaces
{
    public interface IProjectService
    {
        Task<string> AddAsync(ProjectDto projectDto);
        List<TechStackDto> GetTechStackNames();
        Task<List<ProjectEntity>> GetAllProjects();
        bool DeleteProject(string id);
        Task<ProjectEntity> GetProjectById(string id);
        Task<BaseResponse> Update(ProjectDto entity);
        Task<List<ProjectEntity>> SearchByName(string name);
    }
}
