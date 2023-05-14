using H645NF_HFT_2022231.Logic;
using H645NF_HFT_2022231.Models;
using H645NF_HFT_2022231.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H645NF_HFT_2022231.Test
{
    [TestFixture]
    public class Tester
    {
        MovieLogic movieLogic;
        GenreLogic genreLogic;
        RentLogic rentLogic;

        Mock<IRepository<Movie>> mockMovieRepository;
        Mock<IRepository<Genre>> mockGenreRepository;
        Mock<IRepository<Rent>> mockRentRepository;

        [Test]
        public void GetMoviesAverageRatingTest()
        {
            //ARRANGE
            var inputdata = new List<Movie>()
            {
                new Movie(){MovieId = 1, Title = "Fight Club", Rents = new List<Rent>(){ new Rent() { Rating = 10}, new Rent() { Rating = 9} } }
            }.AsQueryable();

            mockMovieRepository = new Mock<IRepository<Movie>>();
            mockMovieRepository.Setup(m => m.ReadAll()).Returns(inputdata);
            
            movieLogic = new MovieLogic(mockMovieRepository.Object);

            var expected = new List<MoviesAverageRating>() { new MoviesAverageRating() { MovieTitle = "Fight Club", AverageRating = 9.5 } };

            //ACT
            var result = movieLogic.GetMoviesAverageRating();

            //ASSERT
            Assert.That(result.FirstOrDefault().MovieTitle == expected.FirstOrDefault().MovieTitle && result.FirstOrDefault().AverageRating == expected.FirstOrDefault().AverageRating);
        }

        [Test]
        public void GetGenreWithAverageBudgetTest()
        {
            //ARRANGE
            var inputdata = new List<Genre>()
            {
                new Genre()
                {
                    GenreId = 1, Value = "Horror",
                    Movies = new List<Movie>(){ new Movie() { Budget = 100}, new Movie() { Budget = 200} }
                },
                new Genre()
                {
                    GenreId = 2, Value = "Comedy",
                    Movies = new List<Movie>(){ new Movie() { Budget = 50}, new Movie() { Budget = 500} }
                }
            }.AsQueryable();

            mockGenreRepository = new Mock<IRepository<Genre>>();
            mockGenreRepository.Setup(m => m.ReadAll()).Returns(inputdata);

            genreLogic = new GenreLogic(mockGenreRepository.Object);

            var expected = new List<GenreWithAverageBudget>()
            { 
                new GenreWithAverageBudget() { Genre = "Horror", BudgetAverage = 150 },
                new GenreWithAverageBudget() { Genre = "Comedy", BudgetAverage = 275 }
            };

            //ACT
            var result = genreLogic.GetGenreWithAverageBudget();

            //ASSERT
            Assert.That(result.FirstOrDefault(t => t.Genre == "Horror").BudgetAverage == expected.FirstOrDefault(t => t.Genre == "Horror").BudgetAverage);
            Assert.That(result.FirstOrDefault(t => t.Genre == "Comedy").BudgetAverage == expected.FirstOrDefault(t => t.Genre == "Comedy").BudgetAverage);
        }

        [Test]
        public void GetMoviesByGenreTest()
        {
            //ARRANGE
            var inputdata = new List<Genre>()
            {
                new Genre()
                {
                    GenreId = 1, Value = "Horror",
                    Movies = new List<Movie>(){ new Movie() { Title = "Terrifier 1"}, new Movie() { Title = "Terrifier 2"} }
                },
                new Genre()
                {
                    GenreId = 2, Value = "Drama",
                    Movies = new List<Movie>(){ new Movie() { Title = "Fight Club"}, new Movie() { Title = "Marriage Story" } }
                }
            }.AsQueryable();

            mockGenreRepository = new Mock<IRepository<Genre>>();
            mockGenreRepository.Setup(m => m.ReadAll()).Returns(inputdata);

            genreLogic = new GenreLogic(mockGenreRepository.Object);

            var expected = new List<MoviesByGenre>()
            {
                new MoviesByGenre() { GenreName = "Horror", MovieTitles = new List<string>() { "Terrifier 1", "Terrifier 2" } },
                new MoviesByGenre() { GenreName = "Drama", MovieTitles = new List<string>() { "Fight Club", "Marriage Story" } }
            };

            //ACT
            var result = genreLogic.GetMoviesByGenre();

            //ASSERT
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Horror").MovieTitles.Count() == 2);
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Drama").MovieTitles.Count() == 2);
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Horror").MovieTitles.Count() == expected.FirstOrDefault(t => t.GenreName == "Horror").MovieTitles.Count());
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Drama").MovieTitles.Count() == expected.FirstOrDefault(t => t.GenreName == "Drama").MovieTitles.Count());
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Horror").MovieTitles.Contains("Terrifier 1"));
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Horror").MovieTitles.Contains("Terrifier 2"));
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Drama").MovieTitles.Contains("Fight Club"));
            Assert.That(result.FirstOrDefault(t => t.GenreName == "Drama").MovieTitles.Contains("Marriage Story"));
        }

        [Test]
        public void GetRentalNameWithMovieTitleAndGenreTest()
        {
            //ARRANGE
            var inputdata = new List<Rent>()
            {
                new Rent()
                {
                    Name = "Kiss János",
                    Movie = new Movie(){Title = "Joker", Genre = new Genre(){ Value = "Drama"} }
                },
                new Rent()
                {
                    Name = "Kovács Mária",
                    Movie = new Movie(){Title = "Interstellar", Genre = new Genre(){ Value = "Sci-fi"} }
                }
            }.AsQueryable();

            mockRentRepository = new Mock<IRepository<Rent>>();
            mockRentRepository.Setup(m => m.ReadAll()).Returns(inputdata);

            rentLogic = new RentLogic(mockRentRepository.Object);

            var expected = new List<RentalNameWithMovieTitleAndGenre>()
            {
                new RentalNameWithMovieTitleAndGenre(){Name = "Kiss János", MovieName = "Joker", Genre = "Drama"},
                new RentalNameWithMovieTitleAndGenre(){Name = "Kovács Mária", MovieName = "Interstellar", Genre = "Sci-fi"}
            };

            //ACT
            var result = rentLogic.GetRentalNameWithMovieTitleAndGenre();

            //ASSERT
            Assert.That(result.Count() == 2);

            Assert.That(result.FirstOrDefault(t => t.Name == "Kiss János").Name == expected.FirstOrDefault(t => t.Name == "Kiss János").Name);
            Assert.That(result.FirstOrDefault(t => t.Name == "Kiss János").MovieName == expected.FirstOrDefault(t => t.Name == "Kiss János").MovieName);
            Assert.That(result.FirstOrDefault(t => t.Name == "Kiss János").Genre == expected.FirstOrDefault(t => t.Name == "Kiss János").Genre);

            Assert.That(result.FirstOrDefault(t => t.Name == "Kovács Mária").Name == expected.FirstOrDefault(t => t.Name == "Kovács Mária").Name);
            Assert.That(result.FirstOrDefault(t => t.Name == "Kovács Mária").MovieName == expected.FirstOrDefault(t => t.Name == "Kovács Mária").MovieName);
            Assert.That(result.FirstOrDefault(t => t.Name == "Kovács Mária").Genre == expected.FirstOrDefault(t => t.Name == "Kovács Mária").Genre);
        }

        [Test]
        public void GetNationalMovieRentTest()
        {
            //ARRANGE
            var inputdata = new List<Rent>()
            {
                new Rent()
                {
                    Name = "Kiss János",
                    Country = "Hungary",
                    Movie = new Movie(){Title = "Toxikoma", Country = "Hungary" },
                },
                new Rent()
                {
                    Name = "Kovács Mária",
                    Country = "Hungary",
                    Movie = new Movie(){Title = "Interstellar", Country = "USA" }
                }
            }.AsQueryable();

            mockRentRepository = new Mock<IRepository<Rent>>();
            mockRentRepository.Setup(m => m.ReadAll()).Returns(inputdata);

            rentLogic = new RentLogic(mockRentRepository.Object);

            var expected = new List<NationalMovieRent>()
            {
                new NationalMovieRent(){Name="Kiss János", Country="Hungary", Title = "Toxikoma"}
            };

            //ACT
            var result = rentLogic.GetNationalMovieRent();

            //ASSERT
            Assert.That(result.Count() == 1);
            Assert.That(result.FirstOrDefault().Name == expected.FirstOrDefault().Name);
            Assert.That(result.FirstOrDefault().Country == expected.FirstOrDefault().Country);
            Assert.That(result.FirstOrDefault().Title == expected.FirstOrDefault().Title);
        }

        [Test]
        public void GetRentedMovieTitlesOfPersonTest()
        {
            //ARRANGE
            var inputdata = new List<Rent>()
            {
                new Rent()
                {
                    Name = "Kiss János",
                    Movie = new Movie(){Title = "Toxikoma" },
                },
                new Rent()
                {
                    Name = "Móka Mátyás",
                    Movie = new Movie(){Title = "Silent Hill"}
                },
                new Rent()
                {
                    Name = "Kiss János",
                    Movie = new Movie(){Title = "Interstellar" }
                },
                new Rent()
                {
                    Name = "Nagy Franciska",
                    Movie = new Movie(){Title = "Fight Club"}
                }
            }.AsQueryable();

            mockRentRepository = new Mock<IRepository<Rent>>();
            mockRentRepository.Setup(m => m.ReadAll()).Returns(inputdata);

            rentLogic = new RentLogic(mockRentRepository.Object);

            var expected = new List<RentedMovieTitleOfPerson>()
            {
                new RentedMovieTitleOfPerson(){Name = "Kiss János", MovieTitle = "Toxikoma"},
                new RentedMovieTitleOfPerson(){Name = "Kiss János", MovieTitle = "Interstellar"}
            };

            //ACT
            var result = rentLogic.GetRentedMovieTitlesOfPerson("Kiss János");

            //ASSERT
            Assert.That(result.Count() == expected.Count());
        }

        [Test]
        public void MovieLogicCreateTest()
        {
            //ARRANGE
            mockMovieRepository = new Mock<IRepository<Movie>>();
            mockMovieRepository.Setup(m => m.ReadAll()).Returns(new List<Movie>().AsQueryable());

            Movie nullMovie = null;
            var goodMovie = new Movie() { Title = "It", Runtime = 120, Year = 2016, Budget = 200, Country = "US", GenreId = 1 };
            var badMovie = new Movie() { Title = "It", Runtime = 120, Year = 1000, Budget = 200, Country = "US", GenreId = 1 };

            movieLogic = new MovieLogic(mockMovieRepository.Object);

            //ACT ASSERT
            Assert.That(() => movieLogic.Create(new Movie()), Throws.ArgumentException);
            Assert.That(() => movieLogic.Create(new Movie() { MovieId = 1}), Throws.ArgumentException);
            Assert.That(() => movieLogic.Create(badMovie), Throws.Exception);
            Assert.That(() => movieLogic.Create(nullMovie), Throws.ArgumentNullException);

            try
            {
                movieLogic.Create(goodMovie);
            }
            catch
            {

            }
            mockMovieRepository.Verify(m => m.Create(goodMovie),Times.Once);
        }

        [Test]
        public void GenreLogicCreateTest()
        {
            //ARRANGE
            mockGenreRepository = new Mock<IRepository<Genre>>();
            mockGenreRepository.Setup(m => m.ReadAll()).Returns(new List<Genre>().AsQueryable());

            Genre nullGenre = null;
            var goodGenre = new Genre() { Value = "abc"};
            var badGenre = new Genre() { Value = "ab" };

            genreLogic = new GenreLogic(mockGenreRepository.Object);

            //ACT ASSERT
            Assert.That(() => genreLogic.Create(new Genre()), Throws.Exception);
            Assert.That(() => genreLogic.Create(new Genre() { GenreId = 1 }), Throws.Exception);
            Assert.That(() => genreLogic.Create(goodGenre), Throws.Nothing);
            Assert.That(() => genreLogic.Create(nullGenre), Throws.ArgumentNullException);

            try
            {
                genreLogic.Create(badGenre);
            }
            catch
            {

            }
            mockGenreRepository.Verify(g => g.Create(badGenre), Times.Never);
        }

        [Test]
        public void RentLogicCreateTest()
        {
            //ARRANGE
            mockRentRepository = new Mock<IRepository<Rent>>();
            mockRentRepository.Setup(m => m.ReadAll()).Returns(new List<Rent>().AsQueryable());

            Rent nullRent = null;
            var goodRent = new Rent() { Name = "Kis Jozsef", Age = 16, Country = "Hungary", Gender = "male", Rating = 1, Start = new DateTime(2016,12,12), End =  new DateTime(2016,12,13), MovieId = 1};
            var badRent = new Rent() { Name = "Kis Jozsef", Age = 15, Country = "Hungary", Gender = "male", Rating = 1, Start = new DateTime(2016, 12, 12), End = new DateTime(2016, 12, 13), MovieId = 1 };

            rentLogic = new RentLogic(mockRentRepository.Object);

            //ACT ASSERT
            Assert.That(() => rentLogic.Create(new Rent()), Throws.ArgumentException);
            Assert.That(() => rentLogic.Create(new Rent() { RentId = 1 }), Throws.ArgumentException);
            Assert.That(() => rentLogic.Create(goodRent), Throws.Nothing);
            Assert.That(() => rentLogic.Create(badRent), Throws.Exception);

            try
            {
                rentLogic.Create(nullRent);
            }
            catch
            {

            }

            mockRentRepository.Verify(m => m.Create(nullRent), Times.Never);
        }

        [Test]
        public void MovieLogicUpdateTest()
        {
            //ARRANGE
            mockMovieRepository = new Mock<IRepository<Movie>>();
            mockMovieRepository.Setup(m => m.ReadAll()).Returns(new List<Movie>().AsQueryable());

            Movie nullMovie = null;
            var goodMovie = new Movie() { Title = "It", Runtime = 120, Year = 2016, Budget = 200, Country = "US", GenreId = 1 };
            var badMovie = new Movie() { Title = "It", Runtime = 120, Year = 1000, Budget = 200, Country = "US", GenreId = 1 };

            movieLogic = new MovieLogic(mockMovieRepository.Object);

            //ACT ASSERT
            Assert.That(() => movieLogic.Update(new Movie()), Throws.ArgumentNullException);
            Assert.That(() => movieLogic.Update(new Movie() { MovieId = 1 }), Throws.ArgumentNullException);
            Assert.That(() => movieLogic.Update(goodMovie), Throws.Nothing);
            Assert.That(() => movieLogic.Update(badMovie), Throws.Exception);
            Assert.That(() => movieLogic.Update(nullMovie), Throws.ArgumentNullException);
        }

        [Test]
        public void GenreLogicReadTest()
        {
            //ARRANGE
            mockGenreRepository = new Mock<IRepository<Genre>>();
            mockGenreRepository.Setup(m => m.Read(0)).Returns(new Genre() { GenreId = 0, Value = "Crime"});
            mockGenreRepository.Setup(m => m.Read(1)).Returns(new Genre() { GenreId = 1, Value = "Thriller"});

            genreLogic = new GenreLogic(mockGenreRepository.Object);

            var expected = new Genre() { GenreId = 0, Value = "Crime" };

            //ACT
            var result = genreLogic.Read(0);

            //ASSERT
            Assert.That(result.GenreId == expected.GenreId);
            Assert.That(result.Value == expected.Value);
        }

    }
}
