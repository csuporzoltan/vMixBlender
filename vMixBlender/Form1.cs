using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vMixBlender
{

    public partial class Form1 : Form
    {

        public Dictionary<int, vMixSession> sessions = new Dictionary<int, vMixSession>(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void fájlToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iPCímAlapjánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nev = "Default Session";
            string ipcim = "localhost:8088";
            ShowInputDialog(ref nev, "Name of your session");
            ShowInputDialog(ref ipcim, "IP address of the vMix session");
            sessions.Add(sessions.Count + 1, new vMixSession(nev, ipcim));
            listBox1.Items.Add(nev + " @ " + ipcim);
        }

        private static DialogResult ShowInputDialog(ref string input, string name)
        {
            System.Drawing.Size size = new System.Drawing.Size(350, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = name;

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           //MessageBox.Show(sessions[listBox1.SelectedIndex + 1].getName());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void kilépésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class Shortcut
    {
        private string name;
        private Dictionary<vMixSession, String> commands = new Dictionary<vMixSession, string>();
        
        public Shortcut(string name, Dictionary<vMixSession, String> commands)
        {
            this.name = name;
            this.commands = commands;
        }

        public String getName()
        {
            return name;
        }

        public Dictionary<vMixSession, String> getCommands()
        {
            return commands;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public Boolean addCommand(vMixSession vMixSessionNumber, string command)
        {
            commands.Add(vMixSessionNumber, command);
            return true;
        }

        public Boolean executeShortcut()
        {
            WebClient client = new WebClient();
            foreach (KeyValuePair<vMixSession, string> entry in commands)
            {
                string url = "http://" + entry.Key.getIpAddress() + "" + entry.Value;
                try
                {
                    client.DownloadString(url);
                } catch (Exception)
                {
                    MessageBox.Show("Hiba", "Nem sikerült csatlakozni! " + url);
                    return false;
                }
            }
            return true;
        }

    }

    public class vMixSession
    {
        private string name;
        private string ipcim;

        private int shortcutnumber;
        private Dictionary<int, String> shortcuts = new Dictionary<int, string>();

        public vMixSession(string name, string ipcim)
        {
            this.name = name;
            this.ipcim = ipcim;
        }

        public string getName()
        {
            return this.name;
        }

        public string getIpAddress()
        {
            return this.ipcim;
        }

        public Boolean setIPaddress(string ipcim)
        {
            if (this.ipcim == ipcim)
            {
                return false;
            }

            this.ipcim = ipcim;
            return true;
        }

        public Boolean setName(string name)
        {
            if (this.name == name)
            {
                return false;
            }

            this.name = name;
            return true;
        }

    }

}
