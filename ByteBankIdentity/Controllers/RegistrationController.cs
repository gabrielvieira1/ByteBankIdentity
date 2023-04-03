using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ByteBankIdentity.Data;
using ByteBankIdentity.Models;
using ByteBankIdentity.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Net;
using System.Diagnostics;

namespace ByteBankIdentity.Controllers
{
 public class RegistrationController : Controller
 {
  private readonly ApplicationDbContext _context;

  public RegistrationController(ApplicationDbContext context)
  {
   _context = context;
  }

  // GET: Registration
  public async Task<IActionResult> Index()
  {
   return _context.Users != null ?
               View(await _context.Users.ToListAsync()) :
               Problem("Entity set 'ApplicationDbContext.Users'  is null.");
  }

  // GET: Registration/Details/5
  public async Task<IActionResult> Details(int? id)
  {
   if (id == null || _context.Users == null)
   {
    return NotFound();
   }

   var user = await _context.Users
       .FirstOrDefaultAsync(m => m.Id == id);
   if (user == null)
   {
    return NotFound();
   }

   return View(user);
  }

  // GET: Registration/Create
  public IActionResult Create()
  {
   return View();
  }

  // POST: Registration/Create
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("Name,Email,Password")] User user)
  {
   if (!Utils.String.InputIsValid(user.Name))
    ModelState.AddModelError("name", "Name invalid");

   if (!Utils.String.EmailIsValid(user.Email))
    ModelState.AddModelError("email", "Email invalid");

   if (!Security.PasswordIsValid(user.Password))
    ModelState.AddModelError("password", "Password invalid");

   if (ModelState.IsValid)
   {
    try
    {
     user.Password = Utils.Security.HashPassword(user.Password);
     user.Active = true;

     _context.Add(user);
     await _context.SaveChangesAsync();
     TempData["success"] = "User created successfully";
     return RedirectToAction(nameof(Index));
    }
    catch (DbUpdateConcurrencyException ex)
    {
     return RedirectToAction(nameof(Error), new { message = BadRequest().StatusCode });
    }
   }
   return View();
  }

  // GET: Registration/Login
  public IActionResult Login()
  {
   return View();
  }

  // POST: Registration/Login
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Login(User userModel)
  {
   if (userModel != null)
   {
    try
    {
     if (!Utils.String.EmailIsValid(userModel.Email))
     {
      ModelState.AddModelError("email", "Invalid email");
      return View();
     }
     var email = new SqlParameter("Email", userModel.Email);
     var user = await _context.Users.FromSql($"select * from Users where Email = {email}").FirstOrDefaultAsync();

     if (user == null)
     {
      TempData["error"] = "Login failed: Invalid email or password.";
      return View();
     }

     if (!Security.VerifyHashedPassword(userModel.Password, user.Password))
     {
      TempData["error"] = "Login failed: Invalid email or password.";
      return View();
     }

     TempData["success"] = "Login successfully";
     return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
     return RedirectToAction(nameof(Error), new { message = BadRequest().StatusCode });
    }
   }
   else
   {
    return RedirectToAction(nameof(Error), new { message = BadRequest().StatusCode });
   }
  }

  // GET: Registration/Edit/5
  public async Task<IActionResult> Edit(int? id)
  {
   if (id == null || _context.Users == null)
   {
    return NotFound();
   }

   var user = await _context.Users.FindAsync(id);
   if (user == null)
   {
    return NotFound();
   }
   return View(user);
  }

  // POST: Registration/Edit/5
  // To protect from overposting attacks, enable the specific properties you want to bind to.
  // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,CreatedDateTime,Active")] User user)
  {
   if (id != user.Id)
   {
    return NotFound();
   }

   if (ModelState.IsValid)
   {
    try
    {
     _context.Update(user);
     await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
     if (!UserExists(user.Id))
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
   return View(user);
  }

  // GET: Registration/Delete/5
  public async Task<IActionResult> Delete(int? id)
  {
   if (id == null || _context.Users == null)
   {
    return NotFound();
   }

   var user = await _context.Users
       .FirstOrDefaultAsync(m => m.Id == id);
   if (user == null)
   {
    return NotFound();
   }

   return View(user);
  }

  // POST: Registration/Delete/5
  [HttpPost, ActionName("Delete")]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> DeleteConfirmed(int id)
  {
   if (_context.Users == null)
   {
    return Problem("Entity set 'ApplicationDbContext.Users'  is null.");
   }
   var user = await _context.Users.FindAsync(id);
   if (user != null)
   {
    _context.Users.Remove(user);
   }

   await _context.SaveChangesAsync();
   return RedirectToAction(nameof(Index));
  }

  private bool UserExists(int id)
  {
   return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
  }
  public IActionResult Error(string message)
  {
   var viewModel = new ErrorViewModel
   {
    Message = message,
    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
   };
   return View(viewModel);
  }
 }
}
