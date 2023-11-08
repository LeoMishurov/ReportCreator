using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReportCreator.DataAccess.Models;
using ReportCreator.DataAccess.Repositories;

namespace ReportCreator.ViewModel
{
    public class EquipmentEditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Ссылка на репозиторий
        RepositoryEquipment repositoryEquipment = new();

        // Конструктор
        public EquipmentEditViewModel(ObservableCollection<Division> divisions, Equipment? currentEquipment = null ) 
        {
            CurrentEquipment = currentEquipment ?? new Equipment() { CommissioningDate = DateTime.Now };

            Divisions = divisions;

            if (CurrentEquipment != null)
                CurrentDivision = Divisions.FirstOrDefault(x => x.Id == CurrentEquipment.DivisionId);
        }

        // Оборудование
        public Equipment CurrentEquipment { get; set; }
        
        // Коллекция с подразделениями
        public ObservableCollection<Division> Divisions { get; set; }
        public Division CurrentDivision { get; set; }


        /// <summary>
        /// Закрытие окна редактировния оборудования
        /// </summary>
        public Action CloseAction { get; set; }


        /// <summary>
        /// Сохранение или редактирование оборудования
        /// </summary>
        public ICommand EditCommand => new SimpleCommand(() =>
        {
            CurrentEquipment.DivisionId = CurrentDivision.Id;

            // Если инициализирован id то редактируем
            if (CurrentEquipment.Id != 0)
            {
                repositoryEquipment.UpdateEquipment(CurrentEquipment);
            }
            // Если нет то добавляем новое
            else if (CurrentEquipment.Title != null && CurrentEquipment.DivisionId != 0 && CurrentEquipment.CommissioningDate != null
                    && CurrentEquipment.Quantity != 0)
            {
                CurrentEquipment.DivisionId = CurrentDivision.Id;
                repositoryEquipment.AddEquipment(CurrentEquipment);
            }
            else return;

            CloseAction();

        });


        // Закрытие окна при нажатии кнопки Отмена
        public ICommand CancelCommand => new SimpleCommand(CloseAction);

    }
}
