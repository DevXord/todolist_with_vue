# ToDo List with Vue3 

# Technology used:
-  Bootstrap v5.0
-  Vue3,
-  MySQL,
-  Axios,
-  TypeScript,
-  Node,
-  npm,
-  Json,
-  CSS.
# Programs used:
- Inkscape,
- MySQL Workbench,
- XAMPP Control Panel v3.3.0,
- Visual Studio Code,
- Visual Studio 2022 (Web API, backend),
- Notepad++,
- Postman.
 

# How to use this app:
0. In the location "src/Database/" there is a .sql file containing a script to create the database and tables. The database was only used on localhost, so if you want to enter a different database address, edit the ".env" and ".env.production" files in the root directory.
1. On first launch, the app will try to connect to the API to automatically set up a "connectionstring" and fetch the task list from the database. If it fails, the application will not use the database.
2. If you are using Visual Studio Code, open the folder with the project, then launch the terminal and type "npm run dev", when you see two links "Local: xxx" and "Network: xxx", press on the keyboard left Ctrl + left mouse button, with the cursor set on one of the links.
3. Fill in the "Enter your task" and "Date to end" fields, then click "Create Task".
4. Below you will see a square with the text of the task, the date to be completed and three buttons.
5. The first button, "Done", is responsible for marking the task as completed. When clicked, the task will turn green and you will no longer be able to edit it.
6. The next button, "Edit", is responsible for starting the task edit mode. After clicking, the color will change to orange, two fields will appear to make changes, as well as "Cancel" buttons, responsible for exiting the edit mode without saving changes and "Save change", saving changes.
7. The last "Delete" button is responsible for deleting the task.

# Jak uzyc aplikacji:
0. W lokalizacji „src/Database/” znajduje się plik .sql zawierający skrypt do tworzenia bazy danych i tabel. Baza danych była używana tylko na localhost, więc jeśli chcesz wprowadzić inny adres bazy danych, edytuj pliki „.env” i „.env.production” w katalogu głównym.
1. Przy pierwszym uruchomieniu aplikacji, spróbuje ona połączyć się z API, w celu automatycznego skonfigurowania "connectionstring" i pobrania listy zadań z bazy danych. Jeśli się to nie uda, przejdzie ona w tryb pracy bez pobierania i zapisywanie zadań do bazy.
2. Jesli uzywasz Visual Studio Code, otwórz w nim folder z projektem, nastepnie uruchom terminal i wpisz "npm run dev", gdy zobaczysz dwa linki "Local: xxx" i "Network: xxx", wcisnij na klawiaturze lewy ctrl + lewy przycisk myszy, mając ustawiony kursor na którymś z linków.
3. Wypełnij pola "Enter your task" i "Date to end", po czym kliknij "Create Task".
4. Poniżej pojawi sie kwadrat z tekstem zadania, datą do jego wykonania i trzema przyciskami.
5. Pierwszy przycisk "Done", odpowiada za oznaczenie zadania jako wykonane. Po kliknieciu zmieni się kolor zadania na zielony i nie będzie można go już edytować.
6. Następny przycisk "Edit", odpowiada za uruchomienie trypu edycji zadania. Po kliknieciu kolor zmieni sie na pomaranczowy, pojawia się dwa pola do wprowadzenia zmian, a takze przyciski "Cancel", odpowiadajacy za wyjscie z trypu edycji bez zapisania zmian i "Save change", zapisujacy zmiany.
7. Ostatni przycisk "Delete" odpowiada za usuniecie zadania.
