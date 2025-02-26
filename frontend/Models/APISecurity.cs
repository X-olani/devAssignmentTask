using System.Net.Http.Headers;
using System.Text;

namespace frontend.Models
{
    public class APISecurity
    {
        // hard coded header cred
        public static void InitHead(HttpClient httpClient)
        {
            string userName = "admin";
            string password = "cool123";

            string authInfo = userName + ":" + password;

            //encoding username and password when passing it in header
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
        }
    }
}
