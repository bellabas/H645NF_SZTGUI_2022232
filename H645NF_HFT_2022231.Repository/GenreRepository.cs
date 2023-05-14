using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Repository
{
    public class GenreRepository : Repository<Genre>, IRepository<Genre>
    {
        public GenreRepository(MovieRentalDbContext ctx) : base(ctx)
        {

        }

        public override Genre Read(int id)
        {
            return ctx.Genres.FirstOrDefault(g => g.GenreId == id);
        }

        public override void Update(Genre item)
        {
            var old = Read(item.GenreId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "GenreId")
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
