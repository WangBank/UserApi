using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserApi.Data;
using UserApi.Dtos;

namespace UserApi.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ILogger<UsersController> logger;
        private UserContext userContext;
        public UsersController(UserContext userContext)
        {
            this.userContext = userContext;
            this.logger = logger;
        }
        [Route("")]
        [HttpGet]
        public async  Task<IActionResult> Get()
        {
            UserIndentity userIndentity = new UserIndentity { Name = "王振", UserId = 2 };
           var user = userContext.Users
                .AsNoTracking().Include(u=>u.Properties)
                .SingleOrDefault(u=>u.Id== userIndentity.UserId);
            if (user==null)
            {
                throw new UserOperatorExpection($"错误的用户上下文id{userIndentity.UserId}");
            }
            return new JsonResult(user);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
