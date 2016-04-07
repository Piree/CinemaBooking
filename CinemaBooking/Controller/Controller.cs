using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CinemaBooking.Model;
using System.Windows.Forms;
using CinemaBooking.DataAccessLayer;
using System.Data.Entity.Infrastructure;

namespace CinemaBooking
{
    class Controller
    {
        DAL dal = new DAL();

        // Get Customer method
        internal Customer GetCustomer(int customerID)
        {
            return dal.GetCustomer(customerID);
        }

        // Get Customers to list method
        internal List<Customer> SearchCustomer(string searchKeyword)
        {
            return dal.SearchCustomer(searchKeyword);
        }

        // Get Movie method
        internal Movie GetMovie(int movieID)
        {
            return dal.GetMovie(movieID);
        }

        // Get Movies to list method
        internal List<Movie> SearchMovie(string searchKeyword)
        {
            return dal.SearchMovie(searchKeyword);
        }

        // Get ScreeningData to list method
        internal List<ScreeningData> GetScreenings(string date)
        {
            return dal.GetScreenings(date);
        }

        // Get Salons method
        internal List<int> GetSalons()
        {
            return dal.GetSalons();
        }

        // Update Customer method
        internal void UpdateCustomer(int customerId, string firstName, string lastName, string address, string mail, string phoneNbr)
        {
            Customer customer = new Customer();
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Address = address;
            customer.Mail = mail;
            customer.PhoneNbr = phoneNbr;
            customer.CustomerId = customerId;

            dal.UpdateCustomer(customer);
        }

        // Update Movie method
        internal void UpdateMovie(int movieId, string ageLimit, string title, int runTime, string genre, string year)
        {
            Movie movie = new Movie();
            movie.MovieId = movieId;
            movie.Title = title;
            movie.Genre = genre;
            movie.RunTime = runTime;
            movie.AgeLimit = ageLimit;
            movie.MovieId = movieId;
            movie.Year = year;

            dal.UpdateMovie(movie);
        }

        // Register Customer method
        internal void RegisterCustomer(string firstName, string lastName, string address, string mail, string phoneNbr)
        {
            Customer newCustomer = new Customer();

            newCustomer.FirstName = firstName;
            newCustomer.LastName = lastName;
            newCustomer.Address = address;
            newCustomer.Mail = mail;
            newCustomer.PhoneNbr = phoneNbr;

            dal.RegisterNewCustomer(newCustomer);
        }

        // Add Movie method
        internal void AddNewMovie(string movieTitle, string movieYear, string movieGenre, int movieRunTime, string movieAgeLimit)
        {
            Movie newMovie = new Movie();

            newMovie.Title = movieTitle;
            newMovie.Year = movieYear;
            newMovie.Genre = movieGenre;
            newMovie.RunTime = movieRunTime;
            newMovie.AgeLimit = movieAgeLimit;

            dal.AddNewMovie(newMovie);
        }

        // Add Booking method
        internal void AddNewBooking(int customerId, int ScreeningId, int numberOfPeople)
        {
            Booking newBooking = new Booking();

            newBooking.CustomerId = customerId;
            newBooking.ScreeningId = ScreeningId;
            newBooking.NumberOfPeople = numberOfPeople;

            dal.AddNewBooking(newBooking);
        }

        // Add Screening method
        internal void AddNewScreening(int movieId, int salonId, DateTime dateTime)
        {
            Screening newScreening = new Screening();

            newScreening.MovieId = movieId;
            newScreening.SalonId = salonId;
            newScreening.StartDateTime = dateTime;

            dal.AddNewScreening(newScreening);
        }

        // Get number of seats booked on Screening method
        internal int GetNumberOfSeatsBooked(int screeningId)
        {
            return dal.GetNumberOfSeatsBooked(screeningId);
        }

        // Get number seats on Screening method
        internal int GetNumberOfSeats(int screeningId)
        {
            return dal.GetNumberOfSeats(screeningId);
        }

        // Get Customers on Screening to list method
        internal List<BookingData> GetCustomersOnScreening(int screeningId)
        {
            return dal.GetCustomersOnScreening(screeningId);
        }

        // Get Screening datetime method
        internal DateTime GetScreeningDateAndTime(int screeningId)
        {
            return dal.GetScreeningDateAndTime(screeningId);
        }

        // Get Screening runtime method
        internal int GetScreeningRunTime(int screeningId)
        {
            return dal.GetScreeningRunTime(screeningId);
        }

        // Delete Movie method
        internal void DeleteMovie(int selectedMovieId)
        {
            dal.DeleteMovie(selectedMovieId);
        }

        // Delete booking method
        internal void DeleteBooking(int selectedBookingId)
        {
            dal.DeleteBooking(selectedBookingId);
        }

        // Delete Customer method
        internal void DeleteCustomer(int customerId)
        {
            dal.DeleteCustomer(customerId);
        }

        // Get Movietitle form Screening
        internal string GetMovieTitleFromScreening(int screeningId)
        {
            return dal.GetMovieTitleFromScreening(screeningId);
        }

        // Get SalonID from Screening
        internal int GetSalonIDFromScreening(int screeningId)
        {
            return dal.GetSalonIDFromScreening(screeningId);
        }
    }
}