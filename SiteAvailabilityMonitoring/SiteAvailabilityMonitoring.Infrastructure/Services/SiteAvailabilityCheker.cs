﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

using SiteAvailabilityMonitoring.Infrastructure.Services.Contracts;

namespace SiteAvailabilityMonitoring.Infrastructure.Services
{
    public class SiteAvailabilityCheker : ISiteAvailabilityCheker
    {
        private readonly HttpClient _httpClient;

        public SiteAvailabilityCheker(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException();
        }

        public async Task<bool> CheckUrlAsync(string url)
        {
            try
            {
                using (var response = await _httpClient.GetAsync(url))
                {
                    if(response.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}