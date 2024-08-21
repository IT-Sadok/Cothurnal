using System;
using AutoMapper;
using DataAccounts;
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<CreateUserDto,User>();
    }
}