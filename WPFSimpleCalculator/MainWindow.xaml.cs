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

namespace WPFSimpleCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            resultLabel.Content = "0";
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = lastNumber = 0;
        }

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) 
            {
                lastNumber *= -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber)) 
            {
                tempNumber /= 100;
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains(".")) resultLabel.Content += ".";
        }

        private void equalButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Add:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substract:
                        result = SimpleMath.Substract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Divide:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiply:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    default:
                        break;
                }
                resultLabel.Content = result;
            }
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
                resultLabel.Content = selectedValue;
            else
                resultLabel.Content += selectedValue.ToString();
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) 
            {
                resultLabel.Content = "0";
            }

            if (sender == plusButton) selectedOperator = SelectedOperator.Add;
            if (sender == minusButton) selectedOperator = SelectedOperator.Substract;
            if (sender == divisionButton) selectedOperator = SelectedOperator.Divide;
            if (sender == multiplyButton) selectedOperator = SelectedOperator.Multiply;
        }
    }
}

public class SimpleMath
{
    public static double Add(double x, double y) => x + y;
    public static double Substract(double x, double y) => x - y;
    public static double Divide(double x, double y)
    {
        if (y == 0)
        {
            MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
            return 0;
        }
        return x / y;
    }
    public static double Multiply(double x, double y) => x * y;
}


public enum SelectedOperator
{
    Add, Substract, Divide, Multiply
}
