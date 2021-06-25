﻿using DocuSign.Admin.Examples;
using DocuSign.CodeExamples.Common;
using DocuSign.CodeExamples.Controllers;
using DocuSign.CodeExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace DocuSign.CodeExamples.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/Eg05")]
    public class Eg05AuditUsersController : EgController
    {
        public Eg05AuditUsersController(
            DSConfiguration dsConfig,
            IRequestItemsService requestItemsService)
            : base(dsConfig, requestItemsService)
        {
        }

        public override string EgName => "Eg05";
        protected override void InitializeInternal()
        {
            ViewBag.AccountId = RequestItemsService.Session.AccountId;
        }


        [MustAuthenticate]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Audit()
        {
            var organizationId = RequestItemsService.OrganizationId;
            var accessToken = RequestItemsService.User.AccessToken;
            var basePath = RequestItemsService.Session.AdminApiBasePath;
            var accountId = RequestItemsService.Session.AccountId;
            var usersData = AuditUsers.GetRecentlyModifiedUsersData(basePath, accessToken, Guid.Parse(accountId), organizationId);
            // Process results
            ViewBag.h1 = "List users audit results";
            ViewBag.message = "Results from the Envelopes::getUsers calls:";
            ViewBag.Locals.Json = JsonConvert.SerializeObject(usersData, Formatting.Indented);
            return View("example_done");
        }
    }
}