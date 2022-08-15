using System.Collections.Generic;
using AutoMapper;
using Museums.Core.Dtos;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.BusinessLayer;

public class CrontabBl : ICrontabBl
{
    private IRepository _repository;
    private IMapper _mapper;

    public CrontabBl(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> AddAsync(CrontabDtoIn item)
    {
        CrontabEntity entity;

        entity = _mapper.Map<CrontabEntity>(item);
        await _repository.Crontab.AddAsycn(entity);
    
        return entity.Id;
    }

    public async Task<List<CrontabDto>> GetAsync()
    {
        List<CrontabDto> list;
        List<CrontabEntity> entities;

        entities = await _repository.Crontab.GetAsync();
        list = _mapper.Map<List<CrontabDto>>(entities);

        return list;
    }
}