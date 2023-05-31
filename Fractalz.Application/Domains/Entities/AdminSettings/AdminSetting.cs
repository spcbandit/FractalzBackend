using System;

namespace Fractalz.Application.Domains.Entities.AdminSettings;

/// <summary>
/// Объект настроек сервера
/// </summary>
public class AdminSetting
{
    public Guid Id { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}