namespace TechieInputHandling
{
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
}