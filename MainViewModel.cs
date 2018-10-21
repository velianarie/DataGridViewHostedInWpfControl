namespace DataGridViewHostedInWpfControl
{
    using System.ComponentModel;
    using BusinessObjects;

    public class MainViewModel : INotifyPropertyChanged
    {
        private TenorStrikeRates input;
        public TenorStrikeRates Input
        {
            get { return input; }
            set
            {
                input = value;
                OnPropertyChanged("Input");
                Output = BuildOutputValue(value);
            }
        }

        private string output;
        public string Output
        {
            get { return output; }
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string BuildOutputValue(TenorStrikeRates tenorStrikeRates)
        {
            string text = string.Empty;
            foreach (TenorStrikeRate item in tenorStrikeRates)
            {
                text = text + string.Format("Tenor: {0}, Strike: {1}, Rate: {2}\n", item.Tenor, item.Strike.ToString("P2"), item.Rate);
            }

            return text;
        }
    }
}
