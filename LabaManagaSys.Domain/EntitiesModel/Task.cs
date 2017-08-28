using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Task
    {
        [Required]
        public int TaskId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Уровень сложности")]
        public int Level { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Наименование предмета")]
        public string Topic { get; set; }

        [Required]
        [Display(Name = "Текст задания")]
        public string Text { get; set; }
    }
}
