namespace MDP3.Services
{
    public class Calculator : ICalculator
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }

        public int Difference(int x, int y)
        {
            return x - y;
        }
    }
}
