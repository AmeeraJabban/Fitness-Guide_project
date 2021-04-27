using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessAmeera.Models
{
    public class FitnessTips
    {
        public int Id { get; set; } // Primary Key


        [Required]
        [DisplayName("الموجز ")]
        public string TipSummary { get; set; }


        [Required]
        [DisplayName("العنوان ")]
        public string TipTitle { get; set; }



        [Required]
        [AllowHtml]
        [DisplayName("التفاصيل ")]
        public string TipDetails { get; set; }

        [Required]
        [DisplayName("الحالة ")]
        public string TipStatus { get; set; }

        [DisplayName("الصورة ")]
        public string TipImage { get; set; }



        [Required]
        [DisplayName("التاريخ ")]
        public DateTime TipDate { get; set; }



        [Display(Name = "كاتب المقال")]
        public string UserID { get; set; }          //Foergin Key
        public virtual ApplicationUser User { get; set; }



        [Required]
        [DisplayName("النوع ")]
        public int ArticalesTypesID { get; set; }          //Foergin Key
        public virtual ArticalesTypes ArticlesTypes { get; set; }

    }
}