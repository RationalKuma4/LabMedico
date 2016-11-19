using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LabMedico.Models.CustomUser;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabMedico.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class LaboratorioUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Calle { get; set; }
        public int? NumeroInterior { get; set; }
        public int? NumeroExterior { get; set; }
        public string Colonia { get; set; }
        public string DelegacionMunicipio { get; set; }
        public int? CodigoPostal { get; set; }
        public string Estado { get; set; }
        public int? Edad { get; set; }
        public string Sexo { get; set; }
        public string Notas { get; set; }
        public int? SucursalId { get; set; }
        public string Estatus { get; set; }

        
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual Sucursal Sucursales { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<LaboratorioUser, int> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }
}