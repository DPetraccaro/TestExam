using System;
using System.Security.Principal;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Calculator
{
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			//default the months and years to 0
			this.InitializeComponent();
			Months.Text = "0";  
			Years.Text = "0";
			Principal.Text = "0";
			YearlyInterestRate.Text = "0";
		}

		private void CalculateMortgage(object sender, RoutedEventArgs e)
		{

			// Validate the inputs
			if (double.TryParse(YearlyInterestRate.Text, out double annualRate) &&
				int.TryParse(Years.Text, out int years) &&
				double.TryParse(Principal.Text, out double P) &&
				int.TryParse(Months.Text, out int months))
			{

				// Convert the interest rate from yearly to monthly (monthly interest rate)
				double i = (annualRate / 100) / 12;

				// Calculate total number of months (number of months required to repay the loan)
				int n = (years * 12) + months;

				if (n <= 0)  // if no time period is given
				{
					MessageBox.Text = "ERROR: Loan must be a min of 1 month.";
					return;
				}

				double M;

				if (i == 0)  // if 0 interest is entered
				{
					M = P / n;
				}
				else
				{
					// formula: M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
					double power = Math.Pow(1 + i, n);
					M = P * (i * power) / (power - 1);
				}

				// Print the calculated monthly interest rate
				MonthlyInterestRate.Text = (i).ToString("F4") + "%";

				// Print the monthy repayment 
				MonathlyRepayment.Text = $"${M:F2}";
				//clear box if an error was previously stored
				MessageBox.Text = "";
			}
			else
			{
				MessageBox.Text = "ERROR! Please enter valid numbers / fill in all fields.";
			}
		}

		//exit and return
		private void Exit(object sender, RoutedEventArgs e)
		{
			if (this.Frame.CanGoBack)
			{
				this.Frame.GoBack();
			}
		}


	
	}
}