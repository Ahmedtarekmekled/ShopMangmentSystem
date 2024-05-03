using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace MangmentSystem
{
    public partial class Categoryies : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("mangmentShop");
        static IMongoCollection<Category> collection = db.GetCollection<Category>("category");

        public void ReadAllDocument()
        {
            List<Category> list = collection.AsQueryable().ToList<Category>();
            dataGridView1.DataSource = list;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        public Categoryies()
        {
            InitializeComponent();
        }

        private void Categoryies_Load(object sender, EventArgs e)
        {
            ReadAllDocument();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter The Messing Field", "Creating Faild.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Category c = new Category(textBox1.Text);
                collection.InsertOne(c);
                ReadAllDocument();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text) == "" && (textBox2.Text) == "")
            {
                MessageBox.Show("There is a feild messing Try Again", "Update Faild.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var updateField = Builders<Category>.Update.Set("categoryname", textBox1.Text);
                collection.UpdateOne(s => s.Id == ObjectId.Parse(textBox2.Text), updateField);
                ReadAllDocument();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Chose To Dorp The Category", "Deleting Faild.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                collection.DeleteOne(s => s.Id == ObjectId.Parse(textBox2.Text));
                ReadAllDocument();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers customer = new Customers();   
            customer.Show();    
            this.Hide();
        }
    }
}
