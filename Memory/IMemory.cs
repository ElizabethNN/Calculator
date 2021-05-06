namespace Calculator.Memory
{
    interface IMemory
    {
        string this[string key] { get; set; }
        int autosaveinterval { get; set; }
    }
}
