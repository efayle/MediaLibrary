using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);

            string userChoice;
            do {
                Console.WriteLine("1.) Display all movies\n2.) Add a movie\n3.) Find movie\n4.) Exit");
                userChoice = Console.ReadLine();

                if (userChoice == "1") {
                    //Display all movies
                    foreach (Movie m in movieFile.Movies) {
                        Console.WriteLine(m.Display());
                    }
                    Console.WriteLine("Display all movies");
                } else if (userChoice == "2") {
                    //Add a movie
                    Movie movie = new Movie();

                    string userResponse;
                    Console.WriteLine("Do you want to enter a movie? ");
                    userResponse = Console.ReadLine().ToUpper();
                    if (userResponse == "Y") {
                        //Movie information
                        Console.WriteLine("Enter movie title: ");
                        movie.title = Console.ReadLine();
                        
                        string userInput;
                        do {
                            Console.WriteLine("Enter genre (or type 'done' to quit) ");
                            userInput = Console.ReadLine();

                            if (userInput != "done" && userInput.Length > 0) {
                                movie.genres.Add(userInput);
                            }
                        } while (userInput != "done");

                        if (movie.genres.Count == 0) {
                            movie.genres.Add("(no genres listed)");
                        }
                        
                        Console.WriteLine("Enter movie director: ");
                        movie.director = Console.ReadLine();

                        // Console.WriteLine("Enter running time: (h:m:s) ");
                        // string runTime = null;
                        // runTime = movie.runningTime.ToString()

                        movieFile.AddMovie(movie);
                    }
                } else if (userChoice == "3") {
                    //Find movie
                    Console.WriteLine("Enter movie you want to find:");
                    string userMovie = Console.ReadLine();

                    var Movies = movieFile.Movies.Where(m => m.title.Contains(userMovie));

                    foreach(Movie m in Movies)
                    {
                        Console.WriteLine($"  {m.title}");
                    }

                    Console.WriteLine($"There are {Movies.Count()} movies");

                }
            } while (userChoice == "1" || userChoice == "2" || userChoice == "3");


            // IN CLASS 3/22/21
            // Console.ForegroundColor = ConsoleColor.Green;

            // // LINQ - Where filter operator & Contains quantifier operator
            // var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            // // LINQ - Count aggregation method
            // Console.WriteLine($"There are {Movies.Count()} movies from 1990");

            // // LINQ - Any quantifier operator & Contains quantifier operator
            // var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
            // Console.WriteLine($"Any movies from 1921? {validate}");

            // // LINQ - Where filter operator & Contains quantifier operator & Count aggregation method
            // int num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
            // Console.WriteLine($"There are {num} movies from 1921");

            // // LINQ - Where filter operator & Contains quantifier operator
             var Movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
             foreach(Movie m in Movies1921)
            // {
            //     Console.WriteLine($"  {m.title}");
            // }

            // // LINQ - Where filter operator & Select projection operator & Contains quantifier operator
            // var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
            // // LINQ - Count aggregation method
            // Console.WriteLine($"There are {titles.Count()} movies with \"Shark\" in the title:");
            // foreach(string t in titles)
            // {
            //     Console.WriteLine($"  {t}");
            // }

            // // LINQ - First element operator
            // var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
            // Console.WriteLine($"First movie that starts with letter 'Z': {FirstMovie.title}");

            // Console.ForegroundColor = ConsoleColor.White;

            logger.Info("Program ended");
        }
    }
}
