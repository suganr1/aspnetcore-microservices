using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Messages.Events;
using Stock.API.Repositories;
using Microsoft.Extensions.Logging;

namespace Stock.API.EventBusConsumer
{
    public class CompanyDeleteConsumer : IConsumer<CompanyDeleteEvent>
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<CompanyDeleteConsumer> _logger;

        public CompanyDeleteConsumer(IStockRepository stockRepository, ILogger<CompanyDeleteConsumer> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<CompanyDeleteEvent> context)
        {
            await _stockRepository.DeleteStockByCode(context.Message.Code);

            _logger.LogInformation($"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: All Stocks Deleted Successfully.");
        }
    }
}
