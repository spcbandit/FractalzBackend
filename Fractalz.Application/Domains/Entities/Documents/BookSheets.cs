using System;

namespace Fractalz.Application.Domains.Entities.Documents;

public class BookSheets
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid BookSectionsId { get; set; }
    public DateTime Date { get; set; } = new DateTime();
}