using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieCheckout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MovieCheckout.Controllers
{
    public class MovieController : Controller
    {
        public static List<Movie> SavedMovies = new List<Movie>
        {
            new Movie(1, "Forrest Gump", "Drama", new DateTime(1999, 01, 01), "Tom Hanks, Sally Field", "John Smith", 150, 6.99),
            new Movie(2, "The Hangover", "Comedy", new DateTime(2013, 01, 01), "Bradley Cooper, Mike Tyson", "Pete Smith", 119, 6.99),
            new Movie(3, "The Notebook", "Romance", new DateTime(2004, 11, 02), "Ryan Gosling, Rachel McAdams", "Nick Cassavetes", 124, 6.99),
            new Movie(4, "The Lighthouse", "Horror", new DateTime(2019, 05, 12), "Robert Pattinson, William Daofoe, Valeriia Karaman", "Robert Eggers", 110, 6.99)
        };

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult AddNewMovie(Movie movie)
        {
            string movieJson = JsonSerializer.Serialize(movie);

            HttpContext.Session.SetString("Movie", movieJson);

            return RedirectToAction("AddMovieToList");
        }

        public IActionResult AddMovieToList()
        {
            //get movie the new Movie from the MovieSession
            Movie movie = JsonSerializer.Deserialize<Movie>(HttpContext.Session.GetString("Movie"));

            //fill the savedmovies list from MovieList session
            string movieListJson = HttpContext.Session.GetString("MovieList");

            if (movieListJson != null)
            {
                SavedMovies = JsonSerializer.Deserialize<List<Movie>>(movieListJson);
            }

            // Adding the session movie to the SavedMovie List
            SavedMovies.Add(movie);

            //resave the MovieList with the new movie in it
            movieListJson = JsonSerializer.Serialize(SavedMovies);

            HttpContext.Session.SetString("MovieList", movieListJson);

            return RedirectToAction("ListMovies");
        }
        public IActionResult ListMovies()
        {
            string movieList = HttpContext.Session.GetString("MovieList");

            if (movieList != null)
            {
                SavedMovies = JsonSerializer.Deserialize<List<Movie>>(movieList);
            }

            return View(SavedMovies);
        }
      
       public IActionResult AddToCart(string title)
        {
            string cartJson = HttpContext.Session.GetString("CartList");
            List<Movie> CartList = new List<Movie>();

            if (cartJson == null)
            {
                CartList.Clear();
            }
            else
            {
                CartList = JsonSerializer.Deserialize<List<Movie>>(cartJson);
            }
            //this finds the movie with the name selected from the table in the previous view
            //(ListMovies).
            Movie foundMovie = SavedMovies.Where(x => x.MovieTitle== title).First();

            //Add found movie to the cart list and return user to the list of movies
            if (CartList.Exists(x => x.MovieTitle == foundMovie.MovieTitle) == false)
            {
                CartList.Add(foundMovie);

                //resave the CartList with the new movie in it
                string cartListJson = JsonSerializer.Serialize(CartList);
                HttpContext.Session.SetString("CartList", cartListJson);
                return RedirectToAction("ListMovies");
            }
           
            return View("CartError");
        }



        
        public IActionResult Cart()
        {
            string cartJson = HttpContext.Session.GetString("CartList");
            List<Movie> CartList = new List<Movie>();

            if (cartJson != null)
            {
                CartList = JsonSerializer.Deserialize<List<Movie>>(cartJson);
            }

            if (CartList == null)
            {
                return View("ListMovies");
            }

            return View(CartList);
        }

        public IActionResult GetReceipt()
        {
            string cartJson = HttpContext.Session.GetString("CartList");
            List<Movie> CartList = new List<Movie>();
            if (cartJson != null)
            {
                CartList = JsonSerializer.Deserialize<List<Movie>>(cartJson);
            }

            HttpContext.Session.Remove("CartList");

            return View(CartList);

            

        }
    }    
}