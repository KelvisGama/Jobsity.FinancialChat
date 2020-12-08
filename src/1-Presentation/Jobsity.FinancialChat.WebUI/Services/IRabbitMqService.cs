namespace Jobsity.FinancialChat.WebUI.Services
{
    public interface IRabbitMqService
    {
        void ReceiveMessageFromWorker();
        void PushMessageToWorker(string message);
    }
}