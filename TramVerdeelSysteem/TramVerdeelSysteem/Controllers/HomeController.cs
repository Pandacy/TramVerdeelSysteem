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
                AuthView auth = new AuthView();
                Authentication authentication = new Authentication();
                auth = authentication.Login(user.name, user.password);
                if(auth.Key != null || auth.Key != "")
                {
                    HttpContext.Response.Cookies.Append("key", auth.Key);
                    return RedirectToAction("Index","Depot");
                } else return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
