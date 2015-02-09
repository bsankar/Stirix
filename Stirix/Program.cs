using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StirixLib;
namespace Stirix
{
    class Program
    {
        static void Main(string[] args)
        {
            StirixListener strListener = new StirixListener();
            strListener.startListner();
            Console.ReadKey();
        }
    }
}
