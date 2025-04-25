using System;
using System.Windows.Forms;

namespace ManajemenBukuApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeLayout();
        }

        private void InitializeLayout()
        {
            this.Text = "Aplikasi Manajemen Buku";
            this.Size = new System.Drawing.Size(1200, 700);

            // Header
            Panel header = new Panel();
            header.BackColor = System.Drawing.Color.Black;
            header.Dock = DockStyle.Top;
            header.Height = 50;

            Label lblHeader = new Label();
            lblHeader.Text = " Manajemen Buku - Perpustakaan Sekolah";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold);
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            header.Controls.Add(lblHeader);
            this.Controls.Add(header);

            // Sidebar
            Panel sidebar = new Panel();
            sidebar.BackColor = System.Drawing.Color.LightSteelBlue;
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 220;

            string[] menuItems = { "Dashboard", "Data Buku", "Data Anggota", "Peminjaman", "Pengembalian", "Laporan" };
            foreach (var item in menuItems)
            {
                Button btn = new Button();
                btn.Text = item;
                btn.Dock = DockStyle.Top;
                btn.Height = 50;
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = System.Drawing.Color.White;
                sidebar.Controls.Add(btn);
            }
            this.Controls.Add(sidebar);

            // Footer
            Panel footer = new Panel();
            footer.BackColor = System.Drawing.Color.Gainsboro;
            footer.Dock = DockStyle.Bottom;
            footer.Height = 30;

            Label lblFooter = new Label();
            lblFooter.Text = "© 2025 - Aplikasi Perpustakaan | Login sebagai: Admin";
            lblFooter.Dock = DockStyle.Fill;
            lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            footer.Controls.Add(lblFooter);
            this.Controls.Add(footer);

            // Main content
            Panel content = new Panel();
            content.Dock = DockStyle.Fill;
            content.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(content);
        }
    }
}
