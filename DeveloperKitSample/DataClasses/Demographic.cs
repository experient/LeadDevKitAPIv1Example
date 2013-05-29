using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	/// <summary>
	/// Represents a demographic field, containing the name of the field (Key) and
	/// the value of the field (Value).
	/// </summary>
	[DataContract]
	[Serializable]
	public class Demographic
	{
		#region Key
		/// <summary>
		/// Name of the Demographic field.
		/// </summary>
		[DataMember(Name = "Key", EmitDefaultValue = false)]
		public String Key { get; set; } 
		#endregion

		#region Value
		/// <summary>
		/// Value for this Demographic field.
		/// </summary>
		[DataMember(Name = "Value", EmitDefaultValue = false)]
		public String Value { get; set; } 
		#endregion
	}
}
