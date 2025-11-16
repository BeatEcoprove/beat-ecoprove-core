using System.Text.RegularExpressions;

using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.ImageUpload.Commands;

namespace BeatEcoprove.Api.Middlewares;

public partial class PictureFormatterMiddleware: IMiddleware
{
    [GeneratedRegex(@"public/([^/]+)/([^""'\s]+)")]
    private static partial Regex PublicUrlRgx();
    
    private static bool IsValidToHandle(HttpContext context, string responseText)
        => context.Response.ContentType?.Contains("application/json") == true &&
           !string.IsNullOrEmpty(responseText);
    
    private static bool IsPictureUrl(string responseText)
        => responseText.Contains("public/");

    private static string TransformPublicUrls(HttpContext context, string json)
        => PublicUrlRgx().Replace(json, match 
            => new UploadedUrl(match.Value)
                .Format(context));
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        
        if (!IsValidToHandle(context, responseText)) 
            await next(context);

        if (!IsPictureUrl(responseText))
            await next(context);
        
        var modifiedResponse = TransformPublicUrls(context, responseText);
                
        context.Response.Body = originalBodyStream;
        await context.Response.WriteAsync(modifiedResponse);
    }
}