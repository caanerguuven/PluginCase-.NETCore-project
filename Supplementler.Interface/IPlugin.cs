using System;
using System.Collections.Generic;
using System.Text;

namespace Supplementler.Interface
{
    public interface IPlugin
    {
        string ReadFromRedis();
    }
}
