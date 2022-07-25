using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp4
{
    public static class HistoryManager
    {
        public const int MaxHistory = 30;
        private static readonly List<CheckMyVisaSlotResponse> _checkMyVisaSlotHistory = new List<CheckMyVisaSlotResponse>();

        public static bool StoreAndVerify(CheckMyVisaSlotResponse response, DateTime vacDateTime)
        {
            _checkMyVisaSlotHistory.Add(response);

            if (_checkMyVisaSlotHistory.Count > MaxHistory)
                _checkMyVisaSlotHistory.RemoveAt(0);

            var found = _checkMyVisaSlotHistory.Any(x =>
                  x.SlotDetails.Where(y => !y.VisaLocation.Contains("VAC")).Any(y => y.StartDate > vacDateTime));

            return found;
        }
    }
}
