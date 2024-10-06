using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SistemaDeCadastro.Models.ModelsDTO
{
    public class RequestProductDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly Validation { get; set; }
    }
}
