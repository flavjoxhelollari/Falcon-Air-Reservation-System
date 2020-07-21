using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tickets;
using System.IO;

namespace project

{
    public partial class Form1 : Form
    {
        List<Flight> flightsList = new List<Flight>();
        List<Ticket> ticketsList = new List<Ticket>();
        List<Passenger> passengersList = new List<Passenger>();
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void newBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar1.SelectionRange.Start = this.monthCalendar1.SelectionRange.Start;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((textBox1.Text == ""))
            {
                MessageBox.Show("Please enter something in the textbox");
                return;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Please enter something in the textbox");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool ticketflag = false;
            if (!label17.Visible || textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                ticketflag = true;
                MessageBox.Show("Complete all field data for ticket!");
            }

            if (!ticketflag)
            {
                Random rnd = new Random();
                Passenger passenger = new Passenger(textBox1.Text, textBox2.Text);

                Flight theflight = new Flight();
                string type = label17.Text.Substring(0, 7);
                string takeOff = label17.Text.Substring(label17.Text.LastIndexOf(" ") + 1, 3);
                string landing = label17.Text.Substring(label17.Text.LastIndexOf("-") + 1);

                foreach (Flight item in flightsList)
                {
                    if (item.TakeOff == takeOff && item.Landing == landing && item.TakeOffDate == monthCalendar1.SelectionRange.Start)
                    {
                        theflight.FlightID = item.FlightID;
                        theflight.TakeOff = item.TakeOff;
                        theflight.Landing = item.Landing;
                        theflight.Stopover = item.Stopover;
                        theflight.AirlineType = item.AirlineType;
                        theflight.Passengers = item.Passengers;
                        theflight.Rows = item.Rows;
                        theflight.EatingOnBoard = item.EatingOnBoard;
                        theflight.TakeOffDate = item.TakeOffDate;
                        theflight.LandingDate = item.LandingDate;
                    }
                    break;
                }


                Ticket ticket = new Ticket(theflight, rnd.Next(), type, passenger,
                                            monthCalendar1.SelectionRange.Start,
                                           int.Parse(comboBox1.SelectedItem.ToString()),
                                           char.Parse(comboBox2.SelectedItem.ToString()));

                ticketsList.Add(ticket);
                passengersList.Add(passenger);

                string path = @"../../../Tickets.txt";
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine("Tickets Information");

                    }
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(ticket.DisplayInfo() + "\n");

                }

                string path1 = @"../../../Passengers.txt";
                // This text is added only once to the file.
                if (!File.Exists(path1))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path1))
                    {
                        sw.WriteLine("Passenger Information");

                    }
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(path1))
                {
                    sw.WriteLine("Passenger Name: " + passenger.FirstName + "\n" + "Passenger Last Name: " + passenger.LastName);

                }

                MessageBox.Show("Sent to file!");

                comboBox3.Items.Add(passenger.FirstName + " " + passenger.LastName);
            }

            //label19.Text = File.ReadAllText(@"../../../Flights.txt");
        }


        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBox2.Selected)
                this.label17.Text = this.onewayToolStripMenuItem.Text + " " + this.toolStripComboBox2.SelectedItem.ToString();
            this.label17.Visible = true;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.toolStripComboBox1.Selected)
                this.label17.Text = this.twowayToolStripMenuItem.Text + " " + this.toolStripComboBox1.SelectedItem.ToString();
            this.label17.Visible = true;
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            File.WriteAllText(@"../../../Flights.txt", String.Empty);
            File.WriteAllText(@"../../../Tickets.txt", String.Empty);
            File.WriteAllText(@"../../../Passengers.txt", String.Empty);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectTab(2);

        }

        private void passengersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectTab(0);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool flightflag = false;
            if (textBox3.Text == "" || comboBox4.Text == "" || comboBox5.Text == "" || comboBox6.Text == "" || comboBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || comboBox8.Text == "")
            {
                flightflag = true;
                MessageBox.Show("Complete all field data for flight!");
            }

            if (!flightflag)
            {
                int output1;
                if (!int.TryParse(this.textBox8.Text, out output1) || (int.TryParse(this.textBox8.Text, out output1) &&
                    int.Parse(this.textBox8.Text) < 1 || int.Parse(this.textBox8.Text) > 1000))
                {
                    MessageBox.Show("Enter a valid number between 1 to 1000");
                    textBox8.Text = "";
                }



                int output2;
                if (!int.TryParse(this.textBox9.Text, out output2) || (int.TryParse(this.textBox9.Text, out output2) &&
                    int.Parse(this.textBox9.Text) < 1 || int.Parse(this.textBox9.Text) > 100))
                {
                    MessageBox.Show("Enter a valid number between 1 to 100");
                    textBox9.Text = "";
                }



                bool flag = false;
                foreach (Flight item in flightsList)
                {
                    if (item.FlightID == textBox3.Text || (item.TakeOff == comboBox4.SelectedItem.ToString()
                        && item.Landing == comboBox5.SelectedItem.ToString() && item.TakeOffDate == monthCalendar2.SelectionRange.Start))
                    {
                        MessageBox.Show("Flight already exists! Enter new criteria.");
                        textBox3.Text = "";
                        flag = true;
                        break;
                    }
                }




                bool eating = false;
                if (comboBox8.SelectedItem.ToString() == "YES")
                    eating = true;

                else if (comboBox8.SelectedItem.ToString() == "NO")
                    eating = false;

                if (!flag)
                {
                    Flight flight = new Flight(textBox3.Text, comboBox4.SelectedItem.ToString(), comboBox5.SelectedItem.
                       ToString(), comboBox6.SelectedItem.ToString(), comboBox7.SelectedItem.ToString(), int.Parse(textBox8.Text),
                       int.Parse(textBox9.Text), eating, monthCalendar2.SelectionRange.Start, monthCalendar3.SelectionRange.End);


                    flightsList.Add(flight);

                    string path = @"../../../Flights.txt";
                    // This text is added only once to the file.
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine("Flight Information");

                        }
                    }

                    // This text is always added, making the file longer over time
                    // if it is not deleted.
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(flight.DisplayInfo() + "\n");
                        

                    }



                    MessageBox.Show("Sent to file!");

                    foreach (Flight item in flightsList)
                    {
                        toolStripComboBox1.Items.Add(item.TakeOff + "-" + item.Landing);

                    }

                    foreach (Flight item in flightsList)
                    {
                        toolStripComboBox2.Items.Add(item.TakeOff + "-" + item.Landing);

                    }

                    comboBox9.Items.Add(flight.FlightID );




                }
            }


        }

        private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar2.SelectionRange.Start = DateTime.Now;
        }

        private void monthCalendar3_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.monthCalendar3.SelectionRange.Start = this.monthCalendar2.SelectionRange.Start.AddDays(1);
        }


        private void Form1_Leave(object sender, EventArgs e)
        {

        }



        private void twowayToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void onewayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ticketflag = false;
            if (!label17.Visible || textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                ticketflag = true;
                MessageBox.Show("Complete all field data for ticket!");
            }

            if (!ticketflag)
            {


                Flight theflight = new Flight();
                string type = label17.Text.Substring(0, 7);
                string takeOff = label17.Text.Substring(label17.Text.LastIndexOf(" ") + 1, 3);
                string landing = label17.Text.Substring(label17.Text.LastIndexOf("-") + 1);

                foreach (Flight item in flightsList)
                {
                    if (item.TakeOff == takeOff && item.Landing == landing && item.TakeOffDate == monthCalendar1.SelectionRange.Start)
                    {
                        theflight.FlightID = item.FlightID;
                        theflight.TakeOff = item.TakeOff;
                        theflight.Landing = item.Landing;
                        theflight.Stopover = item.Stopover;
                        theflight.AirlineType = item.AirlineType;
                        theflight.Passengers = item.Passengers;
                        theflight.Rows = item.Rows;
                        theflight.EatingOnBoard = item.EatingOnBoard;
                        theflight.TakeOffDate = item.TakeOffDate;
                        theflight.LandingDate = item.LandingDate;
                    }
                    break;
                }

                bool alertmessage = false;
                foreach (Ticket item in ticketsList)
                {
                    if (item.Flight.FlightID == theflight.FlightID && item.SeatNumber == int.Parse(comboBox1.Text) && item.SeatAlignment == char.Parse(comboBox2.Text))
                    {
                        alertmessage = true;
                        MessageBox.Show("Seat already taken!");
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                    }


                    break;
                }

                if (!alertmessage) MessageBox.Show("Seat is free!");


            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Ticket item in ticketsList)
            {
                string fullName = item.Passenger.FirstName + " " + item.Passenger.LastName;
                if (fullName == comboBox3.SelectedItem.ToString())
                {
                    MessageBox.Show(item.DisplayInfo());
                    break;

                }
            }
        }

        private void ticketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label18_Click(object sender, EventArgs e)
        {
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                foreach (Flight item in flightsList)
                {
                    string value = item.FlightID ;
                    if (value == comboBox9.SelectedItem.ToString())
                    {
                        MessageBox.Show(item.DisplayInfo());
                        break;

                    }
                }
        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }

        //Resets the oldest item into an empty one and
        //redirects the user to create a new Flight obj
        //Following queue logic
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flightsList.Count() > 0)
            {
                this.flightsList[0] = new Flight();
                this.tabControl1.SelectTab(2);
            }

            else
                Console.WriteLine("No flights registered");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //deletes the first element in flightList instance
            // resembling the pop function of a queue

            if (flightsList.Count() > 0)
            {
                flightsList.RemoveAt(0);
                comboBox9.Items.RemoveAt(0);

            }

            else

                flightsList.Add(null);

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}


