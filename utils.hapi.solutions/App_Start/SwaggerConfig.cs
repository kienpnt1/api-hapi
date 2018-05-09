using System.Web.Http;
using WebActivatorEx;
using utils.hapi.solutions;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace utils.hapi.solutions
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "utils.hapi.solutions");
                    })
                .EnableSwaggerUi(c =>
                    {

                    });
        }
    }
}
