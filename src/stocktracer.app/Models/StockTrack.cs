namespace stocktracer.app.Models
{
    public class StockTrack
    {
        public string Symbol { get; set; }
        public float Price { get; set; } = -1;
        public bool IsDown { get; set; }
    }
}