using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;

namespace Whoop.Function
{
    public static class WhoopExtractDownloadLink
    {
        [FunctionName("WhoopExtractDownloadLink")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(requestBody);

            var downloadLink = htmlDoc.ExtractDownloadLink();
            log.LogInformation(downloadLink);

            var client = new HttpClient( new HttpClientHandler
            {
                AllowAutoRedirect = false,
            });

            var result = await client.GetAsync(downloadLink);
            var s3Link = result.Headers.Location?.ToString();

            return new OkObjectResult(s3Link);
        }
    }

    public static class HtmlDocumentPackExtensions
    {
        public static string ExtractDownloadLink(this HtmlDocument htmlDoc)
        {
            var downloadLinkNode = htmlDoc.DocumentNode.SelectSingleNode("//a[normalize-space(.)='DOWNLOAD YOUR DATA']");
            var link = downloadLinkNode?.GetAttributeValue("href", string.Empty).Trim('\\').Trim('\"').Trim('\\');
            return link;
        }
    }
}
