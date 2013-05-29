using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;

namespace Experient.DeveloperKitSample
{
	public partial class Form1 : Form
	{
		private APICredentials _APICredInfo = null;

		public Form1()
		{
			InitializeComponent();
		}

		#region Form1_Load
		private void Form1_Load(object sender, EventArgs e)
		{
			ResultsClear();
			_APICredInfo = LoadAPICredentials();
			TextBoxAPIKey.Text = _APICredInfo.APIKey;
			TextBoxActCode.Text = _APICredInfo.ActivationCode;
			TextBoxBadgeID.Text = _APICredInfo.BadgeID;
		}
		#endregion

		#region LinkLabelGuide_LinkClicked
		private void LinkLabelGuide_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(ExperientService.UserGuideURL);
		}
		#endregion

		#region LinkLabelBadgeSamples_LinkClicked
		private void LinkLabelBadgeSamples_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(ExperientService.BadgeSampleURL);
		}
		#endregion

		#region ButtonLeadCaptureByQRCode_Click
		private void ButtonLeadCaptureByQRCode_Click(object sender, EventArgs e)
		{
			ResultsClear();
			APICredentials TempActInfo = CreateAPICredentials();
			if ( TempActInfo == null )
				return;

			if ( string.IsNullOrWhiteSpace(TextBoxQRCodeData.Text) )
				DisplayErrorMessage("You must enter the QRCode Data.",
					"For samples from the Demo Show please use the \"Badge QRCode Samples\" link shown.");
			else
			{
				ServiceInvoke(() =>
				{
					ResultsWriteLine("Capturing Lead: {0}", TextBoxQRCodeData.Text);
					ResultsWriteLine();
					ExperientService Service = new ExperientService();
					if ( CheckBoxLeadCaptureByQRCode.Checked )
					{
						string ResponseJSON = Service.GetLeadInfoJSON(TempActInfo, TextBoxQRCodeData.Text);
						if ( ResponseJSON == null )
							ResultsWriteLine("Null response.");
						else
							ResultsWriteLine(ResponseJSON);
					}
					else
					{
						LeadInfoResult Response = Service.GetLeadInfo(TempActInfo, TextBoxQRCodeData.Text);
						DisplayLeadInfoResults(Response);
					}
					// Update our local activation info storage.
					_APICredInfo = SaveAPICredentials(TempActInfo);
				});
			}
		}
		#endregion

		#region ButtonLeadCaptureByKeyName_Click
		private void ButtonLeadCaptureByKeyName_Click(object sender, EventArgs e)
		{
			ResultsClear();
			APICredentials TempActInfo = CreateAPICredentials();
			if ( TempActInfo == null )
				return;

			if ( string.IsNullOrWhiteSpace(TextBoxAttendeeConnectKey.Text) )
				DisplayErrorMessage("You must enter the Attendee's Connect Key.",
					"For samples from the Demo Show please use the \"Badge QRCode Samples\" link shown.");
			else if ( string.IsNullOrWhiteSpace(TextBoxAttendeeLastName.Text) )
				DisplayErrorMessage("You must enter at least one letter of the Attendee's Last Name.",
					"For samples from the Demo Show please use the \"Badge QRCode Samples\" link shown.");
			else
			{
				ServiceInvoke(() =>
				{
					ResultsWriteLine("Capturing Lead: {0} / {1}", TextBoxAttendeeConnectKey.Text, TextBoxAttendeeLastName.Text);
					ResultsWriteLine();
					ExperientService Service = new ExperientService();
					if ( CheckBoxLeadCaptureByKeyName.Checked )
					{
						string ResponseJSON = Service.GetLeadInfoJSON(TempActInfo, TextBoxAttendeeConnectKey.Text, TextBoxAttendeeLastName.Text);
						if ( ResponseJSON == null )
							ResultsWriteLine("Null response.");
						else
							ResultsWriteLine(ResponseJSON);
					}
					else
					{
						LeadInfoResult Response = Service.GetLeadInfo(TempActInfo, TextBoxAttendeeConnectKey.Text, TextBoxAttendeeLastName.Text);
						DisplayLeadInfoResults(Response);
					}

					// Update our local activation info storage.
					_APICredInfo = SaveAPICredentials(TempActInfo);
				});
			}
		}
		#endregion

		#region ButtonDisplayFields_Click
		private void ButtonDisplayFields_Click(object sender, EventArgs e)
		{
			ResultsClear();
			APICredentials TempActInfo = CreateAPICredentials();
			if ( TempActInfo == null )
				return;

			ServiceInvoke(() =>
			{
				ResultsWriteLine("Querying Fields");
				ResultsWriteLine();
				ExperientService Service = new ExperientService();
				if ( CheckBoxShowJsonFields.Checked )
				{
					string ResponseJSON = Service.QueryFieldsJSON(TempActInfo);
					if ( ResponseJSON == null )
						ResultsWriteLine("Null response.");
					else
						ResultsWriteLine(ResponseJSON);
				}
				else
				{
					LeadInfoResult Response = Service.QueryFields(TempActInfo);
					if ( Response == null )
						ResultsWriteLine("Null response.");
					else
					{
						// Display any messages in the response.
						ResultsWrite(Response.Messages);
						if ( !Response.Success )
							ResultsWriteLine("No data to display since the call was not successful");
						else if ( (Response.Demographics == null) || (Response.Demographics.Length == 0) )
							ResultsWriteLine("This show does not include any fields other than the core contact fields in the LeadInfo class.");
						else
						{
							ResultsWriteLine("In addition to the core contact fields in the LeadInfo class, " +
							"this show also contains the following Demographic fields:");
							ResultsWriteLine();
							foreach ( var D in Response.Demographics )
								ResultsWriteLine(D.Key);
						}
					}
				}
				// Update our local activation info storage.
				_APICredInfo = SaveAPICredentials(TempActInfo);
			});
		}
		#endregion

		#region DisplayLeadInfoResults
		/// <summary>
		/// Simple method to display the results of capturing a lead.
		/// </summary>
		/// <param name="result"></param>
		private void DisplayLeadInfoResults(LeadInfoResult result)
		{
			if ( result == null )
				ResultsWriteLine("Null response.");
			else
			{
				// Display any messages in the response.
				ResultsWrite(result.Messages);
				if ( !result.Success )
					ResultsWriteLine("No data to display since the call was not successful");
				else
				{
					TextBoxLeadID.Text = result.LeadInfo.LeadID ?? "";
					TextBoxCapturedBy.Text = result.LeadInfo.CapturedBy ?? "";
					TextBoxCapturedDate.Text = result.LeadInfo.CapturedDate ?? "";
					TextBoxRegID.Text = result.LeadInfo.RegistrantID ?? "";
					TextBoxFirstName.Text = result.LeadInfo.FirstName ?? "";
					TextBoxLastName.Text = result.LeadInfo.LastName ?? "";
					TextBoxTitle.Text = result.LeadInfo.Title ?? "";
					TextBoxCompany.Text = result.LeadInfo.Company ?? "";
					TextBoxCompany2.Text = result.LeadInfo.Company2 ?? "";
					TextBoxAddress.Text = result.LeadInfo.Address ?? "";
					TextBoxAddress2.Text = result.LeadInfo.Address2 ?? "";
					TextBoxAddress3.Text = result.LeadInfo.Address3 ?? "";
					TextBoxCity.Text = result.LeadInfo.City ?? "";
					TextBoxStateCode.Text = result.LeadInfo.StateCode ?? "";
					TextBoxZipCode.Text = result.LeadInfo.ZipCode ?? "";
					TextBoxCountryCode.Text = result.LeadInfo.CountryCode ?? "";
					TextBoxEmail.Text = result.LeadInfo.Email ?? "";
					TextBoxPhone.Text = result.LeadInfo.Phone ?? "";
					TextBoxPhoneExt.Text = result.LeadInfo.PhoneExtension ?? "";
					TextBoxFax.Text = result.LeadInfo.Fax ?? "";
					TextBoxNotes.Text = result.LeadInfo.Notes ?? "";
					// Demographic fields.
					dgvDemographics.DataSource = null;
					if ( result.Demographics != null )
						dgvDemographics.DataSource = result.Demographics.ToArray();
					// Flip over to the tab to display the results.
					TabPageControl1.SelectedTab = TabPageLeadInfoResults;
				}
			}
		}
		#endregion

		#region ServiceInvoke
		/// <summary>
		/// A helper method that wraps an Action in a try...catch block and handles
		/// errors.
		/// </summary>
		/// <param name="a">Action to perform</param>
		private void ServiceInvoke(Action a)
		{
			try
			{
				ResultsClear();
				Cursor = Cursors.WaitCursor;
				a.Invoke();
			}
			catch ( WebException ex )
			{
				HandleWebException(ex);
			}
			catch ( Exception ex )
			{
				DisplayErrorMessage(CombineExceptionMessages(ex).ToArray());
			}
			finally
			{
				Cursor = Cursors.Default;
			}
		}
		#endregion

		#region DisplayErrorMessage
		/// <summary>
		/// A helper method to display a set of message strings as
		/// an error.
		/// </summary>
		/// <param name="msgs">Messages to display./</param>
		private void DisplayErrorMessage(params string[] msgs)
		{
			var Message = string.Join(Environment.NewLine + Environment.NewLine, msgs);
			ResultsWriteLine();
			ResultsWriteLine(Message);
			MessageBox.Show(this, Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		#endregion

		#region CombineExceptionMessages
		/// <summary>
		/// A helper method to recurse inner exceptions and build 
		/// one big list of error messages.
		/// </summary>
		private IEnumerable<string> CombineExceptionMessages(Exception ex)
		{
			List<string> Message = new List<string>();
			Message.Add(ex.Message);
			if ( ex.InnerException != null )
				Message.AddRange(CombineExceptionMessages(ex.InnerException));
			return Message;
		}
		#endregion

		#region HandleWebException
		/// <summary>
		/// A helper method to process a web service exception.
		/// </summary>
		private void HandleWebException(WebException ex)
		{
			List<string> Msg = new List<string>();
			Msg.Add("The web service responded with an error.");
			HttpWebResponse HR = ex.Response as HttpWebResponse;
			if ( HR != null )
			{
				Msg.Add(string.Format("The status code is {0}, the status message is {1}.",
						(int)HR.StatusCode, HR.StatusDescription));

				using ( var SR = new StreamReader(HR.GetResponseStream()) )
				{
					string ErrorMsg = SR.ReadToEnd();
					SR.Close();
					if ( !string.IsNullOrEmpty(ErrorMsg) )
						Msg.Add(Environment.NewLine + "Additional data:" + Environment.NewLine + ErrorMsg);
				}
			}
			DisplayErrorMessage(Msg.ToArray());
		}
		#endregion

		#region Display the Results
		/// <summary>
		/// Helper methods to display information in the Results textbox.
		/// </summary>
		private void ResultsClear()
		{
			TextBoxResults.Clear();
			TextBoxLeadID.Clear();
			TextBoxCapturedBy.Clear();
			TextBoxCapturedDate.Clear();
			TextBoxRegID.Clear();
			TextBoxFirstName.Clear();
			TextBoxLastName.Clear();
			TextBoxTitle.Clear();
			TextBoxCompany.Clear();
			TextBoxCompany2.Clear();
			TextBoxAddress.Clear();
			TextBoxAddress2.Clear();
			TextBoxAddress3.Clear();
			TextBoxCity.Clear();
			TextBoxStateCode.Clear();
			TextBoxZipCode.Clear();
			TextBoxCountryCode.Clear();
			TextBoxEmail.Clear();
			TextBoxPhone.Clear();
			TextBoxPhoneExt.Clear();
			TextBoxFax.Clear();
			TextBoxNotes.Clear();
			dgvDemographics.DataSource = null;
		}
		private void ResultsWrite(string str)
		{
			TextBoxResults.Text += str;
		}
		private void ResultsWrite(string fmt, params object[] args)
		{
			ResultsWrite(string.Format(fmt, args));
		}
		private void ResultsWriteLine(string str)
		{
			ResultsWrite(str);
			ResultsWriteLine();
		}
		private void ResultsWriteLine(string fmt, params object[] args)
		{
			ResultsWrite(fmt, args);
			ResultsWriteLine();
		}
		private void ResultsWriteLine()
		{
			TextBoxResults.Text += Environment.NewLine;
		}
		private void ResultsWrite(IEnumerable<APIMessage> msgs)
		{
			if ( msgs != null )
			{
				foreach ( var M in msgs )
				{
					string SeverityName;
					switch ( M.Severity )
					{
						case 10:
							SeverityName = "Info";
							break;
						case 20:
							SeverityName = "Question";
							break;
						case 100:
							SeverityName = "Warning";
							break;
						case 500:
							SeverityName = "Problem";
							break;
						case 1000:
							SeverityName = "Error";
							break;
						default:
							SeverityName = "Unknown";
							break;
					}
					ResultsWriteLine("Message ID: {0}", M.MessageID);
					ResultsWriteLine("Severity: {0} ({1})", M.Severity, SeverityName);
					ResultsWriteLine("Text: {0}", M.Message);
					ResultsWriteLine();
				}
			}
		}
		#endregion

		#region APICredentials Local Storage
		#region CreateAPICredentials
		private APICredentials CreateAPICredentials()
		{
			APICredentials NewAPICredentials = new APICredentials(TextBoxActCode.Text.Trim(), TextBoxBadgeID.Text.Trim(), TextBoxAPIKey.Text.Trim());
			if ( string.IsNullOrWhiteSpace(NewAPICredentials.ActivationCode) )
				DisplayErrorMessage("Activation Code is a required part of the API Credentials.");
			else if ( string.IsNullOrWhiteSpace(NewAPICredentials.BadgeID) )
				DisplayErrorMessage("Badge ID is a required part of the API Credentials.");
			else if ( string.IsNullOrWhiteSpace(NewAPICredentials.APIKey) )
				DisplayErrorMessage("API Key is a required part of the API Credentials.");
			else
				return NewAPICredentials;
			return null;
		}
		#endregion

		#region APICredentialsStorageFileName Property
		private string APICredentialsStorageFileName
		{
			get { return Path.Combine(Path.GetTempPath(), "DevKitSample_APICredentials_Storage.txt"); }
		}
		#endregion

		#region SaveAPICredentials
		/// <summary>
		/// Saves the current APICredentials into a text file
		/// for future reference.
		/// </summary>
		private APICredentials SaveAPICredentials(APICredentials apiCred)
		{
			if ( apiCred != null )
			{
				var JsonSerializer = new DataContractJsonSerializer(typeof(APICredentials));
				string JSONString;
				using ( var MS = new MemoryStream() )
				{
					JsonSerializer.WriteObject(MS, apiCred);
					JSONString = Encoding.UTF8.GetString(MS.ToArray());
					MS.Close();
				}
				File.WriteAllText(APICredentialsStorageFileName, JSONString, Encoding.UTF8);
			}
			return apiCred;
		}
		#endregion

		#region LoadAPICredentials
		private APICredentials LoadAPICredentials()
		{
			APICredentials TR = null;
			if ( File.Exists(APICredentialsStorageFileName) )
			{
				try
				{
					string Data = File.ReadAllText(APICredentialsStorageFileName, Encoding.UTF8);
					using ( var MS = new MemoryStream(Encoding.UTF8.GetBytes(Data)) )
					{
						var JsonSerializer = new DataContractJsonSerializer(typeof(APICredentials));
						TR = JsonSerializer.ReadObject(MS) as APICredentials;
						MS.Close();
					}
				}
				catch
				{
					// Just delete the current storage file on any error loading it.
					File.Delete(APICredentialsStorageFileName);
					TR = null;
				}
			}
			if ( TR == null )
				TR = new APICredentials("0000000000000000", "0", "");
			return TR;
		}
		#endregion

		#endregion

	}
}
