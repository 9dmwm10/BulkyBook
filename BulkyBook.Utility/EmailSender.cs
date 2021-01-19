using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSetting)
        {
            _emailSettings = emailSetting.Value;

        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3"); 
            client.Authenticator =  new HttpBasicAuthenticator("api", "73cb83f76bc37785b0bab361b05dc41e-65b08458-9e109d60");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox3ada15b02ebf4df8a2ac14ee832978db.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <mailgun@sandbox3ada15b02ebf4df8a2ac14ee832978db.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", subject);
            request.AddParameter("html", message);
            request.Method = Method.POST;
            await client.ExecuteAsync(request);
        }
    }
}
