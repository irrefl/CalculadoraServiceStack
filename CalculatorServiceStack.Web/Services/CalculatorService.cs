
using CalculatorServiceStack.Services.Math;
using ServiceStack;

namespace CalculatorServiceStack.Web.Services
{
    public class CalculatorService : Service
    {
        public CalculatorDto Get(CalculatorRequest request)
        {
            MathOperandum calc = new MathOperandum(request.FirstNumber, request.SecondNumber, request.Operator);
           
            return new CalculatorDto
            {
                Result = calc.Calculate()
            };
        }
    }
}