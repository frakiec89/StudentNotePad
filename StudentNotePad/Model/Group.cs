namespace StudentNotePad.Model
{
    public class Group
    {
        public int  Id { get; set; }
        public string Name { get; set; }

        public Group(int id, string name)
        {
            Id = id;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "название группы не может быть пустым");

            Name = name;
        }
    }
}
