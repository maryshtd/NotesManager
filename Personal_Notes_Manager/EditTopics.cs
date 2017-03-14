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
    public partial class EditTopics : Form
    {
        string path1;
        string path2;
        XDocument doc1;
        XDocument doc2;

        public EditTopics()
        {
            InitializeComponent();
            path2 = @"..\..\Topics.xml";
            doc2 = XDocument.Load(path2);
            path1 = @"..\..\Categories.xml";
            doc1 = XDocument.Load(path1);
        }

        private void EditTopics_Load(object sender, EventArgs e)
        {
            foreach (XElement x in doc1.Element("root").Elements("categories"))
                comboBox1.Items.Add(x.Attribute("name").Value);
            comboBox1.SelectedIndex = 0;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string category = comboBox1.SelectedItem.ToString();
            var c = from x in doc2.Element("root").Elements("topic")
                    where x.Attribute("category").Value == category
                    select x;
            foreach (XElement y in c)
            {
                listBox1.Items.Add(y.Attribute("name").Value);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string cat = comboBox1.SelectedItem.ToString();
            string name = textBox1.Text;
            if (name == "")
            {
                MessageBox.Show("You didn't enter the name of the topic", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
            }
            else
            {
                doc2.Element("root").Add(
                    new XElement("topic",
                        new XAttribute("name", name),
                         new XAttribute("category", cat)));

                doc2.Save(path2);

                textBox1.Clear();
                MessageBox.Show("Topic was added", "Success",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
