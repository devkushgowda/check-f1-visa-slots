using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleApp4.Bots;

namespace ConsoleApp4
{
    class Program
    {
        private const string TelegramPremiumGroupId = "-1001692749714";
        private const string TelegramNormalGroupId = "-1001549194735";

        private static int _currentInterval;
        private static readonly Bot PriorityNotificationBot = new Bot(new List<IBot>
        {
            new TelegramBot(TelegramPremiumGroupId),
            //new GmailBot(),
        });

        private static readonly Bot GeneralBot = new Bot(new List<IBot>
        {
             new TelegramBot(TelegramNormalGroupId)
        });

        private const int SleepMins = 2;
        static void Main(string[] args)
        {
            if (!(args.Length == 1 && int.TryParse(args[0], out var intervalMins)))
            {
                intervalMins = SleepMins;
            }

            _currentInterval = intervalMins;
            Extension.SetThreadExecutionState(Extension.EXECUTION_STATE.ES_DISPLAY_REQUIRED | Extension.EXECUTION_STATE.ES_CONTINUOUS);
            while (true)
            {
                try
                {
                    CheckVisaSlots();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(intervalMins * 60 * 1000);
            }
        }

        private static void CheckVisaSlots()
        {
            var visaSlotsDetails = CheckVisaSlotRequestHandler.Get();
            visaSlotsDetails.SlotDetails.Sort((x, y) =>
            {
                var xD = x.StartDate ?? DateTime.MaxValue;
                var yD = y.StartDate ?? DateTime.MaxValue;
                return DateTime.Compare(xD, yD);
            });
            var endDate = new DateTime(2022, 08, 25);
            var nearbyVacSlots = visaSlotsDetails.SlotDetails.Where(s => s.VisaLocation.Contains("VAC") && s.StartDate != null && s.StartDate <= endDate).ToList();
            var subject = nearbyVacSlots.Format(" |");
            var message = visaSlotsDetails.SlotDetails.Format("\n");
            var header = $"Interval: {_currentInterval} Minutes" + $"\n\nTime: {DateTime.Now:g},\n\n";
            Console.WriteLine(header + message);
            GeneralBot.Send(header + message, subject);
            if (nearbyVacSlots.Count > 0)
            {
                if (HistoryManager.StoreAndVerify(visaSlotsDetails, nearbyVacSlots.Min(x => x.StartDate ?? DateTime.MaxValue)))
                    PriorityNotificationBot.Send("Priority Notification!\n" + message);
                else
                    PriorityNotificationBot.Send(message);
            }
        }
    }
}
