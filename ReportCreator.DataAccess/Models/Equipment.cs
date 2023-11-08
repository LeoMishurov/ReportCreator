using Microsoft.VisualBasic;

namespace ReportCreator.DataAccess.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DivisionId { get; set; }
        public DateTime CommissioningDate { get; set; } 
        public int Quantity { get; set; }

        // Ссылка на Card для EF
        public Division Division { get; set; }

    }
}
