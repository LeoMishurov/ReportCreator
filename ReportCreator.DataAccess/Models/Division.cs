namespace ReportCreator.DataAccess.Models
{
    public class Division
    {
        public int Id { get; set; }
        public string DivisionTitle { get; set; }

        // Ссылка на User для EF
        public List<Equipment> Equipment { get; set; }
    }
}
