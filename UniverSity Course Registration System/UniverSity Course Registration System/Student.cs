using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Registration_System
{
    // =========================
    // Student Class
    // =========================
    public class Student
    {
        public string StudentId { get; private set; }
        public string Name { get; private set; }
        public string Major { get; private set; }
        public int MaxCredits { get; private set; }

        public List<string> CompletedCourses { get; private set; }
        public List<Course> RegisteredCourses { get; private set; }

        public Student(string id, string name, string major, int maxCredits = 18, List<string> completedCourses = null)
        {
            StudentId = id;
            Name = name;
            Major = major;
            MaxCredits = maxCredits;
            CompletedCourses = completedCourses ?? new List<string>();
            RegisteredCourses = new List<Course>();
        }

        public int GetTotalCredits()
        {
            int totalCredits = 0;
            // TODO: Return sum of credits of all RegisteredCourses
            foreach(var course in RegisteredCourses)
            {
                totalCredits+= course.Credits;
            }
            return totalCredits;
            
        }

        public bool CanAddCourse(Course course)
        {
            // TODO:
            // 1. Course should not already be registered
            // 2. Total credits + course credits <= MaxCredits
            // 3. Course prerequisites must be satisfied
            if (RegisteredCourses.Contains(course))
            {
                Console.WriteLine("Course Already Reistered");
                return false;
            }
            if (GetTotalCredits() + course.Credits > MaxCredits)
            {
                Console.WriteLine("Course Credits exeeds max credits");
                return false;
            }
            if (!course.HasPrerequisites(CompletedCourses))
            {
                Console.WriteLine("Prerequisites not fullfilled");
                return false;
            }
            return true;
            
        }

        public bool AddCourse(Course course)
        {
            // TODO:
            // 1. Call CanAddCourse
            // 2. Check course capacity
            // 3. Add course to RegisteredCourses
            // 4. Call course.EnrollStudent()
            if (!CanAddCourse(course))
                return false;
            if (course.IsFull())
            {
                Console.WriteLine("Course Already full");
                return false;
            }
            RegisteredCourses.Add(course);
            course.EnrollStudent();
            Console.WriteLine($"Registration successful! Total credits: {GetTotalCredits()} / {MaxCredits}.");
            return true;
            
        }

        public bool DropCourse(string courseCode)
        {
            // TODO:
            // 1. Find course by code
            // 2. Remove from RegisteredCourses
            // 3. Call course.DropStudent()
            foreach(var course in RegisteredCourses)
            {
                if(course.CourseCode==courseCode)
                {
                    RegisteredCourses.Remove(course);
                    course.DropStudent();
                    Console.WriteLine($"Drop successful! Total credits: {GetTotalCredits()} / {MaxCredits}.");
                    return true;
                }
            }
            Console.WriteLine("Course Code not found");
            return false;
            
        }

        public void DisplaySchedule()
        {
            // TODO:
            // Display course code, name, and credits
            // If no courses registered, display appropriate message
            foreach(var course in RegisteredCourses)
            {
                Console.WriteLine($"{course.CourseCode} {course.CourseName} {course.Credits}");
            }
            
        }
    }
}
