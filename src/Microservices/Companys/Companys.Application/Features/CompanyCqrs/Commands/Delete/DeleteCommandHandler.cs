using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Companys.Application.Exceptions;
using Companys.Application.Persistence;
using System;
using EventBus.Messages.Events;
using MassTransit;

namespace Companys.Application.Features.CompanyCqrs.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCommandHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteCommandHandler(ICompanyRepository companyRepository, IMapper mapper,
            ILogger<DeleteCommandHandler> logger, IPublishEndpoint publishEndpoint)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            // get
            var companyByCode = await _companyRepository.GetCompanyByCode(request.CompanyCode);
            if (companyByCode == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Company), request.CompanyCode);
            }

            var companyToDelete = await _companyRepository.GetByIdAsync(companyByCode.Id);
            if (companyToDelete == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Company), request.CompanyCode);
            }

            // send delete event to rabbitmq
            var eventMessage = _mapper.Map<StockDeleteEvent>(companyByCode);
            //eventMessage.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // remove
            await _companyRepository.DeleteAsync(companyToDelete);

            _logger.LogInformation($"Company {companyByCode.Code} is successfully deleted.");

            return Unit.Value;
        }
    }
}
