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
    public class ERController : Controller
    {
        private readonly WBA2WebContext _context;

        public ERController(WBA2WebContext context)
        {
            _context = context;
        }

        // GET: AccountGroups
        public async Task<IActionResult> Index()
        {
            var tree = _context.AccountGroup
                .Include(a => a.SubGroups)
                .Include(a => a.FunctionAccounts)
                .Where(a => a.Level > 0 && a.Type == "FG")
                .OrderBy(a => a.Id);

            return View("Tree", await tree.ToListAsync());
        }

        public async Task<IActionResult> _TreeAccounts(string id)
        {
            var group = _context.AccountGroup
                .Include(g => g.FunctionAccounts)
                .Where(a => a.Id == id);

            return PartialView(await group.SingleAsync());
        }

        public async Task<IActionResult> _Subjects()
        {
            var groups = _context.AccountGroup
                .Where(a => a.Level >= 4 && a.Type == "ER")
                .OrderBy(a => a.Id)
                .Select(a => new { 
                    id = a.Id,
                    name = a.Name 
                });

            return Json(await groups.ToListAsync());
        }

        public IActionResult _Create(string id)
        {
            var account = new Account 
            { 
                FunctionId = id,
                SubjectId = String.Empty,
                Number = 0,
                FunctionType = "FG", 
                Type = "ER"
            };

            ViewBag.IsNew = true;

            return PartialView("_AccountEdit", account);
        }

        public IActionResult _Edit(string id)
        {
            var ids = id.Split('.');
            var account = _context.Account
                .Where(a => a.FunctionId == ids[0] && a.SubjectId == ids[1] && a.Number == Int32.Parse(ids[2]))
                .SingleOrDefault();

            ViewBag.IsNew = false;

            return PartialView("_AccountEdit", account);
        }

        [HttpPost]
        public IActionResult _Submit(Account account, [FromQuery]string isNew, [FromQuery]string oldId)
        {
            if (ModelState.IsValid)
            {
                if (!isNew.ToLower().Equals("true"))
                {
                    var ids = oldId.Split('.');
                    var acc = _context.Account
                        .Where(a => a.FunctionId == ids[0] && a.SubjectId == ids[1] && a.Number == Int32.Parse(ids[2]))
                        .FirstOrDefault();
                    if (acc == null)
                        ModelState.AddModelError(null, "Account not found!");
                    else
                    {
                        try
                        {
                            acc.SubjectId = account.SubjectId;
                            acc.Name = account.Name;
                            acc.Number = account.Number;
                            _context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(String.Empty, ex.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        _context.Account.Add(account);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, ex.Message);
                    }
                }
            }

            ViewBag.IsNew = isNew;

            if (ModelState.IsValid)
                return PartialView("_SubmitOk");
            else
                return PartialView("_AccountEdit", account);
        }
    }
}
