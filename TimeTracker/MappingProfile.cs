using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace TimeTracker;

public class MappingProfile : Profile {
    public MappingProfile() {
        CreateMap<Account, AccountDto>();
        CreateMap<ClockworkTask, ClockworkTaskDto>();
        CreateMap<AccountForCreationDto, Account>();
        CreateMap<ClockworkTaskForCreationDto, ClockworkTask>();
        CreateMap<ClockworkTaskForUpdateDto, ClockworkTask>();
        CreateMap<AccountForUpdateDto, Account>();
    }
}