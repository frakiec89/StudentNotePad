using StudentNotePad.Model;
using StudentNotePad.Services;

GroupService groupService = null; // пока пусто - надо 
StudentService studentService = null; 
try
{
    groupService = new GroupService(); /// грузим из файла  могут  быть  исключения 
    studentService = new StudentService();
    PrintMenu(); // список команд
}
catch (Exception ex) // ловим  их
{
    Console.WriteLine(ex.Message);
}

while (true) // будем  ждать команды
{
    Console.WriteLine("Введите команду");
    switch (Console.ReadLine()) // если
    {
        case "all group": ConsoleAllGroup(groupService); break;
        case "add group": ConsoleAddGroup(groupService); break;
        case "delete group": DeleteGroupConsole(groupService, studentService); break;
        case "add student": ConsoleAddStudent(studentService, groupService); break; 
        case "all student": ConsoleAllStudent(studentService); break; 
        case "delete student": DeleteStudentConsole(studentService); break;
        case "help": PrintMenu(); break;
        default: Console.WriteLine("непонятно :("); break; 
    }
}

void DeleteGroupConsole(GroupService groupService, StudentService studentService) 
{
    // нельзя удалить  группу пока в  ней  есть студенты - иначе студент будет без группы
    Console.WriteLine("удаление группы:");
    Console.WriteLine("Введите ид группы которую хотите удалить");
    try
    {
        int id = int.Parse(Console.ReadLine());
        Group group = groupService.GetGroup(id); // найдем ее  - если  такой  нет  будет исключение 
        Console.WriteLine($"Вы уверены что вы хотите удалить {group}?"); // защита  от случайного удаления 
        Console.WriteLine($"Введите первую букву группы если да");

        if (group.Name.StartsWith(Console.ReadLine())) // проверка  первых символов 
        {
            groupService.Delete(studentService ,  id); // удаляем studentService - для  проверки есть ли студенты
            Console.WriteLine("Успешно!");
        }
        else
        {
            Console.WriteLine("Удаление отменено!!!!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message); // ловим  ошибки
    }
}

void DeleteStudentConsole(StudentService studentService)
{
    Console.WriteLine("Удаление студентов");
    Console.WriteLine("Введите ид студента которого хотите удалить");
    try
    {
        int id = int.Parse(Console.ReadLine());
        Student student = studentService.GetStudent(id);
        Console.WriteLine($"Вы уверены что вы хотите удалить {student}?");
        Console.WriteLine($"Введите первую букву имени студента если да ");
        
        if( student.Name.StartsWith(Console.ReadLine())) // зашита от  случайного удаления 
        {
            studentService.Delete(id);
            Console.WriteLine("Успешно!");
        }
        else
        {
            Console.WriteLine("Удаление отменено!!!!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
   
}

//вывести  всех студентов
void ConsoleAllStudent(StudentService studentService) 
{
    Console.WriteLine("Все студенты:");
    foreach (var item in studentService.Students) 
    {
        Console.WriteLine(item);
    }
}

void ConsoleAllGroup(GroupService groupService)
{
    Console.WriteLine("Список групп:");

    foreach (Group group in groupService.Groups)
    {
        Console.WriteLine(group);
    }
}

void ConsoleAddGroup(GroupService groupService)
{
    Console.WriteLine("Добавить новую группу:");
    Console.WriteLine("Введите название новой группы");
    string name = Console.ReadLine();

    try
    {
        groupService.Add(name); 
        Console.WriteLine("Группа добавлена");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void ConsoleAddStudent(StudentService studentService, GroupService groupService) 
{
    Console.WriteLine("Добавить нового студента:");
    Console.WriteLine("Введите имя студента");
    string name = Console.ReadLine();

    Console.WriteLine("Введите ID группы");
    try
    {
        int id = int.Parse(Console.ReadLine());
        Group group = groupService.GetGroup(id);
        studentService.Add(name, group);  
        Console.WriteLine("Студент добавлен");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void PrintMenu()
{
    Console.WriteLine("Список команд:");
    Console.WriteLine("all group - список групп");
    Console.WriteLine("add group - новая группа");
    Console.WriteLine("delete group - удалить группу");
    Console.WriteLine("all student - список студентов");
    Console.WriteLine("add student - новый студент");
    Console.WriteLine("delete student - удалить студента");
    Console.WriteLine("help - список команд");
}
