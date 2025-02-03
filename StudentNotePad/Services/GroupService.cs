
using StudentNotePad.Model;

namespace StudentNotePad.Services
{
    internal class GroupService
    {
        private string _path = "Group.txt";
        public List<Group> Groups { get; private set; }
        
        public GroupService() 
        {
            Groups = GetGroupsForTXT(_path);    
        }

        private List<Group> GetGroupsForTXT(string path)
        {
            string[] lines = null;
            
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения файла c группами");
            }

            List<Group> values = new List<Group>();
            
            foreach (string line in lines)
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
    }
}
