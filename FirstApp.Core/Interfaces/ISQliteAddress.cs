using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.Interfaces
{
    public interface ISQliteAddress
    {
        string GetDatabasePath(string filename);
    }
}
