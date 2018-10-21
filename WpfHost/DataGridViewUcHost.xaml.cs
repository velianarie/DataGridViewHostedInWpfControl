namespace DataGridViewHostedInWpfControl.WpfHost
{
    using System.Windows;
    using BusinessObjects;
    using WinFormLayer;

    /// <summary>
    /// Interaction logic for DataGridViewUcHost.xaml
    /// </summary>
    public partial class DataGridViewUcHost : System.Windows.Controls.UserControl
    {
        public static readonly DependencyProperty GridSourceProperty = DependencyProperty.Register(
             "GridSource",
             typeof(TenorStrikeRates),
             typeof(DataGridViewUcHost),
             new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnGridSourcePropertyChanged));

        public TenorStrikeRates GridSource
        {
            get { return (TenorStrikeRates)GetValue(GridSourceProperty); }
            set { SetValue(GridSourceProperty, value); }
        }

        private static DataGridViewUc dgvUc;

        public DataGridViewUcHost()
        {
            InitializeComponent();

            dgvUc = (DataGridViewUc)MyWinFormsHost.Child;
            dgvUc.ReturnDataGridSource += OnReturnDataGridSource;
        }

        private static void OnGridSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TenorStrikeRates newGridSource = (TenorStrikeRates)e.NewValue;
            TenorStrikeRates currentGridSource = dgvUc.GetDataGridSource();
            if (!currentGridSource.Equals(newGridSource))
            {
                dgvUc.Set(newGridSource);
            }
        }

        private void OnReturnDataGridSource(TenorStrikeRates currentGridSource)
        {
            GridSource = currentGridSource;
        }
    }
}
