﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TunisiaMall.Domain.Entities;
using TunisiaMall.Service.Services;
using TunisiaMallWeb.Logic;

namespace TunisiaMallWeb.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (new UserService().Athenticate(username, password))
            {
                var ident = new ClaimsIdentity(
                  new[] { 
              // adding following 2 claim just for supporting default antiforgery provider
              new Claim(ClaimTypes.NameIdentifier, username),
              new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

              new Claim(ClaimTypes.Name,username),

              // optionally you could add roles if any
              new Claim(ClaimTypes.Role, "Customer"),

                  },
                  DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = false }, ident);
                return RedirectToAction("Index", "Home"); // auth succeed 
            }
            // invalid username or password
            ModelState.AddModelError("", "invalid username or password");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        
        public string MySecretAction()
        {
            user user = CurrentUser.get();
            return  "";
        }
    }
}