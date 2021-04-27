using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
                           // ameera_بيانات المستخدمين *@          45149*@
namespace FitnessAmeera.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }
                                                //Login
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "البريد الإلكتروني")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
                                                // Register
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "العمر")]
        public string Age { get; set; }

        [Required]
        [Display(Name = "الجنس")]
        public string Sex { get; set; }

        [Required]
        [DisplayName ( "الوزن")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "الطول")]
        public int Heigh { get; set; }

        [Display(Name = "الحالة")]
        public string status { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "يرجى أختيار كلمة مرور أكثر أماناً.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور غير مطابقة.")]
        public string ConfirmPassword { get; set; }
    }
                                          //Reset_Password
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يرجى إعادة تعيين كلمة المرور .", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("Password", ErrorMessage = "كلمة المرور غير مطابقة.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "البريد الإلكتروني")]
        public string Email { get; set; }
    }
}
