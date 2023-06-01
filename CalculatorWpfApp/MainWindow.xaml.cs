using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();
            acButton.Click += AcButton_Click;
            inversionButton.Click += InversionButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;

        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                resultLabel.Content = "0";

                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        resultLabel.Content = (lastNumber + newNumber).ToString();
                        break;
                    case SelectedOperator.Subtraction:
                        resultLabel.Content = (lastNumber - newNumber).ToString();
                        break;
                    case SelectedOperator.Division:
                        if (newNumber == 0)
                            MessageBox.Show("На ноль делить нельзя!", "Неправильная операция", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                            resultLabel.Content = (lastNumber /(double)newNumber).ToString();

                        break;
                    case SelectedOperator.Multiplication:
                        resultLabel.Content = (lastNumber * newNumber).ToString();
                        break;
                }
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divisionButton)
                selectedOperator = SelectedOperator.Division;
            if (sender == minusButton)
                selectedOperator = SelectedOperator.Subtraction;
            if (sender == plusButton)
                selectedOperator = SelectedOperator.Addition;
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            { 
                tempNumber /= 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void InversionButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0") 
                resultLabel.Content = selectedValue.ToString();
            else
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}".ToString();
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            lastNumber = 0;
        }

        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString().Contains("."))
                return;
            resultLabel.Content = $"{resultLabel.Content}.";
        }

        public enum SelectedOperator
        {
            Addition,
            Subtraction,
            Multiplication,
            Division
        }
    }
}
