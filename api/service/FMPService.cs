using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interfaces;
using api.models;
using Newtonsoft.Json;

namespace api.service
{
    public class FMPService : IFMService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        private readonly ILogger<FMPService> _logger;
        public FMPService(HttpClient httpClient, IConfiguration config, ILogger<FMPService> logger)
        {
            _config = config;
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
           
             var apikey = _config["FMPKey"];
            _logger.LogInformation(apikey);
           
        
            
            try{
                var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/search?query={symbol}&apikey={_config["FMPKey"]}");

                if (result.IsSuccessStatusCode) {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stocks = JsonConvert.DeserializeObject<List<FMPStock>>(content);
                    // var stock = tasks[0];
                    var stock = stocks?.FirstOrDefault();
                    if (stock != null) {
                        return stock.ToStockFromFMP();
                    }
                    return null;

                }
                return null;
              
            }
            catch(Exception e){
                Console.WriteLine(e);
                return null;
            }
        }
    }
}