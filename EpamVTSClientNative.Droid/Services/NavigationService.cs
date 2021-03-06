﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClientNative.Droid.Activities;
using Microsoft.Practices.Unity;
using Plugin.CurrentActivity;

namespace EpamVTSClientNative.Droid.Services
{
    public class NavigationService : INavigationService
    {

        public static readonly IReadOnlyDictionary<Type, Type> ViewModelPageContainer =
            new Dictionary<Type, Type>()
        {
            {
                    typeof(VacationListViewModel), typeof(VacationListActivity)
            },
            {
                    typeof(VacationViewModel), typeof(VacationActivity)
            },
            {
                    typeof(EditVacationViewModel), typeof(EditVacationActivity)
            }
        };

        public Task NavigateToAsync<TViewModelTo>(string args) where TViewModelTo : ViewModelBase
        {
            Type viewType;
            if (ViewModelPageContainer.TryGetValue(typeof(TViewModelTo), out viewType))
            {
                Factory.UnityContainer.Resolve<TViewModelTo>();
                var currentActivity = CrossCurrentActivity.Current.Activity;

                var intent = new Intent(currentActivity, viewType);
                intent.PutExtra("args", args);
                currentActivity.StartActivity(intent);
            }
            return Task.FromResult(true);
        }
    }
}
