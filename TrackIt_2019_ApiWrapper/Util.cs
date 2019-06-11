using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using System.Dynamic;

namespace TrackIt_2019
{
    public static class Util
    {
        public static dynamic getResultantObject(dynamic jsonResponseObject)
        {
            dynamic result = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)result;
            foreach (var property in (IDictionary<String, Object>)jsonResponseObject)
            {
                dynamic temp = property.Value;
                if(Type.GetTypeCode(temp.Value.GetType()) == TypeCode.Int64)
                {
                    dictionary.Add(temp.DisplayName.Replace(" ", ""), (int)temp.Value);
                } else
                {
                    dictionary.Add(temp.DisplayName.Replace(" ", ""), temp.Value);
                }
            }
            return result;
        }

        public static async Task<dynamic> GetRequest(string[] PathSegments)
        {
            
            IFlurlRequest request = TrackIt_2019.API.BaseUri.AppendPathSegments(PathSegments).WithOAuthBearerToken((string)TrackIt_2019.API.Authentication.Token.access_token);
            dynamic result = await request.GetAsync().ReceiveJson();
            return result;
        }

        public static async Task<dynamic> PostRequest(string[] PathSegments, object RequestBodyObject)
        {
            IFlurlRequest request = TrackIt_2019.API.BaseUri.AppendPathSegments(PathSegments).WithOAuthBearerToken((string)TrackIt_2019.API.Authentication.Token.access_token);
            dynamic result = await request.PostJsonAsync(RequestBodyObject).ReceiveJson();
            return result;
        }
    }
}
