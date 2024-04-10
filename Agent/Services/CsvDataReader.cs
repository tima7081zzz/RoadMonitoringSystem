using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Agent.Models;
using Agent.Services.Interfaces;
using Common;

namespace Agent.Services
{
    public class CsvDataReader : ICsvDataReader, IDisposable
    {
        private readonly StreamReader _accelerometerStreamReader;
        private readonly StreamReader _gpsStreamReader;
        private readonly ICustomLogger _logger;

        public CsvDataReader(ICustomLogger logger)
        {
            _logger = logger;
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            _accelerometerStreamReader = new StreamReader(@"accelerometer.csv");
            _gpsStreamReader = new StreamReader(@"gps.csv");

            _accelerometerStreamReader.ReadLine();
            _gpsStreamReader.ReadLine();
        }

        public async Task<AggregatedData?> Read()
        {
            var accelerometer = await ReadAccelerometer();
            var gps = await ReadGps();

            if (gps is null && accelerometer is null)
            {
                return null;
            }

            return new AggregatedData
            {
                Accelerometer = accelerometer,
                Gps = gps,
                Timestamp = DateTime.UtcNow,
                UserId = 0
            };
        }

        private async Task<Accelerometer?> ReadAccelerometer()
        {
            var line = await _accelerometerStreamReader.ReadLineAsync();
            if (line is null)
            {
                _logger.Warning("Accelerometer line is null");
                return null;
            }

            var coordinates = line
                .Split(',')
                .Select(x =>
                {
                    if (int.TryParse(x, out var coordinate))
                    {
                        return coordinate;
                    }

                    throw new ArgumentException("invalid accelerometer coordinate format");
                }).ToArray();

            return new Accelerometer
            {
                X = coordinates[0],
                Y = coordinates[1],
                Z = coordinates[2],
            };
        }

        private async Task<Gps?> ReadGps()
        {
            var line = await _gpsStreamReader.ReadLineAsync();
            if (line is null)
            {
                _logger.Warning("Gps line is null");
                return null;
            }

            var props = line
                .Split(',')
                .Select(x =>
                {
                    if (float.TryParse(x, NumberStyles.Any, CultureInfo.InvariantCulture, out var coordinate))
                    {
                        return coordinate;
                    }

                    throw new ArgumentException("invalid accelerometer coordinate format");
                }).ToArray();

            return new Gps
            {
                Longitude = props[0],
                Latitude = props[1],
            };
        }

        public void Dispose()
        {
            _accelerometerStreamReader.Dispose();
            _gpsStreamReader.Dispose();
        }
    }
}