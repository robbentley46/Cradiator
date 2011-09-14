using System;
using System.Net;

namespace Cradiator.Services
{
	public class HttpWebClient : IWebClient
	{
		readonly WebClient _webClient;

		public HttpWebClient()
		{
			_webClient = new WebClient();
		}

		public string DownloadString(string url)
		{
		    var uri = new Uri(url);
		    var userInfo = uri.UserInfo.Split(':');
            _webClient.Credentials = new NetworkCredential(userInfo[0],userInfo[1]);
            return _webClient.DownloadString(uri);
		}
	}
}