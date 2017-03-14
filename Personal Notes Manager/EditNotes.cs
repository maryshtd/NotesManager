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
    public partial class EditNotes : Form
    {
        string path1;
        string path3;
        XDocument doc1;
        XDocument doc3;
        public EditNotes()
        {
            InitializeComponent();
            path1 = @"..\..\Topics.xml";
            doc1 = XDocument.Load(path1);
            path3 = @"..\..\Notes.xml";
            doc3 = XDocument.Load(path3);
        }

        private void EditNotes_Load(object sender, EventArgs e)
        {
            foreach (XElement x in doc1.Element("root").Elements("topic"))
                comboBox1.Items.Add(x.Attribute("name").Value);
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Введите название заметки!", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                richTextBox1.ReadOnly = false;
                richTextBox1.BackColor = Color.Beige;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string topic = comboBox1.SelectedItem.ToString();
            string name = textBox1.Text;
            string text = richTextBox1.Text;

            doc3.Element("root").Add(
                    new XElement("note",
                        new XAttribute("name", name),
                         new XAttribute("topic", topic),
                         new XAttribute("text", text)
                         ));

            doc3.Save(path3);

            textBox1.Clear();
            richTextBox1.Clear();
            MessageBox.Show("Note was added", "Success",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
