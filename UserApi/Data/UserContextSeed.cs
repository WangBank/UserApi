using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Users.Api.Data
{
    public class UserContextSeed
    {
        private ILogger<UserContextSeed> logger;
        public UserContextSeed(ILogger<UserContextSeed> logger)
        {
           this.logger = logger;
        }
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory,int? retry=0)
        {
            var retryFpr = retry.Value;
            try
            {
                using (var scope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = (UserContext)scope.ServiceProvider.GetService(typeof(UserContext));
                    var logger = (ILogger<UserContextSeed>)scope.ServiceProvider.GetService(typeof(ILogger<UserContextSeed>));
                    logger.LogDebug("Begin UserContextSeed SeedAsync");
                    context.Database.Migrate();
                    if (!context.Users.Any())
                    {
                       await context.Users.AddAsync(new Models.User { Name="王振",Gender=1});
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
