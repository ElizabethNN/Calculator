using System;
using System.Collections.Generic;
using System.Timers;
using Calculator.Saver;

namespace Calculator.Memory
{
    class Memory : IMemory
    {
        private Timer _savetimer;
        private int _saveinterval;
        private ISaver _saver = new FileSaver();
        Dictionary<string, string> _memorycells = new Dictionary<string, string> { { "pi", "3.14159265359" }, { "e", "2.71828182846" }, {"sqr2", "1.41421356237" } };
        public string this[string key]
        {
            get => _memorycells[key];
            set {
                if (!decimal.TryParse(value, out _))
                {
                    throw new System.Exception("Wrong data was given");
                }
                if (_memorycells.ContainsKey(key))
                {
                    _memorycells[key] = value;
                }
                else
                {
                    _memorycells.Add(key, value);
                }
            }
        }
        public int autosaveinterval
        {
            get => _saveinterval;
            set {
                if (value > 0)
                {
                    _savetimer.Interval = value;
                    _savetimer.Enabled = true;
                    _saveinterval = value;
                }
                else
                {
                    _savetimer.Enabled = false;
                    _saveinterval = value;
                }
            }
        }

        Memory()
        {
            _savetimer = new Timer(300000)
            {
                AutoReset = true,
                Enabled = true,
            };
            _savetimer.Elapsed += OnTimedEvent;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _saver.saveData(_memorycells);
        }
    }
}
