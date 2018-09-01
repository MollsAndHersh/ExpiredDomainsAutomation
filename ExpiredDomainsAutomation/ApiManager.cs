using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ServiceStack;

namespace ExpiredDomainsAutomation
{
    internal static class JsonContent
    {
        public static StringContent Create(string json)
        {
            return new StringContent(json) { Headers = { ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded") } };
        }

        public static StringContent Create(object obj)
        {
            var json = obj.ToJson();
            return Create(json);
        }
    }

    public class ApiManager
    {
        private readonly HttpClient _client;

        public ApiManager(string url)
        {
			CookieContainer container = new CookieContainer();
			container.Add(new Uri(url), new Cookie("cart_id", "1453353586.7495-59849c0be0141ef762497d3b508bf18ddea93137"));
			container.Add(new Uri(url), new Cookie("BIGipServername.com-PCI-80","220304394.20480.0000"));
			container.Add(new Uri(url), new Cookie("SnapABugHistory", "1#"));
			//container.Add(new Uri(url), new Cookie("acct_login_time", "1453276657"));
			container.Add(new Uri(url), new Cookie("pmolt", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ2aWQiOjQ0MDQ3NDkyLCJsYWlkIjoxMzUxMTU0fQ.XGj4DSr1fox8yFGI59Uq4nIQHwr5LpcgP-vY7c2ZfAo"));
			container.Add(new Uri(url), new Cookie("__utmx", "225248517.ep5u_QPVQ3KtPVAJKFYWJA$3399857-40:1"));
			container.Add(new Uri(url), new Cookie("__utmxx", "225248517.ep5u_QPVQ3KtPVAJKFYWJA$3399857-40:1453148050:15552000"));
			container.Add(new Uri(url), new Cookie("__ar_v4", "%7CQF42R7HX45DAHE7BG6USSO%3A20160117%3A1%7CSAEIBMIHS5GJ3BJRWQ7D2O%3A20160117%3A1%7COWMHWICJZFHB3JCWQQNJ3O%3A20160117%3A1"));
			container.Add(new Uri(url), new Cookie("pmovt", "19b77cfa812e620a209e1198a50652b5fbe5bb7e"));
			container.Add(new Uri(url), new Cookie("SnapABugRef", "https%3A%2F%2Fwww.name.com%2Fexpired_domains.php%20"));
			container.Add(new Uri(url), new Cookie("cart_totals", "0%7C0.00%7C0.00"));
			container.Add(new Uri(url), new Cookie("REG_IDT", "pr32nbk5n0jgkjpc7psluirbn3"));
			container.Add(new Uri(url), new Cookie("__utmt", "1"));
			container.Add(new Uri(url), new Cookie("__utma", "225248517.91347344.1451200092.1453148046.1453275414.6"));
			container.Add(new Uri(url), new Cookie("__utmb", "225248517.5.9.1453275741673"));
			container.Add(new Uri(url), new Cookie("__utmc", "225248517"));
			container.Add(new Uri(url), new Cookie("__utmz", "225248517.1451200092.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)"));
			container.Add(new Uri(url), new Cookie("SnapABugVisit", "42#1451200093"));
			HttpClientHandler handler = new HttpClientHandler { UseCookies = true, CookieContainer = container };
			
            _client = new HttpClient (handler){ BaseAddress = new Uri(url) };
			
            _client.DefaultRequestHeaders.Add("Keep-Alive", "true");
        }

        public bool Login(string path, string user, string pw)
        {
			_client.DefaultRequestHeaders.Referrer = new Uri("https://www.name.com/expired_domains.php");
			_client.DefaultRequestHeaders.Add("Accept", "application/json, text/javascript, */*; q=0.01");
			_client.DefaultRequestHeaders.Add("x-csrf-token-auth", "b77037bbee4830419424c979a2efceb1e315009711ab7a4d44517d0076c8b9282430c90d81bdd905087fe14544711350693890fe062c61d75ec41defa7fe5c8a");
			_client.DefaultRequestHeaders.Add("Origin", "https://www.name.com");
			_client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
			_client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
			_client.DefaultRequestHeaders.Add("Cookie", "REG_IDT=6lhbtrr2g0i29u14ku9g6he1n1; pmovt=cc34b8d4acd9beaaf3427631018b348f4ce0a90d; pmolt=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ2aWQiOjQ4NjUxNjM3fQ.yEPcp0Vl_kWo5wxEdyBD24hGmsksO2C3HU6496UMg8g; cart_id=1453353586.7495-59849c0be0141ef762497d3b508bf18ddea93137; cart_totals=0%7C0.00%7C0.00; BIGipServername.com-PCI-80=220304394.20480.0000; __utmt=1; __utma=225248517.1096425831.1453353590.1453353590.1453353590.1; __utmb=225248517.1.10.1453353590; __utmc=225248517; __utmz=225248517.1453353590.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); SnapABugRef=https%3A%2F%2Fwww.name.com%2Fexpired_domains.php%20; SnapABugHistory=1#; SnapABugVisit=1#1453353590");
			string response =
				_client.PostAsync(path, JsonContent.Create(string.Format("username={0}&password={1}&vip_id=&vip_pin=&vip_flag=off&csrf_token=b77037bbee4830419424c979a2efceb1e315009711ab7a4d44517d0076c8b9282430c90d81bdd905087fe14544711350693890fe062c61d75ec41defa7fe5c8a", user, pw)))
                    .Result.Content.ReadAsStringAsync().Result;
			Console.Write(response);
			return response.Contains("Welcome back");// ?????????            
        }

        public bool SetBid(string path, string bid)
        {
            string response =
                _client.PostAsync(path, JsonContent.Create(string.Format("trigger_auction=1&bid={0}&terms=1", bid)))
                    .Result.Content.ReadAsStringAsync().Result;
            if (response.Contains("<div class=\"error_message\">"))
            {
                Console.WriteLine("### Error: Cannot set bid '{0}' for url '{1}':", bid, path);
                int ind = response.IndexOf("<div class=\"error_message\">", StringComparison.InvariantCulture);
                response = response.Substring(ind + 28).TrimStart();
                response = response.Substring(0, response.IndexOf("</div>", StringComparison.InvariantCulture));
                Console.Write(response);
                return false;
            }
            if (response.Contains("Your bid has been accepted"))
            {
                Console.WriteLine("### Success: Bid '{0}' is set successfully for path '{1}'.", bid, path);
                return true;
            }
            //other cases
            try
            {
                int index = response.IndexOf("<div id=\"innerContentContainerContent\">",
                    StringComparison.InvariantCulture);
                response = response.Substring(index + 34).TrimStart();
                response = response.Substring(0,
                    response.IndexOf("<div class=\"clear\"></div>", StringComparison.InvariantCulture));
            }
            catch {}
            Console.WriteLine("Cannot initialize status of bid, response text: " + response);
            return false;            
        }

    }
}
