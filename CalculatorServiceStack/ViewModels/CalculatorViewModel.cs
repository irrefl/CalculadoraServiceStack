
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Collections.Generic;
using Calculadora.Comunes;
using CalculatorServiceStack.Model;
using ServiceStack;

namespace CalculatorServiceStack.ViewModels
{
    public class CalculatorViewModel : NotificationObject
    {
        private readonly JsonServiceClient client;
        public CalculatorViewModel()
        {           
            InicializarPropiedades();
            InicializarComandos();
            client = new JsonServiceClient("http://localhost:64911/");
        }
        public string Operation
        {
            get { return operacion; }
            set
            {
                if (operacion != value)
                {
                    operacion = value;
                    RaisePropertyChanged(nameof(Operation));
                }
            }
        }
        private string operacion;
        public ObservableCollection<string> ListaOriginalOperaciones
        {
            get { return _listaOriginalOperaciones; }
            set
            {
                if (_listaOriginalOperaciones != value)
                {
                    _listaOriginalOperaciones = value;
                    RaisePropertyChanged(nameof(ListaOriginalOperaciones));
                }
            }
        }
        private ObservableCollection<string> _listaOriginalOperaciones;
        public ObservableCollection<string> ListaTemporalOperaciones
        {
            get { return _listaTemporalOperaciones; }
            set
            {
                if (_listaTemporalOperaciones != value)
                {
                    _listaTemporalOperaciones = value;
                    RaisePropertyChanged(nameof(ListaTemporalOperaciones));
                }
            }
        }
        private ObservableCollection<string> _listaTemporalOperaciones;
        public string Resultado
        {
            get { return _resultado; }
            set
            {
                if (_resultado != value)
                {
                    _resultado = value;
                    RaisePropertyChanged(nameof(Resultado));
                }
            }
        }
        private string _resultado;
        void AgregarOperacion(string state)
        {
            Operation += state;
        }
        public void InicializarPropiedades()
        {
            Operation = "";
            ListaTemporalOperaciones = new ObservableCollection<string>();
        }
        void AgregarALista()
        {
            ListaTemporalOperaciones.Add(Operation);
            ListaOriginalOperaciones = ListaTemporalOperaciones;

        }
        void InicializarComandos()
        {
            ComandoAgregarOperacion = new RelayCommand<string>(AgregarOperacion);
            ComandoEjecutarOperacion = new RelayCommand(EjecutarOperacion);
            ComandoLimpiar = new RelayCommand(Limpiar);
            ComandoBorrarCaracter = new RelayCommand(BorrarCaracter);
            ComandoFiltrarPorSuma = new RelayCommand(FiltrarPorSuma);
            ComandoFiltrarPorResta = new RelayCommand(FiltrarPorResta);
            ComandoFiltrarPorMulti = new RelayCommand(FiltrarPorMulti);
            ComandoFiltrarPorDivision = new RelayCommand(FiltrarPorDivision);
            ComandoFiltrarTodo = new RelayCommand(FiltrarTodo);

            ComandoAgregarSigno = new RelayCommand(AgregarSigno);//Agregar funcion signo junto a la funciones para calcular cualquier operacion
            
        }
        void AgregarSigno()
        {

        }
        public ObservableCollection<T> ConvertToObservableCollection<T>(IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
        private void FiltrarPorSuma()
        {
            ListaTemporalOperaciones = ConvertToObservableCollection(ListaOriginalOperaciones
                                       .Where(suma => suma.Contains(MathOperation.SUM)));        
        }
        private void FiltrarPorResta()
        {
            ListaTemporalOperaciones = ConvertToObservableCollection(ListaOriginalOperaciones.Where(resta => resta.Contains(MathOperation.REST)));

        }
        private void FiltrarPorMulti()
        {
            ListaTemporalOperaciones = ConvertToObservableCollection(ListaOriginalOperaciones.Where(mutli => mutli.Contains(MathOperation.MULTIPLY)));
        }
        private void FiltrarPorDivision()
        {
            ListaTemporalOperaciones = ConvertToObservableCollection(ListaOriginalOperaciones.Where(division => division.Contains(MathOperation.DIVISION)));

        }

        private void FiltrarTodo()
        {
            ListaTemporalOperaciones = ListaOriginalOperaciones;

        }

        void Limpiar()
        {
            Operation = string.Empty;
            Resultado = string.Empty;
        }

        void BorrarCaracter()
        {
            string s = Operation.Substring(0, Operation.Length - 1);
            Operation = s;
        }

        void EjecutarOperacion()
        {

            string operation = Operation.Contains("+") ? MathOperation.SUM : Operation.Contains("-") ? MathOperation.REST
                                : Operation.Contains("*") ? MathOperation.MULTIPLY
                                : Operation.Contains("/") ? MathOperation.DIVISION : "NOTHIN";

            int operationIndex = Operation.IndexOf(operation);

            string firstIput = Operation.Substring(0, operationIndex);
            decimal firstNumber = decimal.Parse(firstIput);

            string secundInput = Operation.Substring(operationIndex + 1, Operation.Length - operationIndex - 1);
            decimal secondNumber = decimal.Parse(secundInput);

            string uri2 = $"api/Calculator" + firstNumber + secondNumber +  operation;


            string uri = $"api/Calculator?firstNumber={firstNumber}&secondNumber={secondNumber}&operator={operation}";

            client.GetAsync<CalculatorDto>(uri).Success(res => Resultado = res.Result.ToString());
            AgregarALista();
        }


        public RelayCommand<string> ComandoAgregarOperacion { get; set; }
        public RelayCommand ComandoBorrarCaracter { get; set; }
        public RelayCommand ComandoEjecutarOperacion { get; set; }
        public RelayCommand ComandoLimpiar { get; set; }
        public RelayCommand ComandoAgregarALista { get;  set; }
        public RelayCommand ComandoFiltrarPorSuma { get; set; }
        public RelayCommand ComandoFiltrarPorResta { get;  set; }
        public RelayCommand ComandoFiltrarPorMulti { get; set; }
        public RelayCommand ComandoFiltrarPorDivision { get; set; }
        public RelayCommand ComandoFiltrarTodo { get; set; }
        public RelayCommand ComandoAgregarSigno { get; set; }
    }
}
