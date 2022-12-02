using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Car_rental_Systemm
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;



        private void Car_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=LAPTOP-HUOGNCN9;Initial Catalog=Carrental;Integrated Security=True");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("car_cardetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (carlid.Text != " " && button1.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("@carid", Convert.ToInt32(carlid.Text));
                    cmd.Parameters.AddWithValue("@carmodel", carmodel.Text);
                    cmd.Parameters.AddWithValue("@carno", carnumber.Text);
                    cmd.Parameters.AddWithValue("@chassisno", chassisno.Text);
                    cmd.Parameters.AddWithValue("@caravg",caravrage.Text);
                    cmd.Parameters.AddWithValue("@rent", rent.Text);
                    cmd.Parameters.AddWithValue("@fuel", fuel.Text);
                    cmd.Parameters.AddWithValue("@seat", seat.Text);
                    cmd.Parameters.AddWithValue("@flag", "update");
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("record Updated");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("record not Updated");
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@flag", "insert");
                    cmd.Parameters.AddWithValue("@carmodel", carmodel.Text);
                    cmd.Parameters.AddWithValue("@carno", carnumber.Text);
                    cmd.Parameters.AddWithValue("@chassisno", chassisno.Text);
                    cmd.Parameters.AddWithValue("@caravg", caravrage.Text);
                    cmd.Parameters.AddWithValue("@rent", rent.Text);
                    cmd.Parameters.AddWithValue("@fuel", fuel.Text);
                    cmd.Parameters.AddWithValue("@seat", seat.Text);



                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("record Saved");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("record not Saved");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            con.Close();
        }
        public void clearAll()
        {
            carmodel.Text = "";
            carnumber.Text = "";
            chassisno.Text = "";
            caravrage.Text = "";
            rent.Text = "";
            fuel.Text = "";
            seat.Text = "";
            carlid.Text = " ";
            button1.Text = "Save";
            dataGridView1.DataSource = null;



        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (carlid.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("car_cardetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.Parameters.AddWithValue("@carid", carlid.Text);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("record deleted");
                        clearAll();
                    }
                    else
                    {
                        MessageBox.Show("record not deleted");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("car_cardetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "search");
                cmd.Parameters.AddWithValue("@carmodel", carmodel.Text);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception)
            {
            }
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                carlid.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                carmodel.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                carnumber.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                chassisno.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
                caravrage.Text = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
                rent.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                fuel.Text = dataGridView1.Rows[rowindex].Cells[6].Value.ToString();
                seat.Text = dataGridView1.Rows[rowindex].Cells[7].Value.ToString();
                button1.Text = "Update";
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
