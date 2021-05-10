using System;
using System.Collections.Generic;
using System.Timers;
using Calculator.Saver;

namespace Calculator.Data
{
    class History : IData
    {
        private Timer _savetimer;
        private int _saveinterval;
        private ISaver _saver;
        Dictionary<string, string> _memorycells = new Dictionary<string, string> {};
        public string this[string key]
        {
            get => _memorycells[key];
            set {
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

        public History(string path = "history", int interval = 300000)
        {
            _savetimer = new Timer(interval)
            {
                AutoReset = true,
                Enabled = true,
            };
            _savetimer.Elapsed += OnTimedEvent;
            _saver = new FileSaver(path);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            save();
        }

        public Dictionary<string, string> getDataDump()
        {
            return _memorycells;
        }

        public void loadFromDisk()
        {
            try
            {
                _memorycells = _saver.loadData();
            }
            catch {
                _memorycells = new Dictionary<string, string>();
            }
        }
        public void save()
        {
            _saver.saveData(_memorycells);
        }
    }
}
