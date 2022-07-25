using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConsoleApp4
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }

    public class CheckMyVisaSlotResponse
    {
        public List<SlotDetail> SlotDetails { get; set; }

        public UserDetails UserDetails { get; set; }
    }

    public class SlotDetail
    {
        [JsonProperty("visa_location")]
        public string VisaLocation { get; set; }

        public int Slots { get; set; }

        public string SlotDetails { get; set; }

        [JsonProperty("start_date")]
        [JsonConverter(typeof(DateFormatConverter), "dd MMM yyyy")]
        public DateTime? StartDate { get; set; }

        public string GetLocationCode()
        {
            var shortCode = VisaLocation.Substring(0, 3);
            return VisaLocation.Contains("VAC", StringComparison.InvariantCultureIgnoreCase) ? shortCode + "-V" : $"{shortCode,-5}";
        }
    }

    public class UserDetails
    {
        [JsonProperty("visa_type")]
        public string VisaType { get; set; }

        [JsonProperty("appointment_type")]
        public string AppointmentType { get; set; }

        public string Subscription { get; set; }
    }


}
