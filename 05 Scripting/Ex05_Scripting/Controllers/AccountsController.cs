using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ex05_Scripting.Models;

namespace Ex05_Scripting.Controllers
{
    public class AccountsController : Controller
    {
        private readonly WBA2WebContext _context;

        public AccountsController(WBA2WebContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var wBA2WebContext = _context.Account.Include(a => a.AccountGroup).Include(a => a.Function);
            return View(await wBA2WebContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.AccountGroup)
                .Include(a => a.Function)
                .FirstOrDefaultAsync(m => m.Type == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["Type"] = new SelectList(_context.AccountGroup, "Type", "Type");
            ViewData["FunctionType"] = new SelectList(_context.AccountGroup, "Type", "Type");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,SubjectId,FunctionType,FunctionId,Number,Name")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Type"] = new SelectList(_context.AccountGroup, "Type", "Type", account.Type);
            ViewData["FunctionType"] = new SelectList(_context.AccountGroup, "Type", "Type", account.FunctionType);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["Type"] = new SelectList(_context.AccountGroup, "Type", "Type", account.Type);
            ViewData["FunctionType"] = new SelectList(_context.AccountGroup, "Type", "Type", account.FunctionType);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Type,SubjectId,FunctionType,FunctionId,Number,Name")] Account account)
        {
            if (id != account.Type)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Type))
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
            ViewData["Type"] = new SelectList(_context.AccountGroup, "Type", "Type", account.Type);
            ViewData["FunctionType"] = new SelectList(_context.AccountGroup, "Type", "Type", account.FunctionType);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .Include(a => a.AccountGroup)
                .Include(a => a.Function)
                .FirstOrDefaultAsync(m => m.Type == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return _context.Account.Any(e => e.Type == id);
        }
    }
}
