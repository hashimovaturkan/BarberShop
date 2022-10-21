using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp.Authenticators;

namespace BarberShop.Application.Common.Components
{
    public static class SmsVerification
    {
        static public async Task<bool> SendSms(string phoneNumber, string value)
        {
            string result;
            try
            {
                //var client = new RestClient("http://www.poctgoyercini.com/api_http/sendsms.asp");
                //var request = new RestRequest("http://www.poctgoyercini.com/api_http/sendsms.asp", Method.Post);

                //request.AddParameter("user", "burncode");
                //request.AddParameter("password", "myaccess");
                //request.AddParameter("gsm", phoneNumber);
                //request.AddParameter("text", value);

                //request.AddHeader(HttpRequestHeader.ContentType.ToString(), "text/html");

                //var response = await client.ExecuteAsync(request);

                var client = new RestClient("https://api.labsmobile.com/json/send");
                client.Authenticator = new HttpBasicAuthenticator("main@legacybarber.pl", "oCnkp6yygwS4q5pmFHQ8UpKLmbW5gH5u");
                var request = new RestRequest("https://api.labsmobile.com/json/send", Method.Post);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddBody("{\"message\":\"" + value + "\", \"tpoa\":\"BarberShop\",\"recipient\":[{\"msisdn\":\"" + phoneNumber + "\"}]}");
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                    return true;
            }
            catch (Exception ex)
            {
                throw;
            }

            return false;
        }
    }

    public class SaveResponseModel
    {
        [JsonPropertyName("data")] public SaveResponseData Data { get; set; }
        [JsonPropertyName("error")] bool Error { get; init; }
        [JsonPropertyName("exception")] string Exception { get; init; }
    }

    class GetResponseModel
    {
        [JsonPropertyName("data")] public GetReponseData Data { get; set; }
        [JsonPropertyName("error")] bool Error { get; set; }

        [JsonPropertyName("exception")] string Exception { get; set; }
    }


    public class GetReponseData
    {
        [JsonPropertyName("url")] public string Url { get; set; }
        [JsonPropertyName("size")] string Size { get; init; }
        [JsonPropertyName("name")] string Name { get; init; }
        [JsonPropertyName("mimeType")] string MimeType { get; init; }
    }

    public class SaveResponseData
    {
        [JsonPropertyName("file")] public FileExt File { get; set; }
    }

    public class FileExt
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("size")] long Size { get; init; }
        [JsonPropertyName("mimeType")] string MimeType { get; init; }
    }
}
