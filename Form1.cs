using MySqlConnector;
using System.Data;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lr25
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string DB_URL = "Server=localhost;Database=university;Uid=root;Pwd=203TN;";

        public static MySqlConnection Connect()
        {
            MySqlConnection connection = new MySqlConnection(DB_URL);
            connection.Open();
            return connection;
        }

        private void btnAddUniversity_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameUniversity.Text != "" && town.Text != "" && Year.Text != "")
                {
                    AddUniversity(nameUniversity.Text, town.Text, Convert.ToInt32(Year.Text));
                    MessageBox.Show("Університет доданий до бази");
                }
                else
                    MessageBox.Show("Заповніть всі поля");
            }
            catch
            {
                MessageBox.Show("Упс. Сталася помилка");
            }
        }

        public static void AddUniversity(string name, string town, int year)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO university_list (name, town, year) VALUES (@name, @town, @year)", connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@town", town);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnAddActivities_Click(object sender, EventArgs e)
        {
            try
            {
                if (nameUniversityAct.Text != "")
                {
                    AddActivities(nameUniversityAct.Text, DateTime.Parse(dateTimePicker3.Value.ToString("yyyy-MM-dd")));
                    MessageBox.Show("Захід доданий до бази");
                }
                else
                    MessageBox.Show("Заповніть всі поля");
            }
            catch
            {
                MessageBox.Show("Упс. Сталася помилка");
            }

        }

        public static void AddActivities(string name, DateTime date)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO university_activities (name, date) VALUES (@name, @date)", connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnFindByName_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(name.Text))
            {
                FindByName(dataGridView1, name.Text);
            }
            else
            {
                MessageBox.Show("Заповніть поле");
            }
        }

        public static void FindByName(DataGridView dataGridView, string name)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM university_list WHERE name = @name", connection))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void btnFindByTown_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(town_txt.Text))
            {
                FindByTown(dataGridView1, town_txt.Text);
            }
            else
            {
                MessageBox.Show("Заповніть поле");
            }
        }

        public static void FindByTown(DataGridView dataGridView, string town)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM university_list WHERE town = @town", connection))
                {
                    cmd.Parameters.AddWithValue("@town", town);
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void btnUniversity_Click(object sender, EventArgs e)
        {
            dispUniversity(dataGridView1);
        }

        public static void dispUniversity(DataGridView dataGridView)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM university_list", connection))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void btnActivities_Click(object sender, EventArgs e)
        {
            dispTown(dataGridView1);
        }

        public static void dispTown(DataGridView dataGridView)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM university_activities", connection))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

        private void btnActivitiesByDate_Click(object sender, EventArgs e)
        {
            ActivitiesByDate(dataGridView1, DateTime.Parse(dateTimePicker1.Value.ToString("yyyy-MM-dd")), DateTime.Parse(dateTimePicker2.Value.ToString("yyyy-MM-dd")));
        }

        public static void ActivitiesByDate(DataGridView dataGridView, DateTime startDate, DateTime endDate)
        {
            using (MySqlConnection connection = Connect())
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM university_activities WHERE date >= @startDate AND date <= @endDate", connection))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
        }

    }
}