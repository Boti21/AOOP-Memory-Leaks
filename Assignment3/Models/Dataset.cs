using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using LiveChartsCore;

namespace Assignment3.Models;

public class Dataset
{ 
    // Make 5-10 preset queries, func return collections
    // possibly add variables into the methods that LINQ the collections
    
    // CSV config
    private CsvConfiguration config;
   
    // Dataset read into Record classes, not sure if it will be useful
    public IEnumerable<Record> Records { get; }
    
    // Dataset read into List<dynamic> for casting each row into IDictionary for string based column extraction
    public List<dynamic> RecordsList { get; }
    
    // List to store column header, if datatype is bad feel free to change it
    public string[] Header { get; }
    
    // List of categories
    public string[] Categories { get; init; }
    
    // List of years of data
    public string[] Years { get; init; }
    
    // List of countries
    public string[] Countries { get; init; }
    
    public Dataset()
    {
        config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };
        var csv = new CsvReader(new StreamReader("../../../Assets/global_food_wastage_dataset.csv"), config);
        csv.Read();
        csv.ReadHeader();
        Header = csv.HeaderRecord;
        
        csv.Dispose();
        csv = new CsvReader(new StreamReader("../../../Assets/global_food_wastage_dataset.csv"), config);
        csv.Context.RegisterClassMap<RecordMap>();
        Records = csv.GetRecords<Record>().ToList();
        
        csv.Dispose();
        csv = new CsvReader(new StreamReader("../../../Assets/global_food_wastage_dataset.csv"), config);
        RecordsList = csv.GetRecords<dynamic>().ToList();

        Categories = GetColumn(Header[2]).Distinct().Cast<string>().ToArray();
        Years = GetColumn(Header[1]).Distinct().Cast<string>().ToArray();
        Countries = GetColumn(Header[0]).Distinct().Cast<string>().ToArray();
    }

    // Returns IEnumerable column specified by columnName
    public IEnumerable<object> GetColumn(string columnName)
    {  
        return RecordsList.Select(row => ((IDictionary<string, object>)row)[columnName]);
    }

    // Bar chart
    public IEnumerable<decimal> GetRowOfCountryInCatInYear(string Country, string Cat, string Year)
    {
        if (!Categories.Contains(Cat))
        {
            throw new ArgumentException("Invalid arguments: Cat");
        }
        if (!Years.Contains(Year))
        {
            throw new ArgumentException("Invalid arguments: Year");
        }

        if (!Countries.Contains(Country))
        {
            throw new ArgumentException("Invalid arguments: Country");
        }
        
        return Records.Where(r => r.Country == Country && r.FoodCategory == Cat && r.Year == Convert.ToInt32(Year)).First().GetDecimalValuesAsCollection();
    }

    public Datapoints GetCountryTrendInCat(string Country, string Cat)
    {
        var result = new Datapoints
        {
            XAxis = Records.Select(r => r.Year)
                .Distinct()
                .Order()
                .Cast<object>()
        };

        int headerIndex = 0;
        for (int i = 0; i < Header.Length; i++)
        {
            if (Header[i] == Cat)
            {
                headerIndex = i;
            }
        }
        
        // All this to remove duplicate by year rows
        var countrySorted = Records.Where(r => r.Country == Country)
            .DistinctBy(r => r.Year)
            .OrderBy(r => r.Year)
            .ToList();
        
        // Don't know of a better way than this to parametrize the column based on the header
        if (headerIndex == 0)
        {
            result.YAxis = countrySorted.Select(r => r.Country)
                .Distinct()
                .Cast<object>()
                .ToList();
        } 
        else if (headerIndex == 1)
        {
            result.YAxis = countrySorted.Select(r => r.Year)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 2)
        {
            result.YAxis = countrySorted.Select(r => r.FoodCategory)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 3)
        {
            result.YAxis = countrySorted.Select(r => r.TotalWaste)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 4)
        {
            result.YAxis = countrySorted.Select(r => r.EconomicLoss)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 5)
        {
            result.YAxis = countrySorted.Select(r => r.AvgWastePerCapita)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 6)
        {
            result.YAxis = countrySorted.Select(r => r.Population)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        else if (headerIndex == 7)
        {
            result.YAxis = countrySorted.Select(r => r.HouseholdWaste)
                .Distinct()
                .Cast<object>()
                .ToList();
        }
        
        return result;
    }
    
}
