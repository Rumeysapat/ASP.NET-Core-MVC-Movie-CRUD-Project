using DynamicData.Entity;
using Microsoft.EntityFrameworkCore;

namespace DynamicData.Data
{
    public static class DataSeeding
    {
        public static void Seed(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieContext>();

            context.Database.Migrate();

            // 1. GENRES
            if (!context.Genres.Any())
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Macera" },
                    new Genre { Name = "Komedi" },
                    new Genre { Name = "Romantik" },
                    new Genre { Name = "Savaş" }
                };
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }

            // Genre referanslarını al
            var macera = context.Genres.First(g => g.Name == "Macera");
            var komedi = context.Genres.First(g => g.Name == "Komedi");
            var romantik = context.Genres.First(g => g.Name == "Romantik");
            var savas = context.Genres.First(g => g.Name == "Savaş");

            // 2. MOVIES
            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie { MovieTitle = "Yeni Macera Filmi 1", MovieDescription = "Macera filmi açıklaması", MovieUrl = "1.png",Genres=new List<Genre>(){macera,komedi}},
                    new Movie { MovieTitle = "Hızlı Kaçış", MovieDescription = "Aksiyon dolu kaçış filmi", MovieUrl = "1.png", Genres=new List<Genre>(){macera,savas}},
                    new Movie { MovieTitle = "The Shawshank Redemption", MovieDescription = "Efsane film", MovieUrl = "1.png",Genres=new List<Genre>(){romantik,savas} },
                    new Movie { MovieTitle = "Inception", MovieDescription = "Zihin hırsızlığı", MovieUrl = "1.png",Genres=new List<Genre>(){komedi,savas} },
                    new Movie { MovieTitle = "Komedi Filmi", MovieDescription = "Komik sahneler", MovieUrl = "1.png",Genres=new List<Genre>(){macera,savas} },
                    new Movie { MovieTitle = "Savaş Filmi", MovieDescription = "Savaş sahneleri", MovieUrl = "1.png",Genres=new List<Genre>(){komedi,savas} }
                };
                context.Movies.AddRange(movies);
                context.SaveChanges();
            }

            // 3. USERS
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User { UserName = "rumeysa", Email = "rumeysa@example.com", Password = "12345", ImageUrl = "user1.png" },
                    new User { UserName = "mehmet", Email = "mehmet@example.com", Password = "54321", ImageUrl = "user2.png" },
                    new User { UserName = "ayse", Email = "ayse@example.com", Password = "99999", ImageUrl = "user3.png" }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }

            // 4. PERSONS
            if (!context.People.Any())
            {
                var allUsers = context.Users.ToList(); // UserId'ler DB'den alınır
                var persons = new List<Person>
                {
                    new Person { Name = "Rumeysa Pat", Biography = "Yapay zeka ve .NET geliştiricisi", UserId = allUsers[0].UserId },
                    new Person { Name = "Mehmet Yılmaz", Biography = "Full stack developer", UserId = allUsers[1].UserId },
                    new Person { Name = "Ayşe Demir", Biography = "Film yorumcusu", UserId = allUsers[2].UserId }
                };
                context.People.AddRange(persons);
                context.SaveChanges();
            }

            // 5. CREWS
            if (!context.Crews.Any())
            {
                var allMovies = context.Movies.ToList();
                var allPersons = context.People.ToList();
                var crews = new List<Crew>
                {
                    new Crew { MovieId = allMovies[0].MovieId, PersonId = allPersons[0].PersonId, Job = "Yönetmen" },
                    new Crew { MovieId = allMovies[0].MovieId, PersonId = allPersons[1].PersonId, Job = "Yapımcı" },
                    new Crew { MovieId = allMovies[1].MovieId, PersonId = allPersons[2].PersonId, Job = "Oyuncu" }
                };
                context.Crews.AddRange(crews);
                context.SaveChanges();
            }

            // 6. CASTS
            if (!context.Casts.Any())
            {
                var allMovies = context.Movies.ToList();
                var allPersons = context.People.ToList();
                var casts = new List<Cast>
                {
                    new Cast { MovieId = allMovies[0].MovieId, PersonId = allPersons[0].PersonId, Character = "Karakter 1", Name = "İsim 1" },
                    new Cast { MovieId = allMovies[0].MovieId, PersonId = allPersons[1].PersonId, Character = "Karakter 2", Name = "İsim 2" },
                    new Cast { MovieId = allMovies[1].MovieId, PersonId = allPersons[2].PersonId, Character = "Karakter 3", Name = "İsim 3" }
                };
                context.Casts.AddRange(casts);
                context.SaveChanges();
            }
        }
    }
}
