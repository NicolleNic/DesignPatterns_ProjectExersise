using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderStructuralCode
{
    //Структурен код на шаблона Builder (Строител)
    public interface MyBuilder
    {
        //В абстрактния интерфейс Builder задаваме методите за създаването
        //на различните части на обектите от тип Product, Които ще изграждаме
        void buildPartOne();
        void buildPartTwo();
        void buildPartThree();
    }
    
    //ConcreteBuilder имплементира Builder интерфейса, включичтелно и
    //дефинира отделните стъпки за конструиране 
    public class ConcreteBuilder : MyBuilder
    {
        private Product myProduct = new Product();

        //В нова инстанция на Builder трябва да има празен обект от тип Product,
        //който ще използваме в конструирането нататък

        public ConcreteBuilder()
        {
            this.Reset();
        }

        //Препоръчително. но не и задължително, е да си създадем метод за Reset.
        //Така след като конструирането приключи и клиентът е получил крайния резултат
        //сме вече подготвени процесът да започне отначало 
        public void Reset()
        {
            this.myProduct = new Product();
        }

        //Всички отделни стъпки работят с една и съща инстанция на продукт
        public void buildPartOne()
        {
            this.myProduct.Add("Първа част");
        }

        public void buildPartTwo()
        {
            this.myProduct.Add("Втора част");
        }

        public void buildPartThree()
        {
            this.myProduct.Add("Трета част");
        }

        //В конкретните конструктори трябва да има предоставени собствени методи за
        //извличане на продукта. Съобразяваме се с това, че различните конструктори
        //създават различни продукти.
        public Product GetProduct()
        {
            Product result = this.myProduct;

            this.Reset();
            return result;
        }

    }

    public class Product
    {
        private List<object> myParts = new List<object>();

        public void Add(string part)
        {
            this.myParts.Add(part);
        }

        public string ListParts()
        {
            string st = string.Empty;

            for (int i = 0; i < this.myParts.Count; i++)
            {
                st += this.myParts[i] + ", ";
            }

            st = st.Remove(st.Length - 2);
            return "Части на продукта:" + st + "\n";
        }
    }

    //Директорът конструира сложния обект в определена поредица от стъпки
    //Не сме задължени да го използваме, тъй като клиентът може директно
    //да контролира Строителя, но е по-добра практика
    public class Director
    {
        private MyBuilder myBuilder;

        public MyBuilder Builder
        {
            set { myBuilder = value; }
        }

        //Един директор не е ограничен да конструира само една вариация на продукт
        //Може да има различни варианти с едни и същи стъпки на асемблиране
        public void BuildSmallProduct()
        {
            this.myBuilder.buildPartThree();
        }

        public void BuildLargeProduct()
        {
            this.myBuilder.buildPartOne();
            this.myBuilder.buildPartTwo();
            this.myBuilder.buildPartThree();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //В клиентския код се създава обект Строител, който се предава към 
            //Директор, а той инициира процесът на конструиране
            var director = new Director();
            var builder = new ConcreteBuilder();
            director.Builder = builder;

            Console.WriteLine("Мини продукт: ");
            director.BuildSmallProduct();
            //Крайният резултат извличаме от обекта Строител
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.WriteLine("Мега продукт: ");
            director.BuildLargeProduct();
            Console.WriteLine(builder.GetProduct().ListParts());

            //Примерно прилагане на шаблона без наличието на Директор 
            Console.WriteLine("Изцяло клиентски контролиран продукт: ");
            builder.buildPartTwo();
            builder.buildPartOne();
            Console.WriteLine(builder.GetProduct().ListParts());

            Console.ReadLine();

        }
    }
}
