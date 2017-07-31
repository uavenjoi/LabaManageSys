using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста имя пользователя")]
        [Display(Name = "Имя пользователя")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пожалуйста email пользователя")]
        [Display(Name = "Email пользователя")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Пароль пользователя")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int RoleId { get; set; }
    }
}