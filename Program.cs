internal class Program
{
    private static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee() { Id = 105, Name = "Jack", Age = 3 },
            new Employee() { Id = 103, Name = "Jill", Age = 4 },
            new Employee() { Id = 101, Name = "Joe", Age = 4 },
            new Employee() { Id = 108, Name = "Janick", Age = 4 }
        };

        Console.WriteLine("Default order of the list: ");

        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.Id + ", " + employee.Name);
        }
        employees.Sort();

        Console.WriteLine("Sorted order of the list: ");
        foreach (Employee employee in employees)
        {
            Console.WriteLine(employee.Id + ", " + employee.Name);
        }

        List<EmployeeNotComparable> employeesNC = new List<EmployeeNotComparable>()
        {
            new EmployeeNotComparable() { Id = 107, Name = "Samuel", Age = 5 },
            new EmployeeNotComparable() { Id = 105, Name = "Jack", Age = 3 },
            new EmployeeNotComparable() { Id = 103, Name = "Peter", Age = 4 },
            new EmployeeNotComparable() { Id = 101, Name = "Hedvig", Age = 4 },
            new EmployeeNotComparable() { Id = 108, Name = "Emil", Age = 4 }
        };

        CustomComparer comparer = new CustomComparer();

        Console.WriteLine("Default order of the default non comparable list: ");

        foreach (EmployeeNotComparable employee in employeesNC)
        {
            Console.WriteLine(employee.Id + ", " + employee.Name + ", " + employee.Age);
        }

        comparer.SortBy = SortByType.Name;
        employeesNC.Sort(comparer);

        Console.WriteLine("Sorted order of the default not comparable list by Name: ");
        foreach (EmployeeNotComparable employee in employeesNC)
        {
            Console.WriteLine(employee.Id + ", " + employee.Name + ", " + employee.Age);
        }

        comparer.SortBy = SortByType.Id;
        employeesNC.Sort(comparer);

        Console.WriteLine("Sorted order of the default not comparable list by Id: ");
        foreach (EmployeeNotComparable employee in employeesNC)
        {
            Console.WriteLine(employee.Id + ", " + employee.Name + ", " + employee.Age);
        }
    }

    //This class to represent making a not comparable class to be able to compare
    public class EmployeeNotComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CustomComparer : IComparer<EmployeeNotComparable>
    {
        public int Compare(EmployeeNotComparable? x, EmployeeNotComparable? y)
        {
            int result = 0;
            switch (this.SortBy)
            {
                case SortByType.Id:
                    result = x.Id - y.Id; 
                    break;
                case SortByType.Name:
                    result = (x.Name == null) ? 0 : x.Name.CompareTo(y.Name); 
                    break; 
                case SortByType.Age:
                    result = x.Age - y.Age;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
            ////It's possible to sort by 2 or more properties in order
            //int result = 0;
            //if (x.Age != 0 && y.Age != 0)
            //{
            //    result = x.Age - y.Age;
            //    if (result == 0)
            //    {
            //        result = x.Name.CompareTo(y.Name);
            //    }
            //}
            //return result;
        }

        public SortByType SortBy { get; set; }
    }

    //It helps for the sorting to decide on what kind of data it should sort
    public enum SortByType
    {
        Id, Name, Age
    }

    public class Employee : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        ////CompareTo checks the parameter object with the actual object and if it returns negative number it makes the changes
        //public int CompareTo(object? obj)
        //{
        //    Employee otherEmp = (Employee)obj;
        //    return this.Id - otherEmp.Id;
        //}

        public int CompareTo(object? obj)
        {
            Employee emp = obj as Employee;
            return this.Name.CompareTo(emp.Name);
        }
    }
}