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

        // Register Customer method
        internal void RegisterNewCustomer(Customer newCustomer)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    db.Customer.Add(newCustomer);
                    db.SaveChanges();
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

        // Add Booking method
        internal void AddNewBooking(Booking newBooking)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    db.Booking.Add(newBooking);
                    db.SaveChanges();
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

        // Add Screening method
        internal void AddNewScreening(Screening newScreening)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    db.Screening.Add(newScreening);
                    db.SaveChanges();
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

        // Add Movie method
        internal void AddNewMovie(Movie newMovie)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    db.Movie.Add(newMovie);
                    db.SaveChanges();
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            } 
        }
    }
}