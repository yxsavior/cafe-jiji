namespace CafeJiji.DTOs
{
    public class MesaCozinhaDTO
    {
        public int NumeroMesa { get; set; }
        public DateTime HorarioMaisAntigo { get; set; }
        public List<ItemMesaCozinhaDTO> Itens { get; set; } = new();
    }
}