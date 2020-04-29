using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiBase.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O endereço fornecido é inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O login é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }
    }
}
