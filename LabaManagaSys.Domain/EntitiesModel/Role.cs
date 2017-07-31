using System.ComponentModel.DataAnnotations;

namespace LabaManageSys.Domain.EntitiesModel
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}