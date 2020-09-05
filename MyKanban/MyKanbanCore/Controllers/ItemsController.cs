using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKanbanCore.Models;
using MyKanbanCore.Data;
using MyKanbanCore.Business_logic;

namespace MyKanbanCore.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Item.Include(i => i.Board).Include(i => i.Column);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Board)
                .Include(i => i.Column)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create(int? id)
        {
            var newItem = new Item()
            {
                DateCreated = DateTime.Today
            };

            var column = _context.Column.Include(s => s.Board).Where(s => s.ColumnId == id).FirstOrDefault();
            if (column != null)
            {
                newItem.ColumnId = column.ColumnId;
                newItem.Column = column;
                newItem.BoardId = column.BoardId;
                newItem.Board = column.Board;
            }

            //ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name");
            //ViewData["ColumnId"] = new SelectList(_context.Column, "ColumnId", "Description");
            return View(newItem);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,DateCreated,Description,ColumnId,BoardId,Critical")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                new ItemsHandler(_context).setPositionForNew(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), "Boards", new { id = item.BoardId });
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", item.BoardId);
            ViewData["ColumnId"] = new SelectList(_context.Column, "ColumnId", "Description", item.ColumnId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", item.BoardId);
            ViewData["ColumnId"] = new SelectList(_context.Column, "ColumnId", "Description", item.ColumnId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,DateCreated,Description,ColumnId,BoardId,Critical")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", item.BoardId);
            ViewData["ColumnId"] = new SelectList(_context.Column, "ColumnId", "Description", item.ColumnId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Board)
                .Include(i => i.Column)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }
    }
}
