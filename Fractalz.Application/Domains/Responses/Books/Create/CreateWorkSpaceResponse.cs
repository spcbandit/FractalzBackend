using System;
using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Documents;

namespace Fractalz.Application.Domains.Responses.Books;

public class CreateWorkSpaceResponse:BasicResponse
{
    public List<Guid> AllowedUsersId { get; set; }
}