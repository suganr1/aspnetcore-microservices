using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MediatR;
using AutoMapper;
using Companys.Domain.Entities;
using Companys.Application.Models;
using Companys.Application.Persistence;

namespace Companys.Application.Features.CompanyCqrs.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, int>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCommandHandler> _logger;

        public CreateCommandHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<CreateCommandHandler> logger)
        {
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var companyEntity = _mapper.Map<Company>(request);
            var newCompany = await _companyRepository.AddAsync(companyEntity);

            _logger.LogInformation($"Company {newCompany.Id} is successfully created.");

            return newCompany.Id;
        }
    }
}
