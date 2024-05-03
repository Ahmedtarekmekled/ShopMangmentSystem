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

namespace MangmentSystem
{
    public partial class Items : Form
    {
        static MongoClient client = new MongoClient();
        static IMongoDatabase db = client.GetDatabase("mangmentShop");
        static IMongoCollection<Category> collection = db.GetCollection<Category>("category");
        static IMongoCollection<Item> itemCollection = db.GetCollection<Item>("items");

        public void ReadAllDocument()
        {
            List<Item> list = itemCollection.AsQueryable().ToList<Item>();
            dataGridView1.DataSource = list;
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private List<Category> GetAllCategories()
        {
            try
            {
                return collection.Find(_ => true).ToList(); // Use 'collection' instead of 'categoryCollection'
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<Category>();
            }
        }

        private void PopulateCategoriesComboBox()
        {
            comboBox1.DisplayMember = "categoryname";
            comboBox1.ValueMember = "_id";

            List<Category> categories = GetAllCategories();
            comboBox1.DataSource = categories;
        }

        public Items()
        {
            InitializeComponent();
            PopulateCategoriesComboBox();
            ReadAllDocument();// Populate ComboBox when the form is initialized
        }

        private void label13_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Categoryies category = new Categoryies();
            category.Show();
            this.Hide();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Parse input values
            string itemName = textBox1.Text;
            decimal itemPrice;
            int itemStock;

            if (!decimal.TryParse(textBox3.Text, out itemPrice) || !int.TryParse(textBox4.Text, out itemStock))
            {
                MessageBox.Show("Invalid price or stock value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get selected category ObjectId from ComboBox
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ObjectId categoryId;
            if (!ObjectId.TryParse(comboBox1.SelectedValue as string, out categoryId))
            {
                MessageBox.Show("Invalid category selection.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Create new Item object
                Item newItem = new Item(itemName, itemPrice, itemStock, categoryId);
                itemCollection.InsertOne(newItem);
                MessageBox.Show("Item added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ReadAllDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
