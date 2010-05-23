using System;
using System.Timers;
using Statkraft.TCS.Kake.Domain;
using Statkraft.TCS.Kake.Infrastructure;

namespace Statkraft.TCS.Kake
{
    public class KakeApp
    {
        private const int IntervalSeconds = 60;
        private IKakeFordelerService _kakeFordelerService;
        private KakeFordeler _kakeFordeler;
        private Timer _timer;

        public KakeApp(int intervalSeconds, IKakeFordelerService kakeFordelerService)
        {
            _kakeFordelerService = kakeFordelerService;
            _kakeFordeler = _kakeFordelerService.Initialize();
            SetupTimer(intervalSeconds);
        }

        private void SetupTimer(int intervalSeconds)
        {
            _timer = new Timer(intervalSeconds * 1000) {Enabled = true};
            _timer.Elapsed += TimerElapsedEventHandler;
        }

        private void TimerElapsedEventHandler(object sender, ElapsedEventArgs e)
        {
            _kakeFordelerService.Run(_kakeFordeler, DateTime.Now);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Velkommen til KakeApp 1.0, trykk Esc for å avslutte. God kakespising!");

            new KakeApp(IntervalSeconds, new KakeFordelerService(new EpostSender(), new KakeFordelerRepository()));

            LoopUntilEscIsPressed();
        }

        private static void LoopUntilEscIsPressed()
        {
            ConsoleKeyInfo keypress;
            do { 
                keypress = Console.ReadKey();
            } while(keypress.Key != ConsoleKey.Escape);
        }
    }
}