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
    public partial class Driver : Form
    {
        public Driver()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void exite_Click(object sender, EventArgs e)
        {
            this.Dispose(true); 
        }

        private void Driver_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data Source=LAPTOP-HUOGNCN9;Initial Catalog=Carrental;Integrated Security=True");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("car_driver", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (dlbid.Text != " " && button1.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("@did", Convert.ToInt32(dlbid.Text));
                    cmd.Parameters.AddWithValue("@dname", Dname.Text);
                    cmd.Parameters.AddWithValue("@dmobno", Dmobno.Text);
                    cmd.Parameters.AddWithValue("@dadhar", Dadhar.Text);
                    cmd.Parameters.AddWithValue("@dlno", Dlno.Text);
                    cmd.Parameters.AddWithValue("@dadd", Dadd.Text);
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
                    cmd.Parameters.AddWithValue("@dname", Dname.Text);
                    cmd.Parameters.AddWithValue("@dmobno", Dmobno.Text);
                    cmd.Parameters.AddWithValue("@dadhar", Dadhar.Text);
                    cmd.Parameters.AddWithValue("@dlno", Dlno.Text);
                    cmd.Parameters.AddWithValue("@dadd", Dadd.Text);



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
            Dname.Text = "";
            Dmobno.Text = "";
            Dadhar.Text = "";
            Dlno.Text = "";
            button1.Text = "Save";
            Dadd.Text = "";
            dlbid.Text = " ";
            dataGridView1.DataSource = null;



        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlbid.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("car_driver", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.Parameters.AddWithValue("@did", dlbid.Text);

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
                SqlCommand cmd = new SqlCommand("car_driver", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "search");
                cmd.Parameters.AddWithValue("@dname", Dname.Text);
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
                dlbid.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                Dname.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                Dmobno.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                Dadhar.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
                Dlno.Text = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
                Dadd.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                button1.Text = "Update";
            }
        }

  
    }
    }
