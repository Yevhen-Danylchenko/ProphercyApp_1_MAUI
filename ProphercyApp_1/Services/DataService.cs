using ProphercyApp_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProphercyApp_1.Services
{
    public class DataService
    {
        private readonly string usersFilePath;
        private readonly string propheciesFilePath;
        private readonly string historyFilePath;

        public DataService()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            usersFilePath = Path.Combine(appDataPath, "users.json");
            propheciesFilePath = Path.Combine(appDataPath, "prophecies.json");
            historyFilePath = Path.Combine(appDataPath, "history.json");
        }

        public async Task SaveUserAsync(List<User> users)
        {
            var json = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(usersFilePath, json);
        }

        public async Task<List<User>> LoadUsersAsync()
        {
            if (!File.Exists(usersFilePath))
            {
                return new List<User>();
            }
            var json = await File.ReadAllTextAsync(usersFilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        public async Task SavePropheciesAsync(List<Prophecy> prophecies)
        {
            var json = JsonSerializer.Serialize(prophecies);
            await File.WriteAllTextAsync(propheciesFilePath, json);
        }

        public async Task<List<Prophecy>> LoadPropheciesAsync()
        {
            if (!File.Exists(propheciesFilePath))
            {
                return new List<Prophecy>();
            }
            var json = await File.ReadAllTextAsync(propheciesFilePath);
            return JsonSerializer.Deserialize<List<Prophecy>>(json) ?? new List<Prophecy>();
        }

        public async Task SaveHistoryAsync(List<History> history)
        {
            var json = JsonSerializer.Serialize(history);
            await File.WriteAllTextAsync(historyFilePath, json);
        }

        public async Task<List<History>> LoadHistoryAsync()
        {
            if (!File.Exists(historyFilePath))
                return new List<History>();

            var json = await File.ReadAllTextAsync(historyFilePath);
            return JsonSerializer.Deserialize<List<History>>(json) ?? new List<History>();
        }

    }
}
