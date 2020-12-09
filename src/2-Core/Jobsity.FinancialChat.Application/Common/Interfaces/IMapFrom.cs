using AutoMapper;

namespace Jobsity.FinancialChat.Application.Common.Interfaces
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
