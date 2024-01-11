using AutoMapper;
using Bank.Domain.DTO;
using Bank.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bank.Domain.Profiles
{
    public class TransactionsProfile :Profile
    {
        public TransactionsProfile()
        {
            CreateMap<Transactions, TransactionsDTO>()
                .ForMember(dest => dest.Amount,
                opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Date,
                opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Operation,
                opt => opt.MapFrom(src => src.Operation))
                .ForMember(dest => dest.Comment,
                opt => opt.MapFrom(src => src.Symbol));

                
        }
    }
}
