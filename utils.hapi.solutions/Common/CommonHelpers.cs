using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace utils.hapi.solutions.Common
{
    public static class CommonHelpers
    {
        public static string GetLinkAuthorizationBank(string bank)
        {
            if (bank == null || bank == "")
            {
                throw new Exception("Bank is required.");
            }
            var authorizationBanks = System.Configuration.ConfigurationManager.AppSettings["authorization_bank"];
            string [] authorizationBanksArray = authorizationBanks.Split(',');
            for (int i = 0; i < authorizationBanksArray.Length; i++)
            {
                if (authorizationBanksArray[i].Trim().ToLower() == bank.ToLower())
                {
                    string path = System.Configuration.ConfigurationManager.AppSettings[bank.ToLower()];
                    if (path != "")
                    {
                        return path;
                    }
                }
            }
            throw new Exception("Bank is not match.");
        }

        public static string ToJson(object input)
        {
            string json = JsonConvert.SerializeObject(input);
            return json;
        }

    }
}