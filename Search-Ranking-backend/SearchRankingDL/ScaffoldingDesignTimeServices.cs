using EntityFrameworkCore.Scaffolding.Handlebars;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SearchRankingDL
{
    /// <summary>
    /// Use by EF scaffolding when you run the 'dotnet ef dbcontext scaffold' command. Adds a '_EF' suffix to all table models
    /// </summary>
    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            var suffix = "_EF";
            services.AddHandlebarsScaffolding();

            //Add optional Handlebars transformers
            services.AddHandlebarsTransformers2(
                entityTypeNameTransformer: n => n + suffix,
                entityFileNameTransformer: n => n + suffix,
                constructorTransformer: (e, p) => new EntityPropertyInfo(p.PropertyType + suffix, p.PropertyName + suffix),
                //propertyTransformer: (e, p) => new EntityPropertyInfo(p.PropertyType, p.PropertyName + suffix),
                navPropertyTransformer: (e, p) => new EntityPropertyInfo(p.PropertyType + suffix, p.PropertyName + suffix)
                );
        }
    }
}
