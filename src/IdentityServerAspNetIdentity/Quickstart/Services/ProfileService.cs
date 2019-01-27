﻿using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Quickstart.Services
{
    public class ProfileService : IProfileService
    {

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims.Add(new Claim(ClaimValueTypes.Email, "foo@mail.foo"));
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(true);
        }
    }
}
