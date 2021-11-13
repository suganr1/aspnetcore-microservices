using System;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            Id = random.Next();
            //Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(int id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public int Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
