using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using SearchFight.Model.Interfaces;

/// <summary>
/// class that handles the request and response from the search engine
/// code adapted from http://www.felling-software.com/2015/05/23/a-simple-google-search-client-in-csharp/
/// </summary>

namespace SearchFight.Model
{
    public class SearchClient
    {
        private static readonly Encoding DEFAULT_ENCODING = Encoding.UTF8;

        private static HttpWebRequest CreateRequest(Uri uri)
        {
            var request = WebRequest.Create(uri) as HttpWebRequest;
            if (request == null)
            {
                throw new InvalidOperationException("Could not instantiate web request.");
            }
            return request;
        }


        private static HttpWebResponse RetrieveResponse(WebRequest webRequest)
        {
            var response = webRequest.GetResponse() as HttpWebResponse;
            if (response == null)
            {
                throw new InvalidOperationException("Failed to retrieve response.");
            }

            return response;
        }


        private static Encoding GetEncoding(HttpWebResponse response, Encoding defaultTo)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(response.CharacterSet))
                {
                    return Encoding.GetEncoding(response.CharacterSet);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            // default
            return defaultTo;
        }

        public string GetResultString(Uri uri)
        {
            var request = CreateRequest(uri);
            var response = RetrieveResponse(request);
            var encoding = GetEncoding(response, DEFAULT_ENCODING);           

            using (var responseStream = response.GetResponseStream())
            using (var streamReader = new StreamReader(responseStream, encoding))
            {
                string responseText = streamReader.ReadToEnd();
                return responseText;
            }
        }
    }
}
