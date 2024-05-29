﻿using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HelpDesk.Authorization;

namespace HelpDesk
{
    [DependsOn(
        typeof(HelpDeskCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HelpDeskApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HelpDeskAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HelpDeskApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
