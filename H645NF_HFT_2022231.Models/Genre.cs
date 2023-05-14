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
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GenreId { get; set; }
        public string Value { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Movie> Movies { get; set; }

        public Genre()
        {
            
        }

        public Genre(int genreid, string value)
        {
            GenreId = genreid;
            Value = value;
        }
    }
}
