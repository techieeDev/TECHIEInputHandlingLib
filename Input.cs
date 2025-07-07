namespace TechieInputHandling
{
    public class Input
    {

        public EInputType InputType;
        public bool enableUserInputRead = true;
        private bool duringDecay = false;
        public Dictionary<string, float> keys = new Dictionary<string, float>();
        public Input(EInputType input_type)
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
}

// VARIABLE DEFINITION LIST 
// 'enableUserInputRead' : Controls whether the 'Input' device can receive signal or not
// 'duringDecay' : Controls whether the 'Input' device is in a state of decay or not
// 'keys' : Conceptually, the list of keys that associated with a specific 'Input' device and their values
// 'sensor_realtime_read' : A technical raw read of value that has electrically sent by an 'Input' device sensor 
// 'env_deltatime' : Conceptually, the time difference between the last and previous frame. Technically, a value calculated by the processor in a specific running realtime environment
// 'env_time' : Is the totality of the time since the environment's simulation start
// * * * * * * * * * * * * * *