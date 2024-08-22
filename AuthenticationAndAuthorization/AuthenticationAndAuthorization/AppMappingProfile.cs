using System;
using AutoMapper;
using BusinessLogic;
using DataAccounts;
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>();
    }
}