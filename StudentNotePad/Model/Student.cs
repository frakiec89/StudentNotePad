namespace StudentNotePad.Model
{
    public  class Student
    {
        public int Id { get; set; }
        public string Name { get; private set; }
        public Group Group { get; set; }

        public Student (string name , Group group )
        {
            if (string.IsNullOrWhiteSpace(name))
                 throw new ArgumentNullException(nameof(name), "Имя студента не может быть пустым");
        }
    }
}
