using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace TestLibrary
{
    public class dataMethods
    {
        StreamWriter sw;
        XmlSerializer serial;
        List<Person> Person;
        const string userInfo = @"..\..\userInfo.xml";

        public void createUser(String email, String fName, String lName, String address, String city, String state, int zip)
        {
            Person = new List<Person>();
            address = Regex.Replace(address, @"\s+", " ");
            Person s = new Person(email, fName, lName, address, city, state, zip);
            Person.Add(s);

            sw = new StreamWriter(userInfo);
            serial = new XmlSerializer(Person.GetType());

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces(); //THIS LINE GETS RID OF THE HEADER SO THAT Python.Pandas CAN READ THE XML FILE!!
            ns.Add("", "");//LOOK AT LINE ABOVE

            serial.Serialize(sw, Person, ns);
            sw.Close();

            MessageBox.Show("XML File has been created");
        }

        public void addUser(String email, String fName, String lName, String address, String city, String state, int zip)
        {
            string prefix = "@";
            string suffix = ".com";
            string pattern = string.Format("{0}.*{1}", prefix, suffix);
            bool emailCheck = Regex.IsMatch(email, pattern);

            if (email.Length == 0 || fName.Length == 0 || lName.Length == 0 || address.Length == 0 || city.Length == 0 || state.Length == 0 || zip.ToString().Length == 0)
            {
                MessageBox.Show("Fill in all the boxes");
            }
            else if(Regex.IsMatch(fName, @"\s"))
            {
                MessageBox.Show("First Name can't have any spaces");
            }
            else if (emailCheck == false || Regex.IsMatch(email, @"\s") || email == "@.com")
            {
                MessageBox.Show("Enter a proper email");
            }

            else
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(userInfo);
                XmlNode nl = xd.SelectSingleNode("//ArrayOfPerson");
                XmlDocument xd2 = new XmlDocument();

                if (email.Length == 0 || Regex.IsMatch(email, @"\s"))
                {
                    MessageBox.Show("Email box can't be empty or have spaces");
                }
                else
                {
                    int parsedValue;
                    int length = zip.ToString().Length;
                    if (!int.TryParse(zip.ToString(), out parsedValue) || length != 5)
                    {
                        MessageBox.Show("ZipCode must be 5 digits");
                    }

                    else
                    {
                        XmlElement el = (XmlElement)xd.SelectSingleNode("//ArrayOfPerson/Person[Email='" + email + "']");
                        if (el != null)
                        {
                            MessageBox.Show("Email already exists");
                        }
                        else
                        {
                            address = Regex.Replace(address, @"\s+", " ");
                            xd2.LoadXml("<Person><Email>" + email + "</Email><FirstName>" + fName + "</FirstName><LastName>" + lName + "</LastName><Address>" + address + "</Address><City>" + city + "</City><State>" + state + "</State><ZipCode>" + zip + "</ZipCode></Person>");
                            XmlNode n = xd.ImportNode(xd2.FirstChild, true);
                            nl.AppendChild(n);
                            xd.Save(userInfo);
                            MessageBox.Show("Record was Added");
                        }
                    }
                }
            }
        }

        public void deleteUser(String email, int zip)
        {
            if (email.Length == 0)
            {
                MessageBox.Show("Email box can't be empty");
            }

            else
            {
                int parsedValue;
                int length = zip.ToString().Length;
                if (!int.TryParse(zip.ToString(), out parsedValue) || length != 5)
                {
                    MessageBox.Show("ZipCode must be 5 digits");
                }

                else
                {
                    XmlDocument xd = new XmlDocument();
                    xd.Load(userInfo);
                    XmlElement el = (XmlElement)xd.SelectSingleNode("//ArrayOfPerson/Person[Email='" + email + "']");
                    if (el != null)
                    {
                        el.ParentNode.RemoveChild(el);
                        MessageBox.Show("Record was deleted");
                    }

                    else
                    {
                        MessageBox.Show("Record doesn't exist");
                    }

                    xd.Save(userInfo);
                }
            }
        }

        public void modifyUser(String email, String fName, String lName, String address, String city, String state, String zip)
        {
            if (email.Length == 0 || fName.Length == 0 || lName.Length == 0 || address.Length == 0 || city.Length == 0 || state.Length == 0 || zip.ToString().Length == 0)
            {
                MessageBox.Show("Fill in all the boxes");
            }

            else if (Regex.IsMatch(fName, @"\s"))
            {
                MessageBox.Show("First Name can't have any spaces");
            }

            else
            {
                int parsedValue;
                int length = zip.ToString().Length;
                if (!int.TryParse(zip.ToString(), out parsedValue) || length != 5)
                {
                    MessageBox.Show("ZipCode must be 5 digits");
                }

                else
                {
                    XmlDocument xd = new XmlDocument();
                    xd.Load(userInfo);
                    XmlElement el = (XmlElement)xd.SelectSingleNode("//ArrayOfPerson/Person[Email='" + email + "']");
                    if (el != null)
                    {
                        MessageBox.Show("Record is modified");

                        address = Regex.Replace(address, @"\s+", " ");

                        xd.SelectSingleNode("//Person/FirstName").InnerText = fName;
                        xd.SelectSingleNode("//Person/LastName").InnerText = lName;
                        xd.SelectSingleNode("//Person/Address").InnerText = address;
                        xd.SelectSingleNode("//Person/City").InnerText = city;
                        xd.SelectSingleNode("//Person/State").InnerText = state;
                        xd.SelectSingleNode("//Person/ZipCode").InnerText = zip;
                    }

                    else
                    {
                        MessageBox.Show("Record doesn't exist");
                    }

                    xd.Save(userInfo);
                }
            }
        }

        public List<Person> ListDisplay()
        {
            Person = new List<Person>();
            StreamReader sr = new StreamReader(userInfo);

            serial = new XmlSerializer(Person.GetType());



            Person = (List<Person>)serial.Deserialize(sr);
            sr.Close();

            return Person;
        }
        
    }
}
