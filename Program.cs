using System.Xml;

namespace FixMe;

public static class Program
{
  public static void Main()
  {
    // Open file
    var f = File.Open("currency.xml", FileMode.Open);
      StreamReader s = new StreamReader(f);
  // Read file
  string d = s.ReadToEnd();
  var xml = new XmlDocument();



  // Load xml
  xml.LoadXml(d);
    // Find currency nodes
    var nodes = xml.GetElementsByTagName("currency");
    var data = new Dictionary<string, decimal>();
    
    var dates = new Dictionary<string, DateTime>();
    for (var i = 0; i < nodes.Count; i++)
    {
      // Read code
      var code = nodes[i].SelectSingleNode("code").InnerText;
      // Read Rate
      var rateNode = nodes[0].SelectSingleNode("Rate");
      string rate = "0";
      if (rateNode != null)
      {
        rate = rateNode.InnerText;
      }
      decimal decimalRate = 0;
    try
    {
      decimalRate = decimal.Parse(rate);
    }
    catch { }
    
    
    
    // Read Date
    var date = nodes[i].SelectSingleNode("date").InnerText;
    var dateDate = DateTime.Now;
    try
    {
      dateDate = DateTime.Parse(date);
    }
    catch { }

      data.Add(code, decimalRate);
      dates.Add(code, dateDate);
    }

    // Write to Console
    foreach  (var code in data.Keys)
    {
      var rate = data[code];
      
      
      var date = dates[code];
      Console.Write(code + ", " + rate + ", " + date + "\n");
    }

    // Write to CSV file
    var f2 = File.Open ("currency.csv", FileMode.Create);
    StreamWriter sw = new StreamWriter(f2);


    // Write only 2 rows
    var n = 0;
    foreach(var code in data.Keys )
    {
      var rate = data[code];
      var date = dates[code];
      
      sw.Write(code + ", " + rate + ", " + date + "\n");
      n++;
      if (n > 1)
        break;
    }
  }
}