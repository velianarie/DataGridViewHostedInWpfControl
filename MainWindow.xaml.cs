namespace DataGridViewHostedInWpfControl
{
    using BusinessObjects;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :  System.Windows.Window
    {
        public MainWindow()
        {
            MainViewModel vm = new MainViewModel();
            DataContext = vm;

            InitializeComponent();

            vm.Input = BuildInitialInput();
        }

        private TenorStrikeRates BuildInitialInput()
        {
            var quotes = new TenorStrikeRates();
            quotes.Add(new TenorStrikeRate("1y", -0.01, 0.01));
            quotes.Add(new TenorStrikeRate("1y", 0, 0.02));
            quotes.Add(new TenorStrikeRate("1y", 0.01, 0.03));
            quotes.Add(new TenorStrikeRate("2y", -0.01, 0.04));
            quotes.Add(new TenorStrikeRate("2y", 0, 0.05));
            quotes.Add(new TenorStrikeRate("2y", 0.01, 0.06));
            return quotes;
        }
    }
}
