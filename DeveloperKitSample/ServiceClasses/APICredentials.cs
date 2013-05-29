using System;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	/// <summary>
	/// Simple class to allow us to persist the API Credentials
	/// into a local storage file (a text file).
	/// </summary>
	[DataContract]
	[Serializable]
	public class APICredentials
	{
		public APICredentials()
		{
			ActivationCode = BadgeID = APIKey = string.Empty;
		}
		public APICredentials(string actCode, string badgeID, string apiKey)
		{
			ActivationCode = actCode;
			BadgeID = badgeID;
			APIKey = apiKey;
		}

		[DataMember(Name = "ActivationCode", EmitDefaultValue = true)]
		public String ActivationCode { get; private set; }

		[DataMember(Name = "APIKey", EmitDefaultValue = true)]
		public String APIKey { get; private set; }

		[DataMember(Name = "BadgeID", EmitDefaultValue = true)]
		public String BadgeID { get; private set; }

	}
}
