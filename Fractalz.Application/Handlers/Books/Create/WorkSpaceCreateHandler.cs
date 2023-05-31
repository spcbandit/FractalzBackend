using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Requests.Books;
using Fractalz.Application.Domains.Responses.Books;
using MediatR;

namespace Fractalz.Application.Handlers.Books;

public class WorkSpaceCreateHandler:IRequestHandler<CreateWorkSpaceRequest, CreateWorkSpaceResponse>
{
    private IRepository<Domains.Entities.Profile.User> _repository;
    private IRepository<DocumentWorkSpace> _repositoryWork;
    private List<Guid> allowedId { get; set; } = new List<Guid>();

    public WorkSpaceCreateHandler(IRepository<Domains.Entities.Profile.User> repository,IRepository<DocumentWorkSpace> repositoryWork )
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _repositoryWork = repositoryWork ?? throw new ArgumentNullException(nameof(repositoryWork));
    }

    public async Task<CreateWorkSpaceResponse> Handle(CreateWorkSpaceRequest request, CancellationToken cancellationToken)
    {
        var create = _repositoryWork.Create(new DocumentWorkSpace()
        {
            BookId = request.BookId, Id = Guid.NewGuid(), OwnerId = request.OwnerId,
            AllowedUserId = request.AllowedUserId
        });
        var list = _repositoryWork.GetWithInclude(x => x.OwnerId == request.OwnerId).FirstOrDefault();
        this.allowedId = new List<Guid>(){list.AllowedUserId};
        return new CreateWorkSpaceResponse() { Success = true, Message = "", AllowedUsersId = allowedId};
        
    }
    
}