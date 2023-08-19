using AutoMapper;
using Transaction.Framework.Domain;
using Transaction.WebApi.Models;

namespace Transaction.WebApi.Mappers
{
    public class TransactionMapperImp : Profile
    {
        public TransactionMapperImp()
        {
            CreateMap<TransactionModel, AccountTransaction>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(o => o.Amount.ToString("N")));
            CreateMap<TransactionResult, TransactionResultModel>()
               .ForMember(dest => dest.Balance, opt => opt.MapFrom(o => o.Balance.Amount.ToString("N")))
               .ForMember(dest => dest.Currency, opt => opt.MapFrom(o => o.Balance.Currency.ToString()));
        }

    }
}