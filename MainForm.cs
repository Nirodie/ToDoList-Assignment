using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment6_ToDoList
{

    /// <summary>
    /// The main form class that handles inputs from the user
    /// </summary>
    public partial class MainForm : Form
    {
        TaskManager taskManager = new TaskManager();
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        //Initializes the GUI by clearing controls and adding text to them
        private void InitializeGUI()
        {
            this.Text = "To-Do Reminder" + " by Hannes Westin"; //Top text of the application

            //Clears and fills the combobox with items
            cmbPrio.Items.Clear();
            string[] priority = Enum.GetNames(typeof(PriorityType));
            foreach (var item in priority) cmbPrio.Items.Add(item.Replace("_", " "));

            toolTip.SetToolTip(dateTimePicker, "Click to open calendar for date, write the time here."); //Sets the tooltip

            //Sets a custom format for the datetimepicker control 
            dateTimePicker.Text = string.Empty;
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "yyyy-MM-dd    HH:mm";

            //Clears the task textbox and listbox
            txtTask.Text = string.Empty;
            lstTasks.Items.Clear();

        }

        private void UpdateGUI()
        {
            lstTasks.Items.Clear();
            string[] taskStrings = taskManager.ListToStringArray();
            if (taskStrings.Length != null)
                lstTasks.Items.AddRange(taskStrings);
        }

        //Displays the time
        private void Timer_Seconds_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void GiveMessage(string message)
        {
            MessageBox.Show(message,
                "error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
                );
        }

        private string ReadTask(out bool success)
        {
            success = false;
            string text = txtTask.Text.Trim();
            if (!string.IsNullOrEmpty(text))
                success = true;
            else
                GiveMessage("No task given, try again!");

            return text;
                
        }

        private DateTime ReadTime(out bool success)
        {
            success = false;
            DateTime date = DateTime.MinValue;
            if (dateTimePicker.Value < new DateTime(2000, 01, 01) || dateTimePicker.Value >= new DateTime(2222, 01, 01))
            {
                GiveMessage("Faulty date, the date must be between 2000 - 2222");
                success = false;
            }
            else
            {
                success = true;
                 date = dateTimePicker.Value;
            }


            return date;
        }

        private PriorityType ReadPriority(out bool success)
        {
            success = false;
            PriorityType prio = PriorityType.Less_Important; //Initialization

            if (cmbPrio.SelectedIndex >= 0)
            {
                success = true;
                prio = (PriorityType)cmbPrio.SelectedIndex;
            }
            else
                GiveMessage("No priority given, try again!");

            return prio;
        }

        //Gets the inputs from the user and checks validity
       private Task GetTaskFromUserInputs(out bool success)
        {
            success = false;

            Task taskitems = new Task();

            //Reads the task
            taskitems.Description = ReadTask(out success);
            if (!success)
                return null;

            //Reads the priority
            taskitems.Priority = ReadPriority(out success);
            if (!success)
                return null;

            //Reads the time
            taskitems.Date = ReadTime(out success);
            if (!success)
                return null;

            return taskitems;
        }

        //Shows a messagebox when the application closes
        private bool ResultFromQuitDialog()
        {
            bool quit;

            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Close application?",
                MessageBoxButtons.YesNo );

            if (result == DialogResult.Yes)
                quit = true;
            else
                quit = false;

            return quit;
        }

        //When the exit button is clicked on the menu
        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Gives a message making sure the user want to exit the application
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !ResultFromQuitDialog();
        }

        private void Menu_New_Click(object sender, EventArgs e)
        {
            InitializeGUI();
            //Clears the list inside of TaskManager.cs
            taskManager.ClearList();
        }

        private void Menu_About_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool success = false;

            Task newTask = GetTaskFromUserInputs(out success);
            if (newTask != null)
            {
                taskManager.AddItem(newTask);
                UpdateGUI();
            }
        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex < 0)
                return;
            Task task = taskManager.GetItem(lstTasks.SelectedIndex);
            txtTask.Text = task.Description.ToString();
            cmbPrio.SelectedIndex = (int)task.Priority;
        }

        //Deletes the selected item
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstTasks.SelectedIndex;
            if (index < 0)
                return;

            else
            {
                taskManager.DeleteItem(index);
                UpdateGUI();
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            bool ok = true;
            Task task = GetTaskFromUserInputs(out ok);
            int index = lstTasks.SelectedIndex;
            if (index < 0)
                return;

            if (ok)
            {
                taskManager.ChangeItem(task, index);
                UpdateGUI();
            }
        }
    }
}
