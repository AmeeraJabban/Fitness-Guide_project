using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessAmeera.Models
{                                 
    public class MySavedArticles
    {
        public int Id { get; set; }  //primary key

        public int articlesId { get; set; }   //foerign key
        public virtual Articles articles { get; set; }

        public String UserId { get; set; }   //foerign key
        public virtual ApplicationUser User { get; set; }

    }
}