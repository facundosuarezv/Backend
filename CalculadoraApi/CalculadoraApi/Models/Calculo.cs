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
            { "suma" => Operando1 + Operando2,
                "resta" => Operando1 - Operando2,
                "multiplicacion" => Operando1 * Operando2,
                "division" => Operando2 != 0 ? Operando1 / Operando2 : throw new DivideByZeroException("El divisor no puede ser cero"),
                _ => throw new InvalidOperationException("Operacion invalida")
            };
            }
        }


    }
}
