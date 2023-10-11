using BasherBlog.Data;
using BasherBlog.Models;
using BasherBlog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasherBlog.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        //---------------USER  roles  
        [Admin]
        [HttpGet]
        public IActionResult UserRoles()
        {
            return View(_user.GetRoles());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateRole(int id=0)
        {
            return View(_user.GetRole(id));
        }
        //----------------UserRole
        [Admin]
        [HttpPost]

        public IActionResult AddUpdateRole(UserRole userRole)
        {
            _user.AddUpdateRole(userRole);
            return RedirectToAction("UserRoles");
        }

        [HttpGet]
        public IActionResult DeleteRole(int id)
        {
            _user.DeleteRole(id);
            return RedirectToAction("UserRoles");
        }
        //----------USERS-----------
        [Admin]
        [HttpGet]
        public IActionResult Users() {

            return View(_user.GetUsers());
        }
        //---------Edit User

        [Admin]
        [HttpGet]
        public IActionResult AddUpdateUser(int id=0)
        {
            if (id==0)
            {
                return RedirectToAction("Register", "Account");


            }
            ViewBag.AllRoles = new SelectList(_user.GetRolesList().ToList(), "Id", "Name");
            return View(_user.GetUser(id));
        }
        //------------AddUpdateUser
        [Admin]
        [HttpPost]

        public IActionResult AddUpdateUser(User user)
        {
            _user.AddUpdateUser(user);
            return RedirectToAction("Users");

        }

        //---------Dlt User
        public IActionResult DeleteUser(int id)
        {
            _user.DeleteUser(id);
            return RedirectToAction("Users");

        }
    }
}
