using System;
using System.Threading.Tasks;
using Agent.Models;

namespace Agent.Services.Interfaces
{
    public interface ICsvDataReader : IDisposable
    {
        Task<AggregatedData?> Read();
    }
}