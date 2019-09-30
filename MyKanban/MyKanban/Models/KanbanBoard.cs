using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyKanban.Models
{
    public class KanbanBoard
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Description { get; set; }

        public virtual ICollection<KanbanItem> Items { get; set; }
    }
}