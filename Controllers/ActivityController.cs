using EventureAPI.Data.Repositories;
using EventureAPI.Data.Repositories.IRepositories;
using EventureAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public ActivityController(IActivityService activityService, ICategoryService categoryService, IUserService userService)
        {
            _activityService = activityService;
            _categoryService = categoryService;
            _userService = userService;
        }
    }
}
