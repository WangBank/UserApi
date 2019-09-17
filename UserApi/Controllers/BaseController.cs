using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Users.Api.Dtos;

namespace Users.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected UserIdentity userIdentity => new UserIdentity { Name = "王振", UserId = 1 };
    }
}