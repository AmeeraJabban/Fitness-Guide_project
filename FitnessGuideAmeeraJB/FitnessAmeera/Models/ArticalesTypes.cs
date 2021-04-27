using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
                                  // ameera_ جدول أصناف المقالات*@          45149*@

namespace FitnessAmeera.Models
{
    public class ArticalesTypes
    {
        public int Id { get; set; } // Primary Key


        [Required]
        [DisplayName( "صنف المقال")]
        public string Type { get; set; }


        [Required]
        [DisplayName("وصف الصنف")]
        public string Description { get; set; }


                                                 // many articles to one type
        public virtual ICollection<Articles> Articles { get; set; }

    }
}