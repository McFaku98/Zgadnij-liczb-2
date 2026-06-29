using System;

namespace ZgadnijLiczbe2
{
    public abstract class GameSession
    {
        protected int difficulty;
        protected int maxAttempts;
        protected int currentAttempt;
        protected int targetNumber;
        protected DateTime startTime;
        protected UI ui;

        public abstract PlayerRecord play();

        protected void generateNumber()
        {
            Random r = new Random();
            int max = 50;
            if (difficulty == 2) max = 100;
            if (difficulty == 3) max = 250;

            targetNumber = r.Next(1, max + 1);
        }

        protected bool checkGuess(int guess)
        {
            if (guess == targetNumber)
            {
                return true;
            }

            bool isTooSmall = guess < targetNumber;
            ui.displayMessage(">> " + ui.getRandomFeedback(isTooSmall));
            return false;
        }
    }
}