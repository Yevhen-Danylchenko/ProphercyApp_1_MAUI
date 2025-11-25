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

        public DataService()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            usersFilePath = Path.Combine(appDataPath, "users.json");
            propheciesFilePath = Path.Combine(appDataPath, "prophecies.json");
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

        public async Task<List<History>> LoadHistoryAsync()
        {
            await Task.Delay(200); 
            return new List<History>
            {
                new History { UserEmail = "user@example.com", Text = "Передбачення 1" },
                new History { UserEmail = "user@example.com", Text = "Передбачення 2" },
                new History { UserEmail = "other@example.com", Text = "Передбачення іншого користувача" }
            };
        }


    }
}
