using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assignment3.Models;

public class Datapoints<XType, YType>
{
    public List<XType> XAxis { get; set; }
    public List<YType> YAxis { get; set; }
}