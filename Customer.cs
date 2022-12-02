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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void exite_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void Customer_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=LAPTOP-HUOGNCN9;Initial Catalog=Carrental;Integrated Security=True");
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("car_customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (lbid.Text != " " && button1.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("@cid",Convert.ToInt32(lbid.Text));
                    cmd.Parameters.AddWithValue("@cname", Cname.Text);
                    cmd.Parameters.AddWithValue("@cmob", Cmobno.Text);
                    cmd.Parameters.AddWithValue("@cadhar", Cadhar.Text);
                    cmd.Parameters.AddWithValue("@clino", Clino.Text);
                    cmd.Parameters.AddWithValue("@cadd", Cadd.Text);
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
                    cmd.Parameters.AddWithValue("@cname", Cname.Text);
                    cmd.Parameters.AddWithValue("@cmob", Cmobno.Text);
                    cmd.Parameters.AddWithValue("@cadhar", Cadhar.Text);
                    cmd.Parameters.AddWithValue("@clino", Clino.Text);
                    cmd.Parameters.AddWithValue("@cadd", Cadd.Text);



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
            Cname.Text = "";
            Cmobno.Text = "";
            Cadhar.Text = "";
            Clino.Text = "";
            button1.Text = "Save";
            Cadd.Text = "";
            lbid.Text = " ";
           
            dataGridView1.DataSource = null;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbid.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("car_customer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.Parameters.AddWithValue("@cid", lbid.Text);

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
                SqlCommand cmd = new SqlCommand("car_customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "search");
                cmd.Parameters.AddWithValue("@cname", Cname.Text);
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
                lbid.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                Cname.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                Cmobno.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                Cadhar.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
                Clino.Text = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
                Cadd.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                button1.Text = "Update";
            }
        }

   
    }

       

       
    }

