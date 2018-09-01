using System.IO;
using System.Net;

namespace ExpiredDomainsAutomation
{
    public class HttpConnector
    {
        public CookieContainer CookieContainer { get; private set; }

        public string BaseUrl { get; private set; }

        private string _auth;

        public HttpConnector(string host)
        {
            CookieContainer = new CookieContainer();            
            BaseUrl = "https://" + host;
        }

        public void Authenticate(string user, string password, string url)
        {
            string authentication = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(user + ":" + password));
            _auth = authentication;
            FollowRedirects(url, authentication, 100);
        }

        public Stream Get(string url, bool refreshContainer, int connectionLimit)
        {
            Stream stream = null;

            if (refreshContainer)
            {
                //Refresh(connectionLimit);
            }

            HttpWebRequest request = CreateRequest(url, connectionLimit);
        
            request.Method = "GET";            
            request.KeepAlive = true;                     
           
            if (!refreshContainer)
            {
                request.ContentType = "text/plain";
                request.AutomaticDecompression = DecompressionMethods.GZip;

            }

      
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();           
            return stream;
        }
        
        /// <summary>
        /// Due to that storages aren't syncronous a refreshment of the storage is needed.
        /// The runmr.json call has been implemented from Andrei Maus only for testing / The runmr.json should not be used in a production environment
        /// Just ignore the response.
        /// </summary>
        private void Refresh(int connectionLimit)
        {

            try
            {
                HttpWebRequest request = CreateRequest("/maintenance/ilog/runmr.json", connectionLimit);
                request.Method = "GET";
                request.Timeout = 2000;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();
            }
            catch { 
            
            }    
            
        }


        public HttpWebRequest CreateRequest(string path, int connectionLimit)
        {
            string url = path.StartsWith("http") ? path : (BaseUrl + path);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ServicePoint.ConnectionLimit = connectionLimit;
            request.UserAgent = "AXN Http Client";
            request.Headers.Add("Authorization",_auth);
            request.CookieContainer = CookieContainer;            
            
            return request;
        }


        private void FollowRedirects(string url, string auth, int connectionLimit)
        {
            HttpWebRequest request = CreateRequest(url, connectionLimit);
            request.Method = "GET";              
            //request.Method = "POST";      
        	//car inputDate = 
            request.AllowAutoRedirect = false;          

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();            
            HttpStatusCode code = response.StatusCode;         
            if (code == HttpStatusCode.Found||code == HttpStatusCode.Redirect)
            {
                string location = response.Headers["Location"];
                response.Close();                     
                FollowRedirects(location, auth, connectionLimit);                                               
            }
           
        }

     

    }
}