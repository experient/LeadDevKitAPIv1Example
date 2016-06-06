using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Experient.DeveloperKitSample
{
	public class ExperientService : ExperientServiceBase
	{
		#region GetLeadInfo
		/// <summary>
		/// Get the LeadInfo for single Lead, by supplying the QRCode data that was
		/// read from the barcodeon the Attendee's badge.
		/// </summary>
		public LeadInfoResult GetLeadInfo(APICredentials apiCred, string qrCodeData)
		{
			string Response = GetLeadInfoJSON(apiCred, qrCodeData);
			return DeserializeJSON<LeadInfoResult>(Response);
		}

		public string GetLeadInfoJSON(APICredentials apiCred, string qrCodeData)
		{
			if ( string.IsNullOrWhiteSpace(qrCodeData) )
				throw new ArgumentException("QRCode Data must be supplied.");

			string URL = string.Format("{0}?apikey={1}&actcode={2}&badgeid={3}&barcode={4}",
				LeadInfoURL,
				HttpUtility.UrlEncode(apiCred.APIKey, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.ActivationCode, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.BadgeID, Encoding.UTF8),
				HttpUtility.UrlEncode(qrCodeData, Encoding.UTF8));

			string Response = SubmitGet(new Uri(URL));
			return Response;
		}

		/// <summary>
		/// Get the LeadInfo for single Lead, by supplying the ConnectKey from the badge
		/// (this is typically the Registrant ID) and at least one letter from the Attendee's
		/// Last Name.
		/// </summary>
		public LeadInfoResult GetLeadInfo(APICredentials apiCred, string connectKey, string lastName)
		{
			string Response = GetLeadInfoJSON(apiCred, connectKey, lastName);
			return DeserializeJSON<LeadInfoResult>(Response);
		}

		public string GetLeadInfoJSON(APICredentials apiCred, string connectKey, string lastName)
		{
			if ( string.IsNullOrWhiteSpace(connectKey) )
				throw new ArgumentException("Connect Key must be supplied.");
			if ( string.IsNullOrWhiteSpace(lastName) )
				throw new ArgumentException("Last Name must be supplied.");

			string URL = string.Format("{0}?apikey={1}&actcode={2}&badgeid={3}&connectkey={4}&lastinitial={5}",
				LeadInfoURL,
				HttpUtility.UrlEncode(apiCred.APIKey, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.ActivationCode, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.BadgeID, Encoding.UTF8),
				HttpUtility.UrlEncode(connectKey, Encoding.UTF8),
				HttpUtility.UrlEncode(lastName.Substring(0, 1), Encoding.UTF8));

			string Response = SubmitGet(new Uri(URL));
			return Response;
		}
		#endregion

		#region QueryFields
		/// <summary>
		/// Query the web service for a list of all fields for the show associated
		/// with the API Credentials.
		/// </summary>
		public LeadInfoResult QueryFields(APICredentials apiCred)
		{
			string Response = QueryFieldsJSON(apiCred);
			return DeserializeJSON<LeadInfoResult>(Response);
		}
		#endregion

		#region QueryFieldsJSON
		/// <summary>
		/// Query the web service for a list of all fields for the show associated
		/// with the API Credentials.
		/// </summary>
		public string QueryFieldsJSON(APICredentials apiCred)
		{
			string URL = string.Format("{0}?apikey={1}&actcode={2}&badgeid={3}",
				LeadInfoURL,
				HttpUtility.UrlEncode(apiCred.APIKey, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.ActivationCode, Encoding.UTF8),
				HttpUtility.UrlEncode(apiCred.BadgeID, Encoding.UTF8));

			return SubmitGet(new Uri(URL));
		}
		#endregion

	}
}
