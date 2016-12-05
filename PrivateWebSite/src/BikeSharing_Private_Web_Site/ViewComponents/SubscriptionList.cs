﻿using BikeSharing_Private_Web_Site.Models.SubscriptionViewModels;
using BikeSharing_Private_Web_Site.Services;
using BikeSharing_Private_Web_Site.Services.Pagination;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeSharing_Private_Web_Site.Views.Shared.Components
{
    public class SubscriptionList:ViewComponent
    {
        private readonly ISubscriptionService _svc;

        public SubscriptionList(ISubscriptionService svc)
        {
            _svc = svc;
        }

        public async Task<IViewComponentResult> InvokeAsync(IndexViewModel vm)
        {
            var data = await _svc.GetDataAsync(vm.PaginationInfo.ActualPage, vm.PaginationInfo.ItemsPerPage);

            vm.Subscriptions = data;
            vm.PaginationInfo.TotalItems = _svc.TotalItems;
            decimal d = ((decimal)_svc.TotalItems / vm.PaginationInfo.ItemsPerPage);
            vm.PaginationInfo.TotalPages = d > (int)d  ? (int)d + 1 : (int)d;
            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
            vm.PaginationInfo.ItemsPerPage = vm.Subscriptions.Count();

            return View(vm);
        }
    }
}
