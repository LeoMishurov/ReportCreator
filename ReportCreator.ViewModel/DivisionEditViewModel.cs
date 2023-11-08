using System.ComponentModel;
using System.Windows.Input;
using ReportCreator.DataAccess.Models;
using ReportCreator.DataAccess.Repositories;

namespace ReportCreator.ViewModel
{
    public class DivisionEditViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // ссылка на репозиторий
        RepositoryDivisions repositoryDivisions = new();

        // конструктор
        public DivisionEditViewModel(Division? currentDivision = null) 
        {
            CurrentDivision = currentDivision ?? new Division();
        }

        // Подразделение
        public Division CurrentDivision { get; set; }

        // закрытие окна редактировния подразделения
        public Action CloseAction { get; set; }


        /// <summary>
        /// Сохранение или редактирование подразделения
        /// </summary>
        public ICommand EditCommand => new SimpleCommand(() =>
        {
            // если инициализирован id то редактируем
            if (CurrentDivision.Id != 0)
             repositoryDivisions.UpdateDivisions(CurrentDivision);
            // если нет то добавляем новое
            else
             repositoryDivisions.AddDivision(CurrentDivision);

            CloseAction();

        }, (_) => CurrentDivision != null );


        // закрытие окна при нажатии кнопки Отмена
        public ICommand CancelCommand => new SimpleCommand(CloseAction);

    }
}
