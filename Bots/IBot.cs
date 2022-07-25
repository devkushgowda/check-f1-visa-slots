namespace ConsoleApp4.Bots
{
    public interface IBot
    {
        void Send(string message, string subject = null);
    }
}
