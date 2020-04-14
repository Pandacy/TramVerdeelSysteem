﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TramVerdeelSysteem.Models;
using Model.ViewModels;
using Logic;

namespace TramVerdeelSysteem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LoginViewModel user = new LoginViewModel();
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            try
            {
               
                Authentication authentication = new Authentication();
                authentication.Login(user.name, user.password);
                return RedirectToAction("Index", "Home");

                //if(user.name == "admin" && user.password == "admin")
                //{

                //    return RedirectToAction("Index", "Remise");
                //    //backend aanroepen
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
