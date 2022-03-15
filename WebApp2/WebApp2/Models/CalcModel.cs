using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp2.Models
{
    public class CalcModel
    {
        public int x { get; set; }
        public int y { get; set; }
        public string Operation { get; set; }

        public string Calc()
        {
            return Operation switch
            {
                "+" => $"{x} + {y} = {x + y}",
                "-" => $"{x} - {y} = {x - y}",
                "*" => $"{x} * {y} = {x * y}",
                "/" => y != 0 ? $"{x} /  {y} = {x / y}" : "Division by zero",
                _ => "Invalid operation"
            };
        }
    }
}
