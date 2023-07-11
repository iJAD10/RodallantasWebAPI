using API_Roda_Llantas.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace API_Roda_Llantas.Models
{
    public class ReservacionesModel : IReservacionesModel
    {
        private readonly IConfiguration _configuration;
        private readonly IUtilitariosModel _utilitariosModel;

        public ReservacionesModel(IConfiguration configuration, IUtilitariosModel utilitariosModel)
        {
            _configuration = configuration;
            _utilitariosModel = utilitariosModel;
		}

        public int RegistrarReservacion(ReservacionesEntities entidad)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                int reservacion = conexion.Query<int>("RegistrarReservacion",
                    new { Res_Usuario_Id = entidad.Res_Usuario_Id, Res_Vehiculo = entidad.Veh_Id },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                List<string> fechasList = new List<string>();
                foreach (var fecha in entidad.FechasServiciosReservados)
                {
                    if(fecha != null)
                        fechasList.Add(fecha);
                }

                string[] fechas = fechasList.ToArray();
                string cuerpoCorreo = "";

                for (int i = 0; i < entidad.ServiciosReservados.Count; i++) //Se registra cada servicio
                {
                    var servicioId = entidad.ServiciosReservados[i];
                    var servicioFecha = fechas[i];

                    conexion.Query<ReservacionesEntities>("RegistrarServiciosReservados",
                        new { @Res_Id = reservacion, Ser_Id = servicioId, Ser_Fecha = servicioFecha },
                        commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                    var servicio = conexion.Query<ServiciosEntities>("ConsultarServicio",
	                    new { @Ser_Id = servicioId },
	                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

					DateTime fecha = DateTime.ParseExact(servicioFecha, "yyyy-MM-dd", CultureInfo.InvariantCulture);

					cuerpoCorreo += "<li>" + servicio.Ser_Nombre + "-> Fecha: " + fecha.ToString("dd-MM-yyyy") + "</li>";
                }

                cuerpoCorreo = "¡Hola!<br><br>Queremos confirmarte que hemos recibido tu reservación, la misma tiene el número " + reservacion + "." +
							   "<br><br>Aquí te dejamos los detalles de tu reserva:<br><ul>" + cuerpoCorreo + "</ul>" +
                               "Por favor, revisa esta información y asegúrate de que todo esté correcto.<br>" +
                               "Agradecemos tu confianza en nuestro taller y estamos ansiosos por darte el mejor servicio posible. Si tienes alguna pregunta o necesitas más información, no dudes en contactarnos." +
                               "<br><br>¡Que tengas un buen día!<br><br>Atentamente, Roda Llantas.";

				_utilitariosModel.EnviarCorreo(entidad.Usuario_Correo, "Reservación #" + reservacion + " completa", cuerpoCorreo);

                

                return 1;
            }
        }

        public List<ReservacionesEntities> ConsultarReservaciones()
        {
	        using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
	        {
		        return conexion.Query<ReservacionesEntities>("ConsultarReservaciones",
			        new { },
			        commandType: System.Data.CommandType.StoredProcedure).ToList();
	        }
        }

        public List<ReservacionesEntities> DetallesReservacion(long Res_Id)
        {
	        using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
	        {
		        return conexion.Query<ReservacionesEntities>("VerDetallesReservacion",
			        new { Res_Id },
			        commandType: System.Data.CommandType.StoredProcedure).ToList();
	        }
        }

        public int CambiarCompletado(ReservacionesEntities entidad)
        {
	        using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
	        {
		        return conexion.Execute("CambiarCompletado",
			        new { entidad.Res_Id },
			        commandType: System.Data.CommandType.StoredProcedure);
	        }
        }
	}
}
