using Management.Common;
using Management.Common.Enum;
using Management.Common.Models;
using Management.Common.Models.ApiResponse;
using Management.Common.Models.DTO;
using Management.Common.Models.Entity;
using Management.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Drawing.Printing;

namespace Management.Controllers
{

    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        public IApiHelperService _apiHelperService;

        public ProjectController(IApiHelperService apiHelperService)
        {
            _apiHelperService = apiHelperService;
        }


        public async Task<IActionResult> Index(int? page, string sortField, string sortOrder)
        {
            const int pageSize = 2;

            var searchResultsJson = HttpContext.Session.GetString("SearchResults");
            var value=HttpContext.Session.GetString("SearchValue");

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
            //allResults.ForEach(x => x.TechStackUsed = x.TechStackUsedObj.Select(x => x.Id).ToList());

            //int totalPages = (int)Math.Ceiling((double)allResults.Count / pageSize);
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectDto project)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            var content = new MultipartFormDataContent();
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

            foreach (var guid in project.TechStackUsed)
            {
                content.Add(new StringContent(guid.ToString()), "TechStackUsed");
            }

            content.Add(new StreamContent(project.Logo.OpenReadStream()), "Logo", project.Logo.FileName);

            content.Add(new StreamContent(project.Documentation.OpenReadStream()), "Documentation", project.Documentation.FileName);

            foreach (var snapshot in project.SnapShoots)
            {
                content.Add(new StreamContent(snapshot.OpenReadStream()), "SnapShoots", snapshot.FileName);
            }

            var result = await _apiHelperService.PostAsync<BaseResponse>(ApiRoute.CreateProject, content);
            if (result.Success)
            {
                return RedirectToAction("Index", "Project");
            }
            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _apiHelperService.PostAsyncUpdateDeleteData<ProjectDto>(ApiRoute.UpdateProject, id);

            ViewBag.Logo = result.LogoUrl;
            ViewBag.Documentation = result.DocumentationUrl;
            ViewBag.SnapShoot = result.SnapShootsUrl;

            var techStacks = await _apiHelperService.GetAsync<List<TechStackDto>>(ApiRoute.GetTechStackNames);
            ViewBag.TechStackNames = new SelectList(techStacks, "Id", "Name");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProject(ProjectDto project)
        {
            var content = new MultipartFormDataContent();
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
            content.Add(new StringContent(JsonConvert.SerializeObject(new List<TechStackDto> { })), "TechStackUsedObj");

            var result = await _apiHelperService.PostAsync<ProjectDto>(ApiRoute.UpdateProjectPost, content);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null) { return BadRequest(); }
            var result = await _apiHelperService.PostAsyncUpdateDeleteData<BaseResponse>(ApiRoute.DeleteProject, id);
            if (result.Success)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index");
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
                

                return RedirectToAction("Index", new { page, sortField, sortOrder });
            }

            var result = await _apiHelperService.PostAsyncData<List<ProjectDto>>(ApiRoute.SearchByName, value);
            if (result.Count >= 1)
            {
                var paginatedSearchResults = await SortProjects(result, sortField, sortOrder, page, pageSize);
                HttpContext.Session.SetString("SearchResults", JsonConvert.SerializeObject(paginatedSearchResults));
                return RedirectToAction("Index", new { page, sortField, sortOrder });
            }
            return RedirectToAction("Index", result);

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



    }
    public class Delete
    {
        public string Id { get; set; }
    }
}
