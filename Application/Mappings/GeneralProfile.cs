using Application.DTOs;
using AutoMapper;
using Domain.Views;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs Queries
            //CreateMap<Personal, PersonaDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Item, ItemDTO>();
            #endregion

            #region Commands

            #endregion
        }
    }
}
