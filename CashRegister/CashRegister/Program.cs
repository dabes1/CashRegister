using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister
{
    public class Program
    {
        static double[] denominations = { 1000.00, 500.00, 100.00, 50.00, 20.00, 10.00, 5.00, 1.00, .25, .10, .05, .01 };

        static void Main(string[] args)
        {
            bool go = true;
            while (go)
            {
                CashRegister();

                Console.WriteLine("Run the Register again? Y/N");
                var response = Console.ReadLine();

                if (response == "N")
                    go = false;
            }
        }


        #region CashRegister
        static void CashRegister()
        {
            Console.WriteLine("Enter the Amount Charged: (DDDD.CC)");
            var inValue = Console.ReadLine();   // to pause the console from closing -- or you can do a Ctl-F5 and it will automatically pause.            
            Console.WriteLine("Enter the Amount Tendered:");
            var inTendered = Console.ReadLine();
            Console.WriteLine("Amount given: " + inTendered);
            string Change_Amt = ChangeAmount(Convert.ToDouble(inValue), Convert.ToDouble(inTendered));
            Console.WriteLine("Change Denominations: \n" + Change_Amt + '\n');
        }
        #endregion

        #region ChangeAmount
        static string ChangeAmount(double inAmtCharged, double inAmtTendered)
        {
            string oStr = "";
            double AmtChange = 0.00;

            if (inAmtTendered < inAmtCharged)
                return "Insufficient Amount Tendered";

            AmtChange = Convert.ToDouble((decimal)inAmtTendered - (decimal)inAmtCharged);
            Console.WriteLine("TOTAL Amount in Change Due: " + AmtChange + '\n');

            int currCtr = 0;

            foreach (double currentDenom in denominations)                      // Go thru each denomination
            {
                while (Convert.ToDouble(AmtChange) >= currentDenom)             // while the chg amt is greater than denomination, process that change amt
                {
                    currCtr = (int)((double)AmtChange / (double)currentDenom);  // how many multiples of curr denomination goes into current change amt?

                    if (AmtChange > .99)                                        // do we still have a dollar amount?
                        oStr += "$" + Convert.ToString(currentDenom) + " X " + currCtr.ToString() + "\n"; // Yes, display the denomination and its multiple
                    else
                        oStr += "c" + currentDenom.ToString() + " x " + currCtr.ToString() + "\n"; // No, display the cents and its mulitple

                    AmtChange -= Convert.ToDouble(currentDenom * Convert.ToDouble(currCtr)); // subtract the current denomination from the current change amt
                    AmtChange = Math.Round(AmtChange, 2);                       // Round up the values
                }
            }

            return oStr;
        }
        #endregion
    }
}
