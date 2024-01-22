using Bank.Domain.DTO;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Bank.Domain.Profiles
{
    public class AccountsProfile :Profile
    {
        public AccountsProfile()
        {
            //CreateMap<Accounts, AccountsDTO>()
            //   .ForMember(dest => dest.AccountId,
            //       opt => opt.MapFrom(src => src.AccountId))
            //   .ForMember(dest => dest.AccountType,
            //   opt => opt.MapFrom(src => src.AccountTypes.TypeName))
            //   .ForMember(dest => dest.Balance, 
            //   opt => opt.MapFrom(src => src.Balance));

            CreateMap<Accounts, AccountsDTO>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
            .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountTypes.TypeName))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance));
        }


    }
}
