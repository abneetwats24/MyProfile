using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Profile_Database
{
    public partial class Form2 : Form
    {
        Form1 form1;
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VIsual\Profile_Database\Profile_Database\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter sda;
        Form form;
        public string value = "";
        public string value1 = "";
        public string filepath;
        
        public Form2(Form1 frm1)
        {
            InitializeComponent();
            form1 = frm1;
            con.Open();
            sda = new SqlDataAdapter("select * from Personal_Data where Email='" + form1.emailtxt.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach(DataRow row in dt.Rows)
            {
                string welcome = "Welcome " + row["Name"].ToString() + " " + row["Father_Name"].ToString() + " " + row["Surname"].ToString() + ",";
                welcomelbl.Text = welcome;
            }         
            con.Close();
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
            bool isChecked = maleradio.Checked;
            bool isChecked1 = femaleradio.Checked;
            if (isChecked)
                value = maleradio.Text;
            else if (isChecked1)
                value = femaleradio.Text;
            else
                value = "";
            bool isChecked2 = indiaradio.Checked;
            bool isChecked3 = otherradio.Checked;
            if (isChecked2)
                value1 = indiaradio.Text;
            else if (isChecked3)
                value1 = otherradio.Text;
            else
                value1 = "";
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = "Update Personal_Data SET Date_Of_Birth=@dob,Gender=@gender,Nationality=@nat,Mother_Tongue=@mot,Image=@image where Email=@email";
            cmd.Parameters.AddWithValue("@dob", dob.Value);
            cmd.Parameters.AddWithValue("@gender", value);
            cmd.Parameters.AddWithValue("@nat", value1);
            cmd.Parameters.AddWithValue("@mot", mothertonguetxt.Text);
            cmd.Parameters.AddWithValue("@image", filepath);
            cmd.Parameters.AddWithValue("@email", form1.emailtxt.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Form2 Data Inserted Sucessfully", "Data Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (value.Length > 0 && value1.Length > 0 && mothertonguetxt.Text.Length > 0 && pictureBox.Image != null)
            {
                this.Hide();
                form = checkformexist("Form3");
                if (form == null)
                {
                    Form3 f3 = new Form3(form1, this);
                    f3.Show();
                }
                else
                {
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("Please fill remaining details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void previousbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            form = checkformexist("Form1");
            if (form == null)
            {
                Form1 f1 = new Form1();
                f1.Show();
            }
            else
            {
                form.Show();
            }
        }
 
        private void browsebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Chose your image....";
            fd.InitialDirectory = @"C:\Users\mithil\Desktop\";
            fd.Filter = "ImageFiles(*.jpg; *.png; *.jpeg; *.bmp; *.gif)|*.jpeg; *.jpg; *.bmp; *.gif; *.png";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filepath = fd.FileName;
                pictureBox.Image = new Bitmap(filepath);
            }
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
