using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	[DataContract]
	[Serializable]
	[KnownType(typeof(Demographic))]
	[KnownType(typeof(LeadInfo))]
	public class LeadInfoResult : ResultBase
	{
		#region Success
		/// <summary>
		/// Boolean indicator of whether the LeadInfo is good and
		/// can be used, or if there was an error in processing
		/// the request.
		/// </summary>
		[DataMember(Name = "Success", EmitDefaultValue = false)]
		public Boolean Success { get; set; }
		#endregion

		#region LeadInfo
		/// <summary>
		/// Core Lead/Contact Information.
		/// </summary>
		[DataMember(Name = "LeadInfo", EmitDefaultValue = false)]
		public LeadInfo LeadInfo { get; set; }
		#endregion

		#region Demographic Field List
		[DataMember(Name = "Demographics", EmitDefaultValue = false)]
		public Demographic[] Demographics { get; set; }
		#endregion

	}
}
