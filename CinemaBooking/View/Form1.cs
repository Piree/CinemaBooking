using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaBooking
{
    public partial class Form1 : Form
    {
        Controller controller = new Controller();
        Validator validator = new Validator();

        public Form1()
        {
            InitializeComponent();

            // Load data at startup
            FillCustomers();
            FillMovies();
            LoadScreenings();
            FillComboBoxSalons();
        }

        //Register new Customer button
        private void buttonRegisterCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validator.TextBoxFilled(textBoxCustomerFirstName.Text, textBoxCustomerLastName.Text, textBoxCustomerAddress.Text, textBoxCustomerMail.Text, textBoxCustomerPhoneNbr.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }



                else
                {
                    string firstName = textBoxCustomerFirstName.Text;
                    string lastName = textBoxCustomerLastName.Text;
                    string address = textBoxCustomerAddress.Text;
                    string email = textBoxCustomerMail.Text;
                    string phoneNbr = textBoxCustomerPhoneNbr.Text;

                    if (!validator.IsEmailValid(email))
                    {
                        MessageBox.Show(validator.errorMessage);
                    }
                    if (!validator.isCustomerValid(textBoxCustomerFirstName.Text, textBoxCustomerLastName.Text))
                    {
                        MessageBox.Show(validator.errorMessage);
                    }
                    else
                    {
                        controller.RegisterCustomer(firstName, lastName, address, email, phoneNbr);
                        RefreshFeedBack("Du har lagt till kunden " + firstName + " " + lastName);
                        ClearAllCustomersTextFields();
                        FillCustomers();
                    }
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        //Delete Customer button
        private void buttonDeleteCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedCustomerID = Int32.Parse(dataGridViewCustomerSearch.CurrentRow.Cells[0].Value.ToString());
                string firstName = controller.GetCustomer(selectedCustomerID).FirstName;
                string lastName = controller.GetCustomer(selectedCustomerID).LastName;

                if (MessageBox.Show("Är du säker på att du vill ta bort kunden " + firstName + " " + lastName + "?", "Ta bort kund", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    controller.DeleteCustomer(selectedCustomerID);
                    RefreshFeedBack("Du har tagit bort kunden " + firstName + " " + lastName);
                    FillCustomers();
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        //Add new Movie button
        private void buttonAddMovie_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validator.TextBoxFilled(textBoxRegisterMovieTitle.Text, textBoxRegisterMovieYear.Text, textBoxRegisterMovieGenre.Text, textBoxRegisterMovieRunTime.Text, textBoxRegisterMovieAgeLimit.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }
                else if (!validator.IsMovieValid(textBoxRegisterMovieRunTime.Text, textBoxRegisterMovieYear.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }
                else
                {
                    string movieTitle = textBoxRegisterMovieTitle.Text.Trim();
                    string movieYear = textBoxRegisterMovieYear.Text.Trim();
                    string movieGenre = textBoxRegisterMovieGenre.Text.Trim();
                    int movieRunTime = Convert.ToInt32(textBoxRegisterMovieRunTime.Text.Trim());
                    string movieAgeLimit = textBoxRegisterMovieAgeLimit.Text.Trim();

                    if (MessageBox.Show("Vill du lägga till filmen " + movieTitle + "?", "Lägg till film", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        controller.AddNewMovie(movieTitle, movieYear, movieGenre, movieRunTime, movieAgeLimit);
                        RefreshFeedBack("Du har lagt till filmen " + movieTitle);
                        FillMovies();
                    }
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        //Delete Movie button
        private void buttonDeleteMovie_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedMovieID = Int32.Parse(dataGridViewMovieSearch.CurrentRow.Cells[0].Value.ToString());
                string title = controller.GetMovie(selectedMovieID).Title;

                if (MessageBox.Show("Är du säker på att du vill ta bort filmen " + title + "?", "Ta bort film", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    controller.DeleteMovie(selectedMovieID);
                    RefreshFeedBack("Du har tagit bort filmen " + title);
                    FillMovies();
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        //Add new Booking button
        private void buttonBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewScreenings.Rows.Count > 0)
                {
                    int selectedScreeningID = Int32.Parse(dataGridViewScreenings.CurrentRow.Cells[0].Value.ToString());
                    int selectedCustomerID = Int32.Parse(dataGridViewCustomerSearch.CurrentRow.Cells[0].Value.ToString());
                    int selectedNumberOfPeople = Int32.Parse(comboBoxNumberOfPeople.Text);
                    int numberOfSeatsBooked = controller.GetNumberOfSeatsBooked(selectedScreeningID);
                    int numberOfSeatsTotal = controller.GetNumberOfSeats(selectedScreeningID);
                    DateTime currentDate = DateTime.Now;
                    DateTime selectedScreeningDate = Convert.ToDateTime(dataGridViewScreenings.CurrentRow.Cells[2].Value.ToString());

                    if (selectedScreeningDate > currentDate)
                    {
                        if ((selectedNumberOfPeople + numberOfSeatsBooked) <= numberOfSeatsTotal)
                        {
                            string ticket = "biljett";
                            if (selectedNumberOfPeople > 1) { ticket = "biljetter"; }

                            controller.AddNewBooking(selectedCustomerID, selectedScreeningID, selectedNumberOfPeople);
                            RefreshFeedBack("Du har bokat " + " ' " + selectedNumberOfPeople + " ' " + " " + ticket + " för kunden " + " ' " + controller.GetCustomer(selectedCustomerID).FirstName + " " + controller.GetCustomer(selectedCustomerID).LastName + " ' " + " för filmen " + " ' " + controller.GetMovieTitleFromScreening(selectedScreeningID) + " ' " + " i salong nr " + " ' " + controller.GetSalonIDFromScreening(selectedScreeningID) + " ' ");
                            LoadScreenings();
                        }
                        else
                        {
                            MessageBox.Show("Det finns inte tillräckligt med platser ledigt för denna filmvisning!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Du kan inte boka biljetter bakåt i tiden!");
                    }

                }
                else
                {
                    MessageBox.Show("Du måste välja en filmvisning.");
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        //Add new Screening button
        private void buttonAddScreening_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewMovieSearch.Rows.Count > 0)
                {
                    int selectedMovieID = Int32.Parse(dataGridViewMovieSearch.CurrentRow.Cells[0].Value.ToString());
                    int selectedSalonID = Int32.Parse(comboBoxSalon.Text);

                    string selectedDate = monthCalendarScreenings.SelectionRange.Start.ToString("yyyy-MM-dd");
                    DateTime selectedDateTime = Convert.ToDateTime(selectedDate + " " + comboBoxScreeningTime.Text);

                    controller.AddNewScreening(selectedMovieID, selectedSalonID, selectedDateTime);
                    RefreshFeedBack("Filmvisning för " + controller.GetMovie(selectedMovieID).Title + " den " + selectedDateTime + " tillagd");
                    LoadScreenings();
                }
                else
                {
                    MessageBox.Show("Du måste välja en film.");
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Delete Booking button
        private void buttonDeleteBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewCustomersOnScreening.Rows.Count > 0)
                {
                    if (MessageBox.Show("Är du säker på att du vill ta bort bokningen?", "Ta bort kund", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        int selectedBookingID = Int32.Parse(dataGridViewCustomersOnScreening.CurrentRow.Cells[0].Value.ToString());
                        controller.DeleteBooking(selectedBookingID);
                        int screeningID = Int32.Parse(dataGridViewScreenings.CurrentRow.Cells[0].Value.ToString());
                        dataGridViewCustomersOnScreening.DataSource = controller.GetCustomersOnScreening(screeningID);

                        RefreshFeedBack("Du har tagit bort bokningen");
                        LoadScreenings();
                    }
                }
                else
                {
                    MessageBox.Show("Du måste välja en bokning.");
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Update Customer button
        private void buttonChangeCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validator.TextBoxFilled(textBoxCustomerFirstName.Text, textBoxCustomerLastName.Text, textBoxCustomerAddress.Text, textBoxCustomerMail.Text, textBoxCustomerPhoneNbr.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }

                if (!validator.isCustomerValid(textBoxCustomerFirstName.Text, textBoxCustomerLastName.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }

                else
                {

                    int customerId = Int32.Parse(dataGridViewCustomerSearch.CurrentRow.Cells[0].Value.ToString());

                    string firstName = textBoxCustomerFirstName.Text;
                    string lastName = textBoxCustomerLastName.Text;
                    string address = textBoxCustomerAddress.Text;
                    string email = textBoxCustomerMail.Text;
                    string phoneNbr = textBoxCustomerPhoneNbr.Text;

                    if (!validator.IsEmailValid(email))
                    {
                        MessageBox.Show(validator.errorMessage);
                    }
                    else
                    {
                        controller.UpdateCustomer(customerId, firstName, lastName, address, email, phoneNbr);
                        RefreshFeedBack("Du har uppdaterat kund " + firstName + " " + lastName);
                        FillCustomers();
                    }
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Update Movie button
        private void buttonChangeMovie_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!validator.TextBoxFilled(textBoxRegisterMovieTitle.Text, textBoxRegisterMovieYear.Text, textBoxRegisterMovieGenre.Text, textBoxRegisterMovieRunTime.Text, textBoxRegisterMovieAgeLimit.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }
                else if (!validator.IsMovieValid(textBoxRegisterMovieRunTime.Text, textBoxRegisterMovieYear.Text))
                {
                    MessageBox.Show(validator.errorMessage);
                }
                else
                {
                    int movieId = Int32.Parse(dataGridViewMovieSearch.CurrentRow.Cells[0].Value.ToString());
                    string genre = textBoxRegisterMovieGenre.Text;
                    string title = textBoxRegisterMovieTitle.Text;
                    int runTime = Int32.Parse(textBoxRegisterMovieRunTime.Text);
                    string ageLimit = textBoxRegisterMovieAgeLimit.Text;
                    string year = textBoxRegisterMovieYear.Text;

                    controller.UpdateMovie(movieId, ageLimit, title, runTime, genre, year);
                    RefreshFeedBack("Du har uppdaterat filmen " + title);
                    FillMovies();
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Clear fields for customer button
        private void buttonCustomerClearFields_Click(object sender, EventArgs e)
        {
            ClearAllCustomersTextFields();
        }

        // Clear fields for movie button
        private void buttonMovieClearFields_Click(object sender, EventArgs e)
        {
            ClearAllMoviesTextFields();
        }

        // Updates on tabchange
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Name == customerTab.Name)
            {
                groupBoxSelectedCustomer.Visible = true;
                groupBoxSelectedMovie.Visible = false;
                buttonBook.Enabled = true;
                comboBoxNumberOfPeople.Enabled = true;

                labelNumberOfPeople.Visible = true;
                comboBoxNumberOfPeople.Visible = true;
                buttonBook.Visible = true;

                labelSalon.Visible = false;
                comboBoxSalon.Visible = false;
                comboBoxScreeningTime.Visible = false;
                buttonAddScreening.Visible = false;
            }
            if (e.TabPage.Name == moviesTab.Name)
            {
                groupBoxSelectedMovie.Visible = true;
                groupBoxSelectedCustomer.Visible = false;
                buttonBook.Enabled = false;
                comboBoxNumberOfPeople.Enabled = false;

                labelNumberOfPeople.Visible = false;
                comboBoxNumberOfPeople.Visible = false;
                buttonBook.Visible = false;

                labelSalon.Visible = true;
                comboBoxSalon.Visible = true;
                comboBoxScreeningTime.Visible = true;
                buttonAddScreening.Visible = true;
            }
        }

        // Fill bookings on selection changed from Screenings datagrid
        private void dataGridViewScreenings_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int screeningID = Int32.Parse(dataGridViewScreenings.CurrentRow.Cells[0].Value.ToString());
                dataGridViewCustomersOnScreening.DataSource = controller.GetCustomersOnScreening(screeningID);
                dataGridViewCustomersOnScreening.Columns[0].Visible = false;
                dataGridViewCustomersOnScreening.Columns[1].HeaderText = "Namn";
                dataGridViewCustomersOnScreening.Columns[2].HeaderText = "Telefonnummer";
                dataGridViewCustomersOnScreening.Columns[3].HeaderText = "Antal personer";
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Fill Customer info on selection changed in customers datagrid
        private void dataGridViewCustomerSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedCustomerID = Int32.Parse(dataGridViewCustomerSearch.CurrentRow.Cells[0].Value.ToString());

                if (controller.GetCustomer(selectedCustomerID) != null)
                {
                    labelShowFirstname.Text = controller.GetCustomer(selectedCustomerID).FirstName;
                    labelShowLastName.Text = controller.GetCustomer(selectedCustomerID).LastName;
                    labelShowAddress.Text = controller.GetCustomer(selectedCustomerID).Address;
                    labelShowEmail.Text = controller.GetCustomer(selectedCustomerID).Mail;
                    labelShowPhoneNbr.Text = controller.GetCustomer(selectedCustomerID).PhoneNbr;

                    textBoxCustomerFirstName.Text = controller.GetCustomer(selectedCustomerID).FirstName;
                    textBoxCustomerLastName.Text = controller.GetCustomer(selectedCustomerID).LastName;
                    textBoxCustomerAddress.Text = controller.GetCustomer(selectedCustomerID).Address;
                    textBoxCustomerMail.Text = controller.GetCustomer(selectedCustomerID).Mail;
                    textBoxCustomerPhoneNbr.Text = controller.GetCustomer(selectedCustomerID).PhoneNbr;
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Fill Movie info on selection changed in movie datagrid
        private void dataGridViewMovieSearch_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                int selectedMovieID = Int32.Parse(dataGridViewMovieSearch.CurrentRow.Cells[0].Value.ToString());

                if (controller.GetMovie(selectedMovieID) != null)
                {
                    lblShowMovieTitle.Text = controller.GetMovie(selectedMovieID).Title;
                    lblShowMovieYear.Text = controller.GetMovie(selectedMovieID).Year;
                    lblShowMovieGenre.Text = controller.GetMovie(selectedMovieID).Genre;
                    lblShowMovieRunTime.Text = controller.GetMovie(selectedMovieID).RunTime.ToString();
                    lblShowMovieAgeLimit.Text = controller.GetMovie(selectedMovieID).AgeLimit;

                    textBoxRegisterMovieTitle.Text = controller.GetMovie(selectedMovieID).Title;
                    textBoxRegisterMovieYear.Text = controller.GetMovie(selectedMovieID).Year;
                    textBoxRegisterMovieGenre.Text = controller.GetMovie(selectedMovieID).Genre;
                    textBoxRegisterMovieRunTime.Text = controller.GetMovie(selectedMovieID).RunTime.ToString();
                    textBoxRegisterMovieAgeLimit.Text = controller.GetMovie(selectedMovieID).AgeLimit;
                }
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Fill Screenings from selected date
        private void LoadScreenings()
        {
            try
            {
                dataGridViewScreenings.DataSource = controller.GetScreenings(monthCalendarScreenings.SelectionRange.Start.ToString("yyyy-MM-dd"));
                dataGridViewScreenings.Columns[0].Visible = false;
                dataGridViewScreenings.Columns[1].HeaderText = "Film";
                dataGridViewScreenings.Columns[2].HeaderText = "Datum";
                dataGridViewScreenings.Columns[3].HeaderText = "Salong";
                dataGridViewScreenings.Columns[4].HeaderText = "Platser lediga";
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Fill Customers in datagrid from searchfield
        private void FillCustomers()
        {
            try
            {
                dataGridViewCustomerSearch.DataSource = controller.SearchCustomer(textBoxSearchCustomer.Text);
                dataGridViewCustomerSearch.Columns[0].Visible = false;
                dataGridViewCustomerSearch.Columns[3].Visible = false;
                dataGridViewCustomerSearch.Columns[4].Visible = false;
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
            
        }

        // Fill Movies in datagrid from searchfield
        private void FillMovies()
        {
            try
            {
                dataGridViewMovieSearch.DataSource = controller.SearchMovie(textBoxSearchMovie.Text);
                dataGridViewMovieSearch.Columns[0].Visible = false;
                dataGridViewMovieSearch.Columns[2].Visible = false;
                dataGridViewMovieSearch.Columns[3].Visible = false;
                dataGridViewMovieSearch.Columns[4].Visible = false;
                dataGridViewMovieSearch.Columns[5].Visible = false;
                dataGridViewMovieSearch.Columns[6].Visible = false;
            }
            catch (DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Fill combobox with Salons
        private void FillComboBoxSalons()
        {
            try
            {
                comboBoxSalon.DataSource = controller.GetSalons();
            }
            catch(DatabaseException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
            }
        }

        // Load Screenings on datechanged
        private void monthCalendarScreenings_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadScreenings();
        }

        // Fill Customers on textchanged in customer search
        private void textBoxSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            FillCustomers();
        }

        // Fill Movies in textchanged in movie search
        private void textBoxSearchMovie_TextChanged(object sender, EventArgs e)
        {
            FillMovies();
        }

        //Clear fields Movies
        private void ClearAllMoviesTextFields()
        {
            textBoxRegisterMovieTitle.Clear();
            textBoxRegisterMovieYear.Clear();
            textBoxRegisterMovieGenre.Clear();
            textBoxRegisterMovieRunTime.Clear();
            textBoxRegisterMovieAgeLimit.Clear();
        }

        //Clear fields Customers
        private void ClearAllCustomersTextFields()
        {
            dataGridViewCustomerSearch.ClearSelection();
            textBoxCustomerFirstName.Clear();
            textBoxCustomerLastName.Clear();
            textBoxCustomerAddress.Clear();
            textBoxCustomerMail.Clear();
            textBoxCustomerPhoneNbr.Clear();
        }

        // Refresh feedback box
        private void RefreshFeedBack(string message)
        {
            listBoxFeedBack.Items.Insert(0, message);
        }
    }
}