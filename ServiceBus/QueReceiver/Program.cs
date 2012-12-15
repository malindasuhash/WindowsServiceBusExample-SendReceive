using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var receiver = new MessageReceiver();

            do
            {
                Console.WriteLine(receiver.ReceivedMessage());
            }
            while (true);
        }
    }
}
