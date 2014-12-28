using System;
using System.Security.Cryptography;

namespace MechanicalObject.Sandbox.TodoWithSQLite.Model
{
    public class Todo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public TodoStatus Status { get; set; }

        public override string ToString()
        {
            var descriptionToDisplay = Description.Length > 10 ? Description.Substring(0, 10) + "..." : Description;
            return string.Format(@"Id : {0}, Description = {1}, Status = {2} CreatedOn = {3} ModifiedOn = {4}",
                     Id, Description, Status, CreatedOn, ModifiedOn);
        }
    }
    public enum TodoStatus
    {
        ToBeDone = 0,
        Postponed = 1,
        Done = 2,
    }
}
