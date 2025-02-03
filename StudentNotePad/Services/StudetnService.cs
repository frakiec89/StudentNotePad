using StudentNotePad.Model;

namespace StudentNotePad.Services
{
    public class StudentService : FileService
    {
        private const string  _path= "Students.txt";  // путь  к файлу 
        public List<Student> Students { get; set; } = new List<Student>();

        public StudentService() : base(_path)
        {
            Students = GetStudetnsForTXT();
        }

        /// <summary>
        /// Чтение из файла  - парсинг данных формат  (1*иван*1) - где ид студ , имя - ид группы
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private List<Student> GetStudetnsForTXT()
        {
            List<Student> students = new List<Student>();
            GroupService groupService = null;
            try
            {
                groupService = new GroupService();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (string student in base.GetLines())
            {
                string[] strings = student.Split("*"); // 1*иван*1
                int  id = int.Parse(strings[0]);
                string name = strings[1];
                int  idGr = int.Parse(strings[2]);

                try
                {
                    Student newStudent = new Student(id ,  name , groupService.GetGroup(idGr) );
                    students.Add(newStudent);
                }
                catch (ArgumentException ex)
                {
                    throw new Exception("Ошибка группы у  струдента");
                }
            }
            return students;
        }

        public void Add (string name , Group group)
        {
            int id = 0; // поиск  нового  ид

            foreach (Student st in Students)
            {
                if(st.Id>id)
                {
                    id= st.Id;
                }
            }

            Student student = new Student(id+1 , name , group);
            Students.Add(student);
            Save(); //  перезаписывает  файл
        }

        private void Save ()
        {
            List<string> studentStrings = new List<string>();

            foreach (Student student in  Students)
            {
                string studentStr = $"{student.Id}*{student.Name}*{student.Group.Id}"; // 1*иван*1
                studentStrings.Add(studentStr);
            }
            base.Save(studentStrings.ToArray()); // из родителя
        }
    }
}
