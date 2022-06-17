using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteStockManagement.Models;

namespace WebsiteStockManagement.Controllers
{
    public class ProductsController : Controller
    {
       
        private readonly StockManagementWesbiteDBContext _context;

        public static string selectedFarmer= "";
       
        public ProductsController(StockManagementWesbiteDBContext context)
        {
            _context = context;
           
        }

    // GET: Products
    public async Task<IActionResult> Index(string searchString, DateTime searchDate, DateTime searchDate1, string ButtonSearch, string ButtonDate, string SearchFarmer, string ButtonFarmer)
    {

            //If user is not logged in they cannot access the views via the URL path
            if (UsersController.usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            //Creating viewbags under the logged in role in order to hide certain menu items
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }


            if (UsersController.userRole.Equals("Employee"))
            {
                //Creating a viewbag of all the farmers 
                ViewBag.Farmers = new SelectList(_context.Users.Where(x => x.UsersRole.Equals("Farmer")).Select(s => s.UsersFirstname));

                //This if statement executes when the search button for farmer is clicked 
                if (!string.IsNullOrEmpty(ButtonFarmer))
                {
                    //Setting the selected farmer to a local variable
                    selectedFarmer = SearchFarmer;

                    //Displaying a message of the currently selected farmer 
                    ViewBag.MsgSelectedFarmer ="The Current Selected Farmer " + selectedFarmer;

                    //Getting the value of the selected farmer
                    ViewData["CurrentFarmer"] = SearchFarmer;

                    //Using linq in the context in order to display the correct values 
                    var ST10116273_FarmCentralContext3 = _context.Products.Include(p => p.Users).Where(s => s.Users.UsersFirstname.Equals(SearchFarmer));
                    return View(await ST10116273_FarmCentralContext3.ToListAsync());


                }
                else if (!string.IsNullOrEmpty(ButtonSearch))
                {
                    //Displaying a message of the currently selected farmer 
                    ViewBag.MsgSelectedFarmer = "The Current Selected Farmer " + selectedFarmer;

                    //Getting the value of the selected farmer
                    ViewData["CurrentFilter"] = searchString;

                    //Using linq in the context in order to display the correct values 
                    var ST10116273_FarmCentralContext1 = _context.Products.Include(p => p.Users).Where(s => selectedFarmer.Equals("")? s.ProductType.Contains(searchString) : s.Users.UsersFirstname.Equals(selectedFarmer) && s.ProductType.Contains(searchString));
                    return View(await ST10116273_FarmCentralContext1.ToListAsync());


                }
                else if (!string.IsNullOrEmpty(ButtonDate))
                {
                    //Displaying a message of the currently selected farmer 
                    ViewBag.MsgSelectedFarmer = "The Current Selected Farmer " + selectedFarmer;

                    //Getting the value of the selected farmer
                    ViewData["CurrentDate"] = searchDate;
                    ViewData["CurrentDate1"] = searchDate1;

                    //Using linq in the context in order to display the correct values 
                    var ST10116273_FarmCentralContext2 = _context.Products.Include(p => p.Users).Where(s => selectedFarmer.Equals("") ? s.ProductDaterange >= searchDate && s.ProductDaterange <= searchDate1 : s.Users.UsersFirstname.Equals(selectedFarmer) && s.ProductDaterange >= searchDate && s.ProductDaterange <= searchDate1);
                    return View(await ST10116273_FarmCentralContext2.ToListAsync());


                }

                //This view is refreshed after click of a button 
                var ST10116273_FarmCentralContext = _context.Products.Include(p => p.Users);
                return View(await ST10116273_FarmCentralContext.ToListAsync());
            }



            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.RemoveDelete = 0;

                if (!String.IsNullOrEmpty(ButtonSearch))
                {
                    //Getting the value of the selected farmer
                    ViewData["CurrentFilter"] = searchString;

                    //Using linq in the context in order to display the correct values 
                    var ST10116273_FarmCentralContext1 = _context.Products.Include(p => p.Users).Where(x => x.UsersId == UsersController.usernameId && x.ProductType.Contains(searchString));
                    return View(await ST10116273_FarmCentralContext1.ToListAsync());

                }
                else if (!string.IsNullOrEmpty(ButtonDate))
                {
                    //Getting the value of the selected farmer
                    ViewData["CurrentDate"] = searchDate;
                    ViewData["CurrentDate1"] = searchDate1;

                    //Using linq in the context in order to display the correct values 
                    var ST10116273_FarmCentralContext2 = _context.Products.Include(p => p.Users).Where(s => s.UsersId == UsersController.usernameId && s.ProductDaterange >= searchDate && s.ProductDaterange <= searchDate1);
                    return View(await ST10116273_FarmCentralContext2.ToListAsync());

                }
                
                    var ST10116273_FarmCentralContext = _context.Products.Include(p => p.Users).Where(x => x.UsersId == UsersController.usernameId);
                    return View(await ST10116273_FarmCentralContext.ToListAsync());

            }

            return View();
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (UsersController.usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Users)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
           
            if (UsersController.usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
                return RedirectToAction("Index", "Products");
            }


            ViewData["UsersId"] = new SelectList(UsersController.userList.Select(x=>x.UsersId));
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductType,ProductPrice,ProductDaterange,UsersId")] Product product)
        {
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
                return RedirectToAction("Index", "Products");
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsersId"] = new SelectList(_context.Users, "UsersId", "UsersFirstname", product.UsersId);
            return View(product);
        }

      

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (UsersController.usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewData["UsersId"] = new SelectList(_context.Users.Where(x => x.UsersId.Equals(UsersController.usernameId)).Select(s => s.UsersId));
            }else if (UsersController.userRole.Equals("Employee"))
            {
                ViewData["UsersId"] = new SelectList(_context.Products.Where(x => x.ProductId.Equals(id)).Select(s => s.UsersId));
            }
            
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductType,ProductPrice,ProductDaterange,UsersId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
           
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (UsersController.usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            else if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 1;
                return RedirectToAction("Index", "Products");
            }
            if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Users)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);

        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
