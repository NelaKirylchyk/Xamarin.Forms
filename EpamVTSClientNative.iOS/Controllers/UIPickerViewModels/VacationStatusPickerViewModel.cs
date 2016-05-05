using System;
using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.Core.Enums;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers.UIPickerViewModels
{
    public class VacationStatusPickerViewModel : UIPickerViewModel
    {
        public event EventHandler<EventArgs> ValueChanged;

        public List<string> Items { get; }

        public string SelectedItem { get; set; }

        public nint SelectedIndex { get; set; }

        public VacationStatusPickerViewModel(Dictionary<VacationStatus, string> dictionary, VacationStatus vacationStatus)
        {
            Items = dictionary.Select(r => r.Value).ToList();
            string vacationStatusValue = dictionary[vacationStatus];
            SelectedIndex = Items.IndexOf(vacationStatusValue);
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return Items.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return Items[(int)row];
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            SelectedIndex = (int)row;
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}