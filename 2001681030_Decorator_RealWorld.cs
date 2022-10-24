using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator_RealWorld_NikolN
{
    /// <summary>
    /// Дефинираме интерфейс за типът обекти, които ще декорираме - нашите торти
    /// </summary>
    interface SimpleCake
    {
        string GetBakedCake();
    }
   
    /// <summary>
    /// Имплементираме интерфейса на Тортата и метода за взимане на вече изпечена
    /// торта. Ще получим обикновена изпечена торта
    /// </summary>
    class ConcreteSimpleCake : SimpleCake
    {
        public string GetBakedCake()
        {
            return "Торта ";
        }
    }

   /// <summary>
   /// Създаваме абстрактен клас за нашия декоратор на торти. И тук имплементираме
   /// Торта интерфейса и метода за получаване на изпечена торта
   /// </summary>
    abstract class CakeDecorator : SimpleCake
    {
        protected SimpleCake simpleCake;

        public CakeDecorator (SimpleCake simpleCake)
        {
            this.simpleCake = simpleCake;
        }

        //Методът за получаване за вадене на тортата от фурната декларираме като 
        //virtual, защото ще го override-нем в дъщерните класове
        public virtual string GetBakedCake()
        {
            return simpleCake.GetBakedCake();
        }
    }

    /// <summary>
    /// Вече създаваме контретните класове с техните специфики
    /// Всеки декоратор ще е отговорен за добавянето на един вид декорация
    /// В нашия случай ще имаме за фондан и такъв за захаросани плодове
    /// </summary>
    class CakeFondantDecorator : CakeDecorator
    {
        public CakeFondantDecorator (SimpleCake simpleCake) : base(simpleCake)
        {
        }

        public override string GetBakedCake()
        {
            return simpleCake.GetBakedCake() + AddFondant();
        }

        private string AddFondant()
        {
            return "обвита във";
        }
    }

    class CandiedFruitDecorator : CakeDecorator
    {
        public CandiedFruitDecorator(SimpleCake simpleCake) : base(simpleCake)
        {
        }

        public override string GetBakedCake()
        {
            return simpleCake.GetBakedCake() + AddCandiedFruit();
        }

        private string AddCandiedFruit()
        {
            return " с добавени";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteSimpleCake concreteSimpleCakeNonVegan = new ConcreteSimpleCake();
            string simpleCakeNonVegan = concreteSimpleCakeNonVegan.GetBakedCake();
            Console.WriteLine(simpleCakeNonVegan);

            ConcreteSimpleCake concreteSimpleCakeVegan = new ConcreteSimpleCake();
            string simpleCakeVegan = concreteSimpleCakeVegan.GetBakedCake();
            Console.WriteLine("Веган " + simpleCakeVegan);

            CakeFondantDecorator cakeFondantDecorator = new CakeFondantDecorator(concreteSimpleCakeNonVegan);
            string fondantNonVeganCake = cakeFondantDecorator.GetBakedCake();
            Console.WriteLine("\n"+ fondantNonVeganCake + " фондан");

            CakeFondantDecorator cakeFondantDecoratorTwo = new CakeFondantDecorator(concreteSimpleCakeVegan);
            string fondantVeganCake = cakeFondantDecorator.GetBakedCake();
            Console.WriteLine("\n" + "Веган " + fondantVeganCake + " фондан");

            CandiedFruitDecorator candiedFruitDecorator = new CandiedFruitDecorator(concreteSimpleCakeNonVegan);
            string candiedFruit = candiedFruitDecorator.GetBakedCake();
            Console.WriteLine("\n" + candiedFruit + " захаросани плодове");

            Console.ReadLine();
        }
    }
}
