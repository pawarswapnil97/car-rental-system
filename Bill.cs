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
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
            fillCombo();
        }
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
     

        private void Bill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'carrentalDataSet.car' table. You can move, or remove it, as needed.
            this.carTableAdapter.Fill(this.carrentalDataSet.car);
            con = new SqlConnection("Data Source=LAPTOP-HUOGNCN9;Initial Catalog=Carrental;Integrated Security=True");
        
        }
        public string GETNEXTID(string strSql)
        {
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandText = strSql;
            cmd1.CommandType = CommandType.Text;
            cmd1.Connection = con;
            if (con.State != ConnectionState.Open)
                con.Open();
            string cnt;
            cnt = (Convert.ToInt32(cmd1.ExecuteScalar()) + 1).ToString();
            con.Close();
            return (cnt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Billno.Text = GETNEXTID("select isnull( max(BillNo),0)from Bill");
        }
        private void fillCombo()
        {
            try
            {
                con = new SqlConnection("Data Source=LAPTOP-HUOGNCN9;Initial Catalog=Carrental;Integrated Security=True");
                con.Open();
                cmd = new SqlCommand("car_customer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "getall");
                DataSet ds = new System.Data.DataSet();
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);

                DataRow dr = ds.Tables[0].NewRow();
                dr["cid"] = "0";
                dr["cname"] = "-Select-";
               
                ds.Tables[0].Rows.InsertAt(dr, 0);

                comboBox1.DataSource = ds.Tables[0];
                comboBox1.ValueMember = "cid";
                comboBox1.DisplayMember = "cname";
               
                cmd = new SqlCommand("car_cardetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "getall");
                ds = new System.Data.DataSet();
                adpt = new SqlDataAdapter(cmd);
                adpt.Fill(ds);

                dr = ds.Tables[0].NewRow();
                dr["carid"] = "0";
                dr["carmodel"] = "-Select-";
                ds.Tables[0].Rows.InsertAt(dr, 0);

                comboBox2.DataSource = ds.Tables[0];
                comboBox2.ValueMember = "carid";
                comboBox2.DisplayMember = "carmodel";

                con.Close();



            }

            catch (Exception)
            {
                throw;

            }
            
        }

     

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(skm.Text) && !string.IsNullOrEmpty(ekm.Text))
                tkm.Text = (Convert.ToInt32(ekm.Text) - Convert.ToInt32(skm.Text)).ToString();
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(skm.Text) && !string.IsNullOrEmpty(ekm.Text))
                tkm.Text = (Convert.ToInt32(ekm.Text) - Convert.ToInt32(skm.Text)).ToString();
                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

       
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("SELECT * FROM  car where carmodel ='"+comboBox2.Text+"'", con);
            if(con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
            cmd.ExecuteNonQuery();
            SqlDataReader dr;
            dr= cmd.ExecuteReader();    
            while(dr.Read())
            {
                string Rent=(String)dr["rent"].ToString();
                rent.Text = Rent;
                string Type=(String)dr["fuel"].ToString();
                
                fuel.Text= Type;
            }
            con.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rent.Text) && !string.IsNullOrEmpty(tkm.Text))
                amount.Text = (Convert.ToInt32(rent.Text) * Convert.ToInt32(tkm.Text)).ToString();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rent.Text) && !string.IsNullOrEmpty(tkm.Text))
                amount.Text = (Convert.ToInt32(rent.Text) * Convert.ToInt32(tkm.Text)).ToString();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            int amt = 0;
            int dis, totaldiscount, total = 0;
    
            if (!string.IsNullOrEmpty(discount.Text) && !string.IsNullOrEmpty(amount.Text))
            amt = Convert.ToInt32(amount.Text);
            if(int.TryParse(discount.Text, out dis))
            {
                totaldiscount = amt * (dis / 100);
                total = amt - totaldiscount;
                this.total.Text = total.ToString();
            }
           

               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                    con.Open();
                cmd = new SqlCommand("car_bill", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (trano.Text != " " && button2.Text == "Update")
                {
                    cmd.Parameters.AddWithValue("@Billno", Convert.ToInt32(trano.Text));
                    cmd.Parameters.AddWithValue("@Billdate", billdate.Text);
                    cmd.Parameters.AddWithValue("@Cname", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@carmodel", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Rent", Convert.ToInt32(rent.Text));
                    cmd.Parameters.AddWithValue("@Fuel",  fuel.Text.ToString());
                    cmd.Parameters.AddWithValue("@Startkm", Convert.ToInt32(skm.Text));
                    cmd.Parameters.AddWithValue("@Endkm", Convert.ToInt32(ekm.Text));
                    cmd.Parameters.AddWithValue("@Totalkm", Convert.ToInt32(tkm.Text));
                    cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount.Text));
                    cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(discount.Text));
                    cmd.Parameters.AddWithValue("@Total", Convert.ToInt32(total.Text));
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
                    cmd.Parameters.AddWithValue("@Billdate", billdate.Text);
                    cmd.Parameters.AddWithValue("@Cname", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@carmodel", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Rent", Convert.ToInt32(rent.Text));
                    cmd.Parameters.AddWithValue("@Fuel", fuel.Text.ToString());
                    cmd.Parameters.AddWithValue("@Startkm", Convert.ToInt32(skm.Text));
                    cmd.Parameters.AddWithValue("@Endkm", Convert.ToInt32(ekm.Text));
                    cmd.Parameters.AddWithValue("@Totalkm", Convert.ToInt32(tkm.Text));
                    cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount.Text));
                    cmd.Parameters.AddWithValue("@Discount", Convert.ToInt32(discount.Text));
                    cmd.Parameters.AddWithValue("@Total", Convert.ToInt32(total.Text));



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
            trano.Text = "";
            Billno.Text = "";
            billdate.Text = "";
            comboBox1.Text = "";
            button2.Text = "Save";
            comboBox2.Text = "";
            rent.Text = "";
            fuel.Text = "";
            skm.Text = "";
            ekm.Text = "";
            tkm.Text = "";
            amount.Text = "";
            discount.Text = "";
            total.Text =  " ";
            textBox1.Text = "";

            dataGridView1.DataSource = null;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (trano.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("car_bill", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flag", "delete");
                    cmd.Parameters.AddWithValue("@Billno", trano.Text);

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
                SqlCommand cmd = new SqlCommand("car_bill", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@flag", "search");
                cmd.Parameters.AddWithValue("@Billno", Billno.Text);
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
                trano.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                Billno.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
                billdate.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
                rent.Text = dataGridView1.Rows[rowindex].Cells[4].Value.ToString();
                fuel.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
                skm.Text = dataGridView1.Rows[rowindex].Cells[6].Value.ToString();
                ekm.Text = dataGridView1.Rows[rowindex].Cells[7].Value.ToString();
                tkm.Text = dataGridView1.Rows[rowindex].Cells[8].Value.ToString();
                amount.Text = dataGridView1.Rows[rowindex].Cells[9].Value.ToString();
                discount.Text = dataGridView1.Rows[rowindex].Cells[10].Value.ToString();
                total.Text = dataGridView1.Rows[rowindex].Cells[11].Value.ToString();
                button2.Text = "Update";
            }

        }

    
    }
}

