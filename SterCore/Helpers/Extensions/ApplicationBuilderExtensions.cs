
using leave_management.Services.DataSeeds;
using leave_management.Services.DataSeeds.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ExtSeed(this IApplicationBuilder builder, ISeed seed)
        {
            seed.SeedDataBase();
        }
    }
}
