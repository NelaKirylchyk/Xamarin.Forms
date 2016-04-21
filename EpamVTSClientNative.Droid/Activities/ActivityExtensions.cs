using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Android.App;
using Android.Views;
using Android.Widget;

namespace EpamVTSClientNative.Droid.Activities
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
                try
                {
                    if (command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                }
                catch (Exception e)
                {
                }

            };
            command.CanExecuteChanged += (sender, args) =>
            {
                view.Enabled = command.CanExecute(null);
            };
        }

        public static void BindLabel(this Activity activity, int textViewId, string label)
        {
            var view = activity.FindViewById<TextView>(textViewId);
            view.Text = label;
        }

        public static void BindText<TViewModel, TProperty>(this Activity activity, int textViewId, TViewModel viewModel,
            Expression<Func<TViewModel, TProperty>> propertyExpression)
            where TViewModel : INotifyPropertyChanged
        {
            var view = activity.FindViewById<TextView>(textViewId);
            if (view == null)
            {
                throw new ArgumentException(nameof(textViewId));
            }
            string propertyName = propertyExpression.GetPropertyName();
            Func<TViewModel, TProperty> propertyGetter = propertyExpression.Compile();

            TProperty propertyValue = propertyGetter(viewModel);
            //TProperty propertyValue2 = (TProperty) viewModel.GetType().GetProperty(propertyName).GetValue(viewModel);
            view.Text = propertyValue?.ToString();

            //viewModel to view
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == propertyName)
                {
                    TProperty propertyValue2 = propertyGetter(viewModel);
                    //TProperty propertyValue2 = (TProperty) viewModel.GetType().GetProperty(propertyName).GetValue(viewModel);
                    view.Text = propertyValue2?.ToString();
                }
            };
            //view to viewModel
            view.TextChanged += (sender, args) =>
            {
                //todo: try catch ignore?
                object convertedValue = Convert.ChangeType(view.Text, typeof(TProperty));
                viewModel.GetType().GetProperty(propertyName).SetValue(viewModel, convertedValue);
            };
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
                    //TProperty propertyValue2 = (TProperty) viewModel.GetType().GetProperty(propertyName).GetValue(viewModel);
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