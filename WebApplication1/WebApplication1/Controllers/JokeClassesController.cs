using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class JokeClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JokeClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JokeClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.JokeClass.ToListAsync());
        }

        // GET: JokeClasses/ShowSearchForm
        public IActionResult ShowSearchForm()
        {
            return View();
        }

        // POST: JokeClasses/ShowSearchResult
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return View("Index", await _context.JokeClass.Where( j => j.JokeQuestion
            .Contains(SearchPhrase)).ToListAsync());
        }

        // GET: JokeClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeClass = await _context.JokeClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokeClass == null)
            {
                return NotFound();
            }

            return View(jokeClass);
        }

        // GET: JokeClasses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: JokeClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] JokeClass jokeClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jokeClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jokeClass);
        }

        // GET: JokeClasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeClass = await _context.JokeClass.FindAsync(id);
            if (jokeClass == null)
            {
                return NotFound();
            }
            return View(jokeClass);
        }

        // POST: JokeClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JokeQuestion,JokeAnswer")] JokeClass jokeClass)
        {
            if (id != jokeClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jokeClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JokeClassExists(jokeClass.Id))
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
            return View(jokeClass);
        }

        // GET: JokeClasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jokeClass = await _context.JokeClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jokeClass == null)
            {
                return NotFound();
            }

            return View(jokeClass);
        }

        // POST: JokeClasses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jokeClass = await _context.JokeClass.FindAsync(id);
            _context.JokeClass.Remove(jokeClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JokeClassExists(int id)
        {
            return _context.JokeClass.Any(e => e.Id == id);
        }
    }
}
