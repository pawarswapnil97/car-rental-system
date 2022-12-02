using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_rental_Systemm
{
    public partial class CarRental : Form
    {
        public CarRental()
        {
            InitializeComponent();
        }

       

       

     

        private void carsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Car c1=new Car();
            c1.Show();
        }

     
        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer c1 = new Customer();
            c1.Show();
        }

     
        private void carsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Car c1 =new Car();
            c1.Show();
        }

        private void driverToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Driver d1 = new Driver();
            d1.Show();
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bill b1 = new Bill();
            b1.Show();
        }
    }
}
