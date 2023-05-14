using H645NF_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace H645NF_HFT_2022231.Repository
{
    public class MovieRentalDbContext : DbContext
    {
        public DbSet<Rent> Rentals { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public MovieRentalDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseInMemoryDatabase("movierental")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(t => t.Genre)
                .WithMany(t => t.Movies)
                .HasForeignKey(t => t.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rent>()
                .HasOne(t => t.Movie)
                .WithMany(t => t.Rents)
                .HasForeignKey(t => t.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            //2,147,483,647 max budget

            modelBuilder.Entity<Movie>().HasData(new Movie[]
            {
                //       ID,    Title,   Runtime, Year, Country, Budget, GenreID
                new Movie(1, "Interstellar", 169, 2014, "USA", 165000000, 1),
                new Movie(2, "All Quiet on the Western Front", 148, 2022, "Germany", 16000000, 2),
                new Movie(3, "The Good Nurse", 121, 2022, "USA", 20000000, 2),
                new Movie(4, "Joker", 122, 2019, "Canada", 55000000, 3),
                new Movie(5, "Fight Club", 139, 1999, "Germany", 63000000, 2),
                new Movie(6, "The Dark Knight", 152, 2008, "USA", 185000000, 5),
                new Movie(7, "Pineapple Express", 111, 2008, "USA", 27000000, 6),
                new Movie(8, "The Wolf of Wall Street", 180, 2013, "USA", 100000000, 6),
                new Movie(9, "Terrifier", 85, 2016, "USA", 35000, 4),
                new Movie(10, "It", 135, 2017, "Canada", 35000000, 4),
                new Movie(11, "The Prestige", 130, 2006, "UK", 40000000, 3),
                new Movie(12, "Midsommar", 148, 2019, "Sweden", 9000000, 3),
                new Movie(13, "Toxikoma", 124, 2021, "Hungary", 1379586, 2),
                new Movie(14, "Parasite", 132, 2019, "Korea", 11400000, 3),
                new Movie(15, "Silent Hill", 125, 2006, "Canada", 50000000, 4),
                new Movie(16, "American Psycho", 102, 2000, "Canada", 7000000, 2),
                new Movie(17, "Snowpiercer", 126, 2013, "Germany", 39200000, 1)
            });

            modelBuilder.Entity<Genre>().HasData(new Genre[]
            {
                new Genre(1, "Sci-fi"), //1
                new Genre(2, "Drama"), //5
                new Genre(3, "Thriller"), //4
                new Genre(4, "Horror"), //3
                new Genre(5, "Action"), //1
                new Genre(6, "Comedy") //2
            });

            modelBuilder.Entity<Rent>().HasData(new Rent[]
            {
                //       id, name, age, gender, country, rate, start, end, movieID

                //Interstellar (6)
                new Rent(1, "Jeremy Easton", 30, "male", "Canada", 10, new DateTime(2020,1,2), new DateTime(2020,1,3), 1),
                new Rent(2, "Balázs Bodnár", 21, "male", "Hungary", 10, new DateTime(2020,7,15), new DateTime(2020,8,1), 1),
                new Rent(3, "Zsóka Kis", 60, "female", "Hungary", 9, new DateTime(2020,2,2), new DateTime(2020,2,26), 1),
                new Rent(4, "Frank Clarkson", 55, "male", "Canada", 10, new DateTime(2020,4,10), new DateTime(2020,4,20), 1),
                new Rent(5, "Raffael Schreck", 37, "male", "Germany", 9, new DateTime(2021,12,12), new DateTime(2021,12,24), 1),
                new Rent(6, "Jeremy Easton", 30, "male", "Canada", 10, new DateTime(2020,3,2), new DateTime(2020,3,3), 1), //second watch

                //no rental with id 2

                //The Good Nurse (1)
                new Rent(7, "Kandace Kirby", 40, "female", "Canada", 8, new DateTime(2022,11,11), new DateTime(2022,11,14), 3),

                //Joker (2)
                //only females and usa and one person
                new Rent(8, "Fiona Bennington", 31, "female", "USA", 9, new DateTime(2021,6,15), new DateTime(2021,6,21), 4),
                new Rent(9, "Fiona Bennington", 31, "female", "USA", 9, new DateTime(2021,7,20), new DateTime(2021,7,21), 4), //second watch

                //Fight Club (4)
                //only males
                new Rent(10, "Rain Eason", 39, "male", "UK", 8, new DateTime(2019,10,10), new DateTime(2019,10,18), 5),
                new Rent(11, "Merton Senior", 26, "male", "UK", 9, new DateTime(2020,2,2), new DateTime(2020,2,15), 5),
                new Rent(12, "Merton Senior", 26, "male", "UK", 9, new DateTime(2020,11,11), new DateTime(2020,11,20), 5), //second watch
                new Rent(13, "Keiran Holmes", 60, "male", "USA", 10, new DateTime(2021,12,30), new DateTime(2021,12,31), 5),

                //The Dark Knight (5)
                new Rent(14, "Sean Weaver", 15, "male", "USA", 8, new DateTime(2019,1,15), new DateTime(2019,1,17), 6),
                new Rent(15, "Elea Newport", 12, "female", "USA", 10, new DateTime(2020,11,18), new DateTime(2020,12,1), 6),
                new Rent(16, "Laurena Simms", 60, "female", "Canada", 5, new DateTime(2020,10,19), new DateTime(2020,10,22), 6),
                new Rent(17, "Justine Stack", 18, "female", "UK", 10, new DateTime(2021,7,13), new DateTime(2021,7,23), 6),
                new Rent(18, "Corey Statham", 24, "male", "USA", 6, new DateTime(2020,2,14), new DateTime(2020,2,16), 6),

                //Pineapple Express (2)
                //only males
                new Rent(19, "Fülöp Görög", 21, "male", "Hungary", 5, new DateTime(2021,9,9), new DateTime(2021,9,11), 7),
                new Rent(20, "Reagan Fulton", 22, "male", "USA", 9, new DateTime(2020,12,10), new DateTime(2020,12,12), 7),

                //The Wolf of Wall Street (3)
                //only 10 ratings and females and germany
                new Rent(21, "Claudia Voigt", 30, "female", "Germany", 10, new DateTime(2019,6,15), new DateTime(2019,7,1), 8),
                new Rent(22, "Anuschka Fux", 29, "female", "Germany", 10, new DateTime(2020,4,10), new DateTime(2020,4,20), 8),
                new Rent(23, "Corinna Waxweiler", 27, "female", "Germany", 10, new DateTime(2020,10,10), new DateTime(2020,10,30), 8),

                //Terrifier (2)
                //only 18 yos and germany
                new Rent(24, "Nagy Gabriella", 18, "female", "Germany", 5, new DateTime(2021,6,1), new DateTime(2021,6,8), 9),
                new Rent(25, "Edgar Haber", 18, "male", "Germany", 3, new DateTime(2021,10,28), new DateTime(2021,11,5), 9),

                //It (1)
                new Rent(26, "Árpád Orbán", 28, "male", "Hungary", 7, new DateTime(2022,2,15), new DateTime(2022,2,27), 10),

                //The Prestige
                new Rent(27, "Anna Bartos", 29, "female", "Hungary", 8, new DateTime(2018,8,1), new DateTime(2018,8,12), 11),

                //Silent Hill (2)
                //only males and 18 yos and 6 ratings
                new Rent(28, "Gunther Michaelis", 18, "male", "Germany", 6, new DateTime(2022,4,13), new DateTime(2022,5,1), 15),
                new Rent(29, "Bandi Prohászka", 18, "male", "Hungary", 6, new DateTime(2022,8,10), new DateTime(2022,8,30), 15),

                //American Psycho
                //every country
                new Rent(30, "Zackery Walters", 36, "male", "USA", 7, new DateTime(2016,1,5), new DateTime(2016,1,10), 16),
                new Rent(31, "Frances Dabney", 31, "female", "UK", 9, new DateTime(2021,8,2), new DateTime(2021,8,3), 16),
                new Rent(32, "Alvin Fear", 57, "male", "Canada", 10, new DateTime(2016,7,10), new DateTime(2016,7,15), 16),
                new Rent(33, "Nelly Beitel", 60, "female", "Germany", 6, new DateTime(2017,4,20), new DateTime(2017,4,29), 16),
                new Rent(34, "Feri Gábor", 48, "male", "Hungary", 6, new DateTime(2021,9,1), new DateTime(2021,9,5), 16),
                new Rent(35, "Bora Yoon", 19, "female", "Korea", 8, new DateTime(2018,5,11), new DateTime(2018,5,13), 16),


                //Snowpiercer
                new Rent(36, "Helmfried Hase", 50, "male", "Germany", 1, new DateTime(2022,3,25), new DateTime(2022,3,26), 17)

                //new Rent(00, "", 00, "", "", 00, new DateTime(), new DateTime(), 00),

            });
        }
    }
}
