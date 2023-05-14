using ConsoleTools;
using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H645NF_HFT_2022231.Client
{
    class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            if (entity == "Genre")
            {
                Console.Write("Enter Genre value: ");
                string value = Console.ReadLine();

                rest.Post(new Genre() { Value = value }, "genre");
            }
            else if (entity == "Movie")
            {
                Console.WriteLine("Enter Movie values: ");

                Console.Write("Enter Title: ");
                string title = Console.ReadLine();

                Console.Write("Enter Runtime: ");
                int runtime = int.Parse(Console.ReadLine());

                Console.Write("Enter Year: ");
                int year = int.Parse(Console.ReadLine());

                Console.Write("Enter Country: ");
                string country = Console.ReadLine();

                Console.Write("Enter Budget: ");
                int budget = int.Parse(Console.ReadLine());

                Console.Write("Enter GenreId: ");
                int genreid = int.Parse(Console.ReadLine());

                rest.Post(new Movie() { Title = title, Runtime = runtime, Year = year, Country = country, Budget = budget, GenreId = genreid }, "movie");
            }
            else if (entity == "Rent")
            {
                Console.WriteLine("Enter Rent values: ");

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Age: ");
                int age = int.Parse(Console.ReadLine());

                Console.Write("Enter Gender: ");
                string gender = Console.ReadLine();

                Console.Write("Enter Country: ");
                string country = Console.ReadLine();

                Console.Write("Enter Rate: ");
                int rate = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Rental Start:");
                Console.Write("Year: ");
                int startYear = int.Parse(Console.ReadLine());
                Console.Write("Month: ");
                int startMonth = int.Parse(Console.ReadLine());
                Console.Write("Day: ");
                int startDay = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Rental End:");
                Console.Write("Year: ");
                int endYear = int.Parse(Console.ReadLine());
                Console.Write("Month: ");
                int endMonth = int.Parse(Console.ReadLine());
                Console.Write("Day: ");
                int endDay = int.Parse(Console.ReadLine());

                Console.Write("Enter MovieId: ");
                int movieid = int.Parse(Console.ReadLine());

                rest.Post(
                    new Rent() 
                    { Name = name, Age = age, Gender = gender, Country = country, Rating = rate,
                        Start = new DateTime(startYear,startMonth,startDay),
                        End = new DateTime(endYear,endMonth,endDay), MovieId = movieid }, "rent");
            }
        }
        static void List(string entity)
        {
            if (entity == "Genre")
            {
                List<Genre> genres = rest.Get<Genre>("genre");
                Console.WriteLine("GenreId, Value");
                foreach (var item in genres)
                {
                    Console.WriteLine(item.GenreId + ": " + item.Value);
                }
                
            }
            

            else if (entity == "Movie")
            {
                List<Movie> movies = rest.Get<Movie>("movie");
                Console.WriteLine("MovieId, Title, Runtime, Year, Country, Budget, GenreId");
                foreach (var item in movies)
                {
                    Console.WriteLine($"{item.MovieId} : {item.Title} , {item.Runtime} , {item.Year} , {item.Country} , {item.Budget} , {item.GenreId}");
                }
            }
            

            else if (entity == "Rent")
            {
                List<Rent> rents = rest.Get<Rent>("rent");
                Console.WriteLine("RentId, Name, Age, Gender, Country, Rate, Start, End, IntervalInDays, MovieId");
                foreach (var item in rents)
                {
                    Console.WriteLine($"{item.RentId} : {item.Name} , {item.Age} , {item.Gender} , {item.Country} , {item.Rating} , {item.Start.Year}.{item.Start.Month}.{item.Start.Day} , {item.End.Year}.{item.End.Month}.{item.End.Day} , {item.Interval} , {item.MovieId}");
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Genre")
            {
                Console.Write("Enter Genre's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Genre one = rest.Get<Genre>(id, "genre");

                Console.Write($"Enter new value (old: {one.Value}): ");
                string value = Console.ReadLine();

                one.Value = value;
                rest.Put(one, "genre");
            }
            else if (entity == "Movie")
            {
                Console.Write("Enter Movie's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Movie one = rest.Get<Movie>(id, "movie");

                foreach (var prop in one.GetType().GetProperties())
                {
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "MovieId")
                    {
                        Console.Write($"Enter new {prop.Name} (old: {prop.GetValue(one)}): ");
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(one, int.Parse(Console.ReadLine()));
                        }
                        else
                        {
                            prop.SetValue(one, Console.ReadLine());
                        }
                    }
                }

                rest.Put(one, "movie");
            }

            else if (entity == "Rent")
            {
                Console.Write("Enter Rent's Id to update: ");
                int id = int.Parse(Console.ReadLine());
                Rent one = rest.Get<Rent>(id, "rent");

                foreach (var prop in one.GetType().GetProperties())
                {
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "RentId" && prop.Name != "Interval")
                    {
                        Console.Write($"Enter new {prop.Name} (old: {prop.GetValue(one)}): ");
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            Console.WriteLine();
                            Console.Write("Year: ");
                            int year = int.Parse(Console.ReadLine());
                            Console.Write("Month: ");
                            int month = int.Parse(Console.ReadLine());
                            Console.Write("Day: ");
                            int day = int.Parse(Console.ReadLine());
                            prop.SetValue(one, new DateTime(year, month, day));
                        }
                        if (prop.PropertyType == typeof(int))
                        {
                            prop.SetValue(one, int.Parse(Console.ReadLine()));
                        }
                        if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(one, Console.ReadLine());
                        }
                    }
                }

                rest.Put(one, "rent");
            }

        }
        static void Delete(string entity)
        {
            if (entity == "Genre")
            {
                Console.Write("Enter Genre's Id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "genre");
            }
            else if (entity == "Movie")
            {
                Console.Write("Enter Movie's Id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "movie");
            }
            else if (entity == "Rent")
            {
                Console.Write("Enter Rent's Id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "rent");
            }
        }

        static void NONCRUD(string methodName)
        {
            if (methodName == "GetGenreWithAverageBudget")
            {
                var result = rest.Get<GenreWithAverageBudget>("GenreNonCRUDMethods/GetGenreWithAverageBudget");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Genre} - {item.BudgetAverage}");
                }
            }
            else if (methodName == "GetMoviesByGenre")
            {
                var result = rest.Get<MoviesByGenre>("GenreNonCRUDMethods/GetMoviesByGenre");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.GenreName}: ");
                    foreach (var title in item.MovieTitles)
                    {
                        Console.WriteLine($"\t{title}");
                    }
                }
            }
            else if (methodName == "GetMoviesAverageRating")
            {
                var result = rest.Get<MoviesAverageRating>("MovieNonCRUDMethods/GetMoviesAverageRating");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.MovieTitle} - {item.AverageRating}");
                }
            }
            else if (methodName == "GetRentalNameWithMovieTitleAndGenre")
            {
                var result = rest.Get<RentalNameWithMovieTitleAndGenre>("RentNonCRUDMethods/GetRentalNameWithMovieTitleAndGenre");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Name} - {item.MovieName} - {item.Genre}");
                }
            }
            else if (methodName == "GetNationalMovieRent")
            {
                var result = rest.Get<NationalMovieRent>("RentNonCRUDMethods/GetNationalMovieRent");
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.Name} - {item.Title} - {item.Country}");
                }
            }
            else if (methodName == "GetRentedMovieTitlesOfPerson")
            {
                Console.Write("Enter rent name: ");
                string name = Console.ReadLine();
                var result = rest.Get<RentedMovieTitleOfPerson>($"RentNonCRUDMethods/GetRentedMovieTitlesOfPerson?name={name}");
                Console.WriteLine("Name: " + name);
                Console.WriteLine("Rented movie titles:");
                foreach (var item in result)
                {
                    Console.WriteLine("--" + item.MovieTitle);
                }
            }
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:31652/", "genre");

            var genreSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Genre"))
                .Add("Create", () => Create("Genre"))
                .Add("Delete", () => Delete("Genre"))
                .Add("Update", () => Update("Genre"))
                .Add("Exit", ConsoleMenu.Close);

            var rentSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Rent"))
                .Add("Create", () => Create("Rent"))
                .Add("Delete", () => Delete("Rent"))
                .Add("Update", () => Update("Rent"))
                .Add("Exit", ConsoleMenu.Close);

            var movieSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Movie"))
                .Add("Create", () => Create("Movie"))
                .Add("Delete", () => Delete("Movie"))
                .Add("Update", () => Update("Movie"))
                .Add("Exit", ConsoleMenu.Close);

            var noncrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("Get Genre With Average Budget", () => NONCRUD("GetGenreWithAverageBudget"))
                .Add("Get Movies By Genre", () => NONCRUD("GetMoviesByGenre"))
                .Add("Get Movies Average Rating", () => NONCRUD("GetMoviesAverageRating"))
                .Add("Get Rental Name With Movie Title And Genre", () => NONCRUD("GetRentalNameWithMovieTitleAndGenre"))
                .Add("Get National Movie Rent", () => NONCRUD("GetNationalMovieRent"))
                .Add("Get Rented Movie Titles Of Person", () => NONCRUD("GetRentedMovieTitlesOfPerson"))
                .Add("Exit", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Movies", () => movieSubMenu.Show())
                .Add("Genre", () => genreSubMenu.Show())
                .Add("Rent", () => rentSubMenu.Show())
                .Add("NONCRUD", () => noncrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }
    }
}
