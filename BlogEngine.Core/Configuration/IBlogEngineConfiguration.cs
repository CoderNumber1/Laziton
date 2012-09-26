using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.Configuration
{
    public interface IBlogEngineConfiguration
    {
        string ContextConnectionString { get; }
    }
}
