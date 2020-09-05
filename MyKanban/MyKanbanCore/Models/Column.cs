using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKanbanCore.Models
{
    public class Column
    {
        [Key]
        public int ColumnId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Description { get; set; }

        [ForeignKey("Board")]
        [Display(Name = "Board")]
        public int? BoardId { get; set; }
        [Display(Name = "Board")]
        public virtual Board Board { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
