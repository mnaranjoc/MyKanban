using MyKanbanCore.Data;
using MyKanbanCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKanbanCore.Business_logic
{
    public class BoardHandler
    {
        private readonly ApplicationDbContext context;

        public BoardHandler(ApplicationDbContext _context)
        {
            context = _context;
        }

        public Board init(Board board)
        {
            if (board.Description == null || board.Description.Trim().Equals(Constants.EmptyString))
            {
                board.Description = Constants.NoDescription;
            }

            return board;
        }
    }
}
