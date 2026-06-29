using System;

namespace ZgadnijLiczbe2
{
    public class NewGamePlus : GameSession
    {
        private int shotsToReshuffle;
        private int shotsTakenSinceReshuffle;

        public NewGamePlus(int diff, UI ui)
        {
            this.difficulty = diff;
            this.ui = ui;
            this.maxAttempts = int.MaxValue; // Tryb zakładu tu nie działa
            this.currentAttempt = 0;
            this.shotsTakenSinceReshuffle = 0;

            Random r = new Random();
            this.shotsToReshuffle = r.Next(6, 9); // Co 6, 7 lub 8 strzałów
        }

        public override PlayerRecord play()
        {
            generateNumber();
            startTime = DateTime.Now;

            ui.displayMessage(ui.settings.currentLanguage == "PL" ? "[NG+] Zgadnij liczbe! Uwazaj, zmienia sie!" : "[NG+] Guess the number! Careful, it changes!");

            while (currentAttempt < maxAttempts)
            {
                currentAttempt++;
                shotsTakenSinceReshuffle++;

                if (shotsTakenSinceReshuffle > shotsToReshuffle)
                {
                    reshuffleNumber();
                }

                string prompt = ui.settings.currentLanguage == "PL" ? $"Proba nr {currentAttempt}: " : $"Attempt {currentAttempt}: ";
                int guess = ui.readInt(prompt);

                if (checkGuess(guess))
                {
                    ui.displayMessage(ui.settings.currentLanguage == "PL" ? "Zgadles!" : "You guessed it!");
                    int time = (int)(DateTime.Now - startTime).TotalSeconds;

                    return new PlayerRecord
                    {
                        attempts = currentAttempt,
                        timeInSeconds = time,
                        difficultyLevel = difficulty,
                        isNewGamePlus = true
                    };
                }
            }
            return null;
        }

        private void reshuffleNumber()
        {
            generateNumber(); // Losuje nowy targetNumber (metoda z GameSession)
            Random r = new Random();
            shotsToReshuffle = r.Next(6, 9);
            shotsTakenSinceReshuffle = 1;
            ui.displayMessage(ui.settings.currentLanguage == "PL" ? "--- UWAGA! LICZBA ZMIENILA MIEJSCE! ---" : "--- WARNING! NUMBER REROLLED! ---");
        }
    }
}