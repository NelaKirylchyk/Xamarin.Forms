using System;
using System.Collections.Generic;
using System.Linq;
using EpamVTSClient.Core.Enums;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationStatusPickerViewModel : UIPickerViewModel
    {
        public event EventHandler<EventArgs> ValueChanged;

        public List<string> Items { get; }

        public string SelectedItem { get; set; }

        public VacationStatusPickerViewModel(Dictionary<VacationStatus, string> dictionary, VacationStatus selectedItem)
        {
            Items = dictionary.Select(r => r.Value).ToList();
            SelectedItem = dictionary[selectedItem];
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
            if (ValueChanged != null)
            {
                ValueChanged(this, new EventArgs());
            }
        }
    }
}