using System;

namespace GeneticSolver
{
    public class Hobbit
    {
        public string name; //{ get; set; } ?
        public float agility; //{ get; set; } ?
        public int energyPoints; //{ get; set; } ?
        
        public Hobbit(string name)
        {
            this.name = name;
            if (name == "Frodo")
            {
                agility = 1.5f;
            }
            else if (name == "Sam")
            {
                agility = 1.4f;
            }
            else if (name == "Merry")
            {
                agility = 1.3f;
            }
            else if (name == "Pippin")
            {
                agility = 1.2f;
            }
            else
            {
                Console.WriteLine("Hobbit inválido");
                Environment.Exit(1);
            }
            energyPoints = 10;
        }
    }
}