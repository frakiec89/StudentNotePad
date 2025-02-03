namespace StudentNotePad.Services
{
    public abstract class FileService
    {
        private string _path; // путь

        public FileService(string path)
        {
            if (File.Exists(path) == false) // если файла нет 
            {
                File.Create(path).Close(); // создать новый 
            }
            _path = path; // запомнить путь
        }

        /// <summary>
        /// прочитать  лини из файла - будем  думать  что  каждый объект  лежит  в одной строке 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual string[] GetLines()
        {
            string[] lines = null;
            try
            {
                lines = File.ReadAllLines(_path);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка чтения файла");
            }

            List<string> nonEmptyLines = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) == false) // Убираем пустые  линии - они  нам не  нужны
                {
                    nonEmptyLines.Add(line);
                }
            }
            return nonEmptyLines.ToArray();
        }

        /// <summary>
        /// принимаем линии
        /// </summary>
        /// <param name="content"></param>
        /// <exception cref="Exception"></exception>
        public void Save(string[] content)
        {
            try
            {
                File.WriteAllLines(_path, content); // полностью  перезаписываем  файл
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка записи файла");
            }
        }
    }
}
