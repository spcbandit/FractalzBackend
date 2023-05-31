using System;
using System.Collections.Generic;

namespace Fractalz.Application.Domains.Entities.Documents;

public class DocumentWorkSpace
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public Guid AllowedUserId { get; set; }
    public Guid BookId { get; set; }
}