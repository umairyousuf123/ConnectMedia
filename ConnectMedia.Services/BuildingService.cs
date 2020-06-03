using ConnectMedia.Common.IRepositories;
using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ConnectMedia.Repository
{
    public class BuildingService : IBuildingService
    {
        IBuildingRepository _buildingRepository { get; set; }
        IConfiguration _configuration;
        readonly ILogger<BuildingService> _logger;

        public BuildingService(ILogger<BuildingService> logger, IBuildingRepository buildingRepository, IConfiguration configuration)
        {
            _logger = logger;
            this._buildingRepository = buildingRepository;
            this._configuration = configuration;
        }
        public List<Building> getBuildingList(int userId)
        {
            List<Building> buildings = _buildingRepository.getBuildingList(userId);

            return buildings;
        }
        public Building getBuildingDetail(int id)
        {
            Building building = _buildingRepository.getBuildingDetail(id);
            return building;
        }
        public string AddEditBuilding(Building building)
        {
            string message = _buildingRepository.AddEditBuilding(building);
            return message;
        }
        public string Delete(int id, int CurrentUserId)
        {
            string message = _buildingRepository.Delete(id, CurrentUserId);
            return message;
        }
        public string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial,
      int passwordSize)
        {
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
            const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMBERS = "123456789";
            const string SPECIALS = @"!@£$%^&*()#€";

            System.Random _random = new Random();
            int counter;

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;

            if (useUppercase) charSet += UPPER_CAES;

            if (useNumbers) charSet += NUMBERS;

            if (useSpecial) charSet += SPECIALS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return String.Join(null, _password);
        }

    }
}
