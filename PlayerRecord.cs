using System;

namespace ZgadnijLiczbe2
{
    public class PlayerRecord
    {
        public string playerName { get; set; }
        public int attempts { get; set; }
        public int timeInSeconds { get; set; }
        public int difficultyLevel { get; set; }
        public bool isNewGamePlus { get; set; }

        public int compareTo(PlayerRecord other)
        {
            // Najpierw patrzymy na liczbę prób (rosnąco)
            int attemptsComparison = this.attempts.CompareTo(other.attempts);
            if (attemptsComparison != 0)
                return attemptsComparison;

            // Jeśli próby są równe, wygrywa krótszy czas (rosnąco)
            return this.timeInSeconds.CompareTo(other.timeInSeconds);
        }
    }
}