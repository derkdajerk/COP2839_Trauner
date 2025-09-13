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

**Route Map**
- The app uses attribute routing and feature folders to define clear and RESTful endpoints for each controller.

MoviesController Route Map:
- `https://localhost:7134/movies` : List all movies (GET)
- `https://localhost:7134/movies/details/{id}` : Show details for a movie by ID (GET)
- `https://localhost:7134/movies/create` : Show create form (GET), or create a movie (POST)
- `https://localhost:7134/movies/edit/{id}` : Show edit form (GET), or update a movie (POST)
- `https://localhost:7134/movies/delete/{id}` : Show delete confirmation (GET), or delete a movie (POST)
- `https://localhost:7134/movies/bygenre/{genre}` : List movies filtered by genre (GET)
- `https://localhost:7134/movies/released/{year}/{month?}` : List movies released in a specific year and month(optional) (GET)

HelloWorldController Route Map: 
- `https://localhost:7134/hello` : Show the HelloWorld index page (GET)
- `https://localhost:7134/hello/welcome/{name?}/{numTimes?}` : Show a welcome message for a given name and number of times (GET)
