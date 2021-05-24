using System;
using System.Net;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.February2021.Data.Interfaces;

namespace Hahn.ApplicatonProcess.February2021.Data.Services
{
    public class HttpDataAccessService : IHttpDataAccessService
    {
        public async Task<bool> ValidateCountry(string countryName)
        {
            try
            {
                using var client = new WebClient();

                var response = await client.DownloadStringTaskAsync(
                    $"https://restcountries.eu/rest/v2/name/{countryName}?fullText=true"
                );

                return !response.Contains("404") && !response.Contains("Not Found");
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}