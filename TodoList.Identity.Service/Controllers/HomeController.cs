﻿using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Identity.Service.Models;

namespace TodoList.Identity.Service.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger _logger;

        public HomeController(IIdentityServerInteractionService interaction,
                                ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //if (_environment.IsDevelopment())
            //{
            //    // only show in development
            //    return View();
            //}

            //_logger.LogInformation("Homepage is disabled in production. Returning 404.");
            //return NotFound();
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;

                //if (!_environment.IsDevelopment())
                //{
                //    // only show in development
                //    message.ErrorDescription = null;
                //}
            }

            return View("Error", vm);
        }
    }
}