namespace TechieInputHandling
{
    // DMM : Dedicated Math Module
    public static class DMM
    {
        private static double positive_power(double a, double n)
        {
            float b = 1; while (n > 0){
                b *= a;
                n = n - 1;
            }return b;
        }
        // can be just replaced by 'MathF.Pow(a,n)'
        public static double power(double a, double n)
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

        public static double uniform_parabola_k(double x)
        {
            return power(x, 2) + x;
        }
    }
}
