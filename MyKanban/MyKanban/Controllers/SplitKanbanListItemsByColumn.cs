using MyKanban.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyKanban.Controllers
{
    public class SplitKanbanListItemsByColumn
    {
        IEnumerable<KanbanItem> items = Enumerable.Empty<KanbanItem>();
        public List<KanbanItem> toDo, inProcess, done;
        ApplicationDbContext db = new ApplicationDbContext();
        public string boardName;
        int board;

        public SplitKanbanListItemsByColumn(int? board)
        {
            int boardId = (board == null) ? 0 : (int)board;

            var items = db.KanbanItems.Where(x => x.BoardID == boardId).ToList();

            this.items = items;
            this.board = boardId;

            split();

            var boards = db.KanbanBoards.Where(x => x.ID == boardId).ToList();

            if (boards != null && boards.Count > 0)
            {
                boardName = boards.FirstOrDefault().Description;
            }
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