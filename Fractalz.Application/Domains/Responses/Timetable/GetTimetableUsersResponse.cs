using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.Timetable;

public class GetTimetableUsersResponse : BasicResponse
{
    public List<Entities.Timetable.Timetable> Timetables { get; set; }
}