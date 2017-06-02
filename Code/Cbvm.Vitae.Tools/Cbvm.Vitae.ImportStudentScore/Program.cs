using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cbvm.Vitae.ImportStudentScore
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = new ImportStudentScoreTask();
            task.Run();
        }
    }
}
