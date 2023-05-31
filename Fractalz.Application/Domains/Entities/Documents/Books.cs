using System;
using System.Collections.Generic;

namespace Fractalz.Application.Domains.Entities.Documents;

public class Books
{
    public Guid Id { get; set; }
    public string BookName { get; set; }
    public DateTime DateTime { get; set; }
    public List<BookSections> Sections { get; set; }
    public string Color { get; set; }
    public string About { get; set; }
    public Guid OwnerId { get; set; }
}