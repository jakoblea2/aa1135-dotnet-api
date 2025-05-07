using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace aa1135_dotnet_api.Swagger;

public class SwaggerUIConfigurator(IApiVersionDescriptionProvider apiVersionProvider, IConfiguration config) : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        foreach (var description in apiVersionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"aa1135_dotnet_api {description.ApiVersion}"
            );
        }
        options.OAuthAppName(config["Swagger:AppName"]);
        options.OAuthClientId(config["Swagger:ClientId"]);
        options.OAuthScopes($"api://{config["AzureAd:ClientId"]}/user_impersonation");
        options.OAuthUsePkce();
        options.RoutePrefix = string.Empty;
    }
}
