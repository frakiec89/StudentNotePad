namespace StudentNotePad.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public Group Group { get; set; }

        public Student(int id, string name, Group group)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Имя студента не может быть пустым");

            Id = id;
            Name = name;
            Group = group;
        }

        public override string ToString()
        {
            return $"{Id}. {Name}, группа: {Group}";
        }
    }
}
