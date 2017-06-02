using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMCAServer
{
    public interface IJob
    {
        string TaskName
        {
            get;
        }

        void Running(object sender);

        void Stop();
    }
}
