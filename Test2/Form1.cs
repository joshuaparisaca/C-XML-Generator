using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using TestLibrary;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text.RegularExpressions;

/*
 * Author: Joshua Parisaca 
 * Program's Purpose: This program will take the email, first/last name, full address of a person and convert it to a XML file.
 * The 6 buttons are:
 * Create XML: This will take the input information and use it to create an XML file, this has to be pressed before any other options are available
 * Add: This will add a new record as long as the new record isn't using the a already saved email.
 * Delete: This will delete a record based on a saved email
 * Modify: This will modify a record based on a saved email and the changes on the input
 * Display XML: Will open up a file system dialog that will only let you choose a XML file to display on a web browser.
 * Show All: Will open a new windows form to show all the records saved since the start of the program seperated by the input categories
 * Clear: Clears all input
 * 
 * NOTES: ALL fields must be entered
 *        Zip Code needs to be 5 digits
 *        Email has to have a " *@*.com " with * being wildcards
 *        First Name cant have any spaces
*/

namespace Test2
{
    public partial class Project1 : Form
    {
        dataMethods testData;
        List<Person> class1 = new List<Person>();
        const string userInfo = @"..\..\userInfo.xml";


        public Project1()
        {
            InitializeComponent();
        }

        private void Project1_Load(object sender, EventArgs e)
        {
            testData = new dataMethods();

            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string prefix = "@";
            string suffix = ".com";
            string pattern = string.Format("{0}.*{1}", prefix, suffix);
            bool emailCheck = Regex.IsMatch(emailBox.Text, pattern);

            if (emailBox.Text.Length == 0 || FirstNameBox.Text.Length == 0 || LastNameBox.Text.Length == 0 || AddressBox.Text.Length == 0 || CityBox.Text.Length == 0 || StateBox.Text.Length == 0 || ZipBox.Text.Length == 0)
            {
                MessageBox.Show("Fill in all the boxes");

            }
            else if (Regex.IsMatch(FirstNameBox.Text, @"\s"))
            {
                MessageBox.Show("First Name can't have any spaces");
            }

            else if (emailCheck == false || Regex.IsMatch(emailBox.Text, @"\s") || emailBox.Text == "@.com")
            {
                MessageBox.Show("Enter a proper email");
            }

            else
            {
                int parsedValue;
                int length = ZipBox.Text.ToString().Length;
                if (!int.TryParse(ZipBox.Text.ToString(), out parsedValue) || length != 5 || emailCheck == false)
                {
                    MessageBox.Show("ZipCode must be 5 digits");
                }

                else
                {
                    testData.createUser(emailBox.Text, FirstNameBox.Text, LastNameBox.Text, AddressBox.Text, CityBox.Text, StateBox.Text, Int32.Parse(ZipBox.Text));

                    button1.Enabled = false;

                    button2.Enabled = true;

                    button3.Enabled = true;

                    button4.Enabled = true;

                    button5.Enabled = true;

                    button6.Enabled = true;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            testData.addUser(emailBox.Text, FirstNameBox.Text, LastNameBox.Text, AddressBox.Text, CityBox.Text, StateBox.Text, Int32.Parse(ZipBox.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testData.deleteUser(emailBox.Text, Int32.Parse(ZipBox.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            testData.modifyUser(emailBox.Text, FirstNameBox.Text, LastNameBox.Text, AddressBox.Text, CityBox.Text, StateBox.Text, ZipBox.Text); //Int32.Parse(ZipBox.Text)
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileSystem = new OpenFileDialog();
            //a.ShowDialog();

            fileSystem.Filter = "XML | *.xml";
            if (fileSystem.ShowDialog() == DialogResult.OK)
            {
                string file = fileSystem.SafeFileName;
                //MessageBox.Show(file);
                
                BrowserForm x = new BrowserForm();
                //x.URL = "userInfo.xml";
                x.URL = file;
                x.ShowDialog();
                
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = -1;
                }
            }
        }
    }
}
/*PATCHES
Do this same project using LINQ
 */