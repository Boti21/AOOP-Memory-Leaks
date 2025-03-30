using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Assignment3.Models;

public class Dataset
{ 
    // Make 5-10 preset queries, func return collections
    // possibly add variables into the methods that LINQ the collections
   
    // Dataset read into Record classes, not sure if it will be useful
    public IEnumerable<Record> Records { get; }
    
    // Dataset read into List<dynamic> for casting each row into IDictionary for string based column extraction
    public List<dynamic> RecordsList { get; }
    
    // List to store column header, if datatype is bad feel free to change it
    public string[]? Header { get; }
    
    public Dataset()
    {
        var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
        };
        var csv = new CsvReader(new StreamReader("../../../Assetts/global_food_wastage_dataset.csv"), config);
        csv.Read();
        csv.ReadHeader();
        Header = csv.HeaderRecord;
        
        Records = csv.GetRecords<Record>();
        RecordsList = csv.GetRecords<dynamic>().ToList();
    }

    // Returns IEnumerable column specified by columnName
    public IEnumerable<object> GetColumn(string columnName)
    {  
        return RecordsList.Select(row => (IDictionary<string, object>)row[columnName]);
    }
}