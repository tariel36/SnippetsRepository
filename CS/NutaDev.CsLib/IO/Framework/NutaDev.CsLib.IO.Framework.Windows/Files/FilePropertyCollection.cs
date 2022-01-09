// The MIT License (MIT)
// 
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Microsoft.WindowsAPICodePack.Shell;
using NutaDev.CsLib.Maintenance.Exceptions.Abstract;
using NutaDev.CsLib.Maintenance.Exceptions.Delegates;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using System;

namespace NutaDev.CsLib.IO.Framework.Windows.Files
{
    /// <summary>
    /// Provides access to defined properties of file.
    /// </summary>
    public class FilePropertyCollection
        : Core.Interfaces.IDisposable
        , IExceptionHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePropertyCollection"/> class.
        /// </summary>
        /// <param name="filePath">Path to file to load.</param>
        public FilePropertyCollection(string filePath)
        {
            File = ShellFile.FromFilePath(filePath);
        }

        /// <summary>
        /// Gets or sets the exception handler.
        /// </summary>
        public ExceptionHandlerDelegate ExceptionHandler { get; set; }

        /// <summary>
        /// Indicates whether object is disposes or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// File to get values from.
        /// </summary>
        private ShellFile File { get; }

        /// <summary>
        /// Gets specific attribute's value.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="attribute">Attribute to get.</param>
        /// <returns>Value of attribute.</returns>
        public T GetAttributeValue<T>(FileProperties attribute)
        {
            return (T)GetAttributeValue(attribute);
        }

        /// <summary>
        /// Gets specific attribute value.
        /// </summary>
        /// <param name="attribute">Attribute to get.</param>
        /// <returns>Value of attribute.</returns>
        public object GetAttributeValue(FileProperties attribute)
        {
            switch (attribute)
            {
                case FileProperties.AcquisitionID: return File.Properties.System.AcquisitionID.Value;
                case FileProperties.ApplicationName: return File.Properties.System.ApplicationName.Value;
                case FileProperties.Author: return File.Properties.System.Author.Value;
                case FileProperties.Capacity: return File.Properties.System.Capacity.Value;
                case FileProperties.Category: return File.Properties.System.Category.Value;
                case FileProperties.Comment: return File.Properties.System.Comment.Value;
                case FileProperties.Company: return File.Properties.System.Company.Value;
                case FileProperties.ComputerName: return File.Properties.System.ComputerName.Value;
                case FileProperties.ContainedItems: return File.Properties.System.ContainedItems.Value;
                case FileProperties.ContentStatus: return File.Properties.System.ContentStatus.Value;
                case FileProperties.ContentType: return File.Properties.System.ContentType.Value;
                case FileProperties.Copyright: return File.Properties.System.Copyright.Value;
                case FileProperties.DateAccessed: return File.Properties.System.DateAccessed.Value;
                case FileProperties.DateAcquired: return File.Properties.System.DateAcquired.Value;
                case FileProperties.DateArchived: return File.Properties.System.DateArchived.Value;
                case FileProperties.DateCompleted: return File.Properties.System.DateCompleted.Value;
                case FileProperties.DateCreated: return File.Properties.System.DateCreated.Value;
                case FileProperties.DateImported: return File.Properties.System.DateImported.Value;
                case FileProperties.DateModified: return File.Properties.System.DateModified.Value;
                case FileProperties.DescriptionID: return File.Properties.System.DescriptionID.Value;
                case FileProperties.DueDate: return File.Properties.System.DueDate.Value;
                case FileProperties.EndDate: return File.Properties.System.EndDate.Value;
                case FileProperties.FileAllocationSize: return File.Properties.System.FileAllocationSize.Value;
                case FileProperties.FileAttributes: return File.Properties.System.FileAttributes.Value;
                case FileProperties.FileCount: return File.Properties.System.FileCount.Value;
                case FileProperties.FileDescription: return File.Properties.System.FileDescription.Value;
                case FileProperties.FileExtension: return File.Properties.System.FileExtension.Value;
                case FileProperties.FileFRN: return File.Properties.System.FileFRN.Value;
                case FileProperties.FileName: return File.Properties.System.FileName.Value;
                case FileProperties.FileOwner: return File.Properties.System.FileOwner.Value;
                case FileProperties.FileVersion: return File.Properties.System.FileVersion.Value;
                case FileProperties.FindData: return File.Properties.System.FindData.Value;
                case FileProperties.FlagColor: return File.Properties.System.FlagColor.Value;
                case FileProperties.FlagColorText: return File.Properties.System.FlagColorText.Value;
                case FileProperties.FlagStatus: return File.Properties.System.FlagStatus.Value;
                case FileProperties.FlagStatusText: return File.Properties.System.FlagStatusText.Value;
                case FileProperties.FreeSpace: return File.Properties.System.FreeSpace.Value;
                case FileProperties.FullText: return File.Properties.System.FullText.Value;
                case FileProperties.IdentityProperty: return File.Properties.System.IdentityProperty.Value;
                case FileProperties.ImageParsingName: return File.Properties.System.ImageParsingName.Value;
                case FileProperties.Importance: return File.Properties.System.Importance.Value;
                case FileProperties.ImportanceText: return File.Properties.System.ImportanceText.Value;
                case FileProperties.InfoTipText: return File.Properties.System.InfoTipText.Value;
                case FileProperties.InternalName: return File.Properties.System.InternalName.Value;
                case FileProperties.IsAttachment: return File.Properties.System.IsAttachment.Value;
                case FileProperties.IsDefaultNonOwnerSaveLocation: return File.Properties.System.IsDefaultNonOwnerSaveLocation.Value;
                case FileProperties.IsDefaultSaveLocation: return File.Properties.System.IsDefaultSaveLocation.Value;
                case FileProperties.IsDeleted: return File.Properties.System.IsDeleted.Value;
                case FileProperties.IsEncrypted: return File.Properties.System.IsEncrypted.Value;
                case FileProperties.IsFlagged: return File.Properties.System.IsFlagged.Value;
                case FileProperties.IsFlaggedComplete: return File.Properties.System.IsFlaggedComplete.Value;
                case FileProperties.IsIncomplete: return File.Properties.System.IsIncomplete.Value;
                case FileProperties.IsLocationSupported: return File.Properties.System.IsLocationSupported.Value;
                case FileProperties.IsPinnedToNamespaceTree: return File.Properties.System.IsPinnedToNamespaceTree.Value;
                case FileProperties.IsRead: return File.Properties.System.IsRead.Value;
                case FileProperties.IsSearchOnlyItem: return File.Properties.System.IsSearchOnlyItem.Value;
                case FileProperties.IsSendToTarget: return File.Properties.System.IsSendToTarget.Value;
                case FileProperties.IsShared: return File.Properties.System.IsShared.Value;
                case FileProperties.ItemAuthors: return File.Properties.System.ItemAuthors.Value;
                case FileProperties.ItemClassType: return File.Properties.System.ItemClassType.Value;
                case FileProperties.ItemDate: return File.Properties.System.ItemDate.Value;
                case FileProperties.ItemFolderNameDisplay: return File.Properties.System.ItemFolderNameDisplay.Value;
                case FileProperties.ItemFolderPathDisplay: return File.Properties.System.ItemFolderPathDisplay.Value;
                case FileProperties.ItemFolderPathDisplayNarrow: return File.Properties.System.ItemFolderPathDisplayNarrow.Value;
                case FileProperties.ItemName: return File.Properties.System.ItemName.Value;
                case FileProperties.ItemNameDisplay: return File.Properties.System.ItemNameDisplay.Value;
                case FileProperties.ItemNamePrefix: return File.Properties.System.ItemNamePrefix.Value;
                case FileProperties.ItemParticipants: return File.Properties.System.ItemParticipants.Value;
                case FileProperties.ItemPathDisplay: return File.Properties.System.ItemPathDisplay.Value;
                case FileProperties.ItemPathDisplayNarrow: return File.Properties.System.ItemPathDisplayNarrow.Value;
                case FileProperties.ItemType: return File.Properties.System.ItemType.Value;
                case FileProperties.ItemTypeText: return File.Properties.System.ItemTypeText.Value;
                case FileProperties.ItemUrl: return File.Properties.System.ItemUrl.Value;
                case FileProperties.Keywords: return File.Properties.System.Keywords.Value;
                case FileProperties.Kind: return File.Properties.System.Kind.Value;
                case FileProperties.KindText: return File.Properties.System.KindText.Value;
                case FileProperties.Language: return File.Properties.System.Language.Value;
                case FileProperties.MileageInformation: return File.Properties.System.MileageInformation.Value;
                case FileProperties.MIMEType: return File.Properties.System.MIMEType.Value;
                case FileProperties.NamespaceClsid: return File.Properties.System.NamespaceClsid.Value;
                case FileProperties.Null: return File.Properties.System.Null.Value;
                case FileProperties.OfflineAvailability: return File.Properties.System.OfflineAvailability.Value;
                case FileProperties.OfflineStatus: return File.Properties.System.OfflineStatus.Value;
                case FileProperties.OriginalFileName: return File.Properties.System.OriginalFileName.Value;
                case FileProperties.OwnerSid: return File.Properties.System.OwnerSid.Value;
                case FileProperties.ParentalRating: return File.Properties.System.ParentalRating.Value;
                case FileProperties.ParentalRatingReason: return File.Properties.System.ParentalRatingReason.Value;
                case FileProperties.ParentalRatingsOrganization: return File.Properties.System.ParentalRatingsOrganization.Value;
                case FileProperties.ParsingBindContext: return File.Properties.System.ParsingBindContext.Value;
                case FileProperties.ParsingName: return File.Properties.System.ParsingName.Value;
                case FileProperties.ParsingPath: return File.Properties.System.ParsingPath.Value;
                case FileProperties.PerceivedType: return File.Properties.System.PerceivedType.Value;
                case FileProperties.PercentFull: return File.Properties.System.PercentFull.Value;
                case FileProperties.Priority: return File.Properties.System.Priority.Value;
                case FileProperties.PriorityText: return File.Properties.System.PriorityText.Value;
                case FileProperties.Project: return File.Properties.System.Project.Value;
                case FileProperties.ProviderItemID: return File.Properties.System.ProviderItemID.Value;
                case FileProperties.Rating: return File.Properties.System.Rating.Value;
                case FileProperties.RatingText: return File.Properties.System.RatingText.Value;
                case FileProperties.Sensitivity: return File.Properties.System.Sensitivity.Value;
                case FileProperties.SensitivityText: return File.Properties.System.SensitivityText.Value;
                case FileProperties.SFGAOFlags: return File.Properties.System.SFGAOFlags.Value;
                case FileProperties.SharedWith: return File.Properties.System.SharedWith.Value;
                case FileProperties.ShareUserRating: return File.Properties.System.ShareUserRating.Value;
                case FileProperties.SharingStatus: return File.Properties.System.SharingStatus.Value;
                case FileProperties.SimpleRating: return File.Properties.System.SimpleRating.Value;
                case FileProperties.Size: return File.Properties.System.Size.Value;
                case FileProperties.SoftwareUsed: return File.Properties.System.SoftwareUsed.Value;
                case FileProperties.SourceItem: return File.Properties.System.SourceItem.Value;
                case FileProperties.StartDate: return File.Properties.System.StartDate.Value;
                case FileProperties.Status: return File.Properties.System.Status.Value;
                case FileProperties.Subject: return File.Properties.System.Subject.Value;
                case FileProperties.AppUserModel_ExcludeFromShowInNewInstall: return File.Properties.System.AppUserModel.ExcludeFromShowInNewInstall.Value;
                case FileProperties.AppUserModel_ID: return File.Properties.System.AppUserModel.ID.Value;
                case FileProperties.AppUserModel_IsDestinationListSeparator: return File.Properties.System.AppUserModel.IsDestinationListSeparator.Value;
                case FileProperties.AppUserModel_PreventPinning: return File.Properties.System.AppUserModel.PreventPinning.Value;
                case FileProperties.AppUserModel_RelaunchCommand: return File.Properties.System.AppUserModel.RelaunchCommand.Value;
                case FileProperties.AppUserModel_RelaunchDisplayNameResource: return File.Properties.System.AppUserModel.RelaunchDisplayNameResource.Value;
                case FileProperties.AppUserModel_RelaunchIconResource: return File.Properties.System.AppUserModel.RelaunchIconResource.Value;
                case FileProperties.Audio_ChannelCount: return File.Properties.System.Audio.ChannelCount.Value;
                case FileProperties.Audio_Compression: return File.Properties.System.Audio.Compression.Value;
                case FileProperties.Audio_EncodingBitrate: return File.Properties.System.Audio.EncodingBitrate.Value;
                case FileProperties.Audio_Format: return File.Properties.System.Audio.Format.Value;
                case FileProperties.Audio_IsVariableBitrate: return File.Properties.System.Audio.IsVariableBitrate.Value;
                case FileProperties.Audio_PeakValue: return File.Properties.System.Audio.PeakValue.Value;
                case FileProperties.Audio_SampleRate: return File.Properties.System.Audio.SampleRate.Value;
                case FileProperties.Audio_SampleSize: return File.Properties.System.Audio.SampleSize.Value;
                case FileProperties.Audio_StreamName: return File.Properties.System.Audio.StreamName.Value;
                case FileProperties.Audio_StreamNumber: return File.Properties.System.Audio.StreamNumber.Value;
                case FileProperties.Calendar_Duration: return File.Properties.System.Calendar.Duration.Value;
                case FileProperties.Calendar_IsOnline: return File.Properties.System.Calendar.IsOnline.Value;
                case FileProperties.Calendar_IsRecurring: return File.Properties.System.Calendar.IsRecurring.Value;
                case FileProperties.Calendar_Location: return File.Properties.System.Calendar.Location.Value;
                case FileProperties.Calendar_OptionalAttendeeAddresses: return File.Properties.System.Calendar.OptionalAttendeeAddresses.Value;
                case FileProperties.Calendar_OptionalAttendeeNames: return File.Properties.System.Calendar.OptionalAttendeeNames.Value;
                case FileProperties.Calendar_OrganizerAddress: return File.Properties.System.Calendar.OrganizerAddress.Value;
                case FileProperties.Calendar_OrganizerName: return File.Properties.System.Calendar.OrganizerName.Value;
                case FileProperties.Calendar_ReminderTime: return File.Properties.System.Calendar.ReminderTime.Value;
                case FileProperties.Calendar_RequiredAttendeeAddresses: return File.Properties.System.Calendar.RequiredAttendeeAddresses.Value;
                case FileProperties.Calendar_RequiredAttendeeNames: return File.Properties.System.Calendar.RequiredAttendeeNames.Value;
                case FileProperties.Calendar_Resources: return File.Properties.System.Calendar.Resources.Value;
                case FileProperties.Calendar_ResponseStatus: return File.Properties.System.Calendar.ResponseStatus.Value;
                case FileProperties.Calendar_ShowTimeAs: return File.Properties.System.Calendar.ShowTimeAs.Value;
                case FileProperties.Calendar_ShowTimeAsText: return File.Properties.System.Calendar.ShowTimeAsText.Value;
                case FileProperties.Communication_AccountName: return File.Properties.System.Communication.AccountName.Value;
                case FileProperties.Communication_DateItemExpires: return File.Properties.System.Communication.DateItemExpires.Value;
                case FileProperties.Communication_FollowUpIconIndex: return File.Properties.System.Communication.FollowUpIconIndex.Value;
                case FileProperties.Communication_HeaderItem: return File.Properties.System.Communication.HeaderItem.Value;
                case FileProperties.Communication_PolicyTag: return File.Properties.System.Communication.PolicyTag.Value;
                case FileProperties.Communication_SecurityFlags: return File.Properties.System.Communication.SecurityFlags.Value;
                case FileProperties.Communication_Suffix: return File.Properties.System.Communication.Suffix.Value;
                case FileProperties.Communication_TaskStatus: return File.Properties.System.Communication.TaskStatus.Value;
                case FileProperties.Communication_TaskStatusText: return File.Properties.System.Communication.TaskStatusText.Value;
                case FileProperties.Computer_DecoratedFreeSpace: return File.Properties.System.Computer.DecoratedFreeSpace.Value;
                case FileProperties.Contact_Anniversary: return File.Properties.System.Contact.Anniversary.Value;
                case FileProperties.Contact_AssistantName: return File.Properties.System.Contact.AssistantName.Value;
                case FileProperties.Contact_AssistantTelephone: return File.Properties.System.Contact.AssistantTelephone.Value;
                case FileProperties.Contact_Birthday: return File.Properties.System.Contact.Birthday.Value;
                case FileProperties.Contact_BusinessAddress: return File.Properties.System.Contact.BusinessAddress.Value;
                case FileProperties.Contact_BusinessAddressCity: return File.Properties.System.Contact.BusinessAddressCity.Value;
                case FileProperties.Contact_BusinessAddressCountry: return File.Properties.System.Contact.BusinessAddressCountry.Value;
                case FileProperties.Contact_BusinessAddressPostalCode: return File.Properties.System.Contact.BusinessAddressPostalCode.Value;
                case FileProperties.Contact_BusinessAddressPostOfficeBox: return File.Properties.System.Contact.BusinessAddressPostOfficeBox.Value;
                case FileProperties.Contact_BusinessAddressState: return File.Properties.System.Contact.BusinessAddressState.Value;
                case FileProperties.Contact_BusinessAddressStreet: return File.Properties.System.Contact.BusinessAddressStreet.Value;
                case FileProperties.Contact_BusinessFaxNumber: return File.Properties.System.Contact.BusinessFaxNumber.Value;
                case FileProperties.Contact_BusinessHomepage: return File.Properties.System.Contact.BusinessHomepage.Value;
                case FileProperties.Contact_BusinessTelephone: return File.Properties.System.Contact.BusinessTelephone.Value;
                case FileProperties.Contact_CallbackTelephone: return File.Properties.System.Contact.CallbackTelephone.Value;
                case FileProperties.Contact_CarTelephone: return File.Properties.System.Contact.CarTelephone.Value;
                case FileProperties.Contact_Children: return File.Properties.System.Contact.Children.Value;
                case FileProperties.Contact_CompanyMainTelephone: return File.Properties.System.Contact.CompanyMainTelephone.Value;
                case FileProperties.Contact_Department: return File.Properties.System.Contact.Department.Value;
                case FileProperties.Contact_EmailAddress: return File.Properties.System.Contact.EmailAddress.Value;
                case FileProperties.Contact_EmailAddress2: return File.Properties.System.Contact.EmailAddress2.Value;
                case FileProperties.Contact_EmailAddress3: return File.Properties.System.Contact.EmailAddress3.Value;
                case FileProperties.Contact_EmailAddresses: return File.Properties.System.Contact.EmailAddresses.Value;
                case FileProperties.Contact_EmailName: return File.Properties.System.Contact.EmailName.Value;
                case FileProperties.Contact_FileAsName: return File.Properties.System.Contact.FileAsName.Value;
                case FileProperties.Contact_FirstName: return File.Properties.System.Contact.FirstName.Value;
                case FileProperties.Contact_FullName: return File.Properties.System.Contact.FullName.Value;
                case FileProperties.Contact_Gender: return File.Properties.System.Contact.Gender.Value;
                case FileProperties.Contact_GenderValue: return File.Properties.System.Contact.GenderValue.Value;
                case FileProperties.Contact_Hobbies: return File.Properties.System.Contact.Hobbies.Value;
                case FileProperties.Contact_HomeAddress: return File.Properties.System.Contact.HomeAddress.Value;
                case FileProperties.Contact_HomeAddressCity: return File.Properties.System.Contact.HomeAddressCity.Value;
                case FileProperties.Contact_HomeAddressCountry: return File.Properties.System.Contact.HomeAddressCountry.Value;
                case FileProperties.Contact_HomeAddressPostalCode: return File.Properties.System.Contact.HomeAddressPostalCode.Value;
                case FileProperties.Contact_HomeAddressPostOfficeBox: return File.Properties.System.Contact.HomeAddressPostOfficeBox.Value;
                case FileProperties.Contact_HomeAddressState: return File.Properties.System.Contact.HomeAddressState.Value;
                case FileProperties.Contact_HomeAddressStreet: return File.Properties.System.Contact.HomeAddressStreet.Value;
                case FileProperties.Contact_HomeFaxNumber: return File.Properties.System.Contact.HomeFaxNumber.Value;
                case FileProperties.Contact_HomeTelephone: return File.Properties.System.Contact.HomeTelephone.Value;
                case FileProperties.Contact_IMAddress: return File.Properties.System.Contact.IMAddress.Value;
                case FileProperties.Contact_Initials: return File.Properties.System.Contact.Initials.Value;
                case FileProperties.Contact_JobTitle: return File.Properties.System.Contact.JobTitle.Value;
                case FileProperties.Contact_Label: return File.Properties.System.Contact.Label.Value;
                case FileProperties.Contact_LastName: return File.Properties.System.Contact.LastName.Value;
                case FileProperties.Contact_MailingAddress: return File.Properties.System.Contact.MailingAddress.Value;
                case FileProperties.Contact_MiddleName: return File.Properties.System.Contact.MiddleName.Value;
                case FileProperties.Contact_MobileTelephone: return File.Properties.System.Contact.MobileTelephone.Value;
                case FileProperties.Contact_Nickname: return File.Properties.System.Contact.Nickname.Value;
                case FileProperties.Contact_OfficeLocation: return File.Properties.System.Contact.OfficeLocation.Value;
                case FileProperties.Contact_OtherAddress: return File.Properties.System.Contact.OtherAddress.Value;
                case FileProperties.Contact_OtherAddressCity: return File.Properties.System.Contact.OtherAddressCity.Value;
                case FileProperties.Contact_OtherAddressCountry: return File.Properties.System.Contact.OtherAddressCountry.Value;
                case FileProperties.Contact_OtherAddressPostalCode: return File.Properties.System.Contact.OtherAddressPostalCode.Value;
                case FileProperties.Contact_OtherAddressPostOfficeBox: return File.Properties.System.Contact.OtherAddressPostOfficeBox.Value;
                case FileProperties.Contact_OtherAddressState: return File.Properties.System.Contact.OtherAddressState.Value;
                case FileProperties.Contact_OtherAddressStreet: return File.Properties.System.Contact.OtherAddressStreet.Value;
                case FileProperties.Contact_PagerTelephone: return File.Properties.System.Contact.PagerTelephone.Value;
                case FileProperties.Contact_PersonalTitle: return File.Properties.System.Contact.PersonalTitle.Value;
                case FileProperties.Contact_PrimaryAddressCity: return File.Properties.System.Contact.PrimaryAddressCity.Value;
                case FileProperties.Contact_PrimaryAddressCountry: return File.Properties.System.Contact.PrimaryAddressCountry.Value;
                case FileProperties.Contact_PrimaryAddressPostalCode: return File.Properties.System.Contact.PrimaryAddressPostalCode.Value;
                case FileProperties.Contact_PrimaryAddressPostOfficeBox: return File.Properties.System.Contact.PrimaryAddressPostOfficeBox.Value;
                case FileProperties.Contact_PrimaryAddressState: return File.Properties.System.Contact.PrimaryAddressState.Value;
                case FileProperties.Contact_PrimaryAddressStreet: return File.Properties.System.Contact.PrimaryAddressStreet.Value;
                case FileProperties.Contact_PrimaryEmailAddress: return File.Properties.System.Contact.PrimaryEmailAddress.Value;
                case FileProperties.Contact_PrimaryTelephone: return File.Properties.System.Contact.PrimaryTelephone.Value;
                case FileProperties.Contact_Profession: return File.Properties.System.Contact.Profession.Value;
                case FileProperties.Contact_SpouseName: return File.Properties.System.Contact.SpouseName.Value;
                case FileProperties.Contact_Suffix: return File.Properties.System.Contact.Suffix.Value;
                case FileProperties.Contact_TelexNumber: return File.Properties.System.Contact.TelexNumber.Value;
                case FileProperties.Contact_TTYTDDTelephone: return File.Properties.System.Contact.TTYTDDTelephone.Value;
                case FileProperties.Contact_Webpage: return File.Properties.System.Contact.Webpage.Value;
                case FileProperties.Device_PrinterUrl: return File.Properties.System.Device.PrinterUrl.Value;
                case FileProperties.DeviceInterface_PrinterDriverDirectory: return File.Properties.System.DeviceInterface.PrinterDriverDirectory.Value;
                case FileProperties.DeviceInterface_PrinterDriverName: return File.Properties.System.DeviceInterface.PrinterDriverName.Value;
                case FileProperties.DeviceInterface_PrinterName: return File.Properties.System.DeviceInterface.PrinterName.Value;
                case FileProperties.DeviceInterface_PrinterPortName: return File.Properties.System.DeviceInterface.PrinterPortName.Value;
                case FileProperties.Devices_BatteryLife: return File.Properties.System.Devices.BatteryLife.Value;
                case FileProperties.Devices_BatteryPlusCharging: return File.Properties.System.Devices.BatteryPlusCharging.Value;
                case FileProperties.Devices_BatteryPlusChargingText: return File.Properties.System.Devices.BatteryPlusChargingText.Value;
                case FileProperties.Devices_Category: return File.Properties.System.Devices.Category.Value;
                case FileProperties.Devices_CategoryGroup: return File.Properties.System.Devices.CategoryGroup.Value;
                case FileProperties.Devices_CategoryPlural: return File.Properties.System.Devices.CategoryPlural.Value;
                case FileProperties.Devices_ChargingState: return File.Properties.System.Devices.ChargingState.Value;
                case FileProperties.Devices_Connected: return File.Properties.System.Devices.Connected.Value;
                case FileProperties.Devices_ContainerId: return File.Properties.System.Devices.ContainerId.Value;
                case FileProperties.Devices_DefaultTooltip: return File.Properties.System.Devices.DefaultTooltip.Value;
                case FileProperties.Devices_DeviceDescription1: return File.Properties.System.Devices.DeviceDescription1.Value;
                case FileProperties.Devices_DeviceDescription2: return File.Properties.System.Devices.DeviceDescription2.Value;
                case FileProperties.Devices_DiscoveryMethod: return File.Properties.System.Devices.DiscoveryMethod.Value;
                case FileProperties.Devices_FriendlyName: return File.Properties.System.Devices.FriendlyName.Value;
                case FileProperties.Devices_FunctionPaths: return File.Properties.System.Devices.FunctionPaths.Value;
                case FileProperties.Devices_InterfacePaths: return File.Properties.System.Devices.InterfacePaths.Value;
                case FileProperties.Devices_IsDefault: return File.Properties.System.Devices.IsDefault.Value;
                case FileProperties.Devices_IsNetworkConnected: return File.Properties.System.Devices.IsNetworkConnected.Value;
                case FileProperties.Devices_IsShared: return File.Properties.System.Devices.IsShared.Value;
                case FileProperties.Devices_IsSoftwareInstalling: return File.Properties.System.Devices.IsSoftwareInstalling.Value;
                case FileProperties.Devices_LaunchDeviceStageFromExplorer: return File.Properties.System.Devices.LaunchDeviceStageFromExplorer.Value;
                case FileProperties.Devices_LocalMachine: return File.Properties.System.Devices.LocalMachine.Value;
                case FileProperties.Devices_Manufacturer: return File.Properties.System.Devices.Manufacturer.Value;
                case FileProperties.Devices_MissedCalls: return File.Properties.System.Devices.MissedCalls.Value;
                case FileProperties.Devices_ModelName: return File.Properties.System.Devices.ModelName.Value;
                case FileProperties.Devices_ModelNumber: return File.Properties.System.Devices.ModelNumber.Value;
                case FileProperties.Devices_NetworkedTooltip: return File.Properties.System.Devices.NetworkedTooltip.Value;
                case FileProperties.Devices_NetworkName: return File.Properties.System.Devices.NetworkName.Value;
                case FileProperties.Devices_NetworkType: return File.Properties.System.Devices.NetworkType.Value;
                case FileProperties.Devices_NewPictures: return File.Properties.System.Devices.NewPictures.Value;
                case FileProperties.Devices_Notification: return File.Properties.System.Devices.Notification.Value;
                case FileProperties.Devices_NotificationStore: return File.Properties.System.Devices.NotificationStore.Value;
                case FileProperties.Devices_NotWorkingProperly: return File.Properties.System.Devices.NotWorkingProperly.Value;
                case FileProperties.Devices_Paired: return File.Properties.System.Devices.Paired.Value;
                case FileProperties.Devices_PrimaryCategory: return File.Properties.System.Devices.PrimaryCategory.Value;
                case FileProperties.Devices_Roaming: return File.Properties.System.Devices.Roaming.Value;
                case FileProperties.Devices_SafeRemovalRequired: return File.Properties.System.Devices.SafeRemovalRequired.Value;
                case FileProperties.Devices_SharedTooltip: return File.Properties.System.Devices.SharedTooltip.Value;
                case FileProperties.Devices_SignalStrength: return File.Properties.System.Devices.SignalStrength.Value;
                case FileProperties.Devices_Status1: return File.Properties.System.Devices.Status1.Value;
                case FileProperties.Devices_Status2: return File.Properties.System.Devices.Status2.Value;
                case FileProperties.Devices_StorageCapacity: return File.Properties.System.Devices.StorageCapacity.Value;
                case FileProperties.Devices_StorageFreeSpace: return File.Properties.System.Devices.StorageFreeSpace.Value;
                case FileProperties.Devices_StorageFreeSpacePercent: return File.Properties.System.Devices.StorageFreeSpacePercent.Value;
                case FileProperties.Devices_TextMessages: return File.Properties.System.Devices.TextMessages.Value;
                case FileProperties.Devices_Voicemail: return File.Properties.System.Devices.Voicemail.Value;
                case FileProperties.Document_ByteCount: return File.Properties.System.Document.ByteCount.Value;
                case FileProperties.Document_CharacterCount: return File.Properties.System.Document.CharacterCount.Value;
                case FileProperties.Document_ClientID: return File.Properties.System.Document.ClientID.Value;
                case FileProperties.Document_Contributor: return File.Properties.System.Document.Contributor.Value;
                case FileProperties.Document_DateCreated: return File.Properties.System.Document.DateCreated.Value;
                case FileProperties.Document_DatePrinted: return File.Properties.System.Document.DatePrinted.Value;
                case FileProperties.Document_DateSaved: return File.Properties.System.Document.DateSaved.Value;
                case FileProperties.Document_Division: return File.Properties.System.Document.Division.Value;
                case FileProperties.Document_DocumentID: return File.Properties.System.Document.DocumentID.Value;
                case FileProperties.Document_HiddenSlideCount: return File.Properties.System.Document.HiddenSlideCount.Value;
                case FileProperties.Document_LastAuthor: return File.Properties.System.Document.LastAuthor.Value;
                case FileProperties.Document_LineCount: return File.Properties.System.Document.LineCount.Value;
                case FileProperties.Document_Manager: return File.Properties.System.Document.Manager.Value;
                case FileProperties.Document_MultimediaClipCount: return File.Properties.System.Document.MultimediaClipCount.Value;
                case FileProperties.Document_NoteCount: return File.Properties.System.Document.NoteCount.Value;
                case FileProperties.Document_PageCount: return File.Properties.System.Document.PageCount.Value;
                case FileProperties.Document_ParagraphCount: return File.Properties.System.Document.ParagraphCount.Value;
                case FileProperties.Document_PresentationFormat: return File.Properties.System.Document.PresentationFormat.Value;
                case FileProperties.Document_RevisionNumber: return File.Properties.System.Document.RevisionNumber.Value;
                case FileProperties.Document_Security: return File.Properties.System.Document.Security.Value;
                case FileProperties.Document_SlideCount: return File.Properties.System.Document.SlideCount.Value;
                case FileProperties.Document_Template: return File.Properties.System.Document.Template.Value;
                case FileProperties.Document_TotalEditingTime: return File.Properties.System.Document.TotalEditingTime.Value;
                case FileProperties.Document_Version: return File.Properties.System.Document.Version.Value;
                case FileProperties.Document_WordCount: return File.Properties.System.Document.WordCount.Value;
                case FileProperties.DRM_DatePlayExpires: return File.Properties.System.DRM.DatePlayExpires.Value;
                case FileProperties.DRM_DatePlayStarts: return File.Properties.System.DRM.DatePlayStarts.Value;
                case FileProperties.DRM_Description: return File.Properties.System.DRM.Description.Value;
                case FileProperties.DRM_IsProtected: return File.Properties.System.DRM.IsProtected.Value;
                case FileProperties.DRM_PlayCount: return File.Properties.System.DRM.PlayCount.Value;
                case FileProperties.GPS_Altitude: return File.Properties.System.GPS.Altitude.Value;
                case FileProperties.GPS_AltitudeDenominator: return File.Properties.System.GPS.AltitudeDenominator.Value;
                case FileProperties.GPS_AltitudeNumerator: return File.Properties.System.GPS.AltitudeNumerator.Value;
                case FileProperties.GPS_AltitudeRef: return File.Properties.System.GPS.AltitudeRef.Value;
                case FileProperties.GPS_AreaInformation: return File.Properties.System.GPS.AreaInformation.Value;
                case FileProperties.GPS_Date: return File.Properties.System.GPS.Date.Value;
                case FileProperties.GPS_DestinationBearing: return File.Properties.System.GPS.DestinationBearing.Value;
                case FileProperties.GPS_DestinationBearingDenominator: return File.Properties.System.GPS.DestinationBearingDenominator.Value;
                case FileProperties.GPS_DestinationBearingNumerator: return File.Properties.System.GPS.DestinationBearingNumerator.Value;
                case FileProperties.GPS_DestinationBearingRef: return File.Properties.System.GPS.DestinationBearingRef.Value;
                case FileProperties.GPS_DestinationDistance: return File.Properties.System.GPS.DestinationDistance.Value;
                case FileProperties.GPS_DestinationDistanceDenominator: return File.Properties.System.GPS.DestinationDistanceDenominator.Value;
                case FileProperties.GPS_DestinationDistanceNumerator: return File.Properties.System.GPS.DestinationDistanceNumerator.Value;
                case FileProperties.GPS_DestinationDistanceRef: return File.Properties.System.GPS.DestinationDistanceRef.Value;
                case FileProperties.GPS_DestinationLatitude: return File.Properties.System.GPS.DestinationLatitude.Value;
                case FileProperties.GPS_DestinationLatitudeDenominator: return File.Properties.System.GPS.DestinationLatitudeDenominator.Value;
                case FileProperties.GPS_DestinationLatitudeNumerator: return File.Properties.System.GPS.DestinationLatitudeNumerator.Value;
                case FileProperties.GPS_DestinationLatitudeRef: return File.Properties.System.GPS.DestinationLatitudeRef.Value;
                case FileProperties.GPS_DestinationLongitude: return File.Properties.System.GPS.DestinationLongitude.Value;
                case FileProperties.GPS_DestinationLongitudeDenominator: return File.Properties.System.GPS.DestinationLongitudeDenominator.Value;
                case FileProperties.GPS_DestinationLongitudeNumerator: return File.Properties.System.GPS.DestinationLongitudeNumerator.Value;
                case FileProperties.GPS_DestinationLongitudeRef: return File.Properties.System.GPS.DestinationLongitudeRef.Value;
                case FileProperties.GPS_Differential: return File.Properties.System.GPS.Differential.Value;
                case FileProperties.GPS_DOP: return File.Properties.System.GPS.DOP.Value;
                case FileProperties.GPS_DOPDenominator: return File.Properties.System.GPS.DOPDenominator.Value;
                case FileProperties.GPS_DOPNumerator: return File.Properties.System.GPS.DOPNumerator.Value;
                case FileProperties.GPS_ImageDirection: return File.Properties.System.GPS.ImageDirection.Value;
                case FileProperties.GPS_ImageDirectionDenominator: return File.Properties.System.GPS.ImageDirectionDenominator.Value;
                case FileProperties.GPS_ImageDirectionNumerator: return File.Properties.System.GPS.ImageDirectionNumerator.Value;
                case FileProperties.GPS_ImageDirectionRef: return File.Properties.System.GPS.ImageDirectionRef.Value;
                case FileProperties.GPS_Latitude: return File.Properties.System.GPS.Latitude.Value;
                case FileProperties.GPS_LatitudeDenominator: return File.Properties.System.GPS.LatitudeDenominator.Value;
                case FileProperties.GPS_LatitudeNumerator: return File.Properties.System.GPS.LatitudeNumerator.Value;
                case FileProperties.GPS_LatitudeRef: return File.Properties.System.GPS.LatitudeRef.Value;
                case FileProperties.GPS_Longitude: return File.Properties.System.GPS.Longitude.Value;
                case FileProperties.GPS_LongitudeDenominator: return File.Properties.System.GPS.LongitudeDenominator.Value;
                case FileProperties.GPS_LongitudeNumerator: return File.Properties.System.GPS.LongitudeNumerator.Value;
                case FileProperties.GPS_LongitudeRef: return File.Properties.System.GPS.LongitudeRef.Value;
                case FileProperties.GPS_MapDatum: return File.Properties.System.GPS.MapDatum.Value;
                case FileProperties.GPS_MeasureMode: return File.Properties.System.GPS.MeasureMode.Value;
                case FileProperties.GPS_ProcessingMethod: return File.Properties.System.GPS.ProcessingMethod.Value;
                case FileProperties.GPS_Satellites: return File.Properties.System.GPS.Satellites.Value;
                case FileProperties.GPS_Speed: return File.Properties.System.GPS.Speed.Value;
                case FileProperties.GPS_SpeedDenominator: return File.Properties.System.GPS.SpeedDenominator.Value;
                case FileProperties.GPS_SpeedNumerator: return File.Properties.System.GPS.SpeedNumerator.Value;
                case FileProperties.GPS_SpeedRef: return File.Properties.System.GPS.SpeedRef.Value;
                case FileProperties.GPS_Status: return File.Properties.System.GPS.Status.Value;
                case FileProperties.GPS_Track: return File.Properties.System.GPS.Track.Value;
                case FileProperties.GPS_TrackDenominator: return File.Properties.System.GPS.TrackDenominator.Value;
                case FileProperties.GPS_TrackNumerator: return File.Properties.System.GPS.TrackNumerator.Value;
                case FileProperties.GPS_TrackRef: return File.Properties.System.GPS.TrackRef.Value;
                case FileProperties.GPS_VersionID: return File.Properties.System.GPS.VersionID.Value;
                case FileProperties.Identity_Blob: return File.Properties.System.Identity.Blob.Value;
                case FileProperties.Identity_DisplayName: return File.Properties.System.Identity.DisplayName.Value;
                case FileProperties.Identity_IsMeIdentity: return File.Properties.System.Identity.IsMeIdentity.Value;
                case FileProperties.Identity_PrimaryEmailAddress: return File.Properties.System.Identity.PrimaryEmailAddress.Value;
                case FileProperties.Identity_ProviderID: return File.Properties.System.Identity.ProviderID.Value;
                case FileProperties.Identity_UniqueID: return File.Properties.System.Identity.UniqueID.Value;
                case FileProperties.Identity_UserName: return File.Properties.System.Identity.UserName.Value;
                case FileProperties.IdentityProvider_Name: return File.Properties.System.IdentityProvider.Name.Value;
                case FileProperties.IdentityProvider_Picture: return File.Properties.System.IdentityProvider.Picture.Value;
                case FileProperties.Image_BitDepth: return File.Properties.System.Image.BitDepth.Value;
                case FileProperties.Image_ColorSpace: return File.Properties.System.Image.ColorSpace.Value;
                case FileProperties.Image_CompressedBitsPerPixel: return File.Properties.System.Image.CompressedBitsPerPixel.Value;
                case FileProperties.Image_CompressedBitsPerPixelDenominator: return File.Properties.System.Image.CompressedBitsPerPixelDenominator.Value;
                case FileProperties.Image_CompressedBitsPerPixelNumerator: return File.Properties.System.Image.CompressedBitsPerPixelNumerator.Value;
                case FileProperties.Image_Compression: return File.Properties.System.Image.Compression.Value;
                case FileProperties.Image_CompressionText: return File.Properties.System.Image.CompressionText.Value;
                case FileProperties.Image_Dimensions: return File.Properties.System.Image.Dimensions.Value;
                case FileProperties.Image_HorizontalResolution: return File.Properties.System.Image.HorizontalResolution.Value;
                case FileProperties.Image_HorizontalSize: return File.Properties.System.Image.HorizontalSize.Value;
                case FileProperties.Image_ImageID: return File.Properties.System.Image.ImageID.Value;
                case FileProperties.Image_ResolutionUnit: return File.Properties.System.Image.ResolutionUnit.Value;
                case FileProperties.Image_VerticalResolution: return File.Properties.System.Image.VerticalResolution.Value;
                case FileProperties.Image_VerticalSize: return File.Properties.System.Image.VerticalSize.Value;
                case FileProperties.Journal_Contacts: return File.Properties.System.Journal.Contacts.Value;
                case FileProperties.Journal_EntryType: return File.Properties.System.Journal.EntryType.Value;
                case FileProperties.LayoutPattern_ContentViewModeForBrowse: return File.Properties.System.LayoutPattern.ContentViewModeForBrowse.Value;
                case FileProperties.LayoutPattern_ContentViewModeForSearch: return File.Properties.System.LayoutPattern.ContentViewModeForSearch.Value;
                case FileProperties.Link_Arguments: return File.Properties.System.Link.Arguments.Value;
                case FileProperties.Link_Comment: return File.Properties.System.Link.Comment.Value;
                case FileProperties.Link_DateVisited: return File.Properties.System.Link.DateVisited.Value;
                case FileProperties.Link_Description: return File.Properties.System.Link.Description.Value;
                case FileProperties.Link_Status: return File.Properties.System.Link.Status.Value;
                case FileProperties.Link_TargetExtension: return File.Properties.System.Link.TargetExtension.Value;
                case FileProperties.Link_TargetParsingPath: return File.Properties.System.Link.TargetParsingPath.Value;
                case FileProperties.Link_TargetSFGAOFlags: return File.Properties.System.Link.TargetSFGAOFlags.Value;
                case FileProperties.Link_TargetSFGAOFlagsStrings: return File.Properties.System.Link.TargetSFGAOFlagsStrings.Value;
                case FileProperties.Link_TargetUrl: return File.Properties.System.Link.TargetUrl.Value;
                case FileProperties.Media_AuthorUrl: return File.Properties.System.Media.AuthorUrl.Value;
                case FileProperties.Media_AverageLevel: return File.Properties.System.Media.AverageLevel.Value;
                case FileProperties.Media_ClassPrimaryID: return File.Properties.System.Media.ClassPrimaryID.Value;
                case FileProperties.Media_ClassSecondaryID: return File.Properties.System.Media.ClassSecondaryID.Value;
                case FileProperties.Media_CollectionGroupID: return File.Properties.System.Media.CollectionGroupID.Value;
                case FileProperties.Media_CollectionID: return File.Properties.System.Media.CollectionID.Value;
                case FileProperties.Media_ContentDistributor: return File.Properties.System.Media.ContentDistributor.Value;
                case FileProperties.Media_ContentID: return File.Properties.System.Media.ContentID.Value;
                case FileProperties.Media_CreatorApplication: return File.Properties.System.Media.CreatorApplication.Value;
                case FileProperties.Media_CreatorApplicationVersion: return File.Properties.System.Media.CreatorApplicationVersion.Value;
                case FileProperties.Media_DateEncoded: return File.Properties.System.Media.DateEncoded.Value;
                case FileProperties.Media_DateReleased: return File.Properties.System.Media.DateReleased.Value;
                case FileProperties.Media_Duration: return File.Properties.System.Media.Duration.Value;
                case FileProperties.Media_DVDID: return File.Properties.System.Media.DVDID.Value;
                case FileProperties.Media_EncodedBy: return File.Properties.System.Media.EncodedBy.Value;
                case FileProperties.Media_EncodingSettings: return File.Properties.System.Media.EncodingSettings.Value;
                case FileProperties.Media_FrameCount: return File.Properties.System.Media.FrameCount.Value;
                case FileProperties.Media_MCDI: return File.Properties.System.Media.MCDI.Value;
                case FileProperties.Media_MetadataContentProvider: return File.Properties.System.Media.MetadataContentProvider.Value;
                case FileProperties.Media_Producer: return File.Properties.System.Media.Producer.Value;
                case FileProperties.Media_PromotionUrl: return File.Properties.System.Media.PromotionUrl.Value;
                case FileProperties.Media_ProtectionType: return File.Properties.System.Media.ProtectionType.Value;
                case FileProperties.Media_ProviderRating: return File.Properties.System.Media.ProviderRating.Value;
                case FileProperties.Media_ProviderStyle: return File.Properties.System.Media.ProviderStyle.Value;
                case FileProperties.Media_Publisher: return File.Properties.System.Media.Publisher.Value;
                case FileProperties.Media_SubscriptionContentId: return File.Properties.System.Media.SubscriptionContentId.Value;
                case FileProperties.Media_Subtitle: return File.Properties.System.Media.Subtitle.Value;
                case FileProperties.Media_UniqueFileIdentifier: return File.Properties.System.Media.UniqueFileIdentifier.Value;
                case FileProperties.Media_UserNoAutoInfo: return File.Properties.System.Media.UserNoAutoInfo.Value;
                case FileProperties.Media_UserWebUrl: return File.Properties.System.Media.UserWebUrl.Value;
                case FileProperties.Media_Writer: return File.Properties.System.Media.Writer.Value;
                case FileProperties.Media_Year: return File.Properties.System.Media.Year.Value;
                case FileProperties.Message_AttachmentContents: return File.Properties.System.Message.AttachmentContents.Value;
                case FileProperties.Message_AttachmentNames: return File.Properties.System.Message.AttachmentNames.Value;
                case FileProperties.Message_BccAddress: return File.Properties.System.Message.BccAddress.Value;
                case FileProperties.Message_BccName: return File.Properties.System.Message.BccName.Value;
                case FileProperties.Message_CcAddress: return File.Properties.System.Message.CcAddress.Value;
                case FileProperties.Message_CcName: return File.Properties.System.Message.CcName.Value;
                case FileProperties.Message_ConversationID: return File.Properties.System.Message.ConversationID.Value;
                case FileProperties.Message_ConversationIndex: return File.Properties.System.Message.ConversationIndex.Value;
                case FileProperties.Message_DateReceived: return File.Properties.System.Message.DateReceived.Value;
                case FileProperties.Message_DateSent: return File.Properties.System.Message.DateSent.Value;
                case FileProperties.Message_Flags: return File.Properties.System.Message.Flags.Value;
                case FileProperties.Message_FromAddress: return File.Properties.System.Message.FromAddress.Value;
                case FileProperties.Message_FromName: return File.Properties.System.Message.FromName.Value;
                case FileProperties.Message_HasAttachments: return File.Properties.System.Message.HasAttachments.Value;
                case FileProperties.Message_IsFwdOrReply: return File.Properties.System.Message.IsFwdOrReply.Value;
                case FileProperties.Message_MessageClass: return File.Properties.System.Message.MessageClass.Value;
                case FileProperties.Message_ProofInProgress: return File.Properties.System.Message.ProofInProgress.Value;
                case FileProperties.Message_SenderAddress: return File.Properties.System.Message.SenderAddress.Value;
                case FileProperties.Message_SenderName: return File.Properties.System.Message.SenderName.Value;
                case FileProperties.Message_Store: return File.Properties.System.Message.Store.Value;
                case FileProperties.Message_ToAddress: return File.Properties.System.Message.ToAddress.Value;
                case FileProperties.Message_ToDoFlags: return File.Properties.System.Message.ToDoFlags.Value;
                case FileProperties.Message_ToDoTitle: return File.Properties.System.Message.ToDoTitle.Value;
                case FileProperties.Message_ToName: return File.Properties.System.Message.ToName.Value;
                case FileProperties.Music_AlbumArtist: return File.Properties.System.Music.AlbumArtist.Value;
                case FileProperties.Music_AlbumID: return File.Properties.System.Music.AlbumID.Value;
                case FileProperties.Music_AlbumTitle: return File.Properties.System.Music.AlbumTitle.Value;
                case FileProperties.Music_Artist: return File.Properties.System.Music.Artist.Value;
                case FileProperties.Music_BeatsPerMinute: return File.Properties.System.Music.BeatsPerMinute.Value;
                case FileProperties.Music_Composer: return File.Properties.System.Music.Composer.Value;
                case FileProperties.Music_Conductor: return File.Properties.System.Music.Conductor.Value;
                case FileProperties.Music_ContentGroupDescription: return File.Properties.System.Music.ContentGroupDescription.Value;
                case FileProperties.Music_DisplayArtist: return File.Properties.System.Music.DisplayArtist.Value;
                case FileProperties.Music_Genre: return File.Properties.System.Music.Genre.Value;
                case FileProperties.Music_InitialKey: return File.Properties.System.Music.InitialKey.Value;
                case FileProperties.Music_IsCompilation: return File.Properties.System.Music.IsCompilation.Value;
                case FileProperties.Music_Lyrics: return File.Properties.System.Music.Lyrics.Value;
                case FileProperties.Music_Mood: return File.Properties.System.Music.Mood.Value;
                case FileProperties.Music_PartOfSet: return File.Properties.System.Music.PartOfSet.Value;
                case FileProperties.Music_Period: return File.Properties.System.Music.Period.Value;
                case FileProperties.Music_SynchronizedLyrics: return File.Properties.System.Music.SynchronizedLyrics.Value;
                case FileProperties.Music_TrackNumber: return File.Properties.System.Music.TrackNumber.Value;
                case FileProperties.Note_Color: return File.Properties.System.Note.Color.Value;
                case FileProperties.Note_ColorText: return File.Properties.System.Note.ColorText.Value;
                case FileProperties.Photo_Aperture: return File.Properties.System.Photo.Aperture.Value;
                case FileProperties.Photo_ApertureDenominator: return File.Properties.System.Photo.ApertureDenominator.Value;
                case FileProperties.Photo_ApertureNumerator: return File.Properties.System.Photo.ApertureNumerator.Value;
                case FileProperties.Photo_Brightness: return File.Properties.System.Photo.Brightness.Value;
                case FileProperties.Photo_BrightnessDenominator: return File.Properties.System.Photo.BrightnessDenominator.Value;
                case FileProperties.Photo_BrightnessNumerator: return File.Properties.System.Photo.BrightnessNumerator.Value;
                case FileProperties.Photo_CameraManufacturer: return File.Properties.System.Photo.CameraManufacturer.Value;
                case FileProperties.Photo_CameraModel: return File.Properties.System.Photo.CameraModel.Value;
                case FileProperties.Photo_CameraSerialNumber: return File.Properties.System.Photo.CameraSerialNumber.Value;
                case FileProperties.Photo_Contrast: return File.Properties.System.Photo.Contrast.Value;
                case FileProperties.Photo_ContrastText: return File.Properties.System.Photo.ContrastText.Value;
                case FileProperties.Photo_DateTaken: return File.Properties.System.Photo.DateTaken.Value;
                case FileProperties.Photo_DigitalZoom: return File.Properties.System.Photo.DigitalZoom.Value;
                case FileProperties.Photo_DigitalZoomDenominator: return File.Properties.System.Photo.DigitalZoomDenominator.Value;
                case FileProperties.Photo_DigitalZoomNumerator: return File.Properties.System.Photo.DigitalZoomNumerator.Value;
                case FileProperties.Photo_Event: return File.Properties.System.Photo.Event.Value;
                case FileProperties.Photo_EXIFVersion: return File.Properties.System.Photo.EXIFVersion.Value;
                case FileProperties.Photo_ExposureBias: return File.Properties.System.Photo.ExposureBias.Value;
                case FileProperties.Photo_ExposureBiasDenominator: return File.Properties.System.Photo.ExposureBiasDenominator.Value;
                case FileProperties.Photo_ExposureBiasNumerator: return File.Properties.System.Photo.ExposureBiasNumerator.Value;
                case FileProperties.Photo_ExposureIndex: return File.Properties.System.Photo.ExposureIndex.Value;
                case FileProperties.Photo_ExposureIndexDenominator: return File.Properties.System.Photo.ExposureIndexDenominator.Value;
                case FileProperties.Photo_ExposureIndexNumerator: return File.Properties.System.Photo.ExposureIndexNumerator.Value;
                case FileProperties.Photo_ExposureProgram: return File.Properties.System.Photo.ExposureProgram.Value;
                case FileProperties.Photo_ExposureProgramText: return File.Properties.System.Photo.ExposureProgramText.Value;
                case FileProperties.Photo_ExposureTime: return File.Properties.System.Photo.ExposureTime.Value;
                case FileProperties.Photo_ExposureTimeDenominator: return File.Properties.System.Photo.ExposureTimeDenominator.Value;
                case FileProperties.Photo_ExposureTimeNumerator: return File.Properties.System.Photo.ExposureTimeNumerator.Value;
                case FileProperties.Photo_Flash: return File.Properties.System.Photo.Flash.Value;
                case FileProperties.Photo_FlashEnergy: return File.Properties.System.Photo.FlashEnergy.Value;
                case FileProperties.Photo_FlashEnergyDenominator: return File.Properties.System.Photo.FlashEnergyDenominator.Value;
                case FileProperties.Photo_FlashEnergyNumerator: return File.Properties.System.Photo.FlashEnergyNumerator.Value;
                case FileProperties.Photo_FlashManufacturer: return File.Properties.System.Photo.FlashManufacturer.Value;
                case FileProperties.Photo_FlashModel: return File.Properties.System.Photo.FlashModel.Value;
                case FileProperties.Photo_FlashText: return File.Properties.System.Photo.FlashText.Value;
                case FileProperties.Photo_FNumber: return File.Properties.System.Photo.FNumber.Value;
                case FileProperties.Photo_FNumberDenominator: return File.Properties.System.Photo.FNumberDenominator.Value;
                case FileProperties.Photo_FNumberNumerator: return File.Properties.System.Photo.FNumberNumerator.Value;
                case FileProperties.Photo_FocalLength: return File.Properties.System.Photo.FocalLength.Value;
                case FileProperties.Photo_FocalLengthDenominator: return File.Properties.System.Photo.FocalLengthDenominator.Value;
                case FileProperties.Photo_FocalLengthInFilm: return File.Properties.System.Photo.FocalLengthInFilm.Value;
                case FileProperties.Photo_FocalLengthNumerator: return File.Properties.System.Photo.FocalLengthNumerator.Value;
                case FileProperties.Photo_FocalPlaneXResolution: return File.Properties.System.Photo.FocalPlaneXResolution.Value;
                case FileProperties.Photo_FocalPlaneXResolutionDenominator: return File.Properties.System.Photo.FocalPlaneXResolutionDenominator.Value;
                case FileProperties.Photo_FocalPlaneXResolutionNumerator: return File.Properties.System.Photo.FocalPlaneXResolutionNumerator.Value;
                case FileProperties.Photo_FocalPlaneYResolution: return File.Properties.System.Photo.FocalPlaneYResolution.Value;
                case FileProperties.Photo_FocalPlaneYResolutionDenominator: return File.Properties.System.Photo.FocalPlaneYResolutionDenominator.Value;
                case FileProperties.Photo_FocalPlaneYResolutionNumerator: return File.Properties.System.Photo.FocalPlaneYResolutionNumerator.Value;
                case FileProperties.Photo_GainControl: return File.Properties.System.Photo.GainControl.Value;
                case FileProperties.Photo_GainControlDenominator: return File.Properties.System.Photo.GainControlDenominator.Value;
                case FileProperties.Photo_GainControlNumerator: return File.Properties.System.Photo.GainControlNumerator.Value;
                case FileProperties.Photo_GainControlText: return File.Properties.System.Photo.GainControlText.Value;
                case FileProperties.Photo_ISOSpeed: return File.Properties.System.Photo.ISOSpeed.Value;
                case FileProperties.Photo_LensManufacturer: return File.Properties.System.Photo.LensManufacturer.Value;
                case FileProperties.Photo_LensModel: return File.Properties.System.Photo.LensModel.Value;
                case FileProperties.Photo_LightSource: return File.Properties.System.Photo.LightSource.Value;
                case FileProperties.Photo_MakerNote: return File.Properties.System.Photo.MakerNote.Value;
                case FileProperties.Photo_MakerNoteOffset: return File.Properties.System.Photo.MakerNoteOffset.Value;
                case FileProperties.Photo_MaxAperture: return File.Properties.System.Photo.MaxAperture.Value;
                case FileProperties.Photo_MaxApertureDenominator: return File.Properties.System.Photo.MaxApertureDenominator.Value;
                case FileProperties.Photo_MaxApertureNumerator: return File.Properties.System.Photo.MaxApertureNumerator.Value;
                case FileProperties.Photo_MeteringMode: return File.Properties.System.Photo.MeteringMode.Value;
                case FileProperties.Photo_MeteringModeText: return File.Properties.System.Photo.MeteringModeText.Value;
                case FileProperties.Photo_Orientation: return File.Properties.System.Photo.Orientation.Value;
                case FileProperties.Photo_OrientationText: return File.Properties.System.Photo.OrientationText.Value;
                case FileProperties.Photo_PeopleNames: return File.Properties.System.Photo.PeopleNames.Value;
                case FileProperties.Photo_PhotometricInterpretation: return File.Properties.System.Photo.PhotometricInterpretation.Value;
                case FileProperties.Photo_PhotometricInterpretationText: return File.Properties.System.Photo.PhotometricInterpretationText.Value;
                case FileProperties.Photo_ProgramMode: return File.Properties.System.Photo.ProgramMode.Value;
                case FileProperties.Photo_ProgramModeText: return File.Properties.System.Photo.ProgramModeText.Value;
                case FileProperties.Photo_RelatedSoundFile: return File.Properties.System.Photo.RelatedSoundFile.Value;
                case FileProperties.Photo_Saturation: return File.Properties.System.Photo.Saturation.Value;
                case FileProperties.Photo_SaturationText: return File.Properties.System.Photo.SaturationText.Value;
                case FileProperties.Photo_Sharpness: return File.Properties.System.Photo.Sharpness.Value;
                case FileProperties.Photo_SharpnessText: return File.Properties.System.Photo.SharpnessText.Value;
                case FileProperties.Photo_ShutterSpeed: return File.Properties.System.Photo.ShutterSpeed.Value;
                case FileProperties.Photo_ShutterSpeedDenominator: return File.Properties.System.Photo.ShutterSpeedDenominator.Value;
                case FileProperties.Photo_ShutterSpeedNumerator: return File.Properties.System.Photo.ShutterSpeedNumerator.Value;
                case FileProperties.Photo_SubjectDistance: return File.Properties.System.Photo.SubjectDistance.Value;
                case FileProperties.Photo_SubjectDistanceDenominator: return File.Properties.System.Photo.SubjectDistanceDenominator.Value;
                case FileProperties.Photo_SubjectDistanceNumerator: return File.Properties.System.Photo.SubjectDistanceNumerator.Value;
                case FileProperties.Photo_TagViewAggregate: return File.Properties.System.Photo.TagViewAggregate.Value;
                case FileProperties.Photo_TranscodedForSync: return File.Properties.System.Photo.TranscodedForSync.Value;
                case FileProperties.Photo_WhiteBalance: return File.Properties.System.Photo.WhiteBalance.Value;
                case FileProperties.Photo_WhiteBalanceText: return File.Properties.System.Photo.WhiteBalanceText.Value;
                case FileProperties.PropGroup_Advanced: return File.Properties.System.PropGroup.Advanced.Value;
                case FileProperties.PropGroup_Audio: return File.Properties.System.PropGroup.Audio.Value;
                case FileProperties.PropGroup_Calendar: return File.Properties.System.PropGroup.Calendar.Value;
                case FileProperties.PropGroup_Camera: return File.Properties.System.PropGroup.Camera.Value;
                case FileProperties.PropGroup_Contact: return File.Properties.System.PropGroup.Contact.Value;
                case FileProperties.PropGroup_Content: return File.Properties.System.PropGroup.Content.Value;
                case FileProperties.PropGroup_Description: return File.Properties.System.PropGroup.Description.Value;
                case FileProperties.PropGroup_FileSystem: return File.Properties.System.PropGroup.FileSystem.Value;
                case FileProperties.PropGroup_General: return File.Properties.System.PropGroup.General.Value;
                case FileProperties.PropGroup_GPS: return File.Properties.System.PropGroup.GPS.Value;
                case FileProperties.PropGroup_Image: return File.Properties.System.PropGroup.Image.Value;
                case FileProperties.PropGroup_Media: return File.Properties.System.PropGroup.Media.Value;
                case FileProperties.PropGroup_MediaAdvanced: return File.Properties.System.PropGroup.MediaAdvanced.Value;
                case FileProperties.PropGroup_Message: return File.Properties.System.PropGroup.Message.Value;
                case FileProperties.PropGroup_Music: return File.Properties.System.PropGroup.Music.Value;
                case FileProperties.PropGroup_Origin: return File.Properties.System.PropGroup.Origin.Value;
                case FileProperties.PropGroup_PhotoAdvanced: return File.Properties.System.PropGroup.PhotoAdvanced.Value;
                case FileProperties.PropGroup_RecordedTV: return File.Properties.System.PropGroup.RecordedTV.Value;
                case FileProperties.PropGroup_Video: return File.Properties.System.PropGroup.Video.Value;
                case FileProperties.PropList_ConflictPrompt: return File.Properties.System.PropList.ConflictPrompt.Value;
                case FileProperties.PropList_ContentViewModeForBrowse: return File.Properties.System.PropList.ContentViewModeForBrowse.Value;
                case FileProperties.PropList_ContentViewModeForSearch: return File.Properties.System.PropList.ContentViewModeForSearch.Value;
                case FileProperties.PropList_ExtendedTileInfo: return File.Properties.System.PropList.ExtendedTileInfo.Value;
                case FileProperties.PropList_FileOperationPrompt: return File.Properties.System.PropList.FileOperationPrompt.Value;
                case FileProperties.PropList_FullDetails: return File.Properties.System.PropList.FullDetails.Value;
                case FileProperties.PropList_InfoTip: return File.Properties.System.PropList.InfoTip.Value;
                case FileProperties.PropList_NonPersonal: return File.Properties.System.PropList.NonPersonal.Value;
                case FileProperties.PropList_PreviewDetails: return File.Properties.System.PropList.PreviewDetails.Value;
                case FileProperties.PropList_PreviewTitle: return File.Properties.System.PropList.PreviewTitle.Value;
                case FileProperties.PropList_QuickTip: return File.Properties.System.PropList.QuickTip.Value;
                case FileProperties.PropList_TileInfo: return File.Properties.System.PropList.TileInfo.Value;
                case FileProperties.PropList_XPDetailsPanel: return File.Properties.System.PropList.XPDetailsPanel.Value;
                case FileProperties.RecordedTV_ChannelNumber: return File.Properties.System.RecordedTV.ChannelNumber.Value;
                case FileProperties.RecordedTV_Credits: return File.Properties.System.RecordedTV.Credits.Value;
                case FileProperties.RecordedTV_DateContentExpires: return File.Properties.System.RecordedTV.DateContentExpires.Value;
                case FileProperties.RecordedTV_EpisodeName: return File.Properties.System.RecordedTV.EpisodeName.Value;
                case FileProperties.RecordedTV_IsATSCContent: return File.Properties.System.RecordedTV.IsATSCContent.Value;
                case FileProperties.RecordedTV_IsClosedCaptioningAvailable: return File.Properties.System.RecordedTV.IsClosedCaptioningAvailable.Value;
                case FileProperties.RecordedTV_IsDTVContent: return File.Properties.System.RecordedTV.IsDTVContent.Value;
                case FileProperties.RecordedTV_IsHDContent: return File.Properties.System.RecordedTV.IsHDContent.Value;
                case FileProperties.RecordedTV_IsRepeatBroadcast: return File.Properties.System.RecordedTV.IsRepeatBroadcast.Value;
                case FileProperties.RecordedTV_IsSAP: return File.Properties.System.RecordedTV.IsSAP.Value;
                case FileProperties.RecordedTV_NetworkAffiliation: return File.Properties.System.RecordedTV.NetworkAffiliation.Value;
                case FileProperties.RecordedTV_OriginalBroadcastDate: return File.Properties.System.RecordedTV.OriginalBroadcastDate.Value;
                case FileProperties.RecordedTV_ProgramDescription: return File.Properties.System.RecordedTV.ProgramDescription.Value;
                case FileProperties.RecordedTV_RecordingTime: return File.Properties.System.RecordedTV.RecordingTime.Value;
                case FileProperties.RecordedTV_StationCallSign: return File.Properties.System.RecordedTV.StationCallSign.Value;
                case FileProperties.RecordedTV_StationName: return File.Properties.System.RecordedTV.StationName.Value;
                case FileProperties.Search_AutoSummary: return File.Properties.System.Search.AutoSummary.Value;
                case FileProperties.Search_ContainerHash: return File.Properties.System.Search.ContainerHash.Value;
                case FileProperties.Search_Contents: return File.Properties.System.Search.Contents.Value;
                case FileProperties.Search_EntryID: return File.Properties.System.Search.EntryID.Value;
                case FileProperties.Search_ExtendedProperties: return File.Properties.System.Search.ExtendedProperties.Value;
                case FileProperties.Search_GatherTime: return File.Properties.System.Search.GatherTime.Value;
                case FileProperties.Search_HitCount: return File.Properties.System.Search.HitCount.Value;
                case FileProperties.Search_IsClosedDirectory: return File.Properties.System.Search.IsClosedDirectory.Value;
                case FileProperties.Search_IsFullyContained: return File.Properties.System.Search.IsFullyContained.Value;
                case FileProperties.Search_QueryFocusedSummary: return File.Properties.System.Search.QueryFocusedSummary.Value;
                case FileProperties.Search_QueryFocusedSummaryWithFallback: return File.Properties.System.Search.QueryFocusedSummaryWithFallback.Value;
                case FileProperties.Search_Rank: return File.Properties.System.Search.Rank.Value;
                case FileProperties.Search_Store: return File.Properties.System.Search.Store.Value;
                case FileProperties.Search_UrlToIndex: return File.Properties.System.Search.UrlToIndex.Value;
                case FileProperties.Search_UrlToIndexWithModificationTime: return File.Properties.System.Search.UrlToIndexWithModificationTime.Value;
                case FileProperties.Shell_OmitFromView: return File.Properties.System.Shell.OmitFromView.Value;
                case FileProperties.Shell_SFGAOFlagsStrings: return File.Properties.System.Shell.SFGAOFlagsStrings.Value;
                case FileProperties.Software_DateLastUsed: return File.Properties.System.Software.DateLastUsed.Value;
                case FileProperties.Software_ProductName: return File.Properties.System.Software.ProductName.Value;
                case FileProperties.Sync_Comments: return File.Properties.System.Sync.Comments.Value;
                case FileProperties.Sync_ConflictDescription: return File.Properties.System.Sync.ConflictDescription.Value;
                case FileProperties.Sync_ConflictFirstLocation: return File.Properties.System.Sync.ConflictFirstLocation.Value;
                case FileProperties.Sync_ConflictSecondLocation: return File.Properties.System.Sync.ConflictSecondLocation.Value;
                case FileProperties.Sync_HandlerCollectionID: return File.Properties.System.Sync.HandlerCollectionID.Value;
                case FileProperties.Sync_HandlerID: return File.Properties.System.Sync.HandlerID.Value;
                case FileProperties.Sync_HandlerName: return File.Properties.System.Sync.HandlerName.Value;
                case FileProperties.Sync_HandlerType: return File.Properties.System.Sync.HandlerType.Value;
                case FileProperties.Sync_HandlerTypeLabel: return File.Properties.System.Sync.HandlerTypeLabel.Value;
                case FileProperties.Sync_ItemID: return File.Properties.System.Sync.ItemID.Value;
                case FileProperties.Sync_ItemName: return File.Properties.System.Sync.ItemName.Value;
                case FileProperties.Sync_ProgressPercentage: return File.Properties.System.Sync.ProgressPercentage.Value;
                case FileProperties.Sync_State: return File.Properties.System.Sync.State.Value;
                case FileProperties.Sync_Status: return File.Properties.System.Sync.Status.Value;
                case FileProperties.Task_BillingInformation: return File.Properties.System.Task.BillingInformation.Value;
                case FileProperties.Task_CompletionStatus: return File.Properties.System.Task.CompletionStatus.Value;
                case FileProperties.Task_Owner: return File.Properties.System.Task.Owner.Value;
                case FileProperties.Video_Compression: return File.Properties.System.Video.Compression.Value;
                case FileProperties.Video_Director: return File.Properties.System.Video.Director.Value;
                case FileProperties.Video_EncodingBitrate: return File.Properties.System.Video.EncodingBitrate.Value;
                case FileProperties.Video_FourCC: return File.Properties.System.Video.FourCC.Value;
                case FileProperties.Video_FrameHeight: return File.Properties.System.Video.FrameHeight.Value;
                case FileProperties.Video_FrameRate: return File.Properties.System.Video.FrameRate.Value;
                case FileProperties.Video_FrameWidth: return File.Properties.System.Video.FrameWidth.Value;
                case FileProperties.Video_HorizontalAspectRatio: return File.Properties.System.Video.HorizontalAspectRatio.Value;
                case FileProperties.Video_SampleSize: return File.Properties.System.Video.SampleSize.Value;
                case FileProperties.Video_StreamName: return File.Properties.System.Video.StreamName.Value;
                case FileProperties.Video_StreamNumber: return File.Properties.System.Video.StreamNumber.Value;
                case FileProperties.Video_TotalBitrate: return File.Properties.System.Video.TotalBitrate.Value;
                case FileProperties.Video_TranscodedForSync: return File.Properties.System.Video.TranscodedForSync.Value;
                case FileProperties.Video_VerticalAspectRatio: return File.Properties.System.Video.VerticalAspectRatio.Value;
                case FileProperties.Volume_FileSystem: return File.Properties.System.Volume.FileSystem.Value;
                case FileProperties.Volume_IsMappedDrive: return File.Properties.System.Volume.IsMappedDrive.Value;
                case FileProperties.Volume_IsRoot: return File.Properties.System.Volume.IsRoot.Value;
                case FileProperties.Thumbnail: return File.Properties.System.Thumbnail.Value;
                case FileProperties.ThumbnailCacheId: return File.Properties.System.ThumbnailCacheId.Value;
                case FileProperties.ThumbnailStream: return File.Properties.System.ThumbnailStream.Value;
                case FileProperties.Title: return File.Properties.System.Title.Value;
                case FileProperties.TotalFileSize: return File.Properties.System.TotalFileSize.Value;
                case FileProperties.Trademarks: return File.Properties.System.Trademarks.Value;
                default: throw ExceptionFactory.ArgumentOutOfRangeException(nameof(attribute));
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (File != null) { File.Dispose(); }
            }
            catch (Exception ex)
            {
                ExceptionHandler?.Invoke(ex);
            }

            IsDisposed = true;
        }
    }
}
