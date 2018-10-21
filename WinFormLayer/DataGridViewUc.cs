namespace DataGridViewHostedInWpfControl.WinFormLayer
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Windows.Forms;
    using BusinessObjects;

    public partial class DataGridViewUc : System.Windows.Forms.UserControl
    {
        public DataGridViewUc()
        {
            InitializeComponent();
        }

        public delegate void ReturnGridSourceEventHandler(TenorStrikeRates currentGridSource);

        public event ReturnGridSourceEventHandler ReturnDataGridSource;

        public void Set(TenorStrikeRates source)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            if (source != null)
            {
                PopulateColumnHeader(source.UniqueSortedStrikes);
                PopulateRowHeader(source.UniqueSortedTenors);
                PopulateCells(source);
            }
        }

        public TenorStrikeRates GetDataGridSource()
        {
            TenorStrikeRates quotes = new TenorStrikeRates();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                string columnHeaderValue = column.HeaderText;
                if (columnHeaderValue != null)
                {
                    double strike = ReadStrike(columnHeaderValue);
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        object rowHeaderValue = row.HeaderCell.Value;
                        if (rowHeaderValue != null)
                        {
                            string tenor = rowHeaderValue.ToString();
                            object rateValue = row.Cells[column.Name].Value;
                            double rate = rateValue == null ? double.NaN : Convert.ToDouble(rateValue);
                            quotes.Add(new TenorStrikeRate(tenor, strike, rate));
                        }
                    }
                }
            }

            return quotes;
        }

        private void PopulateColumnHeader(List<double> strikes)
        {
            foreach (double strike in strikes)
            {
                string headerText = strike.ToString("P2");
                dgv.Columns.Add(headerText, headerText);
            }
        }

        private void PopulateRowHeader(List<string> tenors)
        {
            int numberOfRows = tenors.Count;
            dgv.Rows.Add(numberOfRows);
            for (int i = 0; i < numberOfRows; i++)
            {
                dgv.Rows[i].HeaderCell.Value = tenors[i];
            }
        }

        private void PopulateCells(TenorStrikeRates quotes)
        {
            List<string> tenors = quotes.UniqueSortedTenors;
            List<double> strikes = quotes.UniqueSortedStrikes;
            for (int rowIdx = 0; rowIdx < tenors.Count; rowIdx++)
            {
                string optionTenor = tenors[rowIdx];
                for (int colIdx = 0; colIdx < strikes.Count; colIdx++)
                {
                    double strike = strikes[colIdx];
                    TenorStrikeRate quote = quotes.Find(optionTenor, strike);
                    if (quote != null)
                    {
                        double rate = quote.Rate;
                        DataGridViewCell cell = dgv.Rows[rowIdx].Cells[colIdx];
                        cell.Value = rate;
                    }
                }
            }
        }
        
        private double ReadStrike(string input)
        {
            string percentSymbol = Thread.CurrentThread.CurrentCulture.NumberFormat.PercentSymbol;
            input = input.Replace(percentSymbol, string.Empty);
            return double.Parse(input, Thread.CurrentThread.CurrentCulture.NumberFormat) / 100;
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ReturnDataGridSource != null)
            {
                TenorStrikeRates newSource = GetDataGridSource(); 
                ReturnDataGridSource(newSource);
            }
        }
    }
}
