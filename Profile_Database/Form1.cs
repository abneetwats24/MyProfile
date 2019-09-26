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
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VIsual\Profile_Database\Profile_Database\Database1.mdf;Integrated Security=True");
        SqlCommand cmd;
        Form form;
        public Form1()
        {
            InitializeComponent();
        }

        private Form checkformexist(string name)
        {
            FormCollection fc = Application.OpenForms;
            foreach(Form form in fc)
            {
                if(form.Text == name)
                {
                    return form;
                }
            }
            return null;
        }

        private void nextbtn_Click(object sender, EventArgs e)
        {
            if (nametxt.Text.Length != 0 && fathertxt.Text.Length !=0 && surnametxt.Text.Length != 0 && addresstxt.Text.Length != 0 && mobiletxt.Text.Length != 0 && emailtxt.Text.Length !=0 && statecmb.Text.Length != 0 && countrycmb.Text.Length != 0 && pincodetxt.Text.Length != 0)
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "insert into Personal_Data(Name,Father_Name,Surname,Address,Mobile_No,Email,State,Country,Pincode) values (@name,@fat,@sur,@add,@mob,@email,@state,@country,@pin)";
                cmd.Parameters.AddWithValue("@name", nametxt.Text);
                cmd.Parameters.AddWithValue("@fat", fathertxt.Text);
                cmd.Parameters.AddWithValue("@sur", surnametxt.Text);
                cmd.Parameters.AddWithValue("@add", addresstxt.Text);
                cmd.Parameters.AddWithValue("@mob", mobiletxt.Text);
                cmd.Parameters.AddWithValue("@email", emailtxt.Text);
                cmd.Parameters.AddWithValue("@state", statecmb.Text);
                cmd.Parameters.AddWithValue("@country", countrycmb.Text);
                cmd.Parameters.AddWithValue("@pin", pincodetxt.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Form1 Data Inserted Sucessfully", "Data Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                form = checkformexist("Form2");
                if (form == null)
                {
                    Form2 f2 = new Form2(this);
                    f2.Show();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
