namespace API_Roda_Llantas.Interfaces
{
    public interface IUtilitariosModel
    {
        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje);
    }
}
