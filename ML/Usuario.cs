using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        
        [StringLength(50)]
        public string Nombre { get; set; }
      
        [DisplayName("Apellido Paterno")]
        [StringLength(50)]
        public string ApellidoPaterno { get; set; }

        
        [DisplayName("Apellido Materno")]
        [StringLength(50)]
        public string ApellidoMaterno { get; set; }
        
        [DisplayName("Fecha De Nacimiento")]
        //[RegularExpression(@"^(0?[1 - 9] |[12][0 - 9] | 3[01])[\/](0?[1 - 9] | 1[012])[/\\/](19 | 20)\d{2}$")]
        public string FechaDeNacimiento { get; set; }
        [Required]
        public string Sexo { get; set; }
        /// /////
        
        [StringLength(50)]
        public string UserName { get; set; }

      
        [EmailAddress]
        public string Email { get; set; }
       
        public string Password { get; set; }
       
        [Phone]
        public string Telefono { get; set; }
        public string Celular { get; set; }
       
        public string CURP { get; set; }

        public List<object> Usuarios { get; set; }

        //Propiedad de navegacion
        public ML.Rol Rol { get; set; }

        public string Imagen { get; set; }
        public bool Status { get; set; }

        public ML.Direccion Direccion { get; set; }

    }
}
