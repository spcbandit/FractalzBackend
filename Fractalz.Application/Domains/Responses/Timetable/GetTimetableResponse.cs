using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Timetable;

namespace Fractalz.Application.Domains.Responses.Timetable;

public class GetTimetableResponse : BasicResponse
{
    public List<Schedule> Schedules {get; set; }
}