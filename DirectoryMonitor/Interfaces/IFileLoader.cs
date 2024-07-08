using DirectoryMonitor.Models;

namespace DirectoryMonitor.Interfaces
{
    public interface IFileLoader
    {
        IEnumerable<TradeData> Load(string filePath);
    }
}
