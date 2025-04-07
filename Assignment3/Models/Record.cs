using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace Assignment3.Models;

public class Record
{

    [Index(0)] public string Country { get; set; }
    [Index(1)] public int Year { get; set; }
    [Index(2)] public string FoodCategory { get; set; }
    [Index(3)] public decimal TotalWaste { get; set; }
    [Index(4)] public decimal EconomicLoss { get; set; }
    [Index(5)] public decimal AvgWastePerCapita { get; set; }
    [Index(6)] public decimal Population { get; set; }
    [Index(7)] public decimal HouseholdWaste { get; set; }

    public IEnumerable<decimal> GetDecimalValuesAsCollection()
    {
       return [TotalWaste, EconomicLoss, AvgWastePerCapita, Population, HouseholdWaste];
    }
}