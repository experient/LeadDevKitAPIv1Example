using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Experient.DeveloperKitSample
{
	public class ExperientServiceBase
	{
		#region URLs
		/// <summary>
		/// Get the URL for the User Guide page.
		/// </summary>
		public static string UserGuideURL { get { return ConfigurationManager.AppSettings["RealTimeDataAPIUserGuideURL"]; } }

		/// <summary>
		/// Get the URL for the Badge QRCode Sample page.
		/// </summary>
		public static string BadgeSampleURL { get { return ConfigurationManager.AppSettings["RealTimeDataAPIBadgeSampleURL"]; } }

		/// <summary>
		/// Get the URL for Capturing Leads.
		/// </summary>
		protected string LeadInfoURL { get { return ConfigurationManager.AppSettings["RealTimeDataAPILeadInfoURL"]; } }
		#endregion

		#region SubmitGet
		protected string SubmitGet(Uri url)
		{
			HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(url);
			using ( HttpWebResponse Response = (HttpWebResponse)Req.GetResponse() )
			{
				if ( Response.StatusCode != HttpStatusCode.OK )
					throw new Exception(String.Format("Server error (HTTP {0}: {1}).", Response.StatusCode, Response.StatusDescription));
				using ( var SR = new StreamReader(Response.GetResponseStream(), Encoding.UTF8) )
					return SR.ReadToEnd();
			}
		}
		#endregion

		#region DeserializeJSON
		protected TResult DeserializeJSON<TResult>(string json) where TResult : ResultBase
		{
			using ( var MS = new MemoryStream(Encoding.UTF8.GetBytes(json)) )
			{
				var JsonSerializer = new DataContractJsonSerializer(typeof(TResult));
				TResult Result = (TResult)JsonSerializer.ReadObject(MS);
				return Result;
			}
		}
		#endregion
	}
}
