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
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\VIsual\Profile_Database\Profile_Database\Database1.mdf;Integrated Security=True");
        SqlDataAdapter sda;
        Form1 form1;
        Form2 form2;
        Form3 form3;
        public Form4(Form1 frm1, Form2 frm2, Form3 frm3)
        {
            form1 = frm1;
            form2 = frm2;
            form3 = frm3;
            InitializeComponent();
            con.Open();
            sda = new SqlDataAdapter("select * from Personal_Data where Email='" + form1.emailtxt.Text + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                namelbl.Text = row["Name"].ToString() + " " + row["Father_Name"].ToString() + " " + row["Surname"].ToString();
                addresslbl.Text = row["Address"].ToString() + " " + row["State"].ToString() + " " + row["Country"].ToString() + "--" + row["Pincode"].ToString();
                contactlbl.Text = "Mobile No-" + row["Mobile_No"].ToString() + "    Email-" + row["Email"].ToString();
                datelbl.Text = row["Date_Of_Birth"].ToString();
                genderlbl.Text = row["Gender"].ToString();
                nationalitylbl.Text = row["Nationality"].ToString();
                mothertonguelbl.Text = row["Mother_Tongue"].ToString();
                pictureBox1.Image = new Bitmap(row["Image"].ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                var qualification = row["Qualification"].ToString().Split('#');
                var board = row["Board"].ToString().Split('#');
                var percentage = row["Percentage"].ToString().Split('#');
                for(int i = 0; i < qualification.Length; i++)
                {
                    string[] a = { Convert.ToString(i + 1), qualification[i], board[i], percentage[i] };
                    var lvi = new ListViewItem(a);
                    listview1.Items.Add(lvi);
                }
            }
            con.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
