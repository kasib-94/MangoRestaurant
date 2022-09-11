﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity;

public static class SD
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("Mango", "Mango Server "),
            new ApiScope("read", "read your data "),
            new ApiScope("write", "write your data"),
            new ApiScope("delete", "delete your data "),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            new Client
            {
                ClientId = "mango",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RedirectUris = {"https://localhost:37108/signin-oidc"},
                PostLogoutRedirectUris = {"http://localhost:37108/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "mango"
                }
            }
        };
}