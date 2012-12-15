using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            string valueToWrite = "";

            Console.WriteLine("Write a value and press enter to X to quit!");

            var sender = new MessageSender();
            sender.CreateQueue();
            sender.CreateSender();

            do
            {

                valueToWrite = Console.ReadLine();
                sender.Send(valueToWrite);
            }
            while (!valueToWrite.Equals("x"));
        }
    }
}
