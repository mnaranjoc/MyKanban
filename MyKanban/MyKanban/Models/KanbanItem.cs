using System;
using System.ComponentModel.DataAnnotations;

namespace MyKanban.Models
{
    public class KanbanItem
    {
        public int ID { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Column")]
        public int ColumnID { get; set; }

        [Display(Name = "Critical")]
        public Boolean Critical { get; set; }

        [Display(Name = "Board")]
        public int BoardID { get; set; }

        public virtual KanbanBoard Board { get; set; }

        public string daysElapsed()
        {
            if (DateTime.Now.Date <= DateCreated.Date)
            {
                return "created today"; // Days <= 0
            }
            else
            {
                int days = (int)Math.Round((DateTime.Now - DateCreated).TotalDays);

                if (days == 1)
                    return String.Format("created {0} day ago", days); // Days == 0                
                else
                    return String.Format("created {0} days ago", days); // Days > 1
            }
        }

        public string shortDescription()
        {
            if (Description.Length >= 25)
            {
                return String.Format("{0}...", Description.Substring(0, 25));
            }
            else
            {
                return Description;
            }
        }
    }
}