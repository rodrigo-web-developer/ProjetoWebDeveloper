using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace PrimeiraAPI.Validacoes
{
    public class ListSizeAttribute : ValidationAttribute
    {
        public int Minimo { get; set; }
        public int Maximo { get; set; }

        public ListSizeAttribute(int min)
        {
            Minimo = min;
            ErrorMessage = $"A lista deve ter no mínimo {Minimo} itens";
        }

        public override bool IsValid(object value)
        {
            if(value == null && Minimo > 0)
            {
                return false;
            }

            if(value is IList lista)
            {
                if(lista.Count < Minimo)
                {
                    return false;
                }
                else if (Maximo > 0 && lista.Count > Maximo)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                throw new ArgumentException("O tipo deve ser uma Lista");
            }
        }
    }
}
