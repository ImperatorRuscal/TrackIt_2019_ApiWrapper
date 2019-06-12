# TrackIt_2019_ApiWrapper
## A quick C# static wrapper to handle WebAPI calls for the BMC TrackIt 2019 service desk software.

#### Setup the connection.
You should start out by givign the wrapper your login details.
If emulating a technician, then pass their login details here.  If making a service then pass in the details of your "service account technician" instead.
```
TrackIt_2019.API.BaseUri = "https://YourTrackItServer/TrackIt/WebAPI/";
TrackIt_2019.API.Authentication.Username = "UsernameForTrackitLogon";
TrackIt_2019.API.Authentication.Password = "password";
TrackIt_2019.API.Authentication.Group = "Group that this user is part of";
```
#### Make your calls
The return from the `Delete(int ID)` method is a _boolean_ representing success/fail of the deletion.  Everything else returns a _dynamic_ (ExpandoObject) representing the object you are working on.  So a `Get(int ID)` and an `Assign(int ID, string TechnicianLogin)` will both return an object with the properties of a _ticket_.

**TODO ::** Make strongly-typed objects to reflect the returns from the API (ticket, assignment, note)

To get Ticket # 78452 into the variable `foundTicket`
```
dynamic foundTicket = TrackIt_2019.API.Ticket.Get(78452);
```
To assign that ticket to the technician with login "JOHN-DOE"
    _(note: here I assign the ticket but discard the object that is returned)_
```
TrackIt_2019.API.Ticket.Assign(foundTicket.TicketID, "JOHN-DOE");
```

#### You can modify the Enums
There are enumerations created in the Enum.cs file.  I've included a handful, but your setup may not actually have all of these.  At the underlying end TrackIt is actually looking for/using strings.  I just put together the Enums so that I could reference something that already had all of the strings (that I commonly needed).  Feel free to add/remove/change the enums packaged here to more closely match your implementation.


**TODO ::** Actual documentation of the methods.  Most are rather self-explainatory, but docs > no-docs.
