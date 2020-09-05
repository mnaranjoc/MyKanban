using MyKanbanCore.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKanbanCore.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Created date")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [ForeignKey("Column")]
        [Display(Name = "Column")]
        public int? ColumnId { get; set; }
        [Display(Name = "Column")]
        public virtual Column Column { get; set; }

        [ForeignKey("Board")]
        [Display(Name = "Board")]
        public int? BoardId { get; set; }
        [Display(Name = "Board")]
        public virtual Board Board { get; set; }

        [Display(Name = "Critical")]
        public bool Critical { get; set; }

        [Display(Name = "Position")]
        public int Position { get; set; }

        //public string daysElapsed()
        //{
        //    if (DateTime.Now.Date <= DateCreated.Date)
        //    {
        //        return "created today"; // Days <= 0
        //    }
        //    else
        //    {
        //        int days = (int)Math.Round((DateTime.Now - DateCreated).TotalDays);

        //        if (days == 1)
        //            return String.Format("created {0} day ago", days); // Days == 0                
        //        else
        //            return String.Format("created {0} days ago", days); // Days > 1
        //    }
        //}

        //public string shortDescription()
        //{
        //    if (Description.Length >= 25)
        //    {
        //        return String.Format("{0}...", Description.Substring(0, 25));
        //    }
        //    else
        //    {
        //        return Description;
        //    }
        //}
    }
}