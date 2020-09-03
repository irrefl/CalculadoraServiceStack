namespace CalculatorServiceStack.Services.Math
{
    public class MathOperandum : IAritmetica
    {
        public MathOperandum(decimal x, decimal y,string operation)
        {
            firstNumber = x;
            secoundNumber = y;
            Operation = operation;
        }
        public string Operation { get; set; }
        public decimal firstNumber { get; set; }
        public decimal secoundNumber { get; set; }

        private decimal Sum() => firstNumber + secoundNumber;

        private decimal Rest() => firstNumber - secoundNumber;

        private decimal Multiply() => firstNumber * secoundNumber;
        private decimal Divide() => (secoundNumber == 0) ? 0 : (firstNumber / secoundNumber);

        public decimal Calculate()
        {
            decimal total = 0;
            switch (Operation)
            {
                case MathOperation.SUM:
                    total = Sum();
                    break;

                case MathOperation.REST:
                     total = Rest();
                    break;

                case MathOperation.MULTIPLY:
                     total = Multiply();
                    break;

                case MathOperation.DIVISION:
                     total = Divide();
                    break;
                    
            }
            return total;
        }


    }
}
