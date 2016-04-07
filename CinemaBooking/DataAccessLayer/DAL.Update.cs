using CinemaBooking.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaBooking.DataAccessLayer
{
    partial class DAL
    {

        // Update Customer method
        internal void UpdateCustomer(Customer c)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    var original = db.Customer.Find(c.CustomerId);

                    if (original != null)
                    {
                        original.FirstName = c.FirstName;
                        original.Mail = c.Mail;
                        original.PhoneNbr = c.PhoneNbr;
                        original.LastName = c.LastName;
                        original.Address = c.Address;

                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException)
            {
                throw new DatabaseException("Det finns redan en kund registrerad med denna e-postadress.");
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

        // Update movie method
        internal void UpdateMovie(Movie m)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    var original = db.Movie.Find(m.MovieId);

                    if (original != null)
                    {
                        original.Title = m.Title;
                        original.RunTime = m.RunTime;
                        original.Year = m.Year;
                        original.AgeLimit = m.AgeLimit;
                        original.Genre = m.Genre;
                        original.Screening = m.Screening;

                        db.SaveChanges();
                    }
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

    }
}