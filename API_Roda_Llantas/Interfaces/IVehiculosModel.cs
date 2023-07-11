 using API_Roda_Llantas.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API_Roda_Llantas.Interfaces
{
    public interface IVehiculosModel
    {
        public List<VehiculosEntities> ConsultarVehiculos();

        public int RegistrarVehiculos(VehiculosEntities entidad);

        public void ActualizarVehiculos(VehiculosEntities e);

        public VehiculosEntities? ConsultarVehiculo(long Veh_Id);

        public VehiculosEntities? ConsultarPlaca(string Placa);
    }
}
