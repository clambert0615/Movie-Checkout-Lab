using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCheckout.Models
{
    public class Movie
    {
        public int ID { get; set; }
       
        public string MovieTitle { get; set; }
        public string Genre { get; set; }

        public DateTime Year { get; set; }
        public string Actors { get; set; }
        public string Directors { get; set; }
        public int RunTime { get; set; }
        public double Cost { get; set; }

        public Movie() { }

        public Movie(int id, string movietitle, string genre, DateTime year, string actors, string directors, int runTime, double cost)
        {
            ID = id;
            MovieTitle = movietitle;
            Genre = genre;
            Year = year;
            Actors = actors;
            Directors = directors;
            RunTime = runTime;
            Cost = cost;
        }
    }
}

