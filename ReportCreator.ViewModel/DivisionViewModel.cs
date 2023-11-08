using ReportCreator.DataAccess.Models;
using ReportCreator.DataAccess.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace ReportCreator.ViewModel
{
    public class DivisionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Коллекция подразделений
        public ObservableCollection<Division> Divisions { get; set; }
        public Division CurrentDivision { get; set; }

        // Ссылка на репозиторий
        RepositoryDivisions repositoryDivisions = new ();

        // Конструктор
        public DivisionViewModel()
        {    
            
            Divisions = new ObservableCollection<Division>(repositoryDivisions.GetDivisions());

        }

        /// <summary>
        /// Редактирование подразделения
        /// </summary>
        public ICommand EditCommand => new SimpleCommand(() =>
        {
            ShowEditWindow(CurrentDivision);

            Divisions = new ObservableCollection<Division>(repositoryDivisions.GetDivisions());

        }, (_) => CurrentDivision != null);

        /// <summary>
        /// Дбавление подразделения
        /// </summary>
        public ICommand AddCommand => new SimpleCommand(() =>
        {
            ShowEditWindow(null);

            Divisions = new ObservableCollection<Division>(repositoryDivisions.GetDivisions());

        });

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        public ICommand DeleteCommand => new SimpleCommand(() =>
        {
            repositoryDivisions.RemoveDivision(CurrentDivision);
            Divisions.Remove(CurrentDivision); // Удаление строки

        });

        /// <summary>
        /// Создание окна для редактировния подразделения
        /// </summary>
        /// <param name="division"></param>
        private void ShowEditWindow(Division? division)
        {
            var win = new Window() { Width = 400, Height = 210 };
            win.Content = new DivisionEditViewModel(division) { CloseAction = () => win.Close() };
            win.ShowDialog();
        }
    }
}
