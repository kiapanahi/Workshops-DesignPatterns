using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            var sender = new GhasedakSender();

            sender.OnEmptyContent += (o, eventArgs) =>
            {
                Console.WriteLine("empty content received");
            };

            sender.OnEmptyContent += (o, eventArgs) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"oops! => {eventArgs.Message}");
                Console.ResetColor();
            };

            sender.OnEmptyContent += delegate(object o, GhasedakEventArgs eventArgs) {  };

            sender.OnEmptyContent += SenderOnOnEmptyContent;

            sender.SendMessage("hello");
            sender.SendMessage(string.Empty);
        }

        private static void SenderOnOnEmptyContent(object sender, GhasedakEventArgs e)
        {
            

        }

        class GhasedakSender
        {
            public event EventHandler<GhasedakEventArgs> OnEmptyContent;

            public void SendMessage(string messageText)
            {
                Console.WriteLine("sending message...");
                if (string.IsNullOrEmpty(messageText))
                {
                    OnEmptyContent?.Invoke(this, new GhasedakEventArgs
                    {
                        Message = "you shall not pass!"
                    });
                }
            }
        }

        class GhasedakEventArgs : EventArgs
        {
            public string Message { get; set; }
        }




    }
}
