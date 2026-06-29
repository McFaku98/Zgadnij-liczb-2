# Zgadnij Liczbę 2: Polimorfizm kontratakuje

Gra konsolowa stanowiąca bezpośrednią kontynuację "Zgadnij Liczbę". Projekt w 100% zrealizowany obiektowo w oparciu o dostarczony diagram klas (UML), napisany w języku C#.

## Mechaniki:
- **Polimorfizm:** Klasa bazowa `GameSession` z abstrakcyjną metodą `play()` implementowana jest przez dwa tryby: `StandardGame` i `NewGamePlus`.
- **Hall of Fame:** Baza najlepszych wyników sortowana na podstawie liczby prób oraz czasu rozgrywki. Wyniki zapisują się trwale do pliku JSON.
- **Ustawienia:** Zmiana języka (PL/EN), kasowanie rekordów i ukrywanie pytań o tryb zakładu (również trwałe - JSON).
- **New Game Plus:** Liczba-cel zmienia swoją wartość co 6 do 8 tur. Rekordy z tego trybu odznaczają się dopiskiem `[NG+]` w Hall of Fame.

## Jak zagrać:
Uruchom plik wynikowy w konsoli (komenda `dotnet run`). Program przeprowadzi Cię przez proces gry za pośrednictwem responsywnego UI.