using AutoMapper;
using QL_NhanVien.DataAccess.DTOs;
using QL_NhanVien.DataAccess.Entities;

namespace QL_NhanVien.Helper.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<User, UserRegisterRequestDTO>().ReverseMap().ForAllMembers(opt => opt.Condition((src,dest,srcMember,destMember)=> srcMember != null));
            CreateMap<User, UserUpdateRequestDTO>().ReverseMap();
            CreateMap<Submission,SubmissionDTO>().ReverseMap();
        }
    }
}
