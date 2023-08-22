using API_Roda_Llantas.Entities;

namespace API_Roda_Llantas.Interfaces
{
    public interface IUtilitariosModel
    {
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
        public CountEntities Count();
    }
}
