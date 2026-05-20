using System.Globalization;

namespace CafeJiji.DTOs
{
    public class CozinhaPedidoResponseDTO
    {
        public int ItemId { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public DateTime HorarioPedido { get; set; }

    }
}
