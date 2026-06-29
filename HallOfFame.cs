using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ZgadnijLiczbe2
{
    public class HallOfFame
    {
        private List<PlayerRecord> records = new List<PlayerRecord>();
        private string filePath = "halloffame.json";

        public HallOfFame()
        {
            load();
        }

        public void addRecord(PlayerRecord record)
        {
            records.Add(record);
            save();
        }

        public void clearRecords()
        {
            records.Clear();
            save();
        }

        public List<PlayerRecord> getTop5(int difficulty)
        {
            var filtered = records.Where(r => r.difficultyLevel == difficulty).ToList();

            // Wykorzystanie własnej metody compareTo wg diagramu
            filtered.Sort((a, b) => a.compareTo(b));

            return filtered.Take(5).ToList();
        }

        public bool hasAnyRecords()
        {
            return records.Count > 0;
        }

        public void save()
        {
            string json = JsonSerializer.Serialize(records);
            File.WriteAllText(filePath, json);
        }

        public void load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<List<PlayerRecord>>(json);
                if (loaded != null)
                {
                    records = loaded;
                }
            }
        }
    }
}