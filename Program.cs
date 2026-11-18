using System.ComponentModel.Design;
using System.IO;
using System.Text.Json;

string? choice = "";
List<TaskItem> tasks;

if (File.Exists("tasks.json"))
{
    try
    {
        string content = File.ReadAllText("tasks.json");
        tasks = JsonSerializer.Deserialize<List<TaskItem>>(content) ?? new List<TaskItem>();
    }
    catch (JsonException)
    {
        Console.WriteLine("Error: tasks.json is corrupted.");
        tasks = new List<TaskItem>();
    }
}
else
{
    tasks = new List<TaskItem>();
}

while (choice != "5")
{
    Console.WriteLine("=====================");
    Console.WriteLine("    TASK MANAGER");
    Console.WriteLine("=====================");

    Console.WriteLine();
    Console.WriteLine("1. Show Tasks");
    Console.WriteLine("2. Add Task");
    Console.WriteLine("3. Delete Task");
    Console.WriteLine("4. Edit Task");
    Console.WriteLine("5. Exit");

    Console.WriteLine();
    Console.Write("Choose an Option: ");

    choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            //Show Tasks
            if (tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks yet.");
            }
            else
            {
                Console.WriteLine("There are your tasks: ");
                foreach (TaskItem task in tasks)
                {
                    Console.WriteLine($"- {task.Name} (Priority: {task.Priority})");
                }
            }
            break;
        case "2":
            //Add Task
            Console.Write("Enter your task: ");
            string? newtask = Console.ReadLine();
            tasks.Add(new TaskItem { Name = newtask, Description = "", Priority = Priority.Low });

            Console.WriteLine("please choose a priority for your task: ");
            Console.WriteLine("1. High");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Low");
            string? priorityInput = Console.ReadLine();

            switch (priorityInput)
            {
                case "1":
                    tasks[tasks.Count - 1].Priority = Priority.High;
                    break;
                case "2":
                    tasks[tasks.Count - 1].Priority = Priority.Medium;
                    break;
                case "3":
                    tasks[tasks.Count - 1].Priority = Priority.Low;
                    break;
                default:
                    tasks[tasks.Count - 1].Priority = Priority.Low;
                    break;
            }
            Console.WriteLine("Task added successfully!");
            
            string json = JsonSerializer.Serialize(tasks);
            File.WriteAllText("tasks.json", json);

            break;
        case "3":
            //Delete Task
            if (tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks to delete");
            }
            else
            {
                Console.WriteLine("Here are your tasks:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
                Console.WriteLine("Enter the task number you want to delete: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    tasks.RemoveAt(taskNumber - 1);
                    Console.WriteLine("Task deleted successfully!");

                    string jsonDelete = JsonSerializer.Serialize(tasks);
                    File.WriteAllText("tasks.json", jsonDelete);
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter a valid task number.");
                }
            }
            break;
        case "4":
            // Edit Task
            if (tasks.Count == 0)
            {
                Console.WriteLine("There are no tasks to edit.");
            }
            else
            {
                Console.WriteLine("Here are your tasks: ");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
                Console.WriteLine("Enter the task number you want to edit: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    Console.WriteLine("Edit your task: ");
                    tasks[taskNumber - 1] = new TaskItem { Name = Console.ReadLine(), Description = "", Priority = Priority.Low };
                    Console.WriteLine("Task edited successfully!");

                    string jsonEdit = JsonSerializer.Serialize(tasks);
                    File.WriteAllText("tasks.json", jsonEdit);
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please enter a valid task number.");
                }
            }
            break;
        case "5":
            //Exit
            Console.WriteLine("Exiting Task Manager. Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid Option. Please Try Again.");
            break;
    }
}
