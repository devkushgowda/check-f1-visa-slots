using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ConsoleApp4.Bots
{
    public class MessageBot : IBot
    {
        private const string From = "+14422569166";
        private const string To = "+919611350307";
        private const string To1 = "+918050459611";
        static MessageBot()
        {
            // Find your Account SID and Auth Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = "ACc83fd03c474ed715092bf5aadfa27780";
            string authToken = "763af84d4b771ea9e557295815f51c5a";

            TwilioClient.Init(accountSid, authToken);
        }

        private static void Send(string message)
        {
            MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(From),
                to: new Twilio.Types.PhoneNumber(To)
            );
            MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(From),
                to: new Twilio.Types.PhoneNumber(To1)
            );
        }

        public void Send(string message, string subject = null)
        {
            Send(message);
        }
    }
}
