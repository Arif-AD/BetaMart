using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_BetaMart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);//Load image from file to picturebox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtNamaBarang.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                txtNamaBarang.Focus();
                //Add new row
                this.appData.Barang.AddBarangRow(this.appData.Barang.NewBarangRow());
                barangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah kamu yakin ingin menghapus data ini ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                barangBindingSource.RemoveCurrent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                barangBindingSource.EndEdit();
                barangTableAdapter.Update(this.appData.Barang);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                //Fill data to datatable
                this.barangTableAdapter.Fill(this.appData.Barang);
                barangBindingSource.DataSource = this.appData.Barang;
                //dataGridView.DataSource = barangBindingSource;
            }
            else
            {
                //using linq to query data
                var query = from o in this.appData.Barang
                            where o.NamaBarang.Contains(txtSearch.Text) || o.TanggalExp == txtSearch.Text || o.Jumlah == txtSearch.Text || o.Tanggal == txtSearch.Text || o.Harga == txtSearch.Text || o.Brand.Contains(txtSearch.Text)
                            select o;
                barangBindingSource.DataSource = query.ToList();
                //dataGridView.DataSource = query.ToList();
            }
        }

        private void btnKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                //Fill data to datatable
                this.barangTableAdapter.Fill(this.appData.Barang);
                barangBindingSource.DataSource = this.appData.Barang;
                //dataGridView.DataSource = barangBindingSource;
            }
            else
            {
                //using linq to query data
                var query = from o in this.appData.Barang
                            where o.NamaBarang.Contains(txtSearch.Text) || o.TanggalExp == txtSearch.Text || o.Jumlah == txtSearch.Text || o.Tanggal == txtSearch.Text || o.Harga == txtSearch.Text || o.Brand.Contains(txtSearch.Text)
                            select o;
                barangBindingSource.DataSource = query.ToList();
                //dataGridView.DataSource = query.ToList();
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    barangBindingSource.RemoveCurrent();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Barang' table. You can move, or remove it, as needed.
            this.barangTableAdapter.Fill(this.appData.Barang);
            barangBindingSource.DataSource = this.appData.Barang;
        }
    }
}
