using System;
using System.Collections.Specialized;

namespace Utility.Extensions
{
    public static class StringParsingExtensions
    {
        public static NameValueCollection ParseQueryString(this string query)
        {
            NameValueCollection queryParameters = new NameValueCollection();

            if (string.IsNullOrEmpty(query))
            {
                return queryParameters;
            }

            string[] queryParams = query.TrimStart('?').Split('&');

            foreach (string param in queryParams)
            {
                int equalsIndex = param.IndexOf('=');
                if (equalsIndex >= 0)
                {
                    string key = Uri.UnescapeDataString(param.Substring(0, equalsIndex));
                    string value = Uri.UnescapeDataString(param.Substring(equalsIndex + 1));

                    queryParameters.Add(key, value);
                }
                else
                {
                    queryParameters.Add(Uri.UnescapeDataString(param), "");
                }
            }

            return queryParameters;
        }

    }
}