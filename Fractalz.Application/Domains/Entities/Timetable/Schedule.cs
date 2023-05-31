using System;

namespace Fractalz.Application.Domains.Entities.Timetable;

public class Schedule
{
    /// <summary>
    /// 
    /// </summary>
    //[Key]
    public Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime DateStart { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public DateTime DateEnd { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public bool IsRepeat { get; set; }
}