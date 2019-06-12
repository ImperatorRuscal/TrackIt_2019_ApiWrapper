using System;
using System.Dynamic;
using System.Collections.Generic;

namespace TrackIt_2019
{

    public static partial class API
    {
        public static class Assignment
        {
            public static dynamic Get(int ID)
            {
                dynamic result = TrackIt_2019.Util.GetRequest(new string[] { "Assignments", ID.ToString() }).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }

            public static dynamic Create(dynamic newProperties)
            {
                dynamic request = new ExpandoObject();
                request.Properties = newProperties;
                var result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments" }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }

            public static dynamic Update(int TicketID, dynamic updateProperties)
            {
                dynamic request = new ExpandoObject();
                request.Properties = updateProperties;
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString() }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }

            public static dynamic Assign(int TicketID, string TechnicianID)
            {
                var requestProperties = new ExpandoObject() as IDictionary<string, Object>;
                requestProperties.Add("Assigned To Technician", TechnicianID);
                dynamic request = new ExpandoObject();
                request.Properties = requestProperties;
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString() }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }

            public static dynamic SetCategory(int TicketID, string CategoryName)
            {
                dynamic requestProperties = new ExpandoObject();
                requestProperties.Category = CategoryName;
                dynamic request = new ExpandoObject();
                request.Properties = requestProperties;
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString() }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }

            public static dynamic AddNote(int TicketID, string NoteContent, string NoteType = "Technician Note", string ActivityCode = "Note By Technician", bool isPrivate = false)
            {
                var request = new ExpandoObject() as IDictionary<string, Object>;
                request.Add("Note Type", NoteType);
                request.Add("Activity Code", ActivityCode);
                request.Add("Note", NoteContent);
                request.Add("Private", isPrivate.ToString());
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString(), "AddNote" }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Note);
            }
            public static dynamic AddNote(int TicketID, string NoteContent, TrackIt_2019.NoteType noteType, TrackIt_2019.ActivityCode activityCode, bool isPrivate)
            {
                return AddNote(TicketID, NoteContent, TrackIt_2019.EnumUtils.stringValueOf(noteType), TrackIt_2019.EnumUtils.stringValueOf(activityCode), isPrivate);
            }


            public static dynamic SetStatus(int TicketID, string Status = "Closed", string NoteContent = "", string NoteType = "Ticket Resolution", string ActivityCode = "Completed Work", bool isPrivate = false)
            {
                var note = new ExpandoObject() as IDictionary<string, Object>;
                note.Add("Note Type", NoteType);
                note.Add("Activity Code", ActivityCode);
                note.Add("Note", NoteContent);
                note.Add("Private", isPrivate.ToString());
                dynamic request = new ExpandoObject();
                request.StatusName = Status;
                request.Note = note;
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString(), "ChangeStatus" }, request).GetAwaiter().GetResult();
                return TrackIt_2019.Util.getResultantObject(result.Assignments);
            }
            public static dynamic SetStatus(int TicketID, TrackIt_2019.Status status, string NoteContent, TrackIt_2019.NoteType NoteType, TrackIt_2019.ActivityCode activityCode, bool isPrivate)
            {
                return SetStatus(TicketID, TrackIt_2019.EnumUtils.stringValueOf(status), NoteContent, TrackIt_2019.EnumUtils.stringValueOf(NoteType), TrackIt_2019.EnumUtils.stringValueOf(activityCode), isPrivate);
            }

            public static bool Delete(int TicketID)
            {
                dynamic result = TrackIt_2019.Util.PostRequest(new string[] { "Assignments", TicketID.ToString(), "Delete" }, null).GetAwaiter().GetResult();
                return result.success;
            }
        }
    }
}