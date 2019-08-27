using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
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
        UserIndentity userIndentity; 
        public UsersController(UserContext userContext, ILogger<UsersController> logger)
        {
            this.userIndentity = new UserIndentity { Name = "王振", UserId = 1 };
            this.userContext = userContext;
            this.logger = logger;
        }
        [Route("")]
        [HttpGet]
        public async  Task<IActionResult> Get()
        {
           
           var user = await userContext.Users
                .AsNoTracking().Include(u=>u.Properties)
                .SingleOrDefaultAsync(u=>u.Id== userIndentity.UserId);
            if (user==null)
            {
                throw new UserOperatorExpection($"错误的用户上下文id{userIndentity.UserId}");
            }
            return new JsonResult(user);
        }

        [Route("")]
        [HttpPatch]
        public async Task<IActionResult> Patch(JsonPatchDocument<Models.User> patch)
        {
            var user = await userContext.Users.Include(u => u.Properties).SingleOrDefaultAsync(u=>u.Id == userIndentity.UserId);
            foreach (var property in user.Properties)
            {
                userContext.Entry(property).State = EntityState.Detached;
            }
            patch.ApplyTo(user);
            
            var orginProperties = await userContext.UserProperties.AsNoTracking().Where(u=>u.UserId == userIndentity.UserId).ToListAsync();
            var allProperties = orginProperties.Union(user.Properties).Distinct();
            var removeProperties = orginProperties.Except(user.Properties);
            var newProperties = allProperties.Except(orginProperties);
            foreach (var property in removeProperties)
            {
                userContext.Remove(property);
            }

            foreach (var property in newProperties)
            {
                userContext.Add(property);
            }
            userContext.Users.Update(user);
            await userContext.SaveChangesAsync();
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
