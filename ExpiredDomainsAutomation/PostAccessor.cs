using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExpiredDomainsAutomation
{
	class PostAccessor
	{

		public void PerofmLogin(string mainUrl, string firstpart, string authPart, string postData)
		{
			CookieCollection cookies = new CookieCollection();
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(mainUrl + firstpart);
			request.CookieContainer = new CookieContainer();
			request.CookieContainer.Add(cookies);
			//Get the response from the server and save the cookies from the first request..
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			cookies = response.Cookies;

			//

			HttpWebRequest getRequest = (HttpWebRequest)WebRequest.Create(mainUrl + authPart);
			getRequest.CookieContainer = new CookieContainer();
			getRequest.CookieContainer.Add(cookies); //recover cookies First request
			getRequest.Method = WebRequestMethods.Http.Post;
			getRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
			getRequest.AllowWriteStreamBuffering = true;
			getRequest.ProtocolVersion = HttpVersion.Version11;
			getRequest.AllowAutoRedirect = true;
			getRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
			getRequest.Accept = "application/json, text/javascript, */*; q=0.01";
			//getRequest.Connection = "keep-alive";
			getRequest.KeepAlive = true;
			getRequest.Referer = mainUrl + firstpart;

			//Add headers
			getRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
			getRequest.Headers.Add("Accept-Language", "en-US,en;q=0.8,ru;q=0.6");
			getRequest.Headers.Add("x-csrf-token-auth", "a3163733f8652c86d45e11dd3b6f9b320b5e9b8229ef03e949fe3393cbbd595511e3f3a5e900b8e7c1aa93ea04190b3dab90d1a4041d21df3c9ed578552a555d");
			getRequest.Headers.Add("Origin", "https://www.name.com");
			getRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
			//end headers

			byte[] byteArray = Encoding.ASCII.GetBytes(postData);
			getRequest.ContentLength = byteArray.Length;
			Stream newStream = getRequest.GetRequestStream(); //open connection
			newStream.Write(byteArray, 0, byteArray.Length); // Send the data.
			newStream.Close();

			HttpWebResponse getResponse = (HttpWebResponse)getRequest.GetResponse();
			string sourceCode;
			using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
			{
				sourceCode = sr.ReadToEnd();
			}
			Console.WriteLine(sourceCode);
		}
	}
}
