# Documentation for Module 1 Movie MVC App

This app is a base demo for a MVC App using ASP.NET Core w/ C#.
- Used for storing movies in a SQL database with CRUD abilities.

To run this app you must clone this repository locally, install all dependencies needed (Web App Development in Visual Studio Installer)

Open the solution file in Visual Studio Community.

Then build the solution (Ctrl + Shift + B), and then run with or without debug (F5 or Shift + F5).



**Design Note**

Separation of Concerns:  
- The app separates data access, business logic, and presentation layers.
- MoviesController only takes care of the HTTP request itself, none of the data is stored/found in that level.
- Using IMovieService we took the operations to CRUD data out of the controller itself which makes the app easier to maintain, test, update, and debug.

Dependency Injection:  
- Used in the abstraction IMovieService, allowing the MoviesController to interact/update movie data without all the logic of the data itself being there.
- Also, allows for mock service testing without needing to change the actual code. We use ASP.NET Core's built-in Dependency Injection to do so.