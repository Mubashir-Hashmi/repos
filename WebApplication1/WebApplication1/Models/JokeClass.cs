namespace WebApplication1.Models
{
    public class JokeClass
    {
        public int Id { get; set; }
        public string JokeQuestion { get; set; }
        public string JokeAnswer { get; set; }
        public JokeClass()
        {
            JokeQuestion = "";
            JokeAnswer = "";
        }
    }
}
