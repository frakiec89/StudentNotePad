
using StudentNotePad.Model;

namespace StudentNotePad.Services
{
    public class GroupService : FileService
    {
        private const string _path = "Group.txt"; // путь к файлу  
        public List<Group> Groups { get; private set; }

        public GroupService() : base(_path)
        {
            Groups = GetGroupsForTXT();    
        }
        public void Add(string name)
        {
           int  maxId = 0; // поиск  нового ид 
           foreach (Group group in Groups)
           {
                if (group.Id > maxId)
                    maxId = group.Id;

                if (group.Name == name) // проверка  на  одинаковое  название
                    throw new Exception("Такая  группа уже есть");
           }
           Group newGroup = new Group(maxId+1, name);
           Groups.Add(newGroup);
           Save(); // перезаписываем  файл 
        }

        public void Delete (StudentService service , int  idGr  )
        {
            try
            {
                Group gr = GetGroup(idGr);

                if (service.GetStudentGroup(idGr).Length != 0)
                    throw new Exception("нельзя удалить группу пока в ней  есть хоть один студент");
                
                Groups.Remove(gr);
                Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// поиск  группы  по  ид
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Group GetGroup(int  id)
        {
            foreach (Group group in Groups)
            {
                if (group.Id == id)
                    return group;
            }
            throw new ArgumentException("Группа не найдена");
        }

        /// <summary>
        /// парсинг из файла  в формате 1*ис-22-01
        /// </summary>
        /// <returns></returns>
        private List<Group> GetGroupsForTXT()
        {
            List<Group> values = new List<Group>();
            foreach (string line in base.GetLines())
            {
                string[] propertyGroup = line.Split('*');
                try
                {
                    int id = int.Parse(propertyGroup[0]); // предположим что он  там 
                    string name = propertyGroup[1];
                    Group group = new Group(id, name);
                    values.Add(group);
                }
                catch
                {
                    continue; // при ошибки  одной  записи нет  повода  не  читать  остальные
                }
            }
            return values;
        }

        private void Save ()
         {
            List<string> strings = new List<string>(); // строки  в  формате  1*ис-23-03
            
            foreach (Group group in Groups)
            {
                string grString = $"{group.Id}*{group.Name}";
                strings.Add(grString);
            }
            base.Save(strings.ToArray());
        }

    }
}
