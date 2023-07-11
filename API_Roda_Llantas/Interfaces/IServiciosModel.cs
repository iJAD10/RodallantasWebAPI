using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Interfaces
{
    public interface IServiciosModel
    {
        public List<ServiciosEntities> ConsultarServicios();
        public int RegistrarServicios(ServiciosEntities entidad);
        public void ActualizarServicios(ServiciosEntities e);
        public ServiciosEntities ConsultarServicio(long Ser_Id);
    }
}
