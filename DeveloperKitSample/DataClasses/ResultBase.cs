using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	/// <summary>
	/// Simple base class for the result of a web service call.
	/// </summary>
	[DataContract]
	[Serializable]
	[KnownType(typeof(APIMessage))]
	public class ResultBase
	{
		#region Messages
		/// <summary>
		/// Messages associated with the web service call.
		/// </summary>
		[DataMember(Name = "Messages", EmitDefaultValue = false)]
		public APIMessage[] Messages { get; set; }
		#endregion

	}
}
