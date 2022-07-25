using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4.Bots
{
    public class Bot : IBot
    {
        private readonly List<IBot> _bots;

        public Bot(List<IBot> bots)
        {
            _bots = bots;
        }

        public void Send(string message, string subject = null)
        {
            _bots.ForEach(b =>
            {
                try
                {
                    b.Send(message, subject);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
