using AutoMapper;
using Museums.Core.Dtos;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.BusinessLayer;

public class LogBl : ILogBl
{
    private IRepository _repository;
    private IMapper _mapper;

    public LogBl(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public string Add(LogDto logDto)
    {
        LogEntity entity;

        entity = _mapper.Map<LogEntity>(logDto);
        _repository.Log.Add(entity);
        logDto.Id = entity.Id;

        return entity.Id;
    }

    public async Task<LogDto> GetAsync(string id)
    {
        LogDto item;
        LogEntity entity;

        entity = await _repository.Log.GetAsync(id);
        item = _mapper.Map<LogDto>(entity);

        return item;
    }

    public void Update(LogDto logDto)
    {
        LogEntity entity;

        entity = _mapper.Map<LogEntity>(logDto);

        _repository.Log.Update(entity);
    }

    public async Task UpdateAsync(LogDto item)
    {
        LogEntity entity;

        entity = _mapper.Map<LogEntity>(item);

        await _repository.Log.UpdateAsync(entity);
    }
}