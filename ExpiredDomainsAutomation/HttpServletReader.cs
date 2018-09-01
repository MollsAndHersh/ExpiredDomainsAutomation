using System.IO;
using System.Text;
using System.Threading;

namespace ExpiredDomainsAutomation
{
	/// <summary>
	/// Description of HttpServletReader.
	/// </summary>
	public class HttpServletReader
	{
		private static readonly HttpServletReader ServletReader = new HttpServletReader();
		private HttpConnector _connector = null;
		
		static HttpServletReader(){	}
		
		public static HttpServletReader Instance
		{
			get
			{
				return ServletReader;	
			}
		}
		
		internal string GetResponseStream(string userId, string password, string domain, string urlPath)
		{
			string responseString;
			
			Authenticate(domain, urlPath, userId, password);
			
			Stream responseStream = this.GetResponseStream(urlPath, 0, false);
			
            using (StreamReader reader = new StreamReader(responseStream, Encoding.UTF8))
            {
                responseString = reader.ReadToEnd();
                reader.Close();
            }
            
            responseStream.Close();
            return responseString;
		}
		
		private Stream GetResponseStream(string urlPath, int timeOut, bool refreshContainer)
		{
			//wait until all data is filled into the invocation log
			if(timeOut > 0)
			{
				Thread.Sleep(timeOut);
			}
			var response = _connector.Get(urlPath, refreshContainer, 100);
			return response;
		}

		private void Authenticate(string domain, string urlPath, string userId, string password)
		{
			_connector = new HttpConnector(domain);
			_connector.Authenticate(userId, password, urlPath);
		}
	}
}
