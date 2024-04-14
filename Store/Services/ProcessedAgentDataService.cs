using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.DAL;
using Store.DAL.Entities;
using Store.Exceptions;
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

        public async Task BulkAdd(IEnumerable<ProcessedAgentDataRequestModel> data)
        {
            var entities = data.Select(Map);

            await _unitOfWork.ProcessedAgentDataRepository.Add(entities);
            await _unitOfWork.SaveChanges();
        }

        public async Task Update(int id, ProcessedAgentDataRequestModel data)
        {
            var entity = await _unitOfWork.ProcessedAgentDataRepository.Get(id);
            EntityNotFoundException.ThrowIfNull(entity);

            entity!.RoadState = data.RoadState;
            entity.UserId = data.UserId;
            entity.X = data.X;
            entity.Y = data.Y;
            entity.Z = data.Z;
            entity.Longitude = data.Longitude;
            entity.Latitude = data.Latitude;
            entity.TimeStamp = data.TimeStamp;

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