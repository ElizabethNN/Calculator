using System.Collections.Generic;

namespace Calculator.Memory
{
    interface IMemory
    {
        string this[string key] { get; set; }
        int autosaveinterval { get; set; }
        Dictionary<string, string> getMemoryDump();
        void save();
        void loadFromDisk();
    }
}
