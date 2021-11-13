namespace EventBus.Messages.Events
{
    public class StockDeleteEvent : IntegrationBaseEvent
    {
        public string Code { get; set; }
    }
}
