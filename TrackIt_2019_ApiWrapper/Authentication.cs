using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace TrackIt_2019
{
    public static partial class API
    {
        public static class Authentication
        {
            private static string _Username = null;
            public static string Username
            {
                get { return _Username; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Username", "You cannot set an empty username"); }
                    _Username = value.ToUpper();
                }
            }

            public static string Password { get; set; } = null;

            private static string _Group = null;
            public static string Group
            {
                get { return _Group; }
                set
                {
                    if (string.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("Group", "You cannot set an empty group"); }
                    _Group = value.ToUpper();
                }
            }

            private static dynamic _Token = null;
            public static dynamic Token
            {
                set { _Token = value; }
                get
                {
                    if (_Token == null)
                    {
                        getAuthenticationToken().Wait();
                    }
                    string expires = (string)_Token.expires;
                    DateTime ExpireDate = DateTime.Parse(expires);
                    if (System.DateTime.Parse(expires).Subtract(DateTime.Now).TotalMinutes < 2)
                    {
                        getAuthenticationToken().Wait();
                    }
                    return _Token;
                }
            }

            private static async Task getAuthenticationToken()
            {
                //string requestUri = TrackIt_2019.API.BaseUri + "token";
                string resp = await TrackIt_2019.API.BaseUri.AppendPathSegment("token").PostUrlEncodedAsync(new
                {
                    scope = "",
                    grant_type = "password",
                    username = Authentication.Group + "\\" + Authentication.Username,
                    password = Authentication.Password
                }).ReceiveString();
                Token = Newtonsoft.Json.JsonConvert.DeserializeObject(resp.Replace(".issued", "issued").Replace(".expires", "expires").Replace("First Name", "FirstName").Replace("Last Name", "LastName").Replace("EMail Address", "EmailAddress").Replace("Alternate Phone", "AlternatePhone"));
                return;
            }
        }
    }
}
