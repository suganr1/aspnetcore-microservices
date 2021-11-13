using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Companys.Application.Models;
using Companys.Application.Features.CompanyCqrs.Commands.Create;
using Companys.Application.Features.CompanyCqrs.Commands.Delete;
using Companys.Application.Features.CompanyCqrs.Queries.GetAll;
using Companys.Application.Features.CompanyCqrs.Queries.GetByCode;

namespace Companys.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost(Name = "CreateCompany")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateCompany([FromBody] CreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet(Name = "GetCompanyAll")]
        [ProducesResponseType(typeof(IEnumerable<CompanyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyVm>>> GetCompanyAll()
        {
            var query = new GetAllListQuery();
            var allCompany = await _mediator.Send(query);
            return Ok(allCompany);
        }

        [HttpGet("{companyCode}", Name = "GetCompanyByCode")]
        [ProducesResponseType(typeof(IEnumerable<CompanyVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CompanyVm>> GetCompanyByCode(string companyCode)
        {
            var query = new GetByCodeListQuery(companyCode);
            var company = await _mediator.Send(query);
            return Ok(company);
        }

        [HttpDelete("{companyCode}", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(string companyCode)
        {
            var companyByCode = new GetByCodeListQuery(companyCode);
            if (companyByCode == null)
            {
                return BadRequest();
            }

            var command = new DeleteCommand() { CompanyCode = companyCode };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
