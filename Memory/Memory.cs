﻿using System;
using System.Collections.Generic;
using System.Timers;
using Calculator.Saver;

namespace Calculator.Memory
{
    class Memory : IMemory
    {
        private Timer _savetimer;
        private int _saveinterval;
        private ISaver _saver;
        Dictionary<string, string> _memorycells;
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

        public Memory(string path)
        {
            _savetimer = new Timer(300000)
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

        public Dictionary<string, string> getMemoryDump()
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
                _memorycells = new Dictionary<string, string> {
                                                                 { "pi", Math.PI.ToString("F16") },
                                                                 { "e", Math.E.ToString("F16") },
                                                                 { "sqr2", Math.Sqrt(2).ToString("F16") }
                                                                                                          };
            }
        }

        public void save()
        {
            _saver.saveData(_memorycells);
        }
    }
}
