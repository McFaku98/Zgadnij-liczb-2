using System.IO;
using System.Text.Json;

namespace ZgadnijLiczbe2
{
    public class Settings
    {
        public string currentLanguage { get; set; } = "PL";
        public bool askForBetMode { get; set; } = true;

        private string filePath = "settings.json";

        public void save()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(filePath, json);
        }

        public void load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<Settings>(json);
                if (loaded != null)
                {
                    this.currentLanguage = loaded.currentLanguage;
                    this.askForBetMode = loaded.askForBetMode;
                }
            }
        }
    }
}