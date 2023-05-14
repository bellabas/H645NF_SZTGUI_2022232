using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Repository
{
    public class MovieRepository : Repository<Movie>, IRepository<Movie>
    {
        public MovieRepository(MovieRentalDbContext ctx) : base(ctx)
        {

        }

        public override Movie Read(int id)
        {
            return ctx.Movies.FirstOrDefault(m => m.MovieId == id);
        }

        public override void Update(Movie item)
        {
            var old = Read(item.MovieId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "MovieId")
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
