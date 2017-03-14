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
    public partial class Form3 : Form
    {
        string path1;
        string path2;
        string path3;
        XDocument doc1;
        XDocument doc2;
        XDocument doc3;

        public Form3()
        {
            InitializeComponent();
            path2 = @"..\..\Topics.xml";
            doc2 = XDocument.Load(path2);
            path1 = @"..\..\Categories.xml";
            doc1 = XDocument.Load(path1);
            path3 = @"..\..\Notes.xml";
            doc3 = XDocument.Load(path3);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (XElement x in doc1.Element("root").Elements("categories"))
                comboBox2.Items.Add(x.Attribute("name").Value);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string category = comboBox2.SelectedItem.ToString();
            var c = from x in doc2.Element("root").Elements("topic")
                    where x.Attribute("category").Value == category
                    select x;
            foreach (XElement y in c)
            {
                comboBox1.Items.Add(y.Attribute("name").Value);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string topic = comboBox1.SelectedItem.ToString();
            var c = from x in doc3.Element("root").Elements("note")
                    where x.Attribute("topic").Value == topic
                    select x;
            foreach (XElement y in c)
            {
                listBox1.Items.Add(y.Attribute("name").Value);
            }
        }
    }
}
