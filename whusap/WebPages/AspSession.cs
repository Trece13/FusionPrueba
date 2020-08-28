using System;
using System.IO;
using System.Net;
using System.Web;

namespace whusap.WebPages
{
public class AspSession
{
	public static object Get(string name)
	{
		HttpContext context = HttpContext.Current;
		object value = null;
		String[] cookies = context.Request.Cookies.AllKeys;
		for (int i = 0; i < cookies.Length; i++)
		{
			HttpCookie cookie = context.Request.Cookies[cookies[i]];
			if (cookie.Name.StartsWith("ASPSESSION"))
			{
				System.Uri uri = context.Request.Url;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.Scheme + "://" + uri.Host + ":" + uri.Port.ToString() + "/Services/AspSession.asp?mode=get&name=" + name);
				request.Headers.Add("Cookie: " + cookie.Name + "=" + cookie.Value);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream responseStream = response.GetResponseStream();
				System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
				StreamReader readStream = new StreamReader(responseStream, encode);
				value = readStream.ReadToEnd();
				response.Close();
				readStream.Close();
				break;
			}
		}
		return value;
	}

	public static void Set(string name, object value)
	{
		HttpContext context = HttpContext.Current;

		String[] cookies = context.Request.Cookies.AllKeys;

		for (int i = 0; i < cookies.Length; i++)
		{
			HttpCookie cookie = context.Request.Cookies[cookies[i]];

			if (cookie.Name.StartsWith("ASPSESSION"))
			{
				System.Uri uri = context.Request.Url;

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri.Scheme + "://" + uri.Host + ":" + uri.Port.ToString() + "/Services/LegacySession.asp?mode=set&name=" + context.Server.UrlEncode(name) + "&value=" + context.Server.UrlEncode(value.ToString()));
				request.Headers.Add("Cookie: " + cookie.Name + "=" + cookie.Value);
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream responseStream = response.GetResponseStream();
				break;
			}
		}
	}
}

