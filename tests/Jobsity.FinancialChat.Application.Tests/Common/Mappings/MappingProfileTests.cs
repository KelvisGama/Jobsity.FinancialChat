using System;
using AutoMapper;
using Jobsity.FinancialChat.Application.Common.Mappings;
using Jobsity.FinancialChat.Application.Messages.Commands.AddMessage;
using Jobsity.FinancialChat.Application.Messages.Models;
using Jobsity.FinancialChat.Domain.Entities;
using Xunit;

namespace Jobsity.FinancialChat.Application.Tests.Common.Mappings
{
    public class MappingProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(AddMessageCommand), typeof(Message))]
        [InlineData(typeof(Message), typeof(MessageDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}