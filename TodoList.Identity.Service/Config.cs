using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace TodoList.Identity.Service
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("todoListAPI", "Todo List API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource()
                {
                    Name = "todoListAPI",
                    DisplayName = "Todo List API",
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email
                    },
                    Scopes = { "todoListAPI" }

                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // machine to machine client (from quickstart 1)
                new Client
                {
                    ClientId = "todoList_client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "todoListAPI"
                    }
                },
                // interactive ASP.NET Core MVC Client
                new Client
                {
                    ClientId = "todoList_UI",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { 
                        "https://localhost:7001/signin-oidc"
                        },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { 
                        "https://localhost:7001/signout-callback-oidc"
                        },

                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "todoListAPI"
                    }
                },
            };
    }
}
