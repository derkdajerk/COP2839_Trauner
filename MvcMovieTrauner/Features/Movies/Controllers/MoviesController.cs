using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovieTrauner.Features.Movies.Models;
using MvcMovieTrauner.Features.Movies.Services;
using MvcMovieTrauner.Models;

namespace MvcMovieTrauner.Features.Movies.Controllers
{
    // localhost:1234/movies
    [Route("movies")]

    public class MoviesController : Controller
    {
        private readonly IMovieService _movies;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieService movies, ILogger<MoviesController> logger)
        {
            _movies = movies;
            _logger = logger;
        }

        // GET: /movies
        [HttpGet("")]
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IEnumerable<Movie> all = await _movies.GetAllAsync();
            IEnumerable<Movie> movies = all;
            IEnumerable<string?> genreQuery = all.Select(movie => movie.Genre).Distinct();


            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(movie => movie.Title != null
                                        && movie.Title.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));
                _logger.LogInformation("Searching by string {searchString}", searchString);
                // using _logger.Info was unavailable to me, could not figure out why.
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(movie => movie.Genre == movieGenre);
                _logger.LogInformation("Searching by genre {movieGenre}", movieGenre);
                // using _logger.Info was unavailable to me, could not figure out why.
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(genreQuery),
                Movies = movies.ToList()
            };

            _logger.LogDebug("Got movies successfully");
            return View(movieGenreVM);
        }

        // GET: /movies/details/5
        [HttpGet("details/{id:int}", Name = "MovieDetails")]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movies.GetByIdAsync(id);

            _logger.LogInformation("Displaying details for movie {id}", id);
            return View(movie);
        }

        // GET: movies/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET");
            return View();
        }

        // POST: /movies/create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST model invalid");
                return View(movie);
            }

            await _movies.AddAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: /movies/edit/5
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movies.GetByIdAsync(id);

            _logger.LogInformation("Edit GET, movie id {id}", id);
            return View(movie);
        }

        // POST: /movies/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit POST model is invalid");
                return View(movie);
            }

            await _movies.UpdateAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        // GET: /movies/delete/5
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movies.GetByIdAsync(id);
            _logger.LogInformation("Delete GET, movie id {id}", id);
            return View(movie);
        }

        // POST: movies/delete/5
        [HttpPost("delete/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movies.DeleteAsync(id);
            _logger.LogInformation("Deleted movie successfully");
            return RedirectToAction(nameof(Index));
        }

        // GET: /movies/bygenre/
        [HttpGet("bygenre/{genre}")]
        public async Task<IActionResult> ByGenre(string genre)
        {
            var all = await _movies.GetAllAsync();
            var movies = all.Where(movie => movie.Genre != null && string.Equals(movie.Genre, genre, StringComparison.OrdinalIgnoreCase));

            var viewModel = new MovieGenreViewModel
            {
                Genres = new SelectList(all.Select(movie => movie.Genre).Distinct()),
                Movies = movies.ToList(),
                MovieGenre = genre
            };

            return View("Index", viewModel);
        }

        // GET: /movies/released/2010/5
        [HttpGet("released/{year:int:min(1900)}/{month:int:range(1,12)?}")]
        public async Task<IActionResult> Released(int year, int month)
        {
            var all = await _movies.GetAllAsync();
            var movies = all.Where(movie => movie.ReleaseDate.Year == year && (month == 0 ? true : movie.ReleaseDate.Month == month));

            var viewModel = new MovieGenreViewModel
            {
                Genres = new SelectList(all.Select(movie => movie.Genre).Distinct()),
                Movies = movies.ToList(),
            };

            return View("Index", viewModel);
        }
    }
}
