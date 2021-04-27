using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// ameera_ جدول معلومات المقال*@          45149*@
namespace FitnessAmeera.Models
{
    public class Articles
    {

        public int Id { get; set; } // Primary Key


        [Required]
        [DisplayName("موجز المقال")]
        public string ArticleSummary { get; set; }


        [Required]
        [DisplayName("عنوان المقال")]
        public string ArticleTitle { get; set; }



        [Required]
        [AllowHtml]
        [DisplayName("تفاصيل المقال")]
        public string ArticleDetails { get; set; }


        [DisplayName("صورة المقال")]
        public string ArticleImage { get; set; }



        [Required]
        [DisplayName("تاريخ المقال")]
        public DateTime ArticleDate { get; set; }



        [Display(Name ="كاتب المقال")]
        public string UserID { get; set; }          //Foergin Key
        public virtual ApplicationUser User { get; set; } 


        [Required]
        [DisplayName("نوع المقال")]
        public int ArticalesTypesID { get; set; }          //Foergin Key
        public virtual ArticalesTypes ArticlesTypes { get; set; }






    }
}