using System.Threading.Tasks;
using Agent.Exceptions;
using Store.DAL;
using Store.DAL.Entities;
using Store.Models;
using Store.Services.Interfaces;

namespace Store.Services
{
    public class ProcessedAgentDataService : IProcessedAgentDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessedAgentDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProcessedAgentData> Get(int id)
        {
            var data = await _unitOfWork.ProcessedAgentDataRepository.Get(id);
            EntityNotFoundException.ThrowIfNull(data);

            return data!;
        }

        public async Task Delete(int id)
        {
            var data = await _unitOfWork.ProcessedAgentDataRepository.Get(id);
            EntityNotFoundException.ThrowIfNull(data);

            await _unitOfWork.ProcessedAgentDataRepository.Delete(id);
        }

        public async Task<ProcessedAgentData> Add(ProcessedAgentDataRequestModel data)
        {
            var entity = Map(data);

            var added = await _unitOfWork.ProcessedAgentDataRepository.Add(entity);
            await _unitOfWork.SaveChanges();

            return added;
        }

        public async Task Update(int id, ProcessedAgentDataRequestModel data)
        {
            var entity = Map(data);

            await _unitOfWork.ProcessedAgentDataRepository.Get(id);
            EntityNotFoundException.ThrowIfNull(data);

            _unitOfWork.ProcessedAgentDataRepository.Update(entity);
            await _unitOfWork.SaveChanges();
        }

        private static ProcessedAgentData Map(ProcessedAgentDataRequestModel data)
        {
            return new ProcessedAgentData
            {
                RoadState = data.RoadState,
                UserId = data.UserId,
                X = data.X,
                Y = data.Y,
                Z = data.Z,
                Longitude = data.Longitude,
                Latitude = data.Latitude,
                TimeStamp = data.TimeStamp,
            };
        }
    }
}