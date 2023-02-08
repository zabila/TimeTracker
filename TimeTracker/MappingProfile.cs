using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace TimeTracker;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Account, AccountDto>();
    }
}