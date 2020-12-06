namespace Jobsity.FinancialChat.Application.Common.Models
{
    public class Error
    {
        public long ErrorCode { get; set; }
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
