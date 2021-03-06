using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Android.App;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClientNative.Droid.Helpers;
using Microsoft.Practices.Unity;
using DatePicker = Android.Widget.DatePicker;
using View = Android.Views.View;

namespace EpamVTSClientNative.Droid.Activities.Extensions
{
    public static class ActivityExtensions
    {
        public static void BindCommand(this Activity activity, int textViewId, ICommand command)
        {
            View view = activity.FindViewById(textViewId);
            if (view == null)
            {
                throw new ArgumentException(nameof(textViewId));
            }
            view.Enabled = command.CanExecute(null);
            view.Click += (sender, args) =>
            {
                if (command.CanExecute(null))
                {
                    command.Execute(null);
                }

            };
            command.CanExecuteChanged += (sender, args) =>
            {
                view.Enabled = command.CanExecute(null);
            };
        }

        public static void BindDatePicker<TViewModel>(this Activity activity, int datePickerId, TViewModel viewModel, Expression<Func<TViewModel, DateTime>> propertyExpression)
            where TViewModel : INotifyPropertyChanged
        {
            var datePicker = activity.FindViewById<DatePicker>(datePickerId);
            if (datePicker == null)
            {
                throw new ArgumentException(nameof(datePickerId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, DateTime> propertyGetter = propertyExpression.Compile();

            DateTime propertyValue = propertyGetter(viewModel);
            datePicker.MinDate = (long)new DateTime(0001, 1, 1).TimeOfDay.TotalMilliseconds;
            datePicker.DateTime = propertyValue;

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    DateTime propertyValue2 = propertyGetter(viewModel);
                    datePicker.DateTime = propertyValue2;
                }
            };

            datePicker.Init(propertyValue.Year, propertyValue.Month, propertyValue.Day, new DateChangedListener((picker, year, month, day) =>
            {
                object convertedValue = Convert.ChangeType(datePicker.DateTime, typeof(DateTime));
                viewModel.GetType().GetProperty(propertyName).SetValue(viewModel, convertedValue);
            }));
        }

        public static void BindLabel(this Activity activity, int textViewId, string label)
        {
            var view = activity.FindViewById<TextView>(textViewId);
            view.Text = label;
        }

        public static void BindNavMenu(this Activity activity, int menuItemId, string label)
        {
            var navigationView = activity.FindViewById<NavigationView>(Resource.Id.nav_view);
            var menu = navigationView.Menu.FindItem(menuItemId);
            menu.SetTitle(label);
        }

        public static void BindHint(this Activity activity, int editTextViewId, string label)
        {
            var view = activity.FindViewById<EditText>(editTextViewId);
            view.Hint = label;
        }

        public static void BindSpinner<TViewModel, TProperty>(this Activity activity, int textViewId, TViewModel viewModel, Expression<Func<TViewModel, TProperty>> propertyExpression, List<TProperty> getValues)
            where TViewModel : INotifyPropertyChanged
        {
            var spinner = activity.FindViewById<Spinner>(textViewId);
            if (spinner == null)
            {
                throw new ArgumentException(nameof(textViewId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, TProperty> propertyGetter = propertyExpression.Compile();

            var localizationService = Factory.UnityContainer.Resolve<ILocalizationService>();

            TProperty propertyValue = propertyGetter(viewModel);
            Dictionary<TProperty, string> dictionary = getValues.ToDictionary(property => property, property => localizationService.Localize(property.ToString()));
            var list = dictionary.Select(r => r.Value).ToList();
            var adapter = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleSpinnerItem, list);

            spinner.Adapter = adapter;
            string vacValue = dictionary[propertyValue];
            int spinnerPosition = adapter.GetPosition(vacValue);
            spinner.SetSelection(spinnerPosition);

            spinner.ItemSelected += (sender, args) =>
            {
                string selected = adapter.GetItem(args.Position);
                var property = dictionary.FirstOrDefault(r => r.Value == selected).Key;
                viewModel.GetType().GetProperty(propertyName).SetValue(viewModel, property);
            };

            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    TProperty propValue = propertyGetter(viewModel);
                    string value = dictionary[propValue];
                    var position = adapter.GetPosition(value);
                    spinner.SetSelection(position);
                }
            };
        }

        public static void BindImageView<TViewModel, TProperty>(this Activity activity, int imageViewId, TViewModel viewModel,
    Expression<Func<TViewModel, TProperty>> propertyExpression)
    where TViewModel : INotifyPropertyChanged
        {
            var imageView = activity.FindViewById<ImageView>(imageViewId);
            if (imageView == null)
            {
                throw new ArgumentException(nameof(imageViewId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, TProperty> propertyGetter = propertyExpression.Compile();

            SetImage(viewModel, propertyGetter, imageView);

            //viewModel to view
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    SetImage(viewModel, propertyGetter, imageView);
                }
            };
        }

        private static void SetImage<TViewModel, TProperty>(TViewModel viewModel, Func<TViewModel, TProperty> propertyGetter, ImageView imageView)
            where TViewModel : INotifyPropertyChanged
        {
            string value = GetPropertyValue(viewModel, propertyGetter);

            if (value != null)
            {
                Stream memoryStream = new MemoryStream(Convert.FromBase64String(value));
                Drawable drawable = Drawable.CreateFromStream(memoryStream, null);
                imageView.SetImageDrawable(drawable);
            }
        }

        public static void BindText<TViewModel, TProperty>(this Activity activity, int textViewId, TViewModel viewModel,
            Expression<Func<TViewModel, TProperty>> propertyExpression)
            where TViewModel : INotifyPropertyChanged
        {
            TextView textView = activity.FindViewById<TextView>(textViewId);
            if (textView == null)
            {
                throw new ArgumentException(nameof(textViewId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, TProperty> propertyGetter = propertyExpression.Compile();

            textView.Text = GetPropertyValue(viewModel, propertyGetter);

            //viewModel to view
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    string value = GetPropertyValue(viewModel, propertyGetter);
                    if (value != textView.Text)
                    {
                        textView.Text = value;
                    }
                }
            };
            //view to viewModel
            textView.TextChanged += (sender, args) =>
            {
                //todo: try catch ignore?
                object convertedValue = Convert.ChangeType(textView.Text, typeof(TProperty));
                viewModel.GetType().GetProperty(propertyName).SetValue(viewModel, convertedValue);
            };
        }

        public static void BindListView<TViewModel>(this Activity activity, int textViewId, TViewModel viewModel, List<VacationViewModel> vacationList)
    where TViewModel : INotifyPropertyChanged
        {
            var listView = activity.FindViewById<ListView>(Resource.Id.VacationListView);
            _listViewAdapter = new VacationListViewAdapter(activity, vacationList);
            listView.Adapter = _listViewAdapter;
            listView.ItemClick += OnListItemClick;
        }
        private static VacationListViewAdapter _listViewAdapter;
        static void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            VacationViewModel item = _listViewAdapter[e.Position];
            item.ViewDetails.Execute(null);
        }

        private static string GetPropertyValue<TViewModel, TProperty>(TViewModel viewModel, Func<TViewModel, TProperty> propertyGetter)
            where TViewModel : INotifyPropertyChanged
        {
            TProperty propertyValue = propertyGetter(viewModel);
            var value = propertyValue?.ToString();
            return value;
        }

        public static void BindVisibility<TViewModel>(this Activity activity, int textViewId, TViewModel viewModel,
           Expression<Func<TViewModel, bool>> propertyExpression)
           where TViewModel : INotifyPropertyChanged
        {
            View view = activity.FindViewById<View>(textViewId);
            if (view == null)
            {
                throw new ArgumentException(nameof(textViewId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, bool> propertyGetter = propertyExpression.Compile();

            //viewModel to view
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    bool propertyValue = propertyGetter(viewModel);
                    view.Visibility = propertyValue ? ViewStates.Visible : ViewStates.Gone;
                }
            };
        }

        private static string GetPropertyName<TViewModel, TProperty>(this Expression<Func<TViewModel, TProperty>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
            }

            return property.Name;
        }
    }
}