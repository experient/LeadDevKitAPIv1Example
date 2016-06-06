using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Experient.DeveloperKitSample
{
	/// <summary>
	/// Represents a message returned from the web service.
	/// </summary>
	[DataContract]
	[Serializable]
	public class APIMessage
	{
		#region MessageID
		/// <summary>
		/// This number uniquely identifies a message, for purposes of
		/// machine-readability, i.e. the ability to develop programmatic
		/// automated responses based on the message thrown.
		/// </summary>
		[DataMember(Name = "MessageID", EmitDefaultValue = false)]
		public Int64 MessageID { get; set; }
		#endregion

		#region Message
		/// <summary>
		/// "Human-readable" message content.
		/// </summary>
		[DataMember(Name = "Message", EmitDefaultValue = false)]
		public String Message { get; set; }
		#endregion

		#region Severity
		/// <summary>
		/// The severity of the message provides hints about how the message should
		/// be displayed, as well as indication as to whether an operation was aborted.
		/// </summary>
		/// <remarks>
		/// Error and Problem messages typically cause an operation to abort and
		/// rollback. Other message types generally do not interrupt an operation.
		/// <code>
		/// Possible Values:
		/// Info = 10
		/// Question = 20
		/// Warning = 100
		/// Problem = 500
		/// Error = 1000
		/// </code>
		/// </remarks>
		[DataMember(Name = "Severity", EmitDefaultValue = false)]
		public Int32 Severity { get; set; }
		#endregion

	}

}
