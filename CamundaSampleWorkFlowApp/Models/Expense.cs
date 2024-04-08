using System.ComponentModel.DataAnnotations;

namespace CamundaSampleWorkFlowApp.Models
{
    public class Expense
    {

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string email {  get; set; }

        [Required]
        public string deparment {  get; set; }

        public string supervisorEmail {  get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime expenseDate { get; set; }

        [Required]
        public ExpenseType expenseType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime travelStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime travelEnd { get; set; }
        
        public string travelLocation { get; set; }

        public string expensePurpose { get; set; }

        [Required]
        public decimal expenseAmount { get; set; }
    }
}
