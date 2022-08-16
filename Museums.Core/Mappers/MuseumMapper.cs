using AutoMapper;
using Museums.Core.Dtos;
using Museums.Core.Entities;

namespace Museums.Core.Mappers
{
    public class MuseumMapper : Profile
    {
        public MuseumMapper()
        {
            CreateMap<MuseumEntity, MuseumDto>().ReverseMap();

            CreateMap<LogEntity, LogDto>().ReverseMap();

            CreateMap<CrontabDtoIn, CrontabEntity>();
            CreateMap<CrontabEntity, CrontabDto>();

            CreateMap<PagerDto, Pager>();
            CreateMap<Pager, MuseumPagerDto>();
        }
    }
}