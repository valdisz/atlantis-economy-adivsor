namespace atlantis
{
    using System.Collections.Generic;
    using System.Linq;
    using Pidgin;
    using static Pidgin.Parser;
    using static Pidgin.Parser<char>;
    using static Tokens;

    // public class Region {
    //     public Region(RegionInfo info, Population population, Silver taxes, string weatherLastMonth, string weatherNextMonth,
    //         Silver wages, Silver maxWage, Silver entertainment, IEnumerable<ItemWithPrice> wanted,
    //         IEnumerable<ItemWithPrice> forSale, IEnumerable<Item> products) {
    //         Info = info;
    //         Population = population;
    //         Taxes = taxes;
    //         WeatherLastMonth = weatherLastMonth;
    //         WeatherNextMonth = weatherNextMonth;
    //         Wages = wages;
    //         MaxWage = maxWage;
    //         Entertainment = entertainment;

    //         Wanted.AddRange(wanted ?? Enumerable.Empty<ItemWithPrice>());
    //         ForSale.AddRange(forSale ?? Enumerable.Empty<ItemWithPrice>());
    //         Products.AddRange(products ?? Enumerable.Empty<Item>());
    //     }

    //     public RegionInfo Info { get; }
    //     public Population Population { get; }
    //     public Silver Taxes { get; }

    //     public string WeatherLastMonth { get; }
    //     public string WeatherNextMonth { get; }

    //     public Silver Wages { get; }
    //     public Silver MaxWage { get; }
    //     public Silver Entertainment { get; }

    //     public List<ItemWithPrice> Wanted { get; } = new List<ItemWithPrice>();
    //     public List<ItemWithPrice> ForSale { get; } = new List<ItemWithPrice>();
    //     public List<Item> Products { get; } = new List<Item>();

    //     public Dictionary<Direction, RegionInfo> Exits { get; } = new Dictionary<Direction, RegionInfo> {
    //         { Direction.North, null },
    //         { Direction.Northeast, null },
    //         { Direction.Southeast, null },
    //         { Direction.South, null },
    //         { Direction.Southwest, null },
    //         { Direction.Northwest, null }
    //     };
    // }
}
