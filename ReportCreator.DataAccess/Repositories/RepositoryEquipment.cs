using Microsoft.EntityFrameworkCore;
using ReportCreator.DataAccess;
using ReportCreator.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ReportCreator.DataAccess.Repositories
{
    public class RepositoryEquipment
    {
        MyContext myContext = new();

        /// <summary>
        /// Добавление оборудования
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="jobTitle"></param>
        /// <param name="department"></param>
        public void AddEquipment(Equipment equipment)
        {
            myContext.Equipment.Add(equipment);

            myContext.SaveChanges();
        }

        
        /// <summary>
        /// Редактирование оборудования
        /// </summary>
        /// <param name="equipment"></param>
        public void UpdateEquipment(Equipment equipment)
        {            
            myContext.Equipment.Update(equipment);
            myContext.SaveChanges();
        }

        /// <summary>
        /// Получение всех сторок из таблицы
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Equipment> GetEquipment() 
        {
            ObservableCollection<Equipment> equipmentCollection = new (myContext.Equipment);

            return equipmentCollection;
        }

        /// <summary>
        /// Удаление оборудования
        /// </summary>
        /// <param name="equipment"></param>
        public void RemoveEquipment(Equipment equipment)
        {
            //Equipment equipmentDelet = myContext.Equipment.FirstOrDefault(x => x.Id == equipment.Id);
            myContext.Equipment.Remove(equipment);
            myContext.SaveChanges();
        }
    }
}
