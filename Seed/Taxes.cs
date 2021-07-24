using System.Collections.Generic;
using invoice_manager.Models;

namespace invoice_manager.Seed
{
    public class Taxes
    {
        public static IEnumerable<Tax> Data = new List<Tax>
        {
            new() {Id = 1, Multiplier = 0.23f}, 
            new() {Id = 2, Multiplier = 0.08f}, 
            new() {Id = 3, Multiplier = 0.07f},
            new() {Id = 4, Multiplier = 0.04f}
        };
    }
}
