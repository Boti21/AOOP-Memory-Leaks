using CsvHelper.Configuration;

namespace Assignment3.Models;

public class RecordMap : ClassMap<Record>
{
    public RecordMap()
    {
        Map(m => m.Country).Name("Country");
        Map(m => m.Year).Name("Year");
        Map(m => m.FoodCategory).Name("Food Category");
        Map(m => m.TotalWaste).Name("Total Waste (Tons)");
        Map(m => m.EconomicLoss).Name("Economic Loss (Million $)");
        Map(m => m.AvgWastePerCapita).Name("Avg Waste per Capita (Kg)");
        Map(m => m.Population).Name("Population (Million)");
        Map(m => m.HouseholdWaste).Name("Household Waste (%)");
    }
}