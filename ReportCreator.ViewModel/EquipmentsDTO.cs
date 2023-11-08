using System.ComponentModel;

namespace ReportCreator.ViewModel
{
    public class EquipmentsDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string DivisionTitle { get; set; }
        public DateTime CommissioningDate { get; set; }
        public int Quantity { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
