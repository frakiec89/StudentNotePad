using StudentNotePad.Model;
using StudentNotePad.Services;

GroupService groupService = null;
StudentService studentService = null; // Исправлено: "StudetnService" → "StudentService"
try
{
    groupService = new GroupService();
    studentService = new StudentService(); // Исправлено: "StudetnService" → "StudentService"
    PrintMenu();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

while (true)
{
    Console.WriteLine("Введите команду");
    switch (Console.ReadLine())
    {
        case "all group": ConsoleAllGroup(groupService); break;
        case "add group": ConsoleAddGroup(groupService); break;
        case "add student": ConsoleAddStudent(studentService, groupService); break; // Исправлено: "studetnService" → "studentService"
        case "all student": ConsoleAllStudent(studentService, groupService); break; // Исправлено: "studetnService" → "studentService"
        case "help": PrintMenu(); break;
        default: Console.WriteLine("непонятно :("); break; // Исправлено: "не понятно((" → "непонятно :("
    }
}

void ConsoleAllStudent(StudentService studentService, GroupService groupService) // Исправлено: "StudetnService" → "StudentService"
{
    Console.WriteLine("Все студенты:");
    foreach (var item in studentService.Students) // Исправлено: "studetnService" → "studentService"
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
        groupService.Add(name); // Предполагается метод "Add" с заглавной буквой
        Console.WriteLine("Группа добавлена");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

void ConsoleAddStudent(StudentService studentService, GroupService groupService) // Исправлено: "StudetnService" → "StudentService"
{
    Console.WriteLine("Добавить нового студента:");
    Console.WriteLine("Введите имя студента");
    string name = Console.ReadLine();

    Console.WriteLine("Введите ID группы");
    try
    {
        int id = int.Parse(Console.ReadLine());
        Group group = groupService.GetGroup(id);
        studentService.Add(name, group); // Исправлено: "studetnService" → "studentService"
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
    Console.WriteLine("all student - список студентов");
    Console.WriteLine("add student - новый студент");
}
