using System;
using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.Core.Enums;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers.UIPickerViewModels
{
    public class VacationTypePickerViewModel : UIPickerViewModel
    {
        public event EventHandler<EventArgs> ValueChanged;

        public List<string> Items { get; }

        public int SelectedIndex { get; set; }

        public string SelectedItem => Items[SelectedIndex];

        public VacationTypePickerViewModel(Dictionary<VacationType, string> dictionary, VacationType vacationType)
        {
            Items = dictionary.Select(r => r.Value).ToList();
            string vacationTypeValue = dictionary[vacationType];
            SelectedIndex = Items.IndexOf(vacationTypeValue);
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
            SelectedIndex = (int) row;
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
