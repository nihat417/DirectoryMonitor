using System.IO;
using DirectoryMonitor.Interfaces;
using DirectoryMonitor.Models;

namespace DirectoryMonitor.Services
{
    public class TxtFileLoader : IFileLoader
    {
        public IEnumerable<TradeData> Load(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var tradeDataList = new List<TradeData>();

            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(';');
                var tradeData = new TradeData
                {
                    Date = DateTime.Parse(columns[0]),
                    Open = decimal.Parse(columns[1]),
                    High = decimal.Parse(columns[2]),
                    Low = decimal.Parse(columns[3]),
                    Close = decimal.Parse(columns[4]),
                    Volume = int.Parse(columns[5])
                };
                tradeDataList.Add(tradeData);
            }

            return tradeDataList;
        }
    }
}
