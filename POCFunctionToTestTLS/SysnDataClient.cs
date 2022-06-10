using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POCFunctionToTestTLS
{
    public class SysnDataClient
    {

        /// <summary>
        ///  Gets documents from SynsData based on the Url passed
        /// </summary>
        /// <param name="url">URL String sent as a Parameter</param>
        public async Task<ClientResult<HttpResponseMessage>> GetDocumentData(string url)
        {
            var getDocumentDataResult = new ClientResult<HttpResponseMessage>();
            //var hash = X509Certificate.CreateFromSignedFile("31075939.cer").GetCertHashString();
            //ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {

                    //httpClientHandler.ServerCertificateCustomValidationCallback += (message, cert, chain, sslPolicyErrors) => {
                    //    if (cert.GetName().Equals("31075939.cer") || cert.Thumbprint== "49225e297d27c21049383aefa2302496e8b364de")
                    //    {
                    //        return true;
                    //    }
                    //    return true;
                    //};
                    var certFile = Path.Combine("C:\\Users\\extcikaal\\Documents\\FDMCertificates\\PFX", "31075939.cer");
                    httpClientHandler.ClientCertificates.Add(new X509Certificate2(certFile));
                    //certFile = Path.Combine("C:\\Users\\extcikaal\\Documents\\FDMCertificates\\PFX", "30955730.cer");
                    //httpClientHandler.ClientCertificates.Add(new X509Certificate2(certFile));

                    ////Setting TLS 1.2 protocol
                    httpClientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls13;

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

                    //var stream = new FileStream(url, FileMode.Open, FileAccess.Read);
                    Uri absoluteUri = new Uri(url, UriKind.Absolute);

                    HttpClient client = new HttpClient(httpClientHandler);
                    HttpResponseMessage response = await client.GetAsync(url);

                    Byte[] bytes = await response.Content.ReadAsByteArrayAsync();
                    //result.Content = new StreamContent(stream);

                    getDocumentDataResult.StatusCode = response.StatusCode;
                    getDocumentDataResult.ResultContent = bytes.ToString();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());\
                getDocumentDataResult.ExceptionText = ex.Message;
                getDocumentDataResult.StatusCode = HttpStatusCode.InternalServerError;
            }
            return getDocumentDataResult;
        }

        public async Task<byte[]> DownloadFile(string url)
        {
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        var certFile = Path.Combine("C:\\Users\\extcikaal\\Documents\\FDMCertificates\\PFX", "31075939.cer");
                        httpClientHandler.ClientCertificates.Add(new X509Certificate2(certFile));

                        using (var result = await client.GetAsync(url))
                        {
                            if (result.IsSuccessStatusCode)
                            {
                                return await result.Content.ReadAsByteArrayAsync();
                            }

                        }
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
