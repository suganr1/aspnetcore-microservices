using System;
using System.Threading.Tasks;
using MassTransit;
using EventBus.Messages.Events;
using Stock.API.Repositories;
using Microsoft.Extensions.Logging;

namespace Stock.API.EventBusConsumer
{
    public class StockDeleteConsumer : IConsumer<StockDeleteEvent>
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockDeleteConsumer> _logger;

        public StockDeleteConsumer(IStockRepository stockRepository, ILogger<StockDeleteConsumer> logger)
        {
            _stockRepository = stockRepository ?? throw new ArgumentNullException(nameof(stockRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<StockDeleteEvent> context)
        {
            await _stockRepository.DeleteStockByCode(context.Message.Code);

            _logger.LogInformation($"{DateTime.Now:MM/dd/yyyy HH:mm:ss}: All Stocks Deleted Successfully.");
        }
    }
}
