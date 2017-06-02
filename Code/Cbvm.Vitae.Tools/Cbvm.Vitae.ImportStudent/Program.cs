using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cbvm.Vitae.ImportStudent
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new ImportStudentTask();

            task.Run();
        }
    }
}
