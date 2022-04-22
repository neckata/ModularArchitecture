using System;

namespace Shared.Core.Entity
{
    public class EntityReference
    {
        public EntityReference(string entity)
        {
            Entity = entity;
            MonthYearString = DateTime.Now.ToString("MMyy");
            LastUpdateOn = DateTime.Now;
            Count = 1;
        }

        public void Increment()
        {
            LastUpdateOn = DateTime.Now;
            Count++;
        }

        public int Id { get; private set; }

        public string Entity { get; private set; }

        public string MonthYearString { get; private set; }

        public int Count { get; private set; }

        public DateTime LastUpdateOn { get; private set; }
    }
}
