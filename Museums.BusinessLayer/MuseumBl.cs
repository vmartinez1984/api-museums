using AutoMapper;
using Museums.Core.Dtos;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.BusinessLayer;
public class MuseumBl : IMuseum
{
    private IRepository _repository;
    private IMapper _mapper;

    public MuseumBl(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MuseumDto> GetAsync(int museumId)
    {
        MuseumDto item;
        MuseumEntity entity;

        entity = await _repository.Museum.GetAsync(museumId);
        item = _mapper.Map<MuseumDto>(entity);

        return item;
    }

    public async Task<List<MuseumDto>> GetAsync()
    {
        List<MuseumDto> list;
        List<MuseumEntity> entities;

        entities = await _repository.Museum.GetAsync();
        list = _mapper.Map<List<MuseumDto>>(entities);

        return list;
    }

    public async Task<MuseumDto> GetAsync(string id)
    {
        MuseumDto item;
        MuseumEntity entity;
        if (int.TryParse(id, out int museumId))
            entity = await _repository.Museum.GetAsync(museumId);
        else
            entity = await _repository.Museum.GetAsync(id);

        item = _mapper.Map<MuseumDto>(entity);

        return item;
    }

    public async Task<MuseumPagerDto> GetAsync(PagerDto pagerDto)
    {
        MuseumPagerDto museumPager;
        List<MuseumEntity> entities;
        Pager pager;

        pager = _mapper.Map<Pager>(pagerDto);
        entities = await _repository.Museum.GetAsync(pager);
        museumPager = _mapper.Map<MuseumPagerDto>(pager);
        museumPager.ListMuseums = _mapper.Map<List<MuseumDto>>(entities);

        return museumPager;
    }

    public void Update(MuseumDto item)
    {
        MuseumEntity entity;

        entity = _mapper.Map<MuseumEntity>(item);

        _repository.Museum.Update(entity);
    }

    public async Task UpdateAsync(MuseumDto item)
    {
        MuseumEntity entity;

        entity = _mapper.Map<MuseumEntity>(item);

        await _repository.Museum.UpdateAsync(entity);
    }
}
