using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iZathfit.Components.TimePicker
{
    internal class HelperTimerPicker
    {
        public ObservableCollection<string> Hours(bool is24 = false) { 
            var obs = new ObservableCollection<string>();
            for(int x = 1; x <= (is24 ? 24 : 12); x++)
                obs.Add(x.ToString("00"));
            return obs;
        }

        public ObservableCollection<string> Minutes()
        {
            var obs = new ObservableCollection<string>();
            for (int x = 0; x <= 59; x++)
                obs.Add(x.ToString("00"));
            return obs;
        }
        public ObservableCollection<string> Seconds() {
            var obs = new ObservableCollection<string>();
            for (int x = 0; x <= 59; x++)
                obs.Add(x.ToString("00"));
            return obs;
        }

        public ObservableCollection<string> AMPM() { 
            return new ObservableCollection<string>() { "a. m.", "p. m." };
        }
    }
}
