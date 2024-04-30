

using System.ComponentModel.DataAnnotations;

namespace FinSharkAPI.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "CompanyName cannot be over 10 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1_000_000_000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01,100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string Industry { get; set; } = string.Empty;
        [Range(1,5_000_000_000)]
        public long MarketCap { get; set; } = new();
    }
}