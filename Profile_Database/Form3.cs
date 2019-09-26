using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Profile_Database
{
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VIsual\Profile_Database\Profile_Database\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter sda;
        Form form;
        Form1 form1;
        Form2 form2;
        string qualification = "";
        string board = "";
        string percentage = "";
        static int id = 0;
        public Form3(Form1 frm1, Form2 frm2)
        {
            InitializeComponent();
            form1 = frm1;
            form2 = frm2;
            con.Open();
            sda = new SqlDataAdapter("select * from Personal_Data where Email='" + form1.emailtxt.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string welcome = "Welcome " + row["Name"].ToString() + " " + row["Father_Name"].ToString() + " " + row["Surname"].ToString() + ",";
                welcomelbl.Text = welcome;
            }
            con.Close();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            qualification = qualification + qualificationtxt.Text + "#";
            board = board + boardtxt.Text + "#";
            percentage = percentage + percentagetxt.Text + "#";
            id++;
            string[] row = { Convert.ToString(id), qualificationtxt.Text, boardtxt.Text, percentagetxt.Text };
            var lvi = new ListViewItem(row);
            listview.Items.Add(lvi);
            qualificationtxt.Clear();
            boardtxt.Clear();
            percentagetxt.Clear();
            gradetxt.Clear();
            qualificationtxt.Focus();
        }

        private void removebtn_Click(object sender, EventArgs e)
        {
            if (listview.Items.Count > 0)
            {
                listview.Items.Remove(listview.SelectedItems[0]);
            }
        }

        private Form checkformexist(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form form in fc)
            {
                if (form.Text == name)
                {
                    return form;
                }
            }
            return null;
        }

        private void nextbtn_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = "Update Personal_Data SET Qualification=@qua,Board=@board,Percentage=@per where Email=@email";
            cmd.Parameters.AddWithValue("@qua", qualification.Substring(0, qualification.Length - 1));
            cmd.Parameters.AddWithValue("@board", board.Substring(0, board.Length - 1));
            cmd.Parameters.AddWithValue("@per", percentage.Substring(0, percentage.Length - 1));
            cmd.Parameters.AddWithValue("@email", form1.emailtxt.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Form3 Data Inserted Sucessfully", "Data Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            form = checkformexist("Form4");
            if (form == null)
            {
                Form4 f4 = new Form4(form1, form2, this);
                f4.Show();
            }
            else
            {
                form.Show();
            }
        }

        private void previousbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            form = checkformexist("Form2");
            if (form == null)
            {
                Form2 f2 = new Form2(form1);
                f2.Show();
            }
            else
            {
                form.Show();
            }
        }
    }
}
