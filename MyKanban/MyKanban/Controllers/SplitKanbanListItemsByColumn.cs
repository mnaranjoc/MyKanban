using MyKanban.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyKanban.Controllers
{
    public class SplitKanbanListItemsByColumn
    {
        public List<KanbanItem> toDo, inProcess, done;
        IEnumerable<KanbanItem> items = Enumerable.Empty<KanbanItem>();
        int board;

        public SplitKanbanListItemsByColumn(IEnumerable<KanbanItem> items, int board)
        {
            this.items = items;
            this.board = board;

            split();
        }

        private void split()
        {
            toDo = splitList(0);
            inProcess = splitList(1);
            done = splitList(2);
        }

        private List<KanbanItem> splitList(int column)
        {
            List<KanbanItem> ret = new List<KanbanItem>();

            if (items != null && items.Count() > 0)
            {
                ret = items.
                      Where(x => x.ColumnID == column).
                      Where(x => x.BoardID == board).
                      OrderByDescending(x => x.DateCreated).
                      ToList();
            }

            return ret;
        }
    }
}