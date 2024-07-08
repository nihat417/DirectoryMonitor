using DirectoryMonitor.Interfaces;
using DirectoryMonitor.Models;
using System.Xml.Linq;


namespace DirectoryMonitor.Services
{
    public class XmlFileLoader : IFileLoader
    {
        public IEnumerable<TradeData> Load(string filePath)
        {
            var document = XDocument.Load(filePath);
            var tradeDataList = new List<TradeData>();

            foreach (var element in document.Descendants("value"))
            {
                var tradeData = new TradeData
                {
                    Date = DateTime.Parse(element.Attribute("date")!.Value),
                    Open = decimal.Parse(element.Attribute("open")!.Value),
                    High = decimal.Parse(element!.Attribute("high")!.Value!),
                    Low = decimal.Parse(element.Attribute("low")!.Value),
                    Close = decimal.Parse(element.Attribute("close")!.Value),
                    Volume = int.Parse(element.Attribute("volume")!.Value)
                };
                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }
    }
}
