using Microsoft.EntityFrameworkCore;
using MyKanbanCore.Data;
using MyKanbanCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKanbanCore.Business_logic
{
    public class ItemsHandler
    {
        private readonly ApplicationDbContext context;

        public ItemsHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public void setPositionForNew(Item item)
        {
            var nextOrderNo = context.Item
                .AsNoTracking()
                .Where(s => s.BoardId == item.BoardId)
                .Where(s => s.ColumnId == item.ColumnId)
                .OrderByDescending(s => s.Position)
                .FirstOrDefault();

            if (nextOrderNo != null)
            {
                item.Position = nextOrderNo.Position + 1;
            }
        }
    }
}
