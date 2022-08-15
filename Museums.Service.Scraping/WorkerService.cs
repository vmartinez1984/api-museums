using System.ComponentModel;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Museums.Core.Dtos;
using Museums.Core.Entities;
using Museums.Core.Interfaces;

namespace Museums.Service.Scraping;

public class WorkerService : IHostedService, IDisposable
{
    private Timer _timer;
    private IUnitOfWorkBl _unitOfWorkBl;
    private readonly IServiceScopeFactory _scopeFactory;

    public WorkerService(
        IServiceScopeFactory scopeFactory
    )
    {        
        _scopeFactory = scopeFactory;       
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _unitOfWorkBl = _scopeFactory.CreateAsyncScope().ServiceProvider.GetRequiredService<IUnitOfWorkBl>();
        List<CrontabDto> list;

        list = _unitOfWorkBl.Crontab.GetAsync().Result;
        foreach (var item in list)
        {
            DateTime dateTime;

            dateTime = DateTime.Now;
            ValidateCrontab(item);
            // if (item.Minute == dateTime.Minute && item.Hour == dateTime.Hour && item.DayOfWeek == Convert.ToInt32(dateTime.DayOfWeek)
            //     && item.DayOfMonth == dateTime.Day && item.Month == dateTime.Month)
            // {

            //}
        }
    }

    private void ValidateCrontab(CrontabDto item)
    {
        DateTime dateTime;

        dateTime = DateTime.Now;
        item.Minute = item.Minute is null ? dateTime.Minute : item.Minute;
        item.Hour = item.Hour is null ? dateTime.Hour : item.Hour;
        item.DayOfWeek = item.DayOfWeek is null ? Convert.ToInt32(dateTime.DayOfWeek) : item.DayOfWeek;
        item.DayOfMonth = item.DayOfMonth is null ? dateTime.Day : item.DayOfMonth;
        item.Month = item.Month is null ? dateTime.Month : item.Month;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);        

        return Task.CompletedTask;
    }
}
