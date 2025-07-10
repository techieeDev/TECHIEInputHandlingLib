namespace TechieInputHandling
{
    // MIS:The main identification system
    public static class MIS
    {
        public static Random rand = new Random();
        public static string get_random_cycle_id()
        {
            string id = "CYC_";
            id += Convert.ToString(rand.Next(100, 999));
            id += (char)(rand.Next(97, 122));
            id += Convert.ToString(rand.Next(100, 999));
            return id;
        }
    }
}
