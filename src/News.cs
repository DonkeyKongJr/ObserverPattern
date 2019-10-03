namespace src
{
    public class News
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Join(";", new[] { Id.ToString(), Headline, Description });
        }
    }
}