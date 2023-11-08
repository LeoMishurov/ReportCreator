using ReportCreator.DataAccess;
using ReportCreator.DataAccess.Models;
using System.Collections.ObjectModel;

namespace ReportCreator.DataAccess.Repositories
{
    public class RepositoryDivisions
    {
        MyContext myContext = new();

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="jobTitle"></param>
        /// <param name="department"></param>
        public void AddDivision(Division division)
        {
            myContext.Divisions.Add(division);

            myContext.SaveChanges();
        }

        /// <summary>
        /// Редактирование подразделения
        /// </summary>
        /// <param name="id"></param>
        /// <param name="divisionsTitle"></param>
        public void UpdateDivisions(Division division)
        {
            myContext.Divisions.Update(division);
            myContext.SaveChanges();
        }

        /// <summary>
        /// Получение списка подразделений
        /// </summary>
        /// <returns></returns>
        public List<Division> GetDivisions() 
        {
            return myContext.Divisions.ToList();
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="division"></param>
        public void RemoveDivision(Division division) 
        {
            myContext.Divisions.Remove(division);
            myContext.SaveChanges();
        }
    }
}
