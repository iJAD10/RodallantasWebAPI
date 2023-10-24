using API_Roda_Llantas.Entities;
using API_Roda_Llantas.Interfaces;
using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace API_Roda_Llantas.Models
{
    public class ComprasModel: ICompras
    {
        private IConfiguration _configuration;
        private readonly IUtilitariosModel _utilitariosModel;

        public ComprasModel(IConfiguration configuration, IUtilitariosModel utilitariosModel)
        {
            _configuration = configuration;
            _utilitariosModel = utilitariosModel;
        }

        //public void FinalizarCompra(int usuId, int total)
        //{
        //    using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //        try
        //        {
        //            conexion.Execute("FinalizarCompra",
        //                new { @Usu_Id = usuId, @Total = total},
        //                commandType: System.Data.CommandType.StoredProcedure);
        //        }
        //        catch (SqlException ex)
        //        { 
        //            throw;  // Si no es el error esperado, relanza la excepción original.
        //        }
        //}

        public void FinalizarCompra(int usuId, int total, string correoUsuario)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
                {
                    conexion.Execute("FinalizarCompra",
                        new { @Usu_Id = usuId, @Total = total },
                        commandType: System.Data.CommandType.StoredProcedure);

                    string cuerpoCorreo = $"¡Hola!<br><br>¡Tu compra ha sido exitosa! El total de tu compra fue de ${total}." +
                                          "<br><br>Pronto nos pondremos en contacto, agradecemos tu preferencia y esperamos verte pronto. Si tienes alguna pregunta o inquietud, no dudes en contactarnos." +
                                          "<br><br>Saludos, Roda Llantas.";

                    _utilitariosModel.EnviarCorreo(correoUsuario, "Compra exitosa", cuerpoCorreo);
                }
                catch (SqlException ex)
                {
                    throw;
                }
        }

        public IEnumerable<OrdenCompraListar> ListarOrdenCompra()
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                try
            {
                return conexion.Query<OrdenCompraListar>("sp_ListarOrdenCompra",
                   
                    commandType: System.Data.CommandType.StoredProcedure);
            }
                catch (SqlException ex)
                {
                    throw;  // Si no es el error esperado, relanza la excepción original.
                }
        }

        public IEnumerable<ListarDetalleOrdenPorId> ListarDetalleOrdenPorId(int orden_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ListarDetalleOrdenPorId>("sp_ListarDetalleOrden",
                    new { @Orden_Id = orden_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int CambiarCompletadoOrdenCompra(int orden_Id)
        {
            using (var conexion = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("[sp_ToggleEstadoOrdenCompra]",
                    new { @Orden_Id = orden_Id },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
