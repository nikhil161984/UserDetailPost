using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace UserPostDetails
{
   public class UserAPICalls
    {
         HttpClient client = new HttpClient();
        public List<Post> GetUserPosts()
        {
            return CallApi("https://jsonplaceholder.typicode.com/posts", "post") as List<Post>;
        }

        public List<User> GetUsers()
        {
            return CallApi("https://jsonplaceholder.typicode.com/users","user") as List<User>;
        }

        private object CallApi( string url, string calltype)
        {
            JsonSerializer jsonSerializerSettings = new JsonSerializer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            var response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                if (calltype == "user")
                    return JsonConvert.DeserializeObject<User[]>(reader.ReadToEnd()).ToList();
                else
                    return JsonConvert.DeserializeObject<Post[]>(reader.ReadToEnd()).ToList();
            }
        }
    }
}
