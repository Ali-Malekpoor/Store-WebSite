using System.ComponentModel.DataAnnotations;

namespace WebSite.EndPoint.Models.ViewModels.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "نام و نام خانوادگی را وارد نمایید")]
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(50, ErrorMessage = "نام و نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "ایمیل را وارد نمایید")]
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "رمز عبور را وارد نمایید")]
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن باید برابر باشند")]
        public string RePassword { get; set; }
        [Display(Name = "موبایل")]
        public string PhoneNumber { get; set; }

    }
}
