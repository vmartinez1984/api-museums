using System.ComponentModel;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Museums.Core.Dtos;
using Museums.Core.Entities;
using Museums.Core.Interfaces;
using Museums.Service.Scraping;

namespace Museums.BusinessLayer
{
    public class ScrapyBl : IScrapyBl
    {
        private BackgroundWorker _backgroundWorker;
        private ScrapService _scrapService;
        private ILogger<ScrapyBl> _logger;
        private readonly IMapper _mapper;
        private IRepository _repository;

        public ScrapyBl(
            IRepository repository
            , ScrapService scrapService
            , ILogger<ScrapyBl> logger
            , IMapper mapper
        )
        {
            _repository = repository;
            _scrapService = scrapService;
            _logger = logger;
            _mapper = mapper;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += DoWork;
            _backgroundWorker.RunWorkerCompleted += RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += ProgressChanged;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
        }

        public string Process()
        {
            LogEntity logEntity;

            logEntity = new LogEntity();
            logEntity.DateExecution = DateTime.Now;
            _repository.Log.Add(logEntity);
            _backgroundWorker.RunWorkerAsync(logEntity);

            return logEntity.Id;
        }

        /// <summary>
        /// Solo actualiza un museo
        /// </summary>
        /// <param name="id">Id del museo</param>
        /// <returns></returns>
        public string Process(string id)
        {
            LogEntity logEntity;

            //logEntity = _repository.Log.GetByMuseumIdAsync(id);
            logEntity = new LogEntity();
            logEntity.DateExecution = DateTime.Now;
            logEntity.MuseumIdInProcess = id;
            _repository.Log.Add(logEntity);
            _backgroundWorker.RunWorkerAsync(logEntity);

            return logEntity.Id;
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //_unitOfWorkBl.Log.UpdateAsync
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            _logger.LogInformation("Inicia proceso de actualizaci??n");
            LogEntity logEntity;
            //List<MuseumEntity> entities;

            logEntity = e.Argument as LogEntity;
            //entities = UpdateMuseums(logEntity);

            UpdateMuseums(logEntity);
        }

        private List<MuseumEntity> UpdateMuseums(LogEntity logEntity)
        {
            List<MuseumEntity> entities;

            entities = GetListMuseums(logEntity);
            logEntity.TotalRecords = entities.Count();
            _repository.Log.Update(logEntity);
            foreach (var entity in entities)
            {
                logEntity = _repository.Log.GetAsync(logEntity.Id).Result;
                if (logEntity.DateCancelation is null)
                {
                    MuseumEntity museum;

                    museum = _repository.Museum.GetAsync(entity.MuseoId).Result;
                    //if (entity.FechaMod is null || ((DateTime)museum.FechaMod).Date != ((DateTime)entity.FechaMod).Date)
                    //{
                    try
                    {
                        logEntity.MuseumIdInProcess = museum.Id;
                        _repository.Log.Update(logEntity);
                        entity.State = "Process";
                        _repository.Museum.Update(entity);
                        _scrapService.GetMuseum(entity);
                        entity.Id = museum.Id;
                        entity.State = "Update";
                        _repository.Museum.Update(entity);
                        _logger.LogInformation("id: " + museum.Id + $"{logEntity.NumberOfUpdates} updates");
                    }
                    catch (Exception ex)
                    {
                        logEntity.NumberErrors++;
                        _logger.LogError(ex.Message);
                    }
                    //}
                    logEntity.NumberOfUpdates++;
                    _repository.Log.Update(logEntity);
                }
            }
            _logger.LogInformation($"{logEntity.NumberOfUpdates} Updates {logEntity.NumberErrors} erros");
            logEntity.DateEndExecution = DateTime.Now;
            _repository.Log.Update(logEntity);

            return entities;
        }

        public void UpdateMuseums(LogDto logDto)
        {
            Task.Delay(5000);
            _logger.LogInformation("Inicia proceso de actualizaci??n");
            LogEntity logEntity;
            //List<MuseumEntity> listMuseums;

            VerifyLog(logDto);
            logEntity = _mapper.Map<LogEntity>(logDto);

            UpdateMuseums(logEntity);
        }

        public async Task UpdateMuseumsAsync(LogDto logDto)
        {
            await Task.Delay(5000);
            _logger.LogInformation("Inicia proceso de actualizaci??n");
            LogEntity logEntity;
            //List<MuseumEntity> listMuseums;

            VerifyLog(logDto);
            logEntity = _mapper.Map<LogEntity>(logDto);

            UpdateMuseums(logEntity);
        }

        /// <summary>
        /// The log must be registered, otherwise it is registered
        /// </summary>
        /// <param name="logDto"></param>
        private void VerifyLog(LogDto logDto)
        {
            if (string.IsNullOrEmpty(logDto.Id))
            {
                logDto.Id = _repository.Log.Add(new LogEntity
                {
                    MuseumIdInProcess = logDto.MuseumIdInProcess
                });
            }
        }

        private List<MuseumEntity> GetListMuseums(LogEntity logEntity)
        {
            List<MuseumEntity> entities;
            if (logEntity.MuseumIdInProcess is null)
            {
                entities = _scrapService.GetMuseumsAsync().Result;
            }
            else
            {
                //Only in the case of an update
                MuseumEntity museumEntity;

                if (int.TryParse(logEntity.MuseumIdInProcess, out int id))
                {
                    museumEntity = _repository.Museum.GetAsync(id).Result;
                }
                else
                {
                    museumEntity = _repository.Museum.GetAsync(logEntity.MuseumIdInProcess).Result;
                }
                //The current date is placed for processing
                museumEntity.FechaMod = null;
                entities = new List<MuseumEntity>();
                entities.Add(museumEntity);
            }

            return entities;
        }

        private int GetPercentaje(int totalrecods, int process)
        {
            int percentage;

            percentage = 100 * process / totalrecods;

            return percentage;
        }

    }
}