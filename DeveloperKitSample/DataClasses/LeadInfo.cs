using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	/// <summary>
	/// Core Lead/Contact information returned from a LeadCapture call.
	/// </summary>
	[DataContract]
	[Serializable]
	public class LeadInfo
	{
		#region Core Lead/Contact Properties

		[DataMember(Name = "LeadID", EmitDefaultValue = false)]
		public String LeadID { get; set; }

		[DataMember(Name = "CapturedDate", EmitDefaultValue = false)]
		public String CapturedDate { get; set; }

		[DataMember(Name = "CapturedBy", EmitDefaultValue = false)]
		public String CapturedBy { get; set; }

		[DataMember(Name = "RegistrantID", EmitDefaultValue = false)]
		public String RegistrantID { get; set; }

		[DataMember(Name = "FirstName", EmitDefaultValue = false)]
		public String FirstName { get; set; }

		[DataMember(Name = "LastName", EmitDefaultValue = false)]
		public String LastName { get; set; }

		[DataMember(Name = "Title", EmitDefaultValue = false)]
		public String Title { get; set; }

		[DataMember(Name = "Company", EmitDefaultValue = false)]
		public String Company { get; set; }

		[DataMember(Name = "Company2", EmitDefaultValue = false)]
		public String Company2 { get; set; }

		[DataMember(Name = "Address", EmitDefaultValue = false)]
		public String Address { get; set; }

		[DataMember(Name = "Address2", EmitDefaultValue = false)]
		public String Address2 { get; set; }

		[DataMember(Name = "Address3", EmitDefaultValue = false)]
		public String Address3 { get; set; }

		[DataMember(Name = "City", EmitDefaultValue = false)]
		public String City { get; set; }

		[DataMember(Name = "StateCode", EmitDefaultValue = false)]
		public String StateCode { get; set; }

		[DataMember(Name = "ZipCode", EmitDefaultValue = false)]
		public String ZipCode { get; set; }

		[DataMember(Name = "CountryCode", EmitDefaultValue = false)]
		public String CountryCode { get; set; }

		[DataMember(Name = "Email", EmitDefaultValue = false)]
		public String Email { get; set; }

		[DataMember(Name = "Phone", EmitDefaultValue = false)]
		public String Phone { get; set; }

		[DataMember(Name = "PhoneExtension", EmitDefaultValue = false)]
		public String PhoneExtension { get; set; }

		[DataMember(Name = "Fax", EmitDefaultValue = false)]
		public String Fax { get; set; }

		[DataMember(Name = "Notes", EmitDefaultValue = false)]
		public String Notes { get; set; }
		#endregion

	}

}
