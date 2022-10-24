using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder_RealWorld_NikolN
{
    /// <summary>
    /// Директорът, или в нашия случай "Сладкарят", който ще е отговорен
    /// за направата на тортите (тяхното конструиране)
    /// </summary>

    class Baker
    {
        //Вариант за изграждена на тортата от Строителя в определена
        //поредност от стъпки
        public void Bake(CakeBuilder cakeBuilder)
        {
            cakeBuilder.addFlour();
            cakeBuilder.addSweetener();
            cakeBuilder.addLiquid();
            cakeBuilder.addBinder();
            cakeBuilder.addSpecialFlavour();
        }
    }

    /// <summary>
    /// Абстрактния клас за "Строителя" на нашата торта
    /// </summary>
    abstract class CakeBuilder
    {
        //Инстанция с празен обект от типа на крайния ни резултат,
        //която ще използваме в асемблирането на частите
        protected Cake cake;

        //Взимаме инстанцията на вида торта
        public Cake Cake
        {
            get { return cake; }
        }

        //Абстрактните методи за правене на тортата
        public abstract void addFlour();
        public abstract void addSweetener();
        public abstract void addLiquid();
        public abstract void addBinder();
        public abstract void addSpecialFlavour();

    }
    /// <summary>
    /// Първи конкретен "строител" на нашия продукт (торта)
    /// Първи вид торта - Шоколадова Веган
    /// </summary>
    class VeganCakeBuilder : CakeBuilder
    {
        public VeganCakeBuilder()
        {
            cake = new Cake("Шоколадова Веган торта");
        }

        //Задаваме специфичните за този вид торта съставки
        public override void addFlour()
        {
            cake["flour"] = "Овесено";
        }

        public override void addBinder()
        {
            cake["binder"] = "Агаве";

        }

        public override void addLiquid()
        {
            cake["liquid"] = "Овесено мляко";

        }

        public override void addSweetener()
        {
            cake["sweetener"] = "Фурми";

        }

        public override void addSpecialFlavour()
        {
            cake["flavour"] = "Тъмно Какао";

        }
    }

    /// <summary>
    /// Втори конкретен "строител" на нашия продукт (торта)
    /// Втори вид торта - Класик Ванилия
    /// </summary>    
    class NonVeganCakeBuilder : CakeBuilder
    {
        public NonVeganCakeBuilder()
        {
            cake = new Cake("Класик Ванилия");
        }

        public override void addFlour()
        {
            cake["flour"] = "Бяло пшенично";

        }

        public override void addBinder()
        {
            cake["binder"] = "Яйца";

        }

        public override void addLiquid()
        {
            cake["liquid"] = "Прясно мляко";

        }

        public override void addSweetener()
        {
            cake["sweetener"] = "Захар";

        }

        public override void addSpecialFlavour()
        {
            cake["flavour"] = "Ванилия";

        }
    }

    ///<summary></summary>
    ///Класът на крайния ни продукт  - торта
    ///</summary>
    class Cake
    {
        //Ще достъпваме специфичните за всяка торта съставки чрез вида на
        //самата торта
        private string theCakeType;
        private Dictionary<string, string> ingredients = new Dictionary<string, string>();

        //Конструктор за обекта тип Торта
        public Cake(string cakeType)
        {
            this.theCakeType = cakeType;
        }

        //Индексация 
        public string this[string key]
        {
            get { return ingredients[key]; }
            set { ingredients[key] = value; }
        }

        //Формат на изписване на крайния продукт заедно със съставките му
        public void PresentCake()
        {
            Console.WriteLine("                  -                  ");
            Console.WriteLine("Вид: {0}", theCakeType);
            Console.WriteLine("Брашно: {0} ", ingredients["flour"]);
            Console.WriteLine("Спояващ агент: {0}", ingredients["binder"]);
            Console.WriteLine("Течност: {0}", ingredients["liquid"]);
            Console.WriteLine("Подсладител: {0}", ingredients["sweetener"]);
            Console.WriteLine("Главен вкус: {0}", ingredients["flavour"]);
        }
    }

    public class Program
    {
        /// <summary>
        /// Начална точка в нашата програма за правене на торти
        /// с помощта на Builder шаблона
        /// </summary>
        static void Main()
        {
            CakeBuilder cakeBuilder;

            Baker baker = new Baker();

            //Конструирането на тортите и показването им
            cakeBuilder = new VeganCakeBuilder();
            baker.Bake(cakeBuilder);
            cakeBuilder.Cake.PresentCake();

            cakeBuilder = new NonVeganCakeBuilder();
            baker.Bake(cakeBuilder);
            cakeBuilder.Cake.PresentCake();

            //За да може да видим как изглежда крайния резултат в конзолата
            Console.ReadLine();
        }
    }
}
