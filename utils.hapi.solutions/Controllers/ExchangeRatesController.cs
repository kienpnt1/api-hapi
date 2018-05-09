using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Xml;
using System;
using System.Text.RegularExpressions;
using utils.hapi.solutions.Common;
using utils.hapi.solutions.Models;

namespace utils.hapi.solutions.Controllers
{
    [System.Web.Http.RoutePrefix("api/exchange-rates")]
    public class ExchangeRatesController : ApiController
    {
        static HttpClient client = new HttpClient();

        [System.Web.Http.Route("{bank}/{currency}")]
        public IHttpActionResult Get(string bank, string currency)
        {
            try
            {
                if (currency == null || currency == "")
                    throw new Exception("Currency is required.");
                string path = CommonHelpers.GetLinkAuthorizationBank(bank);
                HttpResponseMessage response = client.GetAsync(path).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resultContent = response.Content.ReadAsStringAsync().Result;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(XElement.Parse(resultContent).ToString());
                    string resultJson = Regex.Replace(JsonConvert.SerializeXmlNode(xmlDocument), "(?<=\")(@)(?!.*\":\\s )", string.Empty, RegexOptions.IgnoreCase);
                    RootObject objectConvert = JsonConvert.DeserializeObject<RootObject>(resultJson);
                    if (objectConvert.ExrateList.Exrate.Count == 0)
                    {
                        return NotFound();
                    }
                    foreach (var exrateValue in objectConvert.ExrateList.Exrate)
                    {
                        if (exrateValue.CurrencyCode.ToUpper() == currency.ToUpper())
                        {
                            return Ok(CommonHelpers.ToJson(exrateValue));
                        }
                    }
                    return NotFound();
                }
                return BadRequest(response.RequestMessage.ToString());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}