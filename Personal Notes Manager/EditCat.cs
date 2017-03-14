using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Personal_Notes_Manager
{
    public partial class EditCat : Form
    {
        string path;
        XDocument doc1;

        public EditCat()
        {
            InitializeComponent();
            path = @"..\..\Categories.xml";
            doc1 = XDocument.Load(path);
        }

        private void EditCat_Load(object sender, EventArgs e)
        {
            foreach (XElement x in doc1.Element("root").Elements("categories"))
                listBox1.Items.Add(x.Attribute("name").Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("You didn't enter the name of the category", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            else
            {
                listBox1.Items.Add(name);
                doc1.Element("root").Add(
                    new XElement("categories",
                        new XAttribute("name", name)));

                doc1.Save(path);

                textBox1.Clear();
                MessageBox.Show("Category was added", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = listBox1.SelectedItem.ToString();
            string mess = String.Format("Confirm deleting category {0}", name);
            DialogResult res = MessageBox.Show(mess, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
                listBox1.SelectedIndex = 0;
                var c = from x in doc1.Element("root").Elements("categories")
                        where x.Attribute("name").Value == name
                        select x;
                c.First().Remove();
                //var l = from y in doc2.Element("root").Elements("link")
                //        where y.Attribute("category").Value == name
                //        select y;
                //foreach (XElement z in l)
                //    z.Remove();
                doc1.Save(path);
                //doc2.Save(path2);
                MessageBox.Show("Category was deleted", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
