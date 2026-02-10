using System;
using System.Collections.Generic;
using System.Linq;

namespace University_Course_Registration_System
{
     // =========================
    // Program (Menu-Driven)
    // =========================
    class Program
    {
        static void Main()
        {
            UniversitySystem system = new UniversitySystem();
            bool exit = false;

            Console.WriteLine("Welcome to University Course Registration System");

            while (!exit)
            {
                Console.WriteLine("\n1. Add Course");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. Register Student for Course");
                Console.WriteLine("4. Drop Student from Course");
                Console.WriteLine("5. Display All Courses");
                Console.WriteLine("6. Display Student Schedule");
                Console.WriteLine("7. Display System Summary");
                Console.WriteLine("8. Exit");

                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                try
                {
                    // TODO:
                    // Implement menu handling logic using switch-case
                    // Prompt user inputs
                    // Call appropriate UniversitySystem methods
                    // UniversitySystem system = new UniversitySystem();
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Enter Course Code: ");
                            string courseCode=Console.ReadLine();
                            Console.Write("Enter Course Name: ");
                            string courseName = Console.ReadLine();
                            Console.Write("Enter Credits : ");
                            int credits = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Max Capacity (default 50): ");
                            string ?capacityInput=Console.ReadLine();
                            int ?maxCapacity = string.IsNullOrWhiteSpace(capacityInput)?null:int.Parse(capacityInput);
                            Console.Write("Enter Prerequisites (comma-separated, or Enter for none): ");
                            string? input = Console.ReadLine();
                            List<string> prerequisites = input?
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToList()
                                ?? new List<string>();
                            system.AddCourse(courseCode, courseName, credits, maxCapacity??50, prerequisites);
                                Console.WriteLine($"Course {courseCode} added successfully.");
                            break;
                        case "2":
                            Console.Write("Enter Student ID: ");
                            string studentId = Console.ReadLine();
                            Console.Write("Enter Student Name: ");
                            string studentName = Console.ReadLine();
                            Console.Write("Enter Major : ");
                            string major = Console.ReadLine();
                            Console.Write("Enter Max Credits (default 18): ");
                            string ?creditInput=Console.ReadLine();
                            int? maxCredits= string.IsNullOrWhiteSpace(creditInput)?null:int.Parse(creditInput);
                            Console.Write("Enter Completed Courses (comma-separated, or Enter for none): ");
                            input = Console.ReadLine();
                            List<string> completedCourses = input?
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(p => p.Trim())
                                .ToList()
                                ?? new List<string>();
                            system.AddStudent(studentId, studentName, major, maxCredits??18, completedCourses);
                            Console.WriteLine($"Student {studentId} added successfully.");
                            break;
                        case "3":
                            Console.Write("Enter Student ID: ");
                            studentId = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            courseCode = Console.ReadLine();
                            system.RegisterStudentForCourse(studentId, courseCode);
                            break;
                        case "4":
                            Console.Write("Enter Student ID: ");
                            studentId = Console.ReadLine();
                            Console.Write("Enter Course Code: ");
                            courseCode = Console.ReadLine();
                            system.DropStudentFromCourse(studentId, courseCode);
                            break;
                        case "5":
                            system.DisplayAllCourses();
                            break;
                        case "6":
                            Console.Write("Enter Student ID: ");
                            studentId = Console.ReadLine();
                            system.DisplayStudentSchedule(studentId);
                            break;
                        case "7":
                            system.DisplaySystemSummary();
                            break;
                        case "8":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid Choice");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

