
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using WpfApp1;

namespace BudgetManager
{
    [Serializable]
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<BudgetRecord> _records;
        private string _filePath = "records.dat";

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BudgetRecord> Records
        {
            get
            {
                return _records;
            }
            set
            {
                _records = value;
                OnPropertyChanged("Records");
            }
        }

        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (BudgetRecord record in _records)
                {
                    total += record.Amount;
                }

                return total;
            }
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                using (Stream stream = File.Open(_filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Records = (ObservableCollection<BudgetRecord>)formatter.Deserialize(stream);
                }
            }
            else
            {
                Records = new ObservableCollection<BudgetRecord>();
            }
        }

        private void SaveData()
        {
            using (Stream stream = File.Open(_filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, _records);
            }
        }

      /*  private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddRecordWindow window = new AddRecordWindow();
            window.Owner = this;
            if (window.ShowDialog() == true)
            {
                Records.Add(window.Record);
            }
        }*/

      /*  private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                EditRecordWindow window = new EditRecordWindow((BudgetRecord)dataGrid.SelectedItem);  
                window.Owner = this;
                if (window.ShowDialog() == true)
                {
                    int index = Records.IndexOf((BudgetRecord)dataGrid.SelectedItem);
                    Records.RemoveAt(index);
                    Records.Insert(index, window.Record);
                }
            }
        }*/

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                Records.Remove((BudgetRecord)dataGrid.SelectedItem);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveData();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}