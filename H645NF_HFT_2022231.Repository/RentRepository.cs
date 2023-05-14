using H645NF_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Repository
{
    public class RentRepository : Repository<Rent>, IRepository<Rent>
    {
        public RentRepository(MovieRentalDbContext ctx) : base(ctx)
        {

        }

        public override Rent Read(int id)
        {
            return ctx.Rentals.FirstOrDefault(r => r.RentId == id);
        }

        public override void Update(Rent item)
        {
            var old = Read(item.RentId); //get the old rent by id
            foreach (var prop in old.GetType().GetProperties()) //iterating over on the old rent's properties
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null && prop.Name != "RentId" && prop.Name != "Interval")
                {
                    prop.SetValue(old, prop.GetValue(item)); //setting the properties on the old object using the new rent's same name property value
                }
            }
            ctx.SaveChanges();
        }
    }
}
