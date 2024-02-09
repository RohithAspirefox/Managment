using Management.Common;
using Management.Common.Enum;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace Management.Controllers
{
    [Authorize(Roles = "HR")]
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        public IApiHelperService _apiHelperService;

        public ProjectController(IApiHelperService apiHelperService)
        {
            _apiHelperService = apiHelperService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _apiHelperService.PostAsyncUpdateDeleteData<ProjectDto>(ApiRoute.UpdateProject, id);
            return View(result);
        }

        public async Task<IActionResult> ListView(int? page, string sortField, string sortOrder)
        {
            const int pageSize = 50;

            var searchResultsJson = HttpContext.Session.GetString("SearchResults");
            var value = HttpContext.Session.GetString("SearchValue");

            if (!string.IsNullOrEmpty(searchResultsJson) && !string.IsNullOrEmpty(value))
            {
                var searchResults = JsonConvert.DeserializeObject<PaginatedList<ProjectDto>>(searchResultsJson);

                if (searchResults.Items != null && searchResults.Items.Any())
                {
                    var paginatedSearchResults = await SortProjects(searchResults.Items, sortField, sortOrder, page ?? 1, pageSize);
                    return View(paginatedSearchResults);
                }
                else
                {
                    ViewBag.Message = "No records found.";
                    return View();
                }
            }

            var allResults = await _apiHelperService.GetAsync<List<ProjectDto>>(ApiRoute.GetAllProject);

            int totalPages;
            if (page == null)
            {
                totalPages = (int)Math.Ceiling((double)allResults.Count / pageSize);
                page = totalPages;
            }

            var paginatedAllResults = await SortProjects(allResults, sortField, sortOrder, page.Value, pageSize);

            return View(paginatedAllResults);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _apiHelperService.GetAsync<List<TechStackDto>>(ApiRoute.GetTechStackNames);
            ViewBag.TechStackNames = new SelectList(result, "Id", "Name");

            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(Status)));

            var users = await _apiHelperService.GetAsync<List<User>>(ApiRoute.GetAllUsers);

            var usersSelectList = users.Select(u => new SelectListItem
            {Value = u.Id,Text = $"{u.FirstName}"}).ToList();

            ViewBag.Users = usersSelectList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject([FromForm] ProjectDto project)
        {
            if (!ModelState.IsValid)
                return Json(1);

            try
            {
                var content = new MultipartFormDataContent();
                AddCommonContent(content, project);
                var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.CreateProject, content);
                if (result.Success)
                {
                    return RedirectToAction("ListView", "Project");
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return RedirectToAction("ListView", "Project");
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _apiHelperService.PostAsyncUpdateDeleteData<ProjectDto>(ApiRoute.UpdateProject, id);

            ViewBag.Logo = result.LogoUrl;
            ViewBag.Documentation = result.DocumentationUrl;
            ViewBag.SnapShoot = result.SnapShootsUrl;
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(Status)));
            ViewBag.CurrentStatus = result.Status;

            var users = await _apiHelperService.GetAsync<List<User>>(ApiRoute.GetAllUsers);
            var usersSelectList = users.Select(u => new SelectListItem
            {Value = u.Id,Text = $"{u.FirstName}"}).ToList();

            ViewBag.Users = usersSelectList;

            ViewBag.Developers = new SelectList(result.Developers, "Id", "Name");
            ViewBag.TechStackNames = new SelectList(result.TechStackUsed, "Id", "Name");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject([FromForm] ProjectDto project)
        {
            var content = new MultipartFormDataContent();
            AddCommonContentForUpdate(content, project);
            var result = await _apiHelperService.PostAsync<ProjectDto>(ApiRoute.UpdateProjectPost, content);
            return RedirectToAction("ListView");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return BadRequest(); }
            var result = await _apiHelperService.PostAsyncUpdateDeleteData<BaseResponse>(ApiRoute.DeleteProject, id);
            if (result.Success)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("ListView");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Search(string value, string sortField, string sortOrder, int page = 1, int pageSize = 2)
        {
            HttpContext.Session.SetString("SearchValue", value ?? string.Empty);
            if (string.IsNullOrEmpty(value))
            {
                var allResults = await _apiHelperService.GetAsync<List<ProjectDto>>(ApiRoute.GetAllProject);

                var paginatedAllResults = await SortProjects(allResults, sortField, sortOrder, page, pageSize);

                HttpContext.Session.SetString("SearchResults", JsonConvert.SerializeObject(paginatedAllResults));
                

                return RedirectToAction("ListView", new { page, sortField, sortOrder });
            }

            var result = await _apiHelperService.PostAsyncData<List<ProjectDto>>(ApiRoute.SearchByName, value);
            if (result.Count >= 1)
            {
                var paginatedSearchResults = await SortProjects(result, sortField, sortOrder, page, pageSize);
                HttpContext.Session.SetString("SearchResults", JsonConvert.SerializeObject(paginatedSearchResults));
                return RedirectToAction("ListView", new { page, sortField, sortOrder });
            }
            return RedirectToAction("ListView", result);

        }


        private async Task<PaginatedList<ProjectDto>> SortProjects(IEnumerable<ProjectDto> projects, string sortField, string sortOrder, int pageIndex, int pageSize)
        {
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "name":
                        projects = sortOrder.ToLower() == "asc"
                            ? projects.OrderBy(p => p.Name)
                            : projects.OrderByDescending(p => p.Name);
                        break;
                    case "startdate":
                        projects = sortOrder.ToLower() == "asc"
                            ? projects.OrderBy(p => p.StartDate)
                            : projects.OrderByDescending(p => p.StartDate);
                        break;
                    default:
                        break;
                }
            }

            var paginatedResults = new PaginatedList<ProjectDto>(
                projects.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
                pageIndex,
                pageSize,
                projects.Count()
            );

            return paginatedResults;
        }

        private void AddCommonContent(MultipartFormDataContent content, ProjectDto project)
        {
            content.Add(new StringContent(project.ProjectId.ToString()), "ProjectId");
            content.Add(new StringContent(project.Status.ToString()), "Status");
            content.Add(new StringContent(project.Name), "Name");
            content.Add(new StringContent(project.StartDate.ToString()), "StartDate");
            content.Add(new StringContent(project.Description.ToString()), "Description");
            content.Add(new StringContent(project.DevelopmentName.ToString()), "DevelopmentName");
            content.Add(new StringContent(project.DevelopmentUrl.ToString()), "DevelopmentUrl");
            content.Add(new StringContent(project.StageName.ToString()), "StageName");
            content.Add(new StringContent(project.ProductionName.ToString()), "ProductionName");
            content.Add(new StringContent(project.StageUrl.ToString()), "StageUrl");
            content.Add(new StringContent(project.ProductionUrl.ToString()), "ProductionUrl");


            foreach (var techStackItem in project.TechStackUsed)
            {
                content.Add(new StringContent(techStackItem), "TechStackUsed");
            }
            foreach (var dev in project.Developers)
            {
                content.Add(new StringContent(dev), "Developers");
            }

            content.Add(new StreamContent(project.Logo.OpenReadStream()), "Logo", project.Logo.FileName);

            content.Add(new StreamContent(project.Documentation.OpenReadStream()), "Documentation", project.Documentation.FileName);

            foreach (var snapshot in project.SnapShoots)
            {
                content.Add(new StreamContent(snapshot.OpenReadStream()), "SnapShoots", snapshot.FileName);
            }
        }

        private void AddCommonContentForUpdate(MultipartFormDataContent content, ProjectDto project)
        {
            content.Add(new StringContent(project.ProjectId.ToString()), "ProjectId");
            content.Add(new StringContent(project.Status.ToString()), "Status");
            content.Add(new StringContent(project.Name), "Name");
            content.Add(new StringContent(project.StartDate.ToString()), "StartDate");
            content.Add(new StringContent(project.Description.ToString()), "Description");
            content.Add(new StringContent(project.DevelopmentName.ToString()), "DevelopmentName");
            content.Add(new StringContent(project.DevelopmentUrl.ToString()), "DevelopmentUrl");
            content.Add(new StringContent(project.StageName.ToString()), "StageName");
            content.Add(new StringContent(project.ProductionName.ToString()), "ProductionName");
            content.Add(new StringContent(project.StageUrl.ToString()), "StageUrl");
            content.Add(new StringContent(project.ProductionUrl.ToString()), "ProductionUrl");

            if (project.Logo is not null)
            {
                content.Add(new StreamContent(project.Logo.OpenReadStream()), "Logo", project.Logo.FileName);

            }
            if (project.Documentation is not null)
            {

                content.Add(new StreamContent(project.Documentation.OpenReadStream()), "Documentation", project.Documentation.FileName);
            }

            if (project.SnapShoots is not null)
            {

                foreach (var snapshot in project.SnapShoots)
                {
                    content.Add(new StreamContent(snapshot.OpenReadStream()), "SnapShoots", snapshot.FileName);
                }
            }

            foreach (var guid in project.TechStackUsed)
            {
                content.Add(new StringContent(guid.ToString()), "TechStackUsed");
            }

            foreach (var dev in project.Developers)
            {
                content.Add(new StringContent(dev), "Developers");
            }

            content.Add(new StringContent(JsonConvert.SerializeObject(new List<TechStackDto> { })), "TechStackUsedObj");

            if (project.DeletedSnapShoots[0] is not null)
            {
                string deletedSnapShootsString = project.DeletedSnapShoots[0];
                string[] deletedPaths = deletedSnapShootsString.Split(',');

                foreach (string path in deletedPaths)
                {
                    content.Add(new StringContent(path), "DeletedSnapShoots");
                }
            }

            if (project.DeletedDocuments[0] is not null)
            {
                string deletedDocumentString = project.DeletedDocuments[0];
                string[] deletedPaths = deletedDocumentString.Split(',');

                foreach (string path in deletedPaths)
                {
                    content.Add(new StringContent(path), "DeletedDocuments");
                }
            }

            if (project.DeletedLogo[0] is not null)
            {
                string deletedLogoString = project.DeletedLogo[0];
                string[] deletedPaths = deletedLogoString.Split(',');

                foreach (string path in deletedPaths)
                {
                    content.Add(new StringContent(path), "DeletedLogo");
                }
            }
        }


    }
    public class Delete
    {
        public string Id { get; set; }
    }
}
