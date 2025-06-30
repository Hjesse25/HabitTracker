# HabitTracker
Console based CRUD application to track a habit by quantity and date.

Developed using C# and SQLite

# Features

* SQLite database connection

    - The program uses a SQLite database to store and read information.
    - If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where the user can navigate by key presses

    - ![image](https://i.imgur.com/4eeiNpd.png)

* CRUD DB functions

    - From the main menu, users can Create, Read, Update or Delete for whichever date they want, entered in mm-dd-yyyy.
    - Date and quantity are checked to make sure they're in the correct format.

* Date and Quantity input

    - ![image](https://i.imgur.com/flClhXA.png)

# Challenges
- Ado.NET was challenging to work with at first as it was a new concept to me
- Implementing the delete method for my database was challenging since I didn't know how to implement it from my mind to code. I asked Claude Ai to help me with it.
- Using the SQLite extension with Visual Studio Code wasn't working at first, then I figured I had to install it on my computer since I use Linux
- Figuring out SQL commands and their relationship with my application was somewhat challenging

# Lessons Learned
- Created a SQLite database for the first time and connected to my console app
- Learned how to properly organize my code using Object Oriented Programming concepts
- I figured out when to use the static keyword for my methods vs not using it

## Project Idea
- I used the C# academy to get my project idea: [Link](https://www.thecsharpacademy.com/project/12/habit-logger)

