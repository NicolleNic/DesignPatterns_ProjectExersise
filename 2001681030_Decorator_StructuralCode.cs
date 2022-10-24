using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator_RealWorld_NikolN
{
    /// <summary>
    /// Абстрактен интерфейс за обектите, които динамично могат да
    /// получават допълнителнни отговорности
    /// Тук се дефинират операциите, които могат да бъдат променени от
    /// декоратори
    /// </summary>
    abstract class Component
    {
        public abstract void Operation();
    }

    /// <summary>
    ///Класът на конкретния компонент - имплементация на операцията
    /// </summary>
    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent.Operation()");
        }
    }

    /// <summary>
    /// Абстрактен клас на Декоратора - дефинира се интерфейса за декориране
    /// на всички конретни декоратори
    /// </summary>
    abstract class Decorator : Component
    {
        //Инстанция на компонента по подразбиране, с която ще работим
        protected Component component;

        public void SetComponent (Component component)
        {
            this.component = component;
        }

        //Делегиране на работата към компонента, който е "wrapped"
        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

    /// <summary>
    /// За да добавим допълнителни отговорности към обект,
    /// имплементираме конкретните декоратори
    /// </summary>   
    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("ConcreteDecoratorA.Operation()");
        }

        void AddedBehavior()
        {
        }
    }

    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("ConcreteDecoratorB.Operation()");
        }

        void AddedBehavior()
        {
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Декораторите могат да "декорират" както конкретни компоненти,
            //така и други декоратори
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();

            d1.SetComponent(c);
            d2.SetComponent(d1);

            d2.Operation();

            Console.ReadLine();
        }
    }
}
