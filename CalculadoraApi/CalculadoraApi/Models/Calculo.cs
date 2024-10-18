using System.ComponentModel.DataAnnotations;

namespace CalculadoraApi.Models
{
    public class Calculo
    {
        [Key]
        public int Id { get; set; }

        public double Operando1 { get; set; }
        public double Operando2 { get; set; }
        public string Operacion { get; set; } = string.Empty;
        
        public double Resultado
        {
            get
            { return Operacion.ToLower() switch
            {   "+" => Operando1 + Operando2,
                "-" => Operando1 - Operando2,
                "*" => Operando1 * Operando2,
                "/" => Operando1 / Operando2,
                _ => throw new InvalidOperationException("Operacion invalida")
            };
            }
        }


    }
}
