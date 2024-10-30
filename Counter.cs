using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contadores_simultáneos_con_hilos
{
    public class Counter
    {
        public int Id { get; private set; }
        public int Count { get; private set; }
        public bool IsActive { get; set; } = true;

        public Counter(int id, int initialCount = 0)
        {
            Id = id;
            Count = initialCount;
        }

        public void Increment()
        {
            Random random = new Random();

            while (IsActive)
            {
                Count++;
                Thread.Sleep(1000);  // Simula incremento cada segundo
            }
        }
    }
}

