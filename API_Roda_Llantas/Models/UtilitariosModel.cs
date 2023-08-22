using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System;
using System.Data.SqlClient;
using System.Net.Mail;

namespace API_Roda_Llantas.Models
{
    public class UtilitariosModel : IUtilitariosModel
    {
        private readonly IConfiguration _configuration;
        public UtilitariosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarCorreo(string Destinatario, string Asunto, string Mensaje)
        {
            string correoSMPT = _configuration.GetSection("ParametrosCorreo:correoSMPT").Value;
            string claveSMPT = _configuration.GetSection("ParametrosCorreo:claveSMPT").Value;

            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(Destinatario, "Cliente"));
            msg.From = new MailAddress(correoSMPT, "Roda Llantas");
            msg.Subject = Asunto;
            msg.Body = Mensaje;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(correoSMPT, claveSMPT);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Send(msg);
        }

        public CountEntities Count()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
                {
                    return conexion.Query<CountEntities>("sp_CountIDs",
                                      commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault()!;
                }
                catch (SqlException ex)
                {
                    throw;  // Si no es el error esperado, relanza la excepción original.
                }
        }
    }
}
