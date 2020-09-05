using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyKanbanCore.Data;
using MyKanbanCore.Models;

namespace MyKanbanCore.Controllers
{
    public class ColumnsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColumnsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Columns
        public async Task<IActionResult> Index(int? id)
        {
            IQueryable<Column> applicationDbContext = _context.Column.Include(c => c.Board);

            if (id != null && id > 0)
            {
                applicationDbContext = applicationDbContext.Where(s => s.BoardId == id);
                ViewBag.BoardId = id;
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Columns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Column
                .Include(c => c.Board)
                .FirstOrDefaultAsync(m => m.ColumnId == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // GET: Columns/Create
        public IActionResult Create(int? id)
        {
            Column newColumn = new Column();

            if (id != null && id > 0)
            {
                var board = _context.Board.Find(id);

                newColumn.BoardId = id;
                newColumn.Board = board;
            }

            return View(newColumn);
        }

        // POST: Columns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColumnId,Description,BoardId")] Column column)
        {
            if (ModelState.IsValid)
            {
                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = column.BoardId });
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", column.BoardId);
            return View(column);
        }

        // GET: Columns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Column.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", column.BoardId);
            return View(column);
        }

        // POST: Columns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColumnId,Description,BoardId")] Column column)
        {
            if (id != column.ColumnId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(column);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnExists(column.ColumnId))
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
            ViewData["BoardId"] = new SelectList(_context.Board, "BoardId", "Name", column.BoardId);
            return View(column);
        }

        // GET: Columns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var column = await _context.Column
                .Include(c => c.Board)
                .FirstOrDefaultAsync(m => m.ColumnId == id);
            if (column == null)
            {
                return NotFound();
            }

            return View(column);
        }

        // POST: Columns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var column = await _context.Column.FindAsync(id);
            _context.Column.Remove(column);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColumnExists(int id)
        {
            return _context.Column.Any(e => e.ColumnId == id);
        }
    }
}
