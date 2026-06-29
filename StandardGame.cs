using System;

namespace ZgadnijLiczbe2
{
    public class StandardGame : GameSession
    {
        public StandardGame(int diff, int maxAttempts, UI ui)
        {
            this.difficulty = diff;
            this.maxAttempts = maxAttempts <= 0 ? int.MaxValue : maxAttempts;
            this.ui = ui;
            this.currentAttempt = 0;
        }

        public override PlayerRecord play()
        {
            generateNumber();
            startTime = DateTime.Now;

            ui.displayMessage(ui.settings.currentLanguage == "PL" ? "Zgadnij liczbe!" : "Guess the number!");

            while (currentAttempt < maxAttempts)
            {
                currentAttempt++;
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
                        isNewGamePlus = false
                    };
                }
            }

            ui.displayMessage(ui.settings.currentLanguage == "PL" ? "Przegrales - brak prob!" : "You lost - out of attempts!");
            return null; // Gracz nie odgadł
        }
    }
}