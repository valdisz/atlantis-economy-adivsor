namespace advisor.Persistence {
    using System.ComponentModel.DataAnnotations;
    using advisor.Model;
    using HotChocolate;

    [GraphQLName("Event")]
    public class DbEvent {
        [Key]
        public long Id { get; set; }

        [GraphQLIgnore]
        public long TurnId { get; set; }

        [GraphQLIgnore]
        public long FactionId { get; set; }

        [GraphQLIgnore]
        public long? RegionId { get; set; }

        [GraphQLIgnore]
        public long? UnitId { get; set; }

        [Required]
        public EventType Type { get; set; }

        [Required]
        public EventCategory Category { get; set; } = EventCategory.Unknown;

        [Required]
        public string Message { get; set; }

        public int? Amount { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public int? ItemPrice { get; set; }

        // public int? X { get; set; }
        // public int? Y { get; set; }
        // public int? Z { get; set; }
        // public string Label { get; set; }
        // public string Province { get; set; }
        // public string Terrain { get; set; }


        [GraphQLIgnore]
        public DbTurn Turn { get; set; }

        [GraphQLIgnore]
        public DbFaction Faction { get; set; }

        [GraphQLIgnore]
        public DbRegion Region { get; set; }

        [GraphQLIgnore]
        public DbUnit Unit { get; set; }
    }
}
