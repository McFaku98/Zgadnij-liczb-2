using System;

namespace ZgadnijLiczbe2
{
    public class GameManager
    {
        private Settings settings;
        private HallOfFame hallOfFame;
        private UI ui;

        public GameManager()
        {
            settings = new Settings();
            settings.load();
            hallOfFame = new HallOfFame();
            ui = new UI(settings);
        }

        public void run()
        {
            while (true)
            {
                Console.Clear();
                bool hasRecords = hallOfFame.hasAnyRecords();
                showWelcomeScreen(hasRecords);

                string prompt = settings.currentLanguage == "PL" ? "\n Wybierz opcje: " : "\n Choose option: ";
                string choice = ui.readString(prompt);

                // Dynamiczna obsługa wyborów w zależności od tego, czy Hall of Fame jest widoczne
                if (choice == "1")
                {
                    startNewGame();
                }
                else if (hasRecords)
                {
                    if (choice == "2") showTop5Menu();
                    else if (choice == "3") showSettings();
                    else if (choice == "4") break;
                }
                else
                {
                    // Gdy Hall of Fame jest ukryte, Ustawienia to "2", a Wyjście to "3"
                    if (choice == "2") showSettings();
                    else if (choice == "3") break;
                }
            }
        }

        private void showWelcomeScreen(bool hasRecords)
        {
            string title = settings.currentLanguage == "PL" ? "ZGADNIJ LICZBE 2" : "GUESS THE NUMBER 2";

            ui.displayMessage("");
            ui.displayMessage(" ===================================");
            ui.displayMessage($" ||      {title}       ||");
            ui.displayMessage(" ===================================");

            ui.displayMessage("  1. " + (settings.currentLanguage == "PL" ? "Nowa Gra" : "New Game"));

            if (hasRecords)
            {
                ui.displayMessage("  2. Hall of Fame");
            }

            string optSet = hasRecords ? "3" : "2";
            string optExit = hasRecords ? "4" : "3";

            ui.displayMessage($"  {optSet}. " + (settings.currentLanguage == "PL" ? "Ustawienia" : "Settings"));
            ui.displayMessage($"  {optExit}. " + (settings.currentLanguage == "PL" ? "Wyjscie" : "Exit"));

            ui.displayMessage(" ===================================");
        }

        private void showSettings()
        {
            while (true)
            {
                Console.Clear();
                string title = settings.currentLanguage == "PL" ? "USTAWIENIA" : "SETTINGS";

                ui.displayMessage("");
                ui.displayMessage(" -----------------------------------");
                ui.displayMessage($"           {title}           ");
                ui.displayMessage(" -----------------------------------");

                ui.displayMessage($"  1. Język / Language: {settings.currentLanguage}");

                string betTxt = settings.askForBetMode ? "TAK / YES" : "NIE / NO";
                ui.displayMessage($"  2. Tryb zakładu / Ask for bet: {betTxt}");

                ui.displayMessage("  3. Wyczyść Hall of Fame / Clear HoF");
                ui.displayMessage("  4. Powrót / Back");
                ui.displayMessage(" -----------------------------------");

                string c = ui.readString("\n -> ");
                if (c == "1") { settings.currentLanguage = settings.currentLanguage == "PL" ? "EN" : "PL"; settings.save(); }
                else if (c == "2") { settings.askForBetMode = !settings.askForBetMode; settings.save(); }
                else if (c == "3")
                {
                    hallOfFame.clearRecords();
                    ui.displayMessage(settings.currentLanguage == "PL" ? "\n Wyczyszczono!" : "\n Cleared!");
                    System.Threading.Thread.Sleep(1000);
                }
                else if (c == "4") break;
            }
        }

        private void startNewGame()
        {
            Console.Clear();
            ui.displayMessage(" ===================================");
            ui.displayMessage(settings.currentLanguage == "PL" ? " ||          WYBIERZ TRYB         ||" : " ||          CHOOSE MODE          ||");
            ui.displayMessage(" ===================================");
            ui.displayMessage(settings.currentLanguage == "PL" ? "  1. Standardowa \n  2. Nowa Gra Plus" : "  1. Standard \n  2. New Game Plus");
            string mode = ui.readString("\n -> ");

            Console.Clear();
            ui.displayMessage(" ===================================");
            ui.displayMessage(settings.currentLanguage == "PL" ? " ||         WYBIERZ POZIOM        ||" : " ||         CHOOSE LEVEL          ||");
            ui.displayMessage(" ===================================");
            ui.displayMessage(settings.currentLanguage == "PL" ? "  1. Latwy (1-50)\n  2. Sredni (1-100)\n  3. Trudny (1-250)" : "  1. Easy (1-50)\n  2. Med (1-100)\n  3. Hard (1-250)");

            int diff = ui.readInt("\n -> ");
            if (diff < 1 || diff > 3) diff = 1;

            GameSession session;

            if (mode == "2")
            {
                session = new NewGamePlus(diff, ui);
            }
            else
            {
                int maxAtt = 0;
                if (settings.askForBetMode)
                {
                    Console.Clear();
                    string msg = settings.currentLanguage == "PL" ? " Maksymalna liczba prob (0 = bez limitu): " : " Max attempts (0 = no limit): ";
                    maxAtt = ui.readInt(msg);
                }
                session = new StandardGame(diff, maxAtt, ui);
            }

            Console.Clear();
            PlayerRecord result = session.play();

            if (result != null)
            {
                ui.displayMessage("");
                string namePrompt = settings.currentLanguage == "PL" ? " Podaj imie do Hall of Fame: " : " Enter name for Hall of Fame: ";
                string name = ui.readString(namePrompt);
                result.playerName = string.IsNullOrWhiteSpace(name) ? "Anon" : name;
                hallOfFame.addRecord(result);
            }

            ui.readString(settings.currentLanguage == "PL" ? "\n Nacisnij ENTER..." : "\n Press ENTER...");
        }

        private void showTop5Menu()
        {
            Console.Clear();
            ui.displayMessage(" ===================================");
            ui.displayMessage(" ||         HALL OF FAME          ||");
            ui.displayMessage(" ===================================");
            ui.displayMessage("  1. Latwy/Easy\n  2. Sredni/Medium\n  3. Trudny/Hard");
            int d = ui.readInt("\n -> ");

            Console.Clear();
            ui.showTop5(hallOfFame.getTop5(d));
            ui.readString("\n ENTER...");
        }
    }
}