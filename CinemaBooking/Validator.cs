using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace CinemaBooking
{
    class Validator
    {
        internal string errorMessage { get; set; }

        internal bool TextBoxFilled(params string[] textFields)
        {
            foreach(string textField in textFields)
            {
                if (string.IsNullOrWhiteSpace(textField))
                {
                    errorMessage = "Fyll i alla fält!";
                    return false;
                }
            }
            return true;
        }

        public bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                errorMessage = "Ange en giltig e-postadress.";
                return false;
            }
        }

        public bool IsMovieValid(string movie, string year)
        {
           
                int i;
                bool validRunTime = int.TryParse(movie, out i);
                bool validYear = int.TryParse(year, out i);
                

                if (validRunTime == false)
                {
                    errorMessage = ("Ange korrekt spellängd");
                    return false;
                }
                if (validYear == false)
                {
                    errorMessage = ("Ange korrekt inspelningsår");
                    return false;
                }
                else return true;

            }

                    
        public bool isCustomerValid(string firstName, string lastName)
        {
            foreach (char c in firstName)
            {
                if (!char.IsLetter(c))
                {
                    errorMessage = "Namnet får bara innehålla bokstäver (A-Ö)";
                    return false;
                }
                foreach (char d in lastName)
                {
                    if (!char.IsLetter(d))
                    {
                        errorMessage = "Namnet får bara innehålla bokstäver (A-Ö)";
                        return false;
                    }
                }
            }
            return true;
        }
        
    }
}

