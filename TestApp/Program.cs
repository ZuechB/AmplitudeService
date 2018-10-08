using Authsome;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZuechB.Amplitude;

namespace TestApp
{
    class Program
    {
        private const string apiKey = "[Your key here]";

        static void Main(string[] args)
        {
            var authsome = new AuthsomeService();
            var testApp = new AmplitudeService(authsome);

            Task.Run(async () =>
            {
                var user_properties = new Dictionary<string, string>();
                user_properties.Add("LoginButton", "Clicked");
                user_properties.Add("CreateUserButton", "Clicked");

                await testApp.PostEvent(apiKey, new Event()
                {
                    user_id = "0",
                    event_type = "LoginView",
                    user_properties = user_properties,
                    time = DateTime.Now.Ticks
                });

                Console.WriteLine("Post Sent!");
                Console.ReadLine();
            });

            Console.ReadLine();
            Console.WriteLine("Hello World!");
        }
    }
}