using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace Assignment3.Models;

public class Record
{

    [Index(0)] public string Country { get; set; }
    [Index(1)] public int Year { get; set; }
    [Index(2)] public string FoodCategory { get; set; }
    [Index(3)] public double TotalWaste { get; set; }
    [Index(4)] public double EconomicLoss { get; set; }
    [Index(5)] public double AvgWastePerCapita { get; set; }
    [Index(6)] public double Population { get; set; }
    [Index(7)] public double HouseholdWaste { get; set; }

    public IEnumerable<double> GetDecimalValuesAsCollection()
    {
       return [TotalWaste, EconomicLoss, AvgWastePerCapita, Population, HouseholdWaste];
    }
}