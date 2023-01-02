using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WeldingPassportsApp.IntegrationTests.Helpers
{
    public class AntiForgeryTokenExtractor
    {
        public static string AntiForgeryTokeName { get; } = "__RequestVerificationToken";
        public static string AntiForgeryCookieName { get; } = "AntiForgeryTokenCookie";

        private static string ExtractAntiForgeryCookiesValueFrom(HttpResponseMessage response)
        {
            var AntiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(x => x.Contains(AntiForgeryCookieName));
            
            if (AntiForgeryCookie == null)
                throw new ArgumentException($"Cookie '{AntiForgeryCookieName}' not found in HTTP response", nameof(response));

            var AntiForgeryCookieValue = SetCookieHeaderValue.Parse(AntiForgeryCookie).Value.ToString();
            
            return AntiForgeryCookieValue;
        }

        private static string ExtractAntiForgeryToken(string htmlBody)
        {
            var requestVerificationTokenMatch =
                Regex.Match(htmlBody, $@"\<input name=""{AntiForgeryTokeName}"" type=""hidden"" value=""([^""]+)"" \/\>");

            if (requestVerificationTokenMatch.Success)
                return requestVerificationTokenMatch.Groups[1].Captures[0].Value;

            throw new ArgumentException($"Anti forgery token '{AntiForgeryTokeName}' not found in HTML", nameof(htmlBody));
        }

        public static async Task<(string tokenValue, string cookieValue)> ExtractAntiForgeryValues(HttpResponseMessage response)
        {
            string token = ExtractAntiForgeryCookiesValueFrom(response);
            string cookie = ExtractAntiForgeryToken(await response.Content.ReadAsStringAsync());

            return (tokenValue : token, cookieValue : cookie);
        }
    }
}
