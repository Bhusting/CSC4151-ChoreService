using System;

namespace Domain
{
    public class Chore
    {
        public Guid ChoreId { get; set; }

        public string ChoreName { get; set; }

        public Guid CompletedByUserId { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime CompletionTime { get; set; }

        public Guid HouseId { get; set; }
        public Guid ChoreTypeId { get; set; }
    }
}
