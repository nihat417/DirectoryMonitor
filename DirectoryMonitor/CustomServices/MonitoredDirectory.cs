using DirectoryMonitor.Interfaces;
using DirectoryMonitor.Models;
using DirectoryMonitor.Services;
using System.IO;

namespace DirectoryMonitor.CustomServices
{
    public class MonitoredDirectory
    {
        private readonly string _path;
        private readonly int _frequency;
        private readonly Timer _timer;
        private readonly List<IFileLoader> _loaders;

        public event Action<IEnumerable<TradeData>> FilesProcessed;

        public MonitoredDirectory(string path, int frequency, List<IFileLoader> loaders)
        {
            _path = path;
            _frequency = frequency;
            _loaders = loaders;
            _timer = new Timer(CheckForNewFiles!, null, 0, _frequency);
        }

        private void CheckForNewFiles(object state)
        {
            var files = Directory.GetFiles(_path);

            var tradeDataList = new List<TradeData>();

            Parallel.ForEach(files, file =>
            {
                foreach (var loader in _loaders)
                {
                    if (file.EndsWith(".csv") && loader is CsvFileLoader ||
                        file.EndsWith(".txt") && loader is TxtFileLoader ||
                        file.EndsWith(".xml") && loader is XmlFileLoader)
                    {
                        tradeDataList.AddRange(loader.Load(file));
                    }
                }
            });

            FilesProcessed?.Invoke(tradeDataList);
        }
    }
}
