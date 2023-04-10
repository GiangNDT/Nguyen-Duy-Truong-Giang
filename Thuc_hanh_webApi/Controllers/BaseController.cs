using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Thuc_hanh_webApi.Entites;


namespace Thuc_hanh_webApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly PracticeDbContext _dbContext;

        public BaseController(PracticeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
