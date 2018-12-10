using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.ProductDetails' table. You can move, or remove it, as needed.
            this.productDetailsTableAdapter.Fill(this.dataSet1.ProductDetails);
            // TODO: This line of code loads data into the 'dataSet1.OrderItemStatus' table. You can move, or remove it, as needed.
            this.orderItemStatusTableAdapter.Fill(this.dataSet1.OrderItemStatus);
            initDataStructure();
        }

        private void dataSet1_Initialized(object sender, EventArgs e)
        {
           
        }
        private System.Data.DataTable countries;
        private System.Data.DataTable cities;
        private System.Data.DataTable cinemas;
        private System.Data.DataView viewCinemas;

        private void initDataStructure()
        {
            //init datatable for countries table
            this.countries = new DataTable("Countries");
            //add name column, it must be unique, Name column is PrimaryKey
            DataColumn name = new DataColumn("Name", typeof(string));
            name.Unique = true;
            name.AllowDBNull = false;
            countries.Columns.Add(name);
            countries.PrimaryKey = new DataColumn[] { countries.Columns["Name"] };
            //init datatable for cities table
            this.cities = new DataTable("Cities");
            //add two column to cities table: name and country, add Id as PrimaryKey
            DataColumn country = new DataColumn("Country", typeof(string));
            //country.AllowDBNull = false;
            cities.Columns.Add("Name", typeof(string));
            cities.Columns.Add(country);
            cities.Columns.Add("Id", typeof(Guid));
            cities.PrimaryKey = new DataColumn[] { cities.Columns["Id"] };
            //init datatable for cinemas table
            this.cinemas = new DataTable("Cinemas");
            //add columns: name, city, address, id
            cinemas.Columns.Add("City", typeof(string));
            cinemas.Columns.Add("Name", typeof(string));
            cinemas.Columns.Add("Address", typeof(string));
            cinemas.Columns.Add("Id", typeof(Guid));
            cinemas.PrimaryKey = new DataColumn[] { cinemas.Columns["Id"] };

            dataSet1.Tables.Add(cities);
            dataSet1.Tables.Add(countries);
            dataSet1.Tables.Add(cinemas);

            dataSet1.Relations.Add("cities to country", countries.Columns["Name"], cities.Columns["Country"]);
            dataSet1.Relations.Add("cinemas to city", cities.Columns["Name"], cinemas.Columns["City"]);
            // add data
            countries.Rows.Add("Ukraine");
            countries.Rows.Add("England");
            countries.Rows.Add("Scotland");
            countries.Rows.Add("Ireland");
            countries.Rows.Add("USA");

            cities.Rows.Add("Kyiv", "Ukraine", Guid.NewGuid());
            cities.Rows.Add("Dublin", "Ireland", Guid.NewGuid());
            cities.Rows.Add("Edinburgh", "Scotland", Guid.NewGuid());
            cities.Rows.Add("London", "England", Guid.NewGuid());
            cities.Rows.Add("Washington", "USA", Guid.NewGuid());

            cinemas.Rows.Add("Kyiv", "NewCinema", "Address 1", Guid.NewGuid());
            cinemas.Rows.Add("Dublin", "Plaze", "Address 2", Guid.NewGuid());
            cinemas.Rows.Add("Edinburgh", "Kino", "Address 3", Guid.NewGuid());
            cinemas.Rows.Add("London", "CinemaS", "Address 4", Guid.NewGuid());
            cinemas.Rows.Add("Washington", "BestCinema", "Address 5", Guid.NewGuid());

            dataGridView1.DataSource = countries;
            dataGridView2.DataSource = cities;
            dataGridView3.DataSource = cinemas;

            viewCinemas = new DataView(cinemas);
            viewCinemas.Sort = "Name ASC, City DESC";
            dataGridView4.DataSource = viewCinemas;

        }

        private void lab2DataSetBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                viewCinemas.Sort = "Name ASC, City DESC";   
            else
                viewCinemas.Sort = "Name DESC, City ASC";
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            viewCinemas.RowFilter = "Name like '"+ textBox1.Text+"%'";

        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            countries.Rows.Add(dataGridView1.Columns[e.ColumnIndex].HeaderText);
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            
        }
    }
}
