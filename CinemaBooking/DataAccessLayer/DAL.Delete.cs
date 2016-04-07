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

        // Delete Customer method
        internal void DeleteCustomer(int customerId)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    Customer c = db.Customer.Find(customerId);
                    db.Customer.Remove(c);
                    db.SaveChanges();
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

        // Delete Movie method
        internal void DeleteMovie(int selectedMovieID)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    Movie movie = db.Movie.Find(selectedMovieID);
                    db.Movie.Remove(movie);
                    db.SaveChanges();
                }
            }
            catch (EntityException)
            {
                throw new DatabaseException(defaultErrorMessage);
            }
        }

        // Delete Booking method
        internal void DeleteBooking(int selectedBookingId)
        {
            try
            {
                using (var db = new CinemaBookingDbEntities())
                {
                    Booking booking = db.Booking.Find(selectedBookingId);
                    db.Booking.Remove(booking);
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
