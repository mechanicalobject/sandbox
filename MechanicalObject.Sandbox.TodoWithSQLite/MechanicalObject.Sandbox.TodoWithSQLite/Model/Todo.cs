using System;

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
            return string.Format(@"Id : {0}, Description = {1}, Status = {2} CreatedOn = {3} ModifiedOn = {4}",
                     Id, Description.Substring(0, 10) + "...", Status, CreatedOn, ModifiedOn);
        }
    }
    public enum TodoStatus
    {
        ToBeDone = 0,
        Postponed = 1,
        Done = 2,
    }
}
