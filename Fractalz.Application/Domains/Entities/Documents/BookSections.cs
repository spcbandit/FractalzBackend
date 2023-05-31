using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Application.Domains.Entities.Documents;
public class BookSections
{
    public Guid Id { get; set; }
    public string SectionName { get; set; }
    public List<BookSheets> BookSheets { get; set; }
    public Guid BooksId { get; set; }
    public DateTime Date { get; set; } = new DateTime();
    public Guid OwnerId { get; set; }
}