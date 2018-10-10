using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace vMixBlender
{
    public partial class ShortcutHandler : Form
    {
        public Dictionary<int, vMixSession> sessions = new Dictionary<int, vMixSession>();
        public ShortcutHandler(Dictionary<int, vMixSession> sessions)
        {
            InitializeComponent();
            this.sessions = sessions;
            foreach (KeyValuePair<int, vMixSession> entry in sessions)
            {
                checkedListBox1.Items.Add(entry.Value.getName());
            }
        }
        int i = 1;
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ShortcutHandler_Load(object sender, EventArgs e)
        {

        }

        private vMixSession giveClassByName(string name)
        {
            foreach (KeyValuePair<int, vMixSession> entry in sessions)
            {
                if(entry.Value.getName().Equals(name))
                {
                    return entry.Value;
                }
            }

            return null;
        }

        private void executeButton_Click(object sender, EventArgs e)
        {
            Dictionary<vMixSession, string> shortcutbuilder = new Dictionary<vMixSession, string>();
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                vMixSession build = giveClassByName(itemChecked.ToString());
                shortcutbuilder.Add(build, "test");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (checkedListBox1.CheckedItems.ToString() == null) MessageBox.Show("You must choose at least one instance!", "Error");
            for(int z = 247; z < 700; z = z +3)
            {
                this.Size = new System.Drawing.Size(z, 329);
            }
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                    TextBox textBox = new TextBox();
                    textBox.Size = new System.Drawing.Size(200, 10);
                    textBox.Location = new System.Drawing.Point(250,i * 24);
                    textBox.Text = "Command for: " + itemChecked.ToString();
                    textBox.Name = "textBox" + i.ToString();
                    this.Controls.Add(textBox);
                    Button executeButton = new Button();
                    executeButton.Text = "Save Shortcut!";
                    executeButton.Size = new System.Drawing.Size(103, 23);
                    executeButton.Location = new System.Drawing.Point(250, 100);
                    executeButton.Name = "executeButton";
                    this.Controls.Add(executeButton);
                    i++;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
