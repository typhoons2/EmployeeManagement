using Microsoft.OpenApi.Models;

internal class openApiInfo : OpenApiInfo
{
    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }
    public OpenApiContact Contact { get; set; }
}