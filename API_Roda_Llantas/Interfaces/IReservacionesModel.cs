using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface IReservacionesModel
    {
        public int RegistrarReservacion(ReservacionesEntities entidad);
        public List<ReservacionesEntities> ConsultarReservaciones();
        public List<ReservacionesEntities> DetallesReservacion(long Res_Id);
        public int CambiarCompletado(ReservacionesEntities entidad);
        public int RegistrarVehiculoYReservacion(ReservacionesEntities entidad);
    }
}
