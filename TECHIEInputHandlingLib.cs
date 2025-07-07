// C#
// TechieeDev's Input Handling Library

using System;

// DMM : Dedicated Math Module
public static class DMM
{
    private static float positive_power(float a, float n)
    {
        float b = 1; while (n > 0){
            b *= a;
            n = n - 1;
        }return b;
    }
    // can be just replaced by 'MathF.Pow(a,n)'
    public static float power(float a, float n)
    {
        if (n > 0){return positive_power(a, n);}else{return 1 / positive_power(a, n);}
    }

    public static double clamp(double value, double min, double max)
    {
        if (value < min) {
            return min;
        }
        else if (value > max) {
            return max;
        }
        else {
            return value;
        }
    }

    public static float uniform_parabola_k(float x)
    {
        return power(x, 2) + x;
    }
}

public enum InputType{
    Keyboard,
    Mouse,
    Controller
}

public class Input
{

    public InputType InputType;
    public bool enableUserInputRead = true;
    private bool duringDecay = false;
    public Dictionary<string, float> keys = new Dictionary<string, float>();
    public Input(InputType input_type)
    {
        InputType = input_type;
    }

    public void ReceiveSignal(double sensor_realtime_read, double env_deltatime, string ref_key)
    {
        if (keys.Contains(ref_key))
        {
            keys[ref_key] += (sensor_realtime_read * env_deltatime);
        }
        else
        {
            keys.Add(ref_key, sensor_realtime_read * env_deltatime);
        }
        keys[ref_key] = DMM.clamp(keys[ref_key], 0, 1);
        duringDecay = true;
    }

    // would be called every frame in a realtime simulation if 'duringDecay' bool's enabled
    public void DecayRegime(double env_time)
    {
        foreach (KeyValuePair<string, float> kvp in keys)
        {
            if (kvp.value > 0.001)
            {
                keys[kvp.key] -= DMM.uniform_parabola_k(env_time);
                keys[kvp.key] = DMM.clamp(keys[kvp.key], 0, 1);
            }
        }
        duringDecay = false;
        foreach (KeyValuePair<string, float> kvp in keys)
        {
            if (kvp.value > 0.001)
            {
                duringDecay = true;
            }
        }
    }
}

public class program
{
    public static Input Keyboard01 = new Input(InputType.Keyboard);

    public static void Main(string[] args)
    {
        Keyboard01.ReceiveSignal(0.1, 0.0016, "KeyA");
        Console.WriteLine(Keyboard01.keys["KeyA"]);
    }
}

// VARIABLE DEFINITION LIST 
// 'enableUserInputRead' : Controls whether the 'Input' device can receive signal or not
// 'duringDecay' : Controls whether the 'Input' device is in a state of decay or not
// 'keys' : Conceptually, the list of keys that associated with a specific 'Input' device and their values
// 'sensor_realtime_read' : A technical raw read of value that has electrically sent by an 'Input' device sensor 
// 'env_deltatime' : Conceptually, the time difference between the last and previous frame. Technically, a value calculated by the processor in a specific running realtime environment
// 'env_time' : Is the totality of the time since the environment's simulation start
// * * * * * * * * * * * * * *