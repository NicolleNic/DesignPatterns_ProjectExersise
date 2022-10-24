using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State_StructuralCode_NikolN
{
    /// <summary>
    /// Абстрактен клас на състоянието, капсулира поведенията
    /// </summary>
    abstract class State
    {
        public abstract void Handle(Context context);
    }

    /// <summary>
    /// Клас на конкретно състояние, във всеки отделен такъв имплементираме
    /// поведение, свързано с едно от сътоянията на Контекста
    /// </summary>
    class ConcreteStateA: State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateA();
        }
    }

    /// <summary>
    /// Класът на Контекста дефинира интерфейса, който представлява интерес
    /// за клиентите. Той поддържа референция към инстанция на подклас на State.
    /// </summary>  
    class Context
    {
        //Референция към текущото състояние на Контекста
        private State theState;

        public Context(State state)
        {
            this.State = state;
        }

        public State State
        {
            get { return theState; }
            set
            {
                theState = value;
                Console.WriteLine("State: " + theState.GetType().Name);
            }
        }

        //Контекстът делегира част от поведението си към текущия State обект
        public void Request()
        {
            theState.Handle(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Задаваме контекста в състояние
            Context c = new Context(new ConcreteStateA());

            //Изпълняваме заявки спрямо състоянието, в което е контекста
            c.Request();
            c.Request();
            c.Request();
            c.Request();

            Console.ReadLine();
        }
    }
}
