using System;
using System.Reflection;
using System.ComponentModel;

namespace TrackIt_2019
{
    class EnumUtils
    {
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
    }

    public enum NoteType
    {
        [Description("Technician Note")] TechnicianNote,
        [Description("Ticket Description")] TicketDescription,
        [Description("Ticket Resolution")] TicketResolution,
        [Description("Assignment Description")] AssignmentDescription,
        [Description("Assignment Resolution")] AssignmentResolution
    }

    public enum ActivityCode
    {
        [Description("Note By Technician")] NoteByTechnician,
        [Description("Additional Information")] AdditionalInformation,
        [Description("Completed Work")] CompletedWork,
        [Description("Configuratio nChange")] ConfigurationChange,
        [Description("Contacted User")] ContactedUser,
        [Description("Duplicate Work Order")] DuplicateWorkOrder,
        [Description("Information Required")] InformationRequired,
        [Description("Install")] Install,
        [Description("Original description")] OriginalDescription,
        [Description("Perform Work")] PerformWork,
        [Description("Planned Reboot")] PlannedReboot,
        [Description("Provide Information")] ProvideInformation,
        [Description("Purchase")] Purchase,
        [Description("Research")] Research,
        [Description("Upgrade")] Upgrade,
        [Description("User Error")] UserError,
        [Description("User Resolved")] UserResolved
    }

    public enum Status
    {
        [Description("Cancelled")] Cancelled,
        [Description("Closed")] Closed,
        [Description("Dismissed - No notes")] Dismissed,
        [Description("Escalated")] Escalated,
        [Description("Open")] Open,
        [Description("Paused")] Paused
    }

}
