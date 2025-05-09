using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Manajemen_Buku
{
    public partial class Form1 : Form
    {
        private int selectedId = -1; // Untuk update

        public Form1()
        {
            InitializeComponent();
            CustomizeUI();
        }

        private void CustomizeUI()
        {
            // Mengubah warna dan font
            this.BackColor = Color.LightGray;
            this.Font = new Font("Arial", 10);
            // Mengatur DataGridView
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            // Menambahkan Tooltips
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(button1, "Simpan Data Buku");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=database.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string createTable = @"CREATE TABLE IF NOT EXISTS buku (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    judul TEXT,
                    penulis TEXT,
                    penerbit TEXT,
                    tahun TEXT,
                    isbn TEXT,
                    deskripsi TEXT
                )";

                using (SQLiteCommand cmd = new SQLiteCommand(createTable, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string query;
                if (selectedId == -1)
                {
                    query = @"INSERT INTO buku (judul, penulis, penerbit, tahun, isbn, deskripsi) 
                             VALUES (@judul, @penulis, @penerbit, @tahun, @isbn, @deskripsi)";
                }
                else
                {
                    query = @"UPDATE buku SET judul=@judul, penulis=@penulis, penerbit=@penerbit, 
                             tahun=@tahun, isbn=@isbn, deskripsi=@deskripsi WHERE id=@id";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@judul", textBox1.Text);
                    cmd.Parameters.AddWithValue("@penulis", textBox2.Text);
                    cmd.Parameters.AddWithValue("@penerbit", textBox3.Text);
                    cmd.Parameters.AddWithValue("@tahun", dateTimePicker1.Value.Year.ToString());
                    cmd.Parameters.AddWithValue("@isbn", textBox4.Text);
                    cmd.Parameters.AddWithValue("@deskripsi", richTextBox1.Text);
                    if (selectedId != -1) cmd.Parameters.AddWithValue("@id", selectedId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show(selectedId == -1 ? "Data berhasil disimpan!" : "Data berhasil diperbarui!");

                selectedId = -1;
                ClearForm();
                LoadData();
            }
        }

        private void LoadData()
        {
            string connectionString = "Data Source=database.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string selectQuery = "SELECT * FROM buku";
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(selectQuery, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                TambahKolomAksi();
            }
        }

        private void TambahKolomAksi()
        {
            if (dataGridView1.Columns["Edit"] == null)
            {
                DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
                editBtn.HeaderText = "Edit";
                editBtn.Name = "Edit";
                editBtn.Text = "Edit";
                editBtn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(editBtn);
            }
            if (dataGridView1.Columns["Hapus"] == null)
            {
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.HeaderText = "Hapus";
                deleteBtn.Name = "Hapus";
                deleteBtn.Text = "Hapus";
                deleteBtn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(deleteBtn);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                string connectionString = "Data Source=database.db;Version=3;";
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Hapus")
                {
                    var confirm = MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM buku WHERE id = @id";
                            using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        LoadData();
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                {
                    selectedId = id;
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["judul"].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["penulis"].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["penerbit"].Value.ToString();
                    dateTimePicker1.Value = new DateTime(int.Parse(dataGridView1.Rows[e.RowIndex].Cells["tahun"].Value.ToString()), 1, 1);
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["isbn"].Value.ToString();
                    richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["deskripsi"].Value.ToString();
                }
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            richTextBox1.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }

        // Kosongkan event handler lainnya jika belum digunakan
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e)
        {
        //    Form2 form = new Form2();
        //    form.Show();
        //    this.Hide();
        //}

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
