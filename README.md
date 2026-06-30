
Projekt to  gra konsolowa napisana w języku C#. Zadaniem użytkownika jest odgadnięcie ukrytej liczby wylosowanej przez komputer. 

Lista funkcji:
- Dwa tryby gry (klasyczny oraz tryb, w którym liczba zmienia się po kilku próbach).
- Trzy poziomy trudności.
- Tablica wyników z zapisem do pliku halloffame.json.
- Ustawienia z możliwością zmiany języka.

Technologie i wymagania:
Projekt został napisany obiektowo. Wykorzystano dziedziczenie - klasy StandardGame oraz NewGamePlus dziedziczą z klasy abstrakcyjnej GameSession. Wykorzystano również polimorfizm przy inicjalizacji sesji w klasie GameManager. 
Dane (ustawienia i wyniki) są zapisywane w formacie JSON.

Jak uruchomić:
Projekt należy otworzyć w środowisku Visual Studio i skompilować, lub uruchomić plik .exe z folderu bin.