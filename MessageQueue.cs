using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRoutingAnalyzer
{
    class MessageQueue
    {
        private Queue<string> Messages = new Queue<string>();

        public void Put(string message)
        {
            lock (Messages) Messages.Enqueue(message);
        }
        public string Get()
        {
            lock (Messages)
            {
                if (Messages.Count > 0) return Messages.Dequeue();
                else return null;
            }
        }
    }
}
