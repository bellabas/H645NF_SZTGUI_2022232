using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace H645NF_HFT_2022231.Models
{
    public class Rent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public int Rating { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Interval
        {
            get
            {
                return (End - Start).Days;
            }
        }
        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Movie Movie { get; set; }

        public Rent()
        {

        }

        public Rent(int rentid, string name, int age, string gender, string country, int rating, DateTime start, DateTime end, int movieid)
        {
            RentId = rentid;
            Name = name;
            Age = age;
            Gender = gender;
            Country = country;
            Rating = rating;
            Start = start;
            End = end;
            MovieId = movieid;
        }
    }
}
