using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6_ToDoList
{
    /// <summary>
    /// This class is meant to store data and handle operations.
    /// </summary>
    class Task
    {
        private DateTime date;
        private string description;
        private PriorityType priority;

        public Task (string description, DateTime date, PriorityType priority)
        {
            this.description = description;
            this.date = date;
            this.priority = priority;
        }

        public Task()
        {
        }
        #region Properties
        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    description = value;
            }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public PriorityType Priority
        {
            get { return priority; }
            set
            {
                if (Enum.IsDefined(typeof(PriorityType), value)) //Makes sure there's a defined value in the enum
                    priority = value;
            }
        }
        #endregion

        //Method to return a string from the priority field without the "_" character
        private string GetPriorityString()
        {
           string time = date.ToShortTimeString();

            return time;
        }
        //Method to return a string in the format of HH:mm
        private string GetTimeString()
        {
            string prio = priority.ToString().Replace("_"," ");

            return prio;
        }

        //ToString method to organise it neatly inside of the listbox
        public override string ToString() 
        {
            return $"{date.ToShortDateString(),-14}" +
                   $"{GetPriorityString(),-13}" +
                   $"{GetTimeString(),-20}" +                   
                   $"{description}";
        }
    }
}
