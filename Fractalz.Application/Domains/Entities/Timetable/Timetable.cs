using System;
using System.Collections.Generic;

namespace Fractalz.Application.Domains.Entities.Timetable;

public class Timetable
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public List<Schedule> Schedules { get; set; }
}