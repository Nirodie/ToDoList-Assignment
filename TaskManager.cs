using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6_ToDoList
{
    /// <summary>
    /// The class that handles the Task list
    /// </summary>
    class TaskManager
    {

        //Declares the list
        private List<Task> taskList;

        //Constructor to create the list
        public TaskManager()
        {
            taskList = new List<Task>();
        }

        public bool AddItem(Task task)
        {
            bool ok = false;
            if (task != null)
            {
                taskList.Add(task);
                ok = true;
            }
            return ok;
        }

        //Makes sure the index isnt out of range
        private bool CheckIndex (int index)
        {
            return (index >= 0) && (index < taskList.Count);
        }

        //shows current item for example in the list box
        public Task GetItem (int index)
        {
            if (!CheckIndex(index))
                return null;

            return taskList[index];
        }

        //Changes an item in the list
        public bool ChangeItem (Task taskIn, int index)
        {
            bool ok = false;

            if (CheckIndex(index))
            {
                ok = true;
                taskList[index] = taskIn;
            }
            return ok;
        }

        //Deletes and item from the list
        public bool DeleteItem (int index)
        {
            bool ok = false;

            if(CheckIndex(index))
            {
                taskList.RemoveAt(index);
                ok = true;
            }
            return ok;
        }

        //Property of count
        public int Count
        {
            get { return taskList.Count; }
        }

        //Clears the list
        public void ClearList()
        {
            taskList.Clear();
        }
        //Stores each task in a string
        public string[] ListToStringArray()
        {
            string[] taskArray = new string[Count];
            for (int i = 0; i < Count; i++) taskArray[i] = taskList[i].ToString();
            return taskArray;
        }
    }
}
