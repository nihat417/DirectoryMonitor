using DirectoryMonitor.CustomServices;
using DirectoryMonitor.Interfaces;
using DirectoryMonitor.Models;
using DirectoryMonitor.Services;
using System.Configuration;
using System.Windows;

namespace DirectoryMonitor
{
    public partial class MainWindow : Window
    {
        private MonitoredDirectory _monitor;

        public MainWindow()
        {
            InitializeComponent();
            var path = ConfigurationManager.AppSettings["InputDirectory"];
            var frequency = int.Parse(ConfigurationManager.AppSettings["CheckFrequency"]!);

            var loaders = new List<IFileLoader>
            {
            new CsvFileLoader(),
                new TxtFileLoader(),
                new XmlFileLoader()
            };

            _monitor = new MonitoredDirectory(path!, frequency, loaders);
            _monitor.FilesProcessed += OnFilesProcessed;
        }

        private void OnFilesProcessed(IEnumerable<TradeData> tradeData)
        {
            Dispatcher.Invoke(() =>
            {
                TradeDataGrid.ItemsSource = tradeData;
            });
        }

    }
}
