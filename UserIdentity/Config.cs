using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityModel.OidcConstants;
using GrantTypes = IdentityServer4.Models.GrantTypes;

namespace UserIdentity
{
    public class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "test",
                    Password = "123",

                    Claims = new List<Claim>
                    {
                        new Claim("role", "user")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "admin",
                    Password = "123",

                    Claims = new List<Claim>
                    {
                        new Claim("role", "admin")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("roles","role",new List<string>{ "role"})
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("User_APi", "UserAPI")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Android",
                    RefreshTokenExpiration=TokenExpiration.Sliding,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedGrantTypes = new List<string>{ "wangbank"},
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "User_APi" },
                }
            };
        }
    }
}
