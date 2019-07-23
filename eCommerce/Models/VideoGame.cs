using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents a videogame.
    /// </summary>
    public class VideoGame
    {
        /// <summary>
        /// Unique ID number for the game.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Game Title
        /// </summary>
        [Required]
        [StringLength(90)]
        public string Title { get; set; }

        /// <summary>
        /// Official ESRB Rating
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Game Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Retail Price for the game.
        /// </summary>
       [Range(.01, 999.99)]
       [DataType(DataType.Currency)]
       // Required by default becasue double is a value type
        public double Price { get; set; }
    }
}
