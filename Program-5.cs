/**********************************************************
*   Course:     PROG 110
*   Instructor: Dennis Minium
*   Term:       Spring 2017
*   
*   Programmer: Bobby Mohabeer
*   Assignment: Prog Assignment 5 - Methods
*   
*   Description: This program is meant to be an exact copy of Programming Assignment 3, but the code is organized into methods rather than the entire body
*   being located inside Main(). It involves using methods and return statements to work.
*   
*   Revision    Date        Revision Comment
*   --------    ----        ----------------
*   1.0         6/6/19     Initial Release
*   1.1         6/8/19     Worked on the first few methods and worked out the logic of the rest of the program
*   1.2         6/10/19    Finished all of the methods required by the instructions, as well a fixed a few bugs
*   1.3         6/11/19    Put on the final touches to the program, included comments, extra credit method and last minute bug fixing.

*/using System;
using System.Threading;
using static System.Console;

namespace Programming_Assignment_5___Methods
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            string address;
            string city;
            string goAgain = "";
            string state = "";
            string zip = "";
            string name = "";
            int anvils = 0;
            double total;
            bool member;
            bool winner = false;
            Random r = new Random();

            //Start of code

            do //start of loop
            {
                state = ""; // must be reset for a new order
                zip = "";
                name = "";
                anvils = 0;
                total = 0;
                member = false;
                winner = false;

                PrintBanner();//print out the banner

                Countdown(3);//initiates countdown

                WriteLine("\a******************************* NEW ORDER **********************************");

                Write("Firstly, please enter your full name: "); //Get info for shipping label

                name = GetValidString("name", 1);//verifies name

                WriteLine($"\nThank you, {name}! Now I just need a few pieces of information to process your delivery order.\n");

                Write("Street address: ");
                address = GetValidString("address", 1);//verifies street address

                Write("City: ");
                city = GetValidString("city", 1, 20);//verifies city

                string[] states = { "AK", "WA", "OR", "CA", "AZ", "NM", "CO", "UT" };//verifies state code
                Write("State (two letter code): ");
                state = GetValidState(GetValidString("state", 2, 2), states);

                Write("Zip Code: ");
                zip = GetValidString("zip code", 5, 5);//verifies zip code

                Write($"Thank you for all that {name}, now all I need to know is the number of anvils you would like to order: ");
                anvils = GetPositiveInt(ReadLine());//verifiest number of anvils is a positive int

                Write("\nAre you a member of our Futility Club, the frequent shopper program\nthat rewards persistence over results? (\"Y\" if yes): "); //Check if member of club
                member = LoyaltyDiscount();//checks if they're a member

                if (member == true)
                {
                    winner = EnterContest(r);
                }

                Write("\nPress any key to display invoice..."); //Display shipping label
                ReadKey();
                WriteLine("\n\n****************************************");//top of label
                WriteLine("*** ACME Anvils Corporation ***");
                WriteLine("Customer Invoice\n");
                WriteLine("SHIP TO:");
                WriteLine("\t" + name);
                WriteLine("\t" + address);
                WriteLine("\t" + city);
                WriteLine("\t" + state);
                WriteLine("\t" + zip);

                total = CalcAndPrintInvoiceBody(anvils, member, winner);//Print out reciept for the label

                WriteLine($"Your total today is {total:C}. Thanks for shopping with ACME!\n"); //display total
                WriteLine("And don't forget: ACME anvils fly farther, drop faster, and land harder than any other anvil on the market!\n");

                Write("Ready to take another order? (enter 'yes' or 'y' for yes or anything else for no): ");
                goAgain = ReadLine().ToUpper();

            } while (goAgain == "YES" || goAgain == "Y");

            //Debug Pause
            Write("\nGoodbye then.\nPress any key to end the program...");
            ReadKey();
        }

        private static void PrintBanner()
        {
            WriteLine("***Welcome to the ACME Anviles Corporation***"); //Welcome message
            WriteLine("Supporting the animation industry for 50 years and counting!\n");
        }

        private static void Countdown(int numberOfSeconds)
        {
            Thread.Sleep(1000); //Delay and countdown
            for (int x = numberOfSeconds; x > 0; x--)
            {
                WriteLine(x);
                Thread.Sleep(1000);
            }

        }

        private static int GetPositiveInt(string prompt)
        {
            bool validAnvils = false;
            int anvils2 = 0;

            validAnvils = int.TryParse(prompt, out anvils2);

            if (anvils2 <= 0)
            {
                validAnvils = false;
                WriteLine("Error message: Number of anvils must be a positive, whole number.");
            }

            while (!validAnvils)
            {
                validAnvils = int.TryParse(ReadLine(), out anvils2);

                if (anvils2 <= 0)
                {
                    validAnvils = false;
                    WriteLine("Error message: Number of anvils must be a positive, whole number.");
                }
            }
            return anvils2;
        }

        private static string GetValidString(string prompt, int min, int max)
        {

            bool valid = false;
            string stringName = "";
            while (!valid)
            {
                stringName = ReadLine();
                if (prompt == "city")
                {
                    if (stringName.Length >= min && stringName.Length <= max)
                    {
                        valid = true;
                    }
                    else
                    {
                        WriteLine($"Error message: {prompt} must be at between {min} and {max} characters long.");
                    }
                }
                else
                if (prompt == "zip code")
                {
                    if (stringName.Length == max && stringName.Length == min)
                    {
                        valid = true;
                    }
                    else
                    {
                        WriteLine($"Error message: {prompt} must be exactly {min} characters long.");
                    }
                }
                else
                if (prompt == "state")
                {
                    if (stringName.Length == max && stringName.Length == min)
                    {
                        valid = true;
                    }
                    else
                    {
                        WriteLine($"Error message: {prompt} must be exactly {min} characters long.");
                    }
                }
            }
            return stringName;
        }

        private static string GetValidState(string prompt, string[] sortedStateArray)
        {
            bool valid = false;
            while (!valid)
            {
                for (int x = 0; x < sortedStateArray.Length; x++)
                {
                    if (prompt.ToUpper().Equals(sortedStateArray[x]))
                    {
                        valid = true;
                    }
                }
                if (!valid)
                {
                    WriteLine("Error message: We do not ship to the string the user entered. Please enter again: ");
                    prompt = ReadLine();
                }
            }
            return prompt.ToUpper();
        }

        private static bool LoyaltyDiscount()
        {
            string prompt = ReadLine();

            if (prompt.ToUpper().Equals("Y"))
            {
                WriteLine("\nExcellent! You’ll receive an AMAZING 15% discount on today’s purchase!\nWhat’s more, as a valued " +
                "member of our loyalty program, you’ll have a chance to win a bonus gift in our exciting \nMembers-" +
                "Only Dehydrated Boulder contest!");
                return true;
            }
            else
            {
                WriteLine("\nWhat’s wrong with you? That just cost you a 15% discount and an opportunity to win a dehydrated boulder.Sad.");
                return false;
            }
        }

        private static bool EnterContest(Random ChanceGenerator)
        {
            bool winner = false;
            int guess = 0;
            int num = ChanceGenerator.Next(1, 11);

            Write("Pick a number between 1 and 10: ");
            int.TryParse(ReadLine(), out guess);

            if (guess >= 1 && guess <= 10)
            {
                if (guess == num)
                {
                    WriteLine($"\nWoo hoo! You guessed the secret number: {guess}!. An ACME Dehydrated Boulder is headed your way!");
                    winner = true;
                }
                else
                {
                    WriteLine($"\nAww, too bad. You guessed {guess}, but the secret number was {num}. No boulder. What a loser. Very sad.");
                }
            }
            else
            {
                WriteLine("\nThat’s not a number between 1 and 10. What an ultra-maron! Still, we value your loyalty.");
            }

            return winner;
        }

        private static double CalcAndPrintInvoiceBody(int qty, bool discount, bool promotionPrize)
        {
            const double MEMBER_DISCOUNT = 0.15;
            const double TAX = 0.0995;

            double costPerAnvil = GetAnvilPrice(qty);

            double subtotal = qty * costPerAnvil;
            double subBeforeDiscount = subtotal;
            double discountAmount = 0;
            double taxcost = 0;
            double grandTotal = 0;

            WriteLine($"\nQuantity order:\t\t{qty,16}"); //receipt
            WriteLine($"Cost per anvil:\t\t{costPerAnvil,16:C}");
            WriteLine($"Subtotal:\t\t{subBeforeDiscount,16:C}");

            if (discount == true) //change cost before tax
            {
                discountAmount = subtotal * MEMBER_DISCOUNT;
                subtotal -= discountAmount;
                discountAmount *= -1; //so it shows up as negative on the receipt
                WriteLine($"Less 15% Futility Club:\t{discountAmount,16:C}");
            }
            else
            {
                WriteLine($"No discount for you:\t{discountAmount,16:C}");
            }

            taxcost = subtotal * TAX; //tax
            grandTotal = subtotal + taxcost;

            subtotal = Math.Round(subtotal, 2); //round before to prevent errors
            taxcost = Math.Round(taxcost, 2);
            grandTotal = Math.Round(grandTotal, 2);

            WriteLine($"Sales Tax:\t\t{taxcost,16:C}");
            WriteLine("----------------------------------------");
            WriteLine($"TOTAL:\t\t{grandTotal,24:C}");

            if (promotionPrize == true)
            {
                WriteLine("\nAnd congratulations on winning a FREE DEHYDRATED BOULDER!!!");
            }
            WriteLine("****************************************\n"); //End Shipping label

            return grandTotal;
        }

        private static double GetAnvilPrice(int qty)
        {
            double price;
            if (qty <= 9) //Changes anvil cost depending on amount purchased
            {
                price = 80;
            }
            else if (qty <= 19)
            {
                price = 72.35;
            }
            else
            {
                price = 67.99;
            }
            return price;
        }

        private static string GetValidString(string prompt, int min)
        {
            string stringName = "";
            bool valid = false;

            while (!valid)
            {
                stringName = ReadLine();

                if (stringName.Length >= min)
                {
                    valid = true;
                }
                else
                {
                    WriteLine($"Error message: {prompt} must be at least {min} characters long.");
                }
            }
            return stringName;
        }
    }
}
