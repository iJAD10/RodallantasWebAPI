namespace API_Roda_Llantas.Entities
{
    public class CarritoEntities
    {
        public static List<ProductosEntities> ListaProductos { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }


        public string Nombre { get; set; } = string.Empty;
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public long Usu_Id { get; set; }
        public DateTime FechaCompra { get; set; }
    }
}
