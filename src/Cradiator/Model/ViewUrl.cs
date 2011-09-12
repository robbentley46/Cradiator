using System;
using System.Collections.Generic;
using System.Linq;
using Cradiator.Config;
using Ninject;

namespace Cradiator.Model
{
	public class ViewUrl
	{
		// ReSharper disable UnusedMember.Global
		[Inject]
		public ViewUrl(IConfigSettings settings) : this(settings.URL) {}
		// ReSharper restore UnusedMember.Global

		public ViewUrl(string url)
		{
			Url = url;
		}

	    public ViewUrl(string url, string username, string password) : this (url)
	    {
	        Username = username;
	        Password = password;
	    }

	    public string Url { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

		public IEnumerable<string> UriList
		{
			get
			{
				return from url in Url.Split(' ')
				       let u = new UrlParser(url)
				       where u.IsValid
					   select u.Url;
			}
		}

	    public IEnumerable<string> PasswordList
	    {
	        get { throw new NotImplementedException(); }
	    }

	    public IEnumerable<string> UsernameList
	    {
	        get { throw new NotImplementedException(); }
	    }
	}
}