
namespace ExpiredDomainsAutomation
{
	class Program
	{
		private const string MainUrl = @"https://www.name.com"; //expired_domains.php

		static void Main(string[] args)
		{
			new PostAccessor().PerofmLogin(MainUrl, "/expired_domains.php", "/ajax/account/login", "username=bi0b48&password=514341bi0&vip_id=&vip_pin=&vip_flag=off&csrf_token=a3163733f8652c86d45e11dd3b6f9b320b5e9b8229ef03e949fe3393cbbd595511e3f3a5e900b8e7c1aa93ea04190b3dab90d1a4041d21df3c9ed578552a555d");
			//HttpServletReader.Instance.GetResponseStream("514341bi0", "bi0b48", MainUrl, "/ajax/account/login");
			//var apiManger = new ApiManager(MainUrl);
			//apiManger.Login("/ajax/account/login", "bi0b48", "514341bi0");
		}

		
	}
}
