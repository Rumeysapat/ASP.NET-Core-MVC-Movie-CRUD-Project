using Microsoft.AspNetCore.Mvc;
using DynamicData.Models;
namespace DynamicData.Controllers;


public class UserController : Controller
{
    [HttpGet]
    public IActionResult CreateUser()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CreateUser(UserModel userModel)
    {
        return View();
    }


}