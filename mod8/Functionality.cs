using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod8
{
    class Functionality
    {
        Repository rep = new Repository();
        public void Print(string a)
        {
            Console.WriteLine(a);
        }
        /// <summary>
        /// Обрабатывает исключения Даты
        /// </summary>
        /// <returns></returns>
        public DateTime ExceptionDataTime()
        {
            DateTime db = new DateTime();
            string data;
            do
            {
                data = Console.ReadLine();
                if (DateTime.TryParse(data, out db) == false)
                {
                    Print("неверные данные, повторите ввод");
                }
            } while (DateTime.TryParse(data, out db) == false);
            return db;
        }
        /// <summary>
        /// обрабатывает исключения int
        /// </summary>
        /// <returns></returns>
        public string ExceptionsInt()
        {
            int number;
            do
            {
                number = Rec(Console.ReadLine());
                if (number < 0)
                {
                    Console.WriteLine("Не верное значение повторите ввод");
                }
            } while (number < 0);
            return Convert.ToString(number);
        }
        /// <summary>
        /// доп метод ExceptionsInt
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int Rec(string a)
        {
            bool result;
            int i;
            result = int.TryParse(a, out i);

            if (result)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// обрабатывает исключения string
        /// </summary>
        /// <returns></returns>
        public string ExceptionsString()
        {
            string a;
            do
            {
                a = Console.ReadLine();
                if (a == "")
                {
                    Print("неверное значение повторите ввод");
                }
            } while (a == "");
            return a;
        }
        /// <summary>
        /// Метод вывода в консоль данных о сотрудниках
        /// </summary>
        public void PrintEmployee()
        {
            Console.WriteLine(rep.EmployeeTitle());
            for (int i = 0; i < rep.workers.Count; i++)
            {
                Console.WriteLine(rep.workers[i].Print());
            }
        }
        /// <summary>
        /// метод вывода в консоль данных о депатаменте
        /// </summary>
        public void PrintDepartment()
        {
            Console.WriteLine(rep.DepTitle());
            for (int i = 0; i < rep.dep.Count; i++)
            {
                Console.WriteLine(rep.dep[i].Print());
                Print("Id сотрудников находящихся в департаменте");
                rep.dep[i].PrintList();
            }
        }
        /// <summary>
        ///  Метод добавления департамента
        /// </summary>
        public void RecordDep()
        {
            char key = 'д';
            do
            {
                int tempNumberDep = 0;
                bool result;
                do
                {
                    tempNumberDep++;
                    result = true;
                    for (int i = 0; i < rep.dep.Count; i++)
                    {
                        if (rep.dep[i].NumberDep == tempNumberDep)
                        {
                            result = false;
                        }
                    }
                } while (result == false);

                string[] data = new string[2];
                Print("Введите название депортамента");
                data[0] = ExceptionsString();
                Print("Дата создания депортамента(в формате дд.мм.гггг)");
                data[1] = Convert.ToString(ExceptionDataTime());
                List<int> initializerList = new List<int>();
                initializerList.Add(-1);
                rep.AddDep(new Department(data[0], DateTime.Parse(data[1]), Convert.ToUInt32(tempNumberDep), initializerList));

                Print($"Депортаменту присвоен номер: {tempNumberDep}"); ;
                Print("добавить еще депортамент? д/н");
                key = Console.ReadKey(true).KeyChar;

            } while (char.ToLower(key) == 'д');
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void DeleteDep()
        {
            Print("Номер расформировываемого департамента");
            int number = Convert.ToInt32(ExceptionsInt());
            if (rep.dep.Count == 1)
            {
                Print("В данной организации всего один департамент");
            }
            else if (rep.dep.Count == 0)
            {
                Print("В данной организации не сформированы департаменты");
                RecordDep();
            }
            else
            {

                int indexDep = -1;
                for (int i = 0; i < rep.dep.Count; i++)
                {
                    if (number == Convert.ToInt32(rep.dep[i].NumberDep))
                    {
                        indexDep = i;
                    }

                }
                if (indexDep >= 0)
                {
                    List<int> idEmployee = new List<int>();
                    foreach (var item in rep.dep[indexDep].Id)
                    {
                        idEmployee.Add(item);
                    }
                    if (idEmployee[0] != 1000001)
                    {
                        if (number == 1)
                        {
                            DeleteDepAdditional(idEmployee, 2);
                        }
                        else
                        {
                            DeleteDepAdditional(idEmployee, 1);
                        }
                    }
                    rep.dep.RemoveAt(indexDep);
                }
                else
                {
                    Print("Неверный ID");
                }
            }
        }
        /// <summary>
        /// Доп метод удаленя департамента(перезапись номера департамента у сотрудников)
        /// </summary>
        /// <param name="idEmployee"></param>
        /// <param name="number"></param>
        public void DeleteDepAdditional(List<int> idEmployee, int number)
        {

            foreach (var e in idEmployee)
            {
                rep.dep[number - 1].Id.Add(e);
            }
            if (rep.dep[number - 1].Id[0] == 1000001)
            {
                rep.dep[number - 1].Id.RemoveAt(0);
            }
            List<int> idPeople = new List<int>();
            for (int i = 0; i < idEmployee.Count; i++)
            {
                int id = SearchEmployee(Convert.ToInt32(idEmployee[i]));
                rep.workers[id] = new Worker
                {
                    Name = rep.workers[id].Name,
                    Surname = rep.workers[id].Surname,
                    Age = rep.workers[id].Age,
                    Salary = rep.workers[id].Salary,
                    Projects = rep.workers[id].Projects,
                    Id = rep.workers[id].Id,
                    IdDep = Convert.ToUInt32(number),
                };
            }
        }
        /// <summary>
        /// доп метод удаленя департамента(возврат id сотрудников находящихся в департаменте)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SearchEmployee(int id)
        {
            for (int i = 0; i < rep.workers.Count; i++)
            {
                if (id == rep.workers[i].Id)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Метод редактирование департамента
        /// </summary>
        /// <param name="number"></param>
        public void EditDep(int number)
        {
            number = -1;

            Print("Выбирите редоктироемое поле:");
            Print("1 - Название");
            Print("2 - Дата создания");
            Print("Для редактирования всех полей нажмите: ENTER");

            int index = Convert.ToInt32(ExceptionsInt());

            switch (index)
            {
                case 1:
                    rep.dep[number] = new Department
                    {
                        NumberDep = rep.dep[number].NumberDep,
                        Data = rep.dep[number].Data,
                        Id = rep.dep[number].Id,
                        NameDep = ExceptionsString(),
                    };
                    break;
                case 2:
                    rep.dep[number] = new Department
                    {
                        NumberDep = rep.dep[number].NumberDep,
                        Data = ExceptionDataTime(),
                        Id = rep.dep[number].Id,
                        NameDep = rep.dep[number].NameDep,
                    };
                    break;
                default:
                    rep.dep[number] = new Department
                    {
                        NameDep = Console.ReadLine(),
                        NumberDep = rep.dep[number].NumberDep,
                        Data = ExceptionDataTime(),
                        Id = rep.dep[number].Id,
                    };
                    break;
            }
        }
        /// <summary>
        /// Метод добавления сотрудника
        /// </summary>
        public void RecordEmployee()
        {
            char key = 'д';

            if (rep.dep.Count == 0)
            {
                Print("Создайте департамент");
                RecordDep();
            }
            else
            {
                Console.WriteLine();
                Print("Выберете номер депортамента");
                int indexDep = Convert.ToInt32(Console.ReadLine());

                do
                {
                    int index = rep.SearchEmployeeRreeId();

                    string[] data = new string[5];
                    Print("Имя");
                    data[0] = ExceptionsString();
                    Print("Фамилия");
                    data[1] = ExceptionsString();
                    Print("Возраст");
                    data[2] = ExceptionsInt();
                    Print("Количество закрепленных проектов");
                    data[3] = ExceptionsInt();
                    Print("Зарплата");
                    data[4] = ExceptionsInt();

                    rep.AddWorker(new Worker(data[1], data[0], Convert.ToUInt32(data[2]), Convert.ToUInt32(data[4]), Convert.ToUInt32(data[3]), Convert.ToUInt32(index), Convert.ToUInt32(indexDep)));

                    //List<int> idList = new List<int>();
                    if (rep.dep[indexDep - 1].Id[0] == -1)
                    {
                        rep.dep[indexDep - 1].Id[0] = index;
                    }
                    else
                    {
                        rep.dep[indexDep - 1].AddId(index);
                    }
                    //int countId = rep.dep[indexDep - 1].Id.Count;
                    //idList.Add(rep.index + 1);
                    rep.index++;

                    Print("Добавить еще сотрудника? д/н");
                    key = Console.ReadKey(true).KeyChar;
                } while (char.ToLower(key) == 'д');
            }
        }
        /// <summary>
        /// Метод редактирования сотрудников
        /// </summary>
        /// <param name="number"></param>
        public void EditEmployee(int number)
        {
            number = -1;
            char key = 'д';
            do
            {
                Print("Выбирите редоктироемое поле:");
                Print("1 - Имя");
                Print("2 - Фамилия");
                Print("3 - Возраст");
                Print("4 - Зарплата");
                Print("5 - Колличество проектов");
                Print("Для редактирования всех полей нажмите: ENTER");

                int index = Convert.ToInt32(ExceptionsInt());

                switch (index)
                {
                    case 1:
                        rep.workers[number] = new Worker
                        {
                            Name = ExceptionsString(),
                            Surname = rep.workers[number].Surname,
                            Age = rep.workers[number].Age,
                            Salary = rep.workers[number].Salary,
                            Projects = rep.workers[number].Projects,
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                    case 2:
                        rep.workers[number] = new Worker
                        {
                            Name = rep.workers[number].Name,
                            Surname = ExceptionsString(),
                            Age = rep.workers[number].Age,
                            Salary = rep.workers[number].Salary,
                            Projects = rep.workers[number].Projects,
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                    case 3:
                        rep.workers[number] = new Worker
                        {
                            Name = rep.workers[number].Name,
                            Surname = rep.workers[number].Surname,
                            Age = Convert.ToUInt32(ExceptionsInt()),
                            Salary = rep.workers[number].Salary,
                            Projects = rep.workers[number].Projects,
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                    case 4:
                        rep.workers[number] = new Worker
                        {
                            Name = rep.workers[number].Name,
                            Surname = rep.workers[number].Surname,
                            Age = rep.workers[number].Age,
                            Salary = Convert.ToUInt32(ExceptionsInt()),
                            Projects = rep.workers[number].Projects,
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                    case 5:
                        rep.workers[number] = new Worker
                        {
                            Name = rep.workers[number].Name,
                            Surname = rep.workers[number].Surname,
                            Age = rep.workers[number].Age,
                            Salary = rep.workers[number].Salary,
                            Projects = Convert.ToUInt32(ExceptionsInt()),
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                    default:
                        rep.workers[number] = new Worker
                        {
                            Name = ExceptionsString(),
                            Surname = ExceptionsString(),
                            Age = Convert.ToUInt32(ExceptionsInt()),
                            Salary = Convert.ToUInt32(ExceptionsInt()),
                            Projects = Convert.ToUInt32(ExceptionsInt()),
                            Id = rep.workers[number].Id,
                            IdDep = rep.workers[number].IdDep,
                        };
                        break;
                }
                Print("Редоктировать другие данные? д/н?");
                key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'д');

        }
        /// <summary>
        /// Метод перевода сотрудника
        /// </summary>
        public void TransferEmployee()
        {
            int index;
            int idNewDep;
            int idDep;
            int idEmployee;
            bool logic = true;
            do
            {
                Print("Введите Id сотрудника для перевода");
                index = Convert.ToInt32(Console.ReadLine());
                Print("Номер департамента в который переводим");
                idNewDep = Convert.ToInt32(Console.ReadLine());
                idDep = Convert.ToInt32(rep.workers[index].IdDep);
                idEmployee = Convert.ToInt32(rep.workers[index].Id);

                if (rep.dep[idDep - 1].Id.Contains(idEmployee) == false)
                {
                    Print("в даном департаменте нет сотрудника с таким ID");
                    logic = false;
                }
                else
                {
                    rep.workers[index] = new Worker
                    {
                        Name = rep.workers[index].Name,
                        Surname = rep.workers[index].Surname,
                        Age = rep.workers[index].Age,
                        Salary = rep.workers[index].Salary,
                        Projects = rep.workers[index].Projects,
                        Id = rep.workers[index].Id,
                        IdDep = Convert.ToUInt32(idNewDep),
                    };
                    if (rep.dep[idDep - 1].Id.Count == 1)
                    {
                        rep.dep[idDep - 1].Id.Add(1000001);
                        rep.dep[idDep - 1].Id.Remove(idEmployee);
                    }
                    else
                    {
                        rep.dep[idDep - 1].Id.Remove(idEmployee);
                    }
                    if (rep.dep[idNewDep - 1].Id.Contains(1000001) == true)
                    {
                        rep.dep[idNewDep - 1].Id.Remove(1000001);
                        rep.dep[idNewDep - 1].Id.Add(idEmployee);
                    }
                    else
                    {
                        rep.dep[idNewDep - 1].Id.Add(idEmployee);
                    }
                   
                }
            }
            while (logic == false);
            PrintEmployee();
            PrintDepartment();
        }
        /// <summary>
        /// Удаление сотрудников
        /// </summary>
        public void DismissEmployee()
        {
            int index = Convert.ToInt32(ExceptionsInt());

            int indexEmployee = -1;
            for (int i = 0; i < rep.workers.Count; i++)
            {
                if (index == Convert.ToInt32(rep.workers[i].Id))
                {
                    indexEmployee = i;
                }

            }

            if (indexEmployee >= 0)
            {


                int idDep = Convert.ToInt32(rep.workers[indexEmployee].IdDep);
                int idEmployee = Convert.ToInt32(rep.workers[indexEmployee].Id);


                rep.workers.RemoveAt(indexEmployee);
                if (rep.dep[idDep - 1].Id.Count == 1)
                {
                    rep.dep[idDep - 1].Id.Add(1000001);
                    rep.dep[idDep - 1].Id.Remove(idEmployee);
                }
                else
                {
                    rep.dep[idDep - 1].Id.Remove(idEmployee);
                }
                rep.index--;
            }
            else
            {
                Print("Неверный ID");
            }


        }
        /// <summary>
        /// Проверка существования файла
        /// </summary>
        public void XMLReadControl()
        {
            char key = 'д';
            do
            {
                Print("Введите путь к файлу");
                string path = ExceptionsString();
                bool result = File.Exists(path);
                if (true == result)
                {
                    rep.XMLRead(path);
                }
                else
                {
                    Print("неверный путь к файлу");
                }

                Print("Повторить ввод? д/н");
                key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'д');
            
        }
        /// <summary>
        /// Проверка существования файла
        /// </summary>
        public void JSONReadControl()
        {
            char key = 'д';
            do
            {
                Print("Введите путь к файлу");
                string path = ExceptionsString();
                bool result = File.Exists(path);
                if (true == result)
                {
                    rep.JSONRead(path);
                }
                else
                {
                    Print("неверный путь к файлу");
                }

                Print("Повторить ввод? д/н");
                key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'д');
        }
        /// <summary>
        /// Сортировка сотрудников
        /// </summary>
        public void SortWorker()
        {
            Worker[] employee = new Worker[rep.workers.Count];
            var sortTable = new Worker[1];
            for (int i = 0; i < rep.workers.Count; i++)
            {
                employee[i] = rep.workers[i];
            }
            Print("Выберете" +
                "\n1 - сортировка по возрасту" +
                "\n2 - сортировка по Зарплате" +
                "\n3 - сортировка по колличеству проектов" +
                "\n4 - сортировка по департаменту и зарплате" +
                "\n5 - сортировка по департаменту и количеству проектов" +
                "\n6 - сортировка по возрасту и зарплате в рамках одного департамента");
            int index = Convert.ToInt32(ExceptionsInt());

            switch (index)
            {
                case 1:
                    sortTable = employee.OrderBy(x => x.Age).ToArray();
                    break;
                case 2:
                    sortTable = employee.OrderBy(x => x.Salary).ToArray();
                    break;
                case 3:
                    sortTable = employee.OrderBy(x => x.Projects).ToArray();
                    break;
                case 4:
                    sortTable = employee.OrderBy(x => x.IdDep).ThenBy(x => x.Salary).ToArray();
                    break;
                case 5:
                    sortTable = employee.OrderBy(x => x.IdDep).ThenBy(x => x.Projects).ToArray();
                    break;
                case 6:
                    sortTable = employee.OrderBy(x => x.IdDep).ThenBy(x => x.Age).ThenBy(x => x.Salary).ToArray();
                    break;
                default:
                    break;
            }
            Console.WriteLine(rep.EmployeeTitle());
            for (int i = 0; i < sortTable.Length; i++)
            {
                Console.WriteLine($"{sortTable[i].Surname,30} {sortTable[i].Name,30} {sortTable[i].Salary,5} {sortTable[i].Age,10} {sortTable[i].Projects,15} {sortTable[i].Id,12} {sortTable[i].IdDep,5}");
                Console.WriteLine();
            }
        }
    }
}
