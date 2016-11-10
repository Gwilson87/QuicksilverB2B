﻿using System;
using System.Web.Mvc;
using EPiServer.Reference.Commerce.Site.B2B.Models.Pages;
using EPiServer.Reference.Commerce.Site.B2B.Models.ViewModels;
using EPiServer.Reference.Commerce.Site.B2B.ServiceContracts;
using EPiServer.Reference.Commerce.Site.Features.Organization.ViewModels;
using EPiServer.Reference.Commerce.Site.Infrastructure.Attributes;
using EPiServer.Web.Mvc;

namespace EPiServer.Reference.Commerce.Site.Features.Organization.Controllers
{
    [Authorize]
    public class OrganizationPageController : PageController<OrganizationPage>
    {
        private readonly IOrganizationService _organizationService;
        private readonly IAddressService _addressService;
        public OrganizationPageController(IOrganizationService organizationService, IAddressService addressService)
        {
            _organizationService = organizationService;
            _addressService = addressService;
        }

        public ActionResult Index(OrganizationPage currentPage)
        {
            if (Request.QueryString["showForm"] != null && bool.Parse(Request.QueryString["showForm"]))
            {
                return RedirectToAction("Edit");
            }
            var viewModel = new OrganizationPageViewModel
            {
                CurrentPage = currentPage,
                Organization = _organizationService.GetCurrentUserOrganization()
            };
            if (viewModel.Organization?.Address != null)
            {
                viewModel.Organization.Address.CountryName = _addressService.GetCountryNameByCode(viewModel.Organization.Address.CountryCode);
            }
            else
            {
                if (viewModel.Organization != null) viewModel.Organization.Address = new B2BAddressViewModel();
            }
            if (viewModel.Organization == null)
            {
                return RedirectToAction("Edit");
            }

            return View(viewModel);
        }
        public ActionResult Edit(OrganizationPage currentPage, string organizationId)
        {
            OrganizationPageViewModel viewModel = new OrganizationPageViewModel
            {
                Organization = _organizationService.GetCurrentUserOrganization() ?? new OrganizationModel(),
                CurrentPage = currentPage
            };
            if (viewModel.Organization?.Address != null)
            {
                viewModel.Organization.Address.CountryOptions = _addressService.GetAllCountries();
            }
            else
            {
                if (viewModel.Organization != null)
                {
                    viewModel.Organization.Address = new B2BAddressViewModel
                    {
                        CountryOptions = _addressService.GetAllCountries()
                    };
                }
            }
            return View(viewModel);
        }
        public ActionResult AddSub(OrganizationPage currentPage)
        {
            var viewModel = new OrganizationPageViewModel { CurrentPage = currentPage };
            return View(viewModel);
        }

        [HttpPost]
        [AllowDBWrite]
        public ActionResult Save(OrganizationPageViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Organization.Name))
            {
                ModelState.AddModelError("Organization.Name", "Organization Name is requried");
            }

            if (viewModel.Organization.OrganizationId == Guid.Empty)
            {
                _organizationService.CreateOrganization(viewModel.Organization);
            }
            else
            {
                _organizationService.UpdateOrganization(viewModel.Organization);
            }
            return RedirectToAction("Index");
        }
    }
}