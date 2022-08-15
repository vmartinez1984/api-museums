using System.ComponentModel;
using Museums.Core.Entities;
using Museums.Core.Interfaces;
using Museums.Service.Scraping;

namespace Museums.BusinessLayer
{
    public class ScrapyBl : IScrapyBl
    {
        private BackgroundWorker _backgroundWorker;
        private ScrapService _scrapService;
        private IRepository _repository;

        public ScrapyBl(
            IRepository repository,
            ScrapService scrapService
        )
        {
            _repository = repository;
            _scrapService = scrapService;
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

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //_unitOfWorkBl.Log.UpdateAsync
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            List<MuseumEntity> entities;
            LogEntity logEntity;

            logEntity = e.Argument as LogEntity;
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
                    if (entity.FechaMod is null || ((DateTime)museum.FechaMod).Date != ((DateTime)entity.FechaMod).Date)
                    {
                        try
                        {
                            logEntity.MuseumIdInProcess = museum.Id;
                            _repository.Log.Update(logEntity);
                            museum.State = "Process";
                            _repository.Museum.Update(museum);
                            _scrapService.GetMuseum(entity);
                            entity.Id = museum.Id;
                            entity.State = "Update";
                            _repository.Museum.Update(museum);
                        }
                        catch (Exception ex)
                        {
                            logEntity.NumberErrors++;
                        }
                    }
                    logEntity.NumberOfUpdates++;
                    _repository.Log.Update(logEntity);
                }
            }
            logEntity.DateEndExecution = DateTime.Now;
            _repository.Log.Update(logEntity);
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

        public string Process(string id)
        {
            LogEntity logEntity;

            logEntity = new LogEntity();
            logEntity.DateExecution = DateTime.Now;
            logEntity.MuseumIdInProcess = id;
            _repository.Log.Add(logEntity);
            _backgroundWorker.RunWorkerAsync(logEntity);

            return logEntity.Id;
        }
    }
}