using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WeChatMSGFKPL
{
    public partial class Form1 : Form
    {
        String DBpath;
        SQLiteConnection DBConnection;
        string[] strArray;
        //String[] Coum;

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog nt = new OpenFileDialog())
            {
                nt.Title = "选择要转换的文件";
                nt.Multiselect = false;
                if (nt.ShowDialog() == DialogResult.OK)
                {
                    DBpath = nt.FileName;
                    textBox1.Text = nt.FileName;
                    button2.Enabled = true;
                    this.Text = this.Text+" Select File:"+nt.FileName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBConnection = new SQLiteConnection("data source = " +textBox1.Text);
            DBConnection.Open();
            SQLiteCommand Tablelist = new SQLiteCommand("select name from sqlite_master where type='table' order by name;", DBConnection);
            SQLiteDataReader TableReader = Tablelist.ExecuteReader();
            while (TableReader.Read()) {
                comboBox1.Items.Add(TableReader["name"]);
            }
            comboBox1.SelectedIndex = 1;
            button2.Enabled = false;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            DBConnection.Close();
            comboBox1.Items.Clear();
            button2.Enabled = true;
            button4.Enabled = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.listView1.Clear();
            int l = 0;
            //this.listView1.BeginUpdate();
            SQLiteCommand GetTableC = new SQLiteCommand($"SELECT COUNT(*) FROM {comboBox1.SelectedItem.ToString()}", DBConnection);
            SQLiteDataReader CRead = GetTableC.ExecuteReader();
            while (CRead.Read()) {
                l=Convert.ToInt32(CRead["COUNT(*)"]);
            }
            int ModNum = l % 40;
            int AllPage = l / 40;
            if (ModNum != 0 && ModNum > 0) {
                AllPage++;
            }
            label5.Text = $"{AllPage}";
            List<string> strList = new List<string>();
            //MessageBox.Show("您选中了表:"+comboBox1.SelectedItem.ToString());
            SQLiteCommand TableCoum = new SQLiteCommand($"PRAGMA table_info({comboBox1.SelectedItem.ToString()})", DBConnection);
            SQLiteDataReader TableCoumReader = TableCoum.ExecuteReader();
            //int a = 0;
            while (TableCoumReader.Read()) {
                //this.listView1.Columns.Add(TableCoumReader["name"]);
                strList.Add(TableCoumReader["name"].ToString());
                //this.listView1.Columns.Add("ID");
                this.listView1.Columns.Add($"{TableCoumReader["name"]}", 120, HorizontalAlignment.Left);
            }
             strArray = strList.ToArray();
            Console.WriteLine(strArray.Length);
            
            SQLiteCommand TableCoumData = new SQLiteCommand($"select * FROM {comboBox1.SelectedItem.ToString()} LIMIT 0,40", DBConnection);
            SQLiteDataReader TableCoumDataReader = TableCoumData.ExecuteReader();
            
            String NowArry="";
            int jb = 1;
            while (TableCoumDataReader.Read()) {
                ListViewItem add = new ListViewItem();
                for (int i = 0; i < strArray.Count(); i++) {
                    if (i == 0)
                    {
                        add.Text=Convert.ToString(TableCoumDataReader[i]);
                    }
                    else 
                    {
                        add.SubItems.Add(Convert.ToString(TableCoumDataReader[i]));
                    }
                    Console.WriteLine(TableCoumDataReader[i]);
                }
                listView1.Items.Add(add);
            }
            
            //this.listView1.EndUpdate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ( Convert.ToInt32(textBox2.Text)+1>Convert.ToInt32(label5.Text)) {
                MessageBox.Show("超你妈没那么多页（）");
                return;

            }
            listView1.Items.Clear();
            //textBox2.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) + 1);
            int Limint;
            Limint = Convert.ToInt32(textBox2.Text);
            SQLiteCommand TableCoumData = new SQLiteCommand($"select * FROM {comboBox1.SelectedItem.ToString()} LIMIT {(Limint*40)-40},{Convert.ToInt32(textBox2.Text)*40}", DBConnection);
            SQLiteDataReader TableCoumDataReader = TableCoumData.ExecuteReader();

            String NowArry = "";
            int jb = 1;
            while (TableCoumDataReader.Read())
            {
                ListViewItem add = new ListViewItem();
                for (int i = 0; i < strArray.Count(); i++)
                {
                    if (i == 0)
                    {
                        add.Text = Convert.ToString(TableCoumDataReader[i]);
                    }
                    else
                    {
                        add.SubItems.Add(Convert.ToString(TableCoumDataReader[i]));
                    }
                    Console.WriteLine(TableCoumDataReader[i]);
                }
                listView1.Items.Add(add);
            }
            textBox2.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) + 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox2.Text) - 1 > Convert.ToInt32(label5.Text))
            {
                MessageBox.Show("超你妈底裤la（）");
                return;

            }
            listView1.Items.Clear();
            //textBox2.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) + 1);
            int Limint;
            Limint = Convert.ToInt32(textBox2.Text);
            SQLiteCommand TableCoumData = new SQLiteCommand($"select * FROM {comboBox1.SelectedItem.ToString()} LIMIT {(Limint*40)-40},{Convert.ToInt32(textBox2.Text)*40}", DBConnection);
            SQLiteDataReader TableCoumDataReader = TableCoumData.ExecuteReader();

            String NowArry = "";
            int jb = 1;
            while (TableCoumDataReader.Read())
            {
                ListViewItem add = new ListViewItem();
                for (int i = 0; i < strArray.Count(); i++)
                {
                    if (i == 0)
                    {
                        add.Text = Convert.ToString(TableCoumDataReader[i]);
                    }
                    else
                    {
                        add.SubItems.Add(Convert.ToString(TableCoumDataReader[i]));
                    }
                    Console.WriteLine(TableCoumDataReader[i]);
                }
                listView1.Items.Add(add);
            }
            textBox2.Text = Convert.ToString(Convert.ToInt32(textBox2.Text) -1);
        }
    }
}
