using System.Collections.Generic;

namespace Calculator.Data
{
    interface IData
    {
        string this[string key] { get; set; }
        int autosaveinterval { get; set; }
        Dictionary<string, string> getDataDump();
        void save();
        void loadFromDisk();
    }
}
