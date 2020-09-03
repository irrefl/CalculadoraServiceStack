namespace CalculatorServiceStack.ViewModels
{
    public class ViewModelLocator
    {

        //Nos elige la propiedad de nuestros view models, y los controla para que esten con la vista
        private CalculatorViewModel _calculatorViewModel;
        public CalculatorViewModel CalculatorViewModel
        {
            get
            {
                return _calculatorViewModel ??
                (_calculatorViewModel = new CalculatorViewModel());
            }
        }
    }
}
