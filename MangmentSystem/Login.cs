namespace MangmentSystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.Show();

        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
