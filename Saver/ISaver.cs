using System.Collections.Generic;

namespace Calculator.Saver
{
    interface ISaver
    {
        void saveData(Dictionary<string, string> data);

        Dictionary<string, string> loadData();
    }
}
