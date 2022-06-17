using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebsiteStockManagement.Models;
using WebsiteStockManagement.OOP;

namespace WebsiteStockManagement.Controllers
{
    public class UsersController : Controller
    {
        //Creating 5 static variables that can be used throughout the application
        public static int usernameId = 0;
        public static int check;
        public static string userPassword;
        public static string userName;
        public static string userRole;
        
        //Created 2 static lists that are able to be called throughout the application 
        public static List<Product> productList = new List<Product>();
        public static List<User> userList = new List<User>();


        StockManagementWesbiteDBContext db = new StockManagementWesbiteDBContext();
        private readonly StockManagementWesbiteDBContext _context;

        public UsersController(StockManagementWesbiteDBContext context)
        {
            _context = context;
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            //If user is not logged in they cannot access the views via the URL path
            if (usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            //The farmer should not be able to access the create farmer page at all , thus if the farmer tries to access the create page via url path he cant 
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
                return RedirectToAction("Index", "Home");
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }

            //Populating a selectList for when the employee creates a farmer
            List<SelectListItem> roles = new()
            {
                new SelectListItem { Value = "Farmer", Text = "Farmer" },
                new SelectListItem { Value = "Employee", Text = "Employee" }
            };
            ViewBag.Role = roles;
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string UsersFirstName, string UsersSurname, string UsersPassword, string UsersRole)
        {
            //Redirect the user if he tries to access create view as a farmer
            if (UsersController.userRole.Equals("Farmer"))
            {
                return RedirectToAction("Index", "Home");
            }

            //Passing through the parameter variables into the obkect of user for OOP standards 
            User u = new User(UsersFirstName, UsersSurname, UsersPassword, UsersRole);

            if (ModelState.IsValid)
            {
                _context.Add(u);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UsersFirstname, string UsersPassword)
        {
            try
            {
                if (UsersController.usernameId == 0)
                {
                    RedirectToAction("Login", "Users");
                }
                List<User> userCheck = new List<User>();

                //This linq query allows the user to eneter the website when both the password and username match the database 
                userCheck = _context.Users.Where(x => x.UsersFirstname.Equals(UsersFirstname) && x.UsersPassword.Equals(UsersPassword)).ToList();

                //If this if statement is true it runs the code bellow.
                if (userCheck[0].UsersFirstname.Equals(UsersFirstname) && userCheck[0].UsersPassword.Equals(UsersPassword))
                {
                    check = 1;

                    //Setting a local variable to the users input 
                    userList = _context.Users.Where(x => x.UsersFirstname.Equals(UsersFirstname) && x.UsersPassword.Equals(UsersPassword)).ToList();

                    //Setting username ID to the entered user ID
                    usernameId = userList[0].UsersId;
                    userRole = userList[0].UsersRole;
                    

                    //Poplulating the lists with the unique user who access the webiste 
                    productList = _context.Products.Where(x => x.UsersId == (usernameId)).ToList();

                    ////Adding data to the suer class of my library and adding it to the list 
                    Products product;
                    Users user;


                    //This foreach adds all the data related to the module through an object and adds it to the moduleList 
                    foreach (Product item in productList)
                    {
                        product = new Products(item.ProductType, item.ProductPrice, item.ProductDaterange);
                        ListHandler.pList.Add(product);
                    }

                    user = new Users(userList[0].UsersId, userList[0].UsersFirstname, userList[0].UsersSurname, userList[0].UsersPassword,userList[0].UsersRole);
                    ListHandler.uList.Add(user);

                    //This redirection runs when the user logs in with the correct details 
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                //If this exception is thrown the check increments and clears the textboxes.
                check++;
                if (check >= 1)
                {
                    ModelState.Clear();
                    ViewBag.MessageAlert = "Invaild Credentials:" + Environment.NewLine + "Please make sure your username or password is correct";
                }

            }
            return View();

        }

        public IActionResult Logout()
        {
            //When the user logouts out it sets the check to 0 and number of weeks to 0;
            check = 0;

            return RedirectToAction("Login", "Users");
        }



        //GET: Users/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
            //If user is not logged in they cannot access the views via the URL path
            if (usernameId == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            //Redirect the user if he tries to access create view as a farmer
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }
            if (id == null)
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //If user is not logged in they cannot access the views via the URL path
            if (usernameId == 0)
            {
               return RedirectToAction("Login", "Users");
            }
            if (userRole.Equals("Farmer"))
            {
               return RedirectToAction("Index", "Home");
            }

            //Redirect the user if he tries to access create view as a farmer
            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UsersId == id);
            if (user == null)
            {
                return NotFound();
            }

            

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            try
            {
               
                var user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                
            }
            catch (Exception)
            {
               
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UsersId == id);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (usernameId == 0)
            {
                RedirectToAction("Login", "Users");
            }

            if (UsersController.userRole.Equals("Farmer"))
            {
                ViewBag.hideMenuItem = 0;
            }
            else if (UsersController.userRole.Equals("Employee"))
            {
                ViewBag.hideMenuItem = 1;
            }
            
            return View(await _context.Users.Where(x => x.UsersRole.Equals("Farmer")).ToListAsync());
        }

    
    }
}
