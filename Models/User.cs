namespace ScavengeRUs.Models
{
    public class User
    {
        // Set this to the username from the database
        public string UserName { get; set; }

        // Set this to the list of games from the games table
        public List<string> currentGames { get; set; }

        // Set this to the list of completed tasks from the user table
        public List<string> tasksCompleted { get; set; }
    }
}
