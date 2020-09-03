using CalculatorServiceStack.Services.Math;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalculatorServiceStack.Web.Services
{
    [Route("/Calculator")]
    public class CalculatorRequest 
    {
        public decimal FirstNumber { get; set; }
        public decimal SecondNumber { get; set; }
        public string Operator { get; set; }
 
    }
}