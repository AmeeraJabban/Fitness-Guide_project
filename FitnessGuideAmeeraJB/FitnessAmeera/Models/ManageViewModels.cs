using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.ComponentModel;

namespace FitnessAmeera.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }
    public class ProfileViewModel
    {

        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("الوزن")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "الطول")]
        public int Heigh { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يرجى كتابة كلمة المرور بشكل صحيح.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الحالية")]
        public string Current_Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يرجى أختيار كلمة مرور أكثر أماناً.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string New_Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور الجديدة")]
        [Compare("New_Password", ErrorMessage = "كلمة المرور غير مطابقة.")]
        public string ConfirmPassword { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "يرجى أختيار كلمة مرور أكثر أماناً.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور غير مطابقة.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الحالية")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "يرجى أختيار كلمة مرور أكثر أماناً.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور الجديدة")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تأكيد كلمة المرور")]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور غير مطابقة.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "رقم الهاتف")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "الرمز")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}