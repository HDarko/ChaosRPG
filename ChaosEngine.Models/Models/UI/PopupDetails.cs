﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ChaosEngine.Models.UI
{
    public class PopupDetails : INotifyPropertyChanged
    {
        public bool IsVisible { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        public int MinWidth { get; set; }
        public int MaxWidth { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
