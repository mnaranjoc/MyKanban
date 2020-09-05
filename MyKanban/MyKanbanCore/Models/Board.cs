using MyKanbanCore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyKanbanCore.Models
{
    public class Board
    {
        [Key]
        public int BoardId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
    }
}