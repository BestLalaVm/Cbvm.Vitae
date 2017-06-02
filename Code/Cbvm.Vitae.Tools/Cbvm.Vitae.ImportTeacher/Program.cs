using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cbvm.Vitae.ImportTeacher
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new ImportTeacherTask();
            task.Run();
        }
    }
}
