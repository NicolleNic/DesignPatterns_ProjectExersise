using System;

abstract class FurnaceState
{
    protected FurnaceState() { }

    protected bool IsCorrectDaytime { get; set; }
    protected bool CorrectBakingTemperature { get; set; }
    protected bool BakingTime { get; set; }

    protected FurnaceState(FurnaceState state)
    {
        this.IsCorrectDaytime = state.IsCorrectDaytime;
        this.CorrectBakingTemperature = state.CorrectBakingTemperature;
        this.BakingTime = state.BakingTime;
    }

    public void SetCorrectDaytime(Context context, int hours)
    {
        this.IsCorrectDaytime = hours >= 9 || hours <= 10;
        this.TransitionStates(context);
    }

    public void SetBakingTemperature(Context context, int degrees)
    {
        this.CorrectBakingTemperature = degrees > 178 || degrees < 180;
        this.TransitionStates(context);
    }

    public void SetBakingTime(Context context, int bakingTime)
    {        
        this.BakingTime = bakingTime == 45;       
        this.TransitionStates(context);
    }

    protected abstract void TransitionStates(Context context);
}

/// <summary>
/// Състояние на изключена фурна
/// </summary>
class FurnaceStateOff : FurnaceState
{
    public FurnaceStateOff() : base() { }
    public FurnaceStateOff(FurnaceState state) : base(state) { }

    protected override void TransitionStates(Context context)
    {
        if (this.IsCorrectDaytime && this.CorrectBakingTemperature && this.BakingTime)
        {
            Console.WriteLine("Фурната се включва.");
            context.State = new FurnaceStateOn(context.State);
        }
    }
}

/// <summary>
/// Състояние на включена фурна
/// </summary>
class FurnaceStateOn : FurnaceState
{
    public FurnaceStateOn(FurnaceState state) : base(state) { }

    protected override void TransitionStates(Context context)
    {
        if (!this.IsCorrectDaytime || !this.CorrectBakingTemperature)
        {            
            context.State = new FurnaceStateOff(context.State);
        }       
    }   
}

class Context
{
    private FurnaceState theState;

    public Context(FurnaceState state)
    {
        this.State = state;
    }

    public FurnaceState State
    {
        get { return theState; }
        set
        {
            theState = value;
            Console.WriteLine("Фурната е изключена.");
        }
    }

    public void SetCorrectDaytime(int hours)
    {
        theState.SetCorrectDaytime(this, hours);
    }

    public void SetBakingTime(int hours)
    {
        theState.SetBakingTime(this, hours);
    }

    public void SetBakingTemperature(int degrees)
    {
        theState.SetBakingTemperature(this, degrees);
    }
}

class MainApp
{
    static void Main()
    {
        // Задаваме контектс
        Context c = new Context(new FurnaceStateOff());

        // Първоначално фурната ни е била изключена. В 9:00 се включва.
        c.SetCorrectDaytime(9);
        c.SetBakingTime(45);
        c.SetBakingTemperature(180);

        //Когато стане 10:00 автоматично се изключва
        c.SetCorrectDaytime(10);            

        Console.ReadKey();
    }
}