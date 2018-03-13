namespace ProMama.Model
{
    public class JsonMessage
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int id { get; set; }
        public Usuario user { get; set; }

        public JsonMessage(bool success, string message, int id)
        {
            this.success = success;
            this.message = message;
            this.id = id;
        }

        public JsonMessage(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }

        public JsonMessage(bool success, int id)
        {
            this.success = success;
            this.id = id;
        }

        public JsonMessage(int id)
        {
            this.id = id;
        }

        public JsonMessage() { }
    }
}
