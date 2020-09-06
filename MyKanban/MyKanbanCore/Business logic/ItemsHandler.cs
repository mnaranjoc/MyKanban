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

        public void updatePositions(Item updatedItem, Item origItem)
        {
            // Get the items count
            var itemsQty = context.Item
                .Where(s => s.BoardId == updatedItem.BoardId)
                .Where(s => s.ColumnId == updatedItem.ColumnId)
                .Count();

            if (origItem.ColumnId != updatedItem.ColumnId)
            {
                itemsQty++;
            }

            int?[] itemsArray = new int?[itemsQty];
            itemsArray[updatedItem.Position] = updatedItem.ItemId;
            int posi = 0;

            // Get the items (except the updating)
            var items = context.Item
                .AsNoTracking()
                .Where(s => s.BoardId == updatedItem.BoardId)
                .Where(s => s.ColumnId == updatedItem.ColumnId)
                .Where(s => s.ItemId != updatedItem.ItemId)
                .OrderBy(s => s.Position);
            foreach(var item in items)
            {
                if (itemsArray[posi] != null)
                {
                    posi++;
                }
                    
                itemsArray[posi] = item.ItemId;
                posi++;
            }

            // Finally update all positions
            for(int i=0; i<itemsArray.Length; i++)
            {
                var item = context.Item.Find(itemsArray[i]);
                if (item != null && item.Position != i)
                {
                    item.Position = i;
                    context.Update(item);
                    context.SaveChanges();
                }
            }
        }
    }
}
