using System;
using System.Collections.Generic;

namespace ZgadnijLiczbe2
{
    public class UI
    {
        private string[] textsTooSmall = { "Za malo!", "Celuj wyzej!", "Za niska liczba!", "Wincyj!", "Liczba ukryta jest wieksza!" };
        private string[] textsTooBig = { "Za duzo!", "Celuj nizej!", "Za wysoka liczba!", "Odejmij troche!", "Liczba ukryta jest mniejsza!" };

        public Settings settings; // Referencja by UI wiedziało jaki język

        public UI(Settings settings)
        {
            this.settings = settings;
        }

        public void displayMessage(string messageKey)
        {
            Console.WriteLine(messageKey);
        }

        public int readInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                displayMessage(settings.currentLanguage == "PL" ? "To nie jest poprawna liczba." : "That is not a valid number.");
            }
        }

        public string readString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void showTop5(List<PlayerRecord> records)
        {
            displayMessage(settings.currentLanguage == "PL" ? "--- WYNIKI ---" : "--- RECORDS ---");
            if (records.Count == 0)
            {
                displayMessage(settings.currentLanguage == "PL" ? "Brak wynikow w tej kategorii." : "No records in this category.");
                return;
            }

            for (int i = 0; i < records.Count; i++)
            {
                var r = records[i];
                string tag = r.isNewGamePlus ? "[NG+]" : "";
                string attemptsTxt = settings.currentLanguage == "PL" ? "prob" : "attempts";
                displayMessage($"{i + 1}. {r.playerName} - {r.attempts} {attemptsTxt} | {r.timeInSeconds}s {tag}");
            }
        }

        public string getRandomFeedback(bool isTooSmall)
        {
            Random r = new Random();
            int index = r.Next(0, 5);

            // Wersja uproszczona, w EN możnaby dorobić osobną tablicę
            if (settings.currentLanguage == "EN")
            {
                return isTooSmall ? "Too small!" : "Too big!";
            }
            return isTooSmall ? textsTooSmall[index] : textsTooBig[index];
        }
    }
}