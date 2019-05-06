using System;

namespace TemplateMethod
{
    internal class Program
    {
        private static void Main()
        {
            Brewery doranCafe = new CoffeeBrewery();

            Brewery mamadTea = new HerbalBrewery();

            Console.WriteLine(doranCafe.MakeBeverage());

            Console.WriteLine(new string('-', 40));

            Console.WriteLine(mamadTea.MakeBeverage());
        }
    }


    internal abstract class Brewery
    {
        public double MakeBeverage()
        {
            BoilWater();
            BrewStuff();
            PourIntoContainer();
            AddCondiments();
            var price = Charge();


            return price;
        }

        public abstract double Charge();

        public abstract void AddCondiments();

        private void PourIntoContainer()
        {
            Console.WriteLine("serving in a glass");
        }

        public abstract void BrewStuff();

        private void BoilWater()
        {
            Console.WriteLine("boiling water");
        }
    }

    internal class CoffeeBrewery : Brewery
    {
        public override double Charge()
        {
            Console.WriteLine("charging for coffee");
            return 15d;
        }

        public override void AddCondiments()
        {
            Console.WriteLine("Adding milk, ruining the coffee");
        }

        public override void BrewStuff()
        {
            Console.WriteLine("making double espresso");
        }
    }

    internal class HerbalBrewery : Brewery
    {
        public override double Charge()
        {
            Console.WriteLine("charging for funny stuff");
            return 5d;
        }

        public override void AddCondiments()
        {
            Console.WriteLine("adding funny stuff");
        }

        public override void BrewStuff()
        {
            Console.WriteLine("دمنوش آرامش");
        }
    }
}