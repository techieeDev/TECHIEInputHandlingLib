using TechieInputHandling;

public class Program
{
    public static Input MyKeyboard = new Input(EInputType.Keyboard);
    public static void Main(string[] args)
    {
        for (int i = 0; i < 100; i++)
        {
            MyKeyboard.ReceiveSignal(100, 0.0031, "KeyA");
        }
        
        while (MyKeyboard.duringDecay)
        {
            MyKeyboard.DecayRegime(0.00052);
            Console.WriteLine(MyKeyboard.keys["KeyA"]);
            Thread.Sleep(777);
        }
    }
}
