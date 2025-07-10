namespace TechieInputHandling
{
    public class Cycle
    {
        public int FPS = 30;
        public double elapsedTime = 0.0;
        private int delta_frame_delay;
        public string CycId { get; private set; }
        public bool RealtimeComputation { get; private set; } = true;
        public List<Input> Inputs = new List<Input>();
        public Cycle(int fps, bool rc) 
        {
            FPS = fps;
            FPS = DMM.clamp(FPS, 1, 1000);
            CycId = MIS.get_random_cycle_id();
            RealtimeComputation = rc;
            delta_frame_delay = 1000 / FPS;
        }

        public void Init(bool rc)
        {
            Inputs.Clear();
            RealtimeComputation = rc;
        }
        public void RC()
        {
            while (RealtimeComputation) {
                
                Thread.Sleep(delta_frame_delay);
            }
        }


    }
}
