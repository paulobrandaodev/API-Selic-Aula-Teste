using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace selic.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        [HttpGet("{ano}")]
        public IActionResult Get(int ano){

            var dataInicial = "01/01/"+ano;
            var dataFinal   = "01/12/"+ano;

            var result = this.consultaAPI(dataInicial, dataFinal);
            return result;
        }

        [HttpGet("{mes}/{ano}")]
        public IActionResult Get(int mes, int ano){

            var dataInicial = "01/"+mes+"/"+ano;
            var dataFinal   = "01/"+mes+"/"+ano;

            var result = this.consultaAPI(dataInicial, dataFinal);
            return result;
        }
        public IActionResult consultaAPI(string dataInicial, string dataFinal){
            
            JsonResult result;

            try{

                var URL = "https://api.bcb.gov.br/dados/serie/bcdata.sgs.4189/dados?formato=json&dataInicial="+dataInicial+"&dataFinal="+dataFinal+"";
                
                WebClient httpClient = new WebClient();
                httpClient.Headers["User-Agent"] = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";

                var jsonData = httpClient.DownloadString(URL);
                var jsonConv = JsonConvert.DeserializeObject(jsonData);

                result = Json(jsonConv);


            }catch(Exception ex){
                result = new JsonResult("");
                result.Value = ex.Message;
                result.StatusCode = 403;
                result.ContentType = "aplication/json";
            }
            return result;
        }
    }
}