using AutoMapper;
using Ecomerce_Web.Data;
using Ecomerce_Web.ViewModels;

namespace Ecomerce_Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterVM, KhachHang>();
                //.ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen)).ReverseMap();
            //ReserveMap: Map 2 chiều
        }
    }
}
