using ReportCreator.DataAccess.Models;
using ReportCreator.DataAccess.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using Ookii.Dialogs.Wpf;
//using System.Windows.Forms;
using System.Windows;
using System.IO;

namespace ReportCreator.ViewModel
{
    public class EquipmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // Коллекция с оборудованием
        public ObservableCollection<Equipment> Equipments { get; set; }
        public Equipment CurrentEquipment { get; set; }

        // Коллекция с подразделениями
        public ObservableCollection<Division> Divisions { get; set; }
        public Division CurrentDivision { get; set; }

        // Коллекция с информацией для отчета и вывода в визуалку
        public ObservableCollection<EquipmentsDTO> EquipmentDTO { get; set; }
        public EquipmentsDTO CurrentEquipmentDTO { get; set; }
      
        // Ссылки на репозитории
        RepositoryEquipment repositoryEquipment = new ();

        RepositoryDivisions repositoryDivisions = new ();

        // Конструктор
        public EquipmentViewModel()
        {
            Equipments = repositoryEquipment.GetEquipment();
            Divisions = new ObservableCollection<Division>(repositoryDivisions.GetDivisions());

            EquipmentDTO = new();

            FormatEditDto(Equipments);
        }

        /// <summary>
        /// Редактирование оборудования
        /// </summary>
        public ICommand EditCommand => new SimpleCommand(() =>
        {

            ShowEditWindow(FormatEdit(CurrentEquipmentDTO), Divisions);

            // Обновление визуалки
            Equipments.Clear();
            Equipments = repositoryEquipment.GetEquipment();
            FormatEditDto(Equipments);

        }, (_) => CurrentEquipmentDTO != null);

        /// <summary>
        /// Дбавление оборудования
        /// </summary>
        public ICommand AddCommand => new SimpleCommand(() =>
        {
            ShowEditWindow(null, Divisions);

            // Обновление визуалки
            Equipments = repositoryEquipment.GetEquipment();
            FormatEditDto(Equipments);

        });

        /// <summary>
        /// Удаление оборудования
        /// </summary>
        public ICommand DeleteCommand => new SimpleCommand(() =>
        {
            repositoryEquipment.RemoveEquipment(FormatEdit(CurrentEquipmentDTO));

            // Обновление визуалки
            Equipments = repositoryEquipment.GetEquipment();
            FormatEditDto(Equipments);

        }, (_) => CurrentEquipmentDTO != null);


        /// <summary>
        /// Создание отчета
        /// </summary>
        public ICommand CreatingReportCommand => new SimpleCommand(() =>
        {
            string filePath = ShowFolderDialog();
            if (!string.IsNullOrEmpty(filePath))
            {
                
                CreateExcelFile(filePath, EquipmentDTO);
            }
        });

        /// <summary>
        /// Создание Файла Exel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="equipmentDTOs"></param>
        public void CreateExcelFile(string filePath, ObservableCollection<EquipmentsDTO> equipmentDTOs)
        {
            // Создать новый экземпляр Excel
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

            // Добавить новую рабочую книгу
            Workbook workbook = excelApp.Workbooks.Add();

            // Получить первую рабочую книгу
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            // Заголовки столбцов
            worksheet.Cells[1, 1] = "id";
            worksheet.Cells[1, 2] = "Название";
            worksheet.Cells[1, 3] = "Подразделение";
            worksheet.Cells[1, 4] = "Дата сдачи в эксплуатацию";
            worksheet.Cells[1, 5] = "Количество";

            // Добавить данные
            int rowIndex = 2;
            foreach (var equipmentDTO in equipmentDTOs)
            {
                worksheet.Cells[rowIndex, 1] = equipmentDTO.Id;
                worksheet.Cells[rowIndex, 2] = equipmentDTO.Title;
                worksheet.Cells[rowIndex, 3] = equipmentDTO.DivisionTitle;
                worksheet.Cells[rowIndex, 4] = equipmentDTO.CommissioningDate.ToShortDateString();
                worksheet.Cells[rowIndex, 5] = equipmentDTO.Quantity;
                rowIndex++;
            }

            // Установка ширины столбцов на основе ширины заголовков
            for (int i = 1; i <= 5; i++)
            {
                Microsoft.Office.Interop.Excel.Range columnRange = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i];
                columnRange.EntireColumn.AutoFit();
            }

            // Имя файла
            string fileName = $"Отчет_{DateTime.Now.ToString("dd.MM.yy")}.xlsx";

            string fullFilePath = Path.Combine(filePath, fileName);

            // Сохранить файл
            workbook.SaveAs(fullFilePath);

            // Закрыть и освободить ресурсы
            workbook.Close();
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            // Вызов информационного окна с путем к созданному отчету
            ShowInformationDialog("Отчет создан: " + fullFilePath);
        }

        /// <summary>
        /// Всплывающее окно для указания пути сохранения отчета
        /// </summary>
        /// <returns></returns>
        public string ShowFolderDialog()
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Выберите папку для сохранения";
            dialog.UseDescriptionForTitle = true;

            if (dialog.ShowDialog() == true)
            {
                return dialog.SelectedPath;
            }

            return string.Empty;
        }

        /// <summary>
        /// Информационное окно
        /// </summary>
        /// <param name="message"></param>
        public void ShowInformationDialog(string message) 
        { System.Windows.MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information); }

        /// <summary>
        /// Создание окна для редактировния оборудования
        /// </summary>
        /// <param name="division"></param>
        private void ShowEditWindow(Equipment? currentEquipment, ObservableCollection<Division> divisions)
        {
            var win = new System.Windows.Window() { Width = 670, Height = 260 };
            win.Content = new EquipmentEditViewModel(divisions, currentEquipment) { CloseAction = () => win.Close() };
            win.ShowDialog();
        }

        /// <summary>
        /// Переформатируем CurrentEquipmentDTO в CurrentEquipment
        /// </summary>
        /// <param name="currentEquipmentDTO"></param>
        /// <returns></returns>
        private Equipment FormatEdit(EquipmentsDTO currentEquipmentDTO) 
        {
            Equipment CurrentEquipment = new()
            {
                Id = currentEquipmentDTO.Id,
                Title = currentEquipmentDTO.Title,
                DivisionId = Divisions.FirstOrDefault(x => x.DivisionTitle == currentEquipmentDTO.DivisionTitle).Id,
                CommissioningDate = currentEquipmentDTO.CommissioningDate,
                Quantity = currentEquipmentDTO.Quantity
            };

            return CurrentEquipment;
        }

        /// <summary>
        /// Заполняет EquipmentDTO
        /// </summary>
        /// <param name="equipments"></param>
        private void FormatEditDto(ObservableCollection<Equipment> equipments) 
        {
            EquipmentDTO.Clear();
            foreach (var Equipment in equipments)
            {
                EquipmentDTO.Add(new EquipmentsDTO
                {
                    Id = Equipment.Id,
                    Title = Equipment.Title,
                    DivisionTitle = Divisions.FirstOrDefault(x => x.Id == Equipment.DivisionId)?.DivisionTitle,
                    CommissioningDate = Equipment.CommissioningDate,
                    Quantity = Equipment.Quantity
                });
            }
        }
    }
}
