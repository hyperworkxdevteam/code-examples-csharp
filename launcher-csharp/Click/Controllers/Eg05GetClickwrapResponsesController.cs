﻿using DocuSign.Click.Client;
using DocuSign.Click.Examples;
using DocuSign.CodeExamples.Common;
using DocuSign.CodeExamples.Controllers;
using DocuSign.CodeExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DocuSign.CodeExamples.Click.Controllers
{
    [Area("Click")]
    [Route("[area]/Eg05")]
    public class Eg05GetClickwrapResponsesController : EgController
    {
        public Eg05GetClickwrapResponsesController(
            DSConfiguration dsConfig, 
            IRequestItemsService requestItemsService) 
            : base(dsConfig, requestItemsService)
        {
        }

        public override string EgName => "Eg05";
        protected override void InitializeInternal()
        {
            // Obtain your OAuth token
            var accessToken = RequestItemsService.User.AccessToken;
            var basePath = $"{RequestItemsService.Session.BasePath}/clickapi"; // Base API path
            var accountId = RequestItemsService.Session.AccountId;
            ViewBag.ClickwrapsData = RetrieveClickwraps.GetClickwraps(basePath, accessToken, accountId);
            ViewBag.AccountId = RequestItemsService.Session.AccountId;
        }
    
    [MustAuthenticate]
        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string clickwrapId)
        {

            // Obtain your OAuth token
            var accessToken = RequestItemsService.User.AccessToken;
            var basePath = $"{RequestItemsService.Session.BasePath}/clickapi"; // Base API path

            try
            {
                var accountId = RequestItemsService.Session.AccountId;

                // Call the Click API to get a clickwrap agreements
                var clickWrapAgreements = GetClickwrapResponses.GetAgreements(clickwrapId, basePath, accessToken, accountId);

                // Show results
                ViewBag.h1 = "Get clickwrap responses";
                ViewBag.message = $"Results from the ClickWraps::getClickwrapAgreements method:";
                ViewBag.Locals.Json = JsonConvert.SerializeObject(clickWrapAgreements, Formatting.Indented);

                return View("example_done");
            }
            catch (ApiException apiException)
            {
                ViewBag.errorCode = apiException.ErrorCode;
                ViewBag.errorMessage = apiException.Message;

                return View("Error");
            }
        }
    }
}