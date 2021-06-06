using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mod8
{
    class Repository
    {
        /// <summary>
        /// Коллекция для хранения данных депортамента
        /// </summary>
        public List<Department> dep;
        /// <summary>
        /// Коллекция хранения анных
        /// </summary>
        public List<Worker> workers;
        /// <summary>
        /// Хранение колличества сотрудников
        /// </summary>
        int index;
        /// <summary>
        /// Инициализация метода
        /// </summary>
        /// <param name="path"></param>
        public Repository()
        {
            this.dep = new List<Department>();
            this.workers = new List<Worker>();
        }
        /// <summary>
        /// запись данных в департамент
        /// </summary>
        /// <param name="rec"></param>
        public void AddDep(Department rec)
        {
            dep.Add(rec);
        }
        /// <summary>
        /// запись данных о сотруднике
        /// </summary>
        /// <param name="rec"></param>
        public void AddWorker(Worker rec)
        {
            workers.Add(rec);
        }
        /// <summary>
        /// Метод печати
        /// </summary>
        /// <param name="a">Текст</param>
        public void Print(string a)
        {
            Console.WriteLine(a);
        }
        /// <summary>
        /// печать заголовка депортаментов
        /// </summary>
        /// <returns></returns>
        public string DepTitle()
        {
            return $"{"Название депортамента",30}{"Номер депортамента",30}{"Дата создания",35}";
        }
        /// <summary>
        /// печать заголовка сотрудников
        /// </summary>
        /// <returns></returns>
        public string EmployeeTitle()
        {
            return $"{"Фамилия",30}{"Имя",30}{"  Зарплата",10}{"  Возраст",10}{"  колличество проектов",22}{"   ID",5}{"  ID Dep",8}";
        }
        /// <summary>
        /// Метод вывода в консоль данных о сотрудниках
        /// </summary>
        public void PrintEmployee()
        {
            Console.WriteLine(EmployeeTitle());
            for (int i = 0; i < workers.Count; i++)
            {
                Console.WriteLine(this.workers[i].Print());
            }
        }
        /// <summary>
        /// метод вывода в консоль данных о депатаменте
        /// </summary>
        public void PrintDepartment()
        {
            Console.WriteLine(DepTitle());
            for (int i = 0; i < this.dep.Count; i++)
            {
                Console.WriteLine(this.dep[i].Print());
                Print("Id сотрудников находящихся в департаменте");
                dep[i].PrintList();
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
        /// Метод добавления сотрудника
        /// </summary>
        public void RecordEmployee()
        {
            char key = 'д';

            if (dep.Count == 0)
            {
                Print("Создайте департамент");
                RecordDep();
            }
            else
            {
                Console.WriteLine();
                Print("Выберете номер депортамента");
                int index = Convert.ToInt32(Console.ReadLine());

                do
                {
                    object idWorker = this.workers.Count;
                    if (idWorker == null)
                    {
                        this.index = 0;
                    }
                    else
                    {
                        this.index = Convert.ToInt32(idWorker);
                    }

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

                    AddWorker(new Worker(data[1], data[0], Convert.ToUInt32(data[2]), Convert.ToUInt32(data[4]), Convert.ToUInt32(data[3]), Convert.ToUInt32(this.index), Convert.ToUInt32(index)));

                    List<int> idList = new List<int>();
                    if (dep[index - 1].Id[0] == -1)
                    {
                        dep[index - 1].Id[0] = this.index;
                    }
                    else
                    {
                        dep[index - 1].AddId(this.index);
                    }
                    int countId = dep[index - 1].Id.Count;
                    idList.Add(this.index + 1);


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
                        workers[number] = new Worker
                        {
                            Name = ExceptionsString(),
                            Surname = workers[number].Surname,
                            Age = workers[number].Age,
                            Salary = workers[number].Salary,
                            Projects = workers[number].Projects,
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                    case 2:
                        workers[number] = new Worker
                        {
                            Name = workers[number].Name,
                            Surname = ExceptionsString(),
                            Age = workers[number].Age,
                            Salary = workers[number].Salary,
                            Projects = workers[number].Projects,
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                    case 3:
                        workers[number] = new Worker
                        {
                            Name = workers[number].Name,
                            Surname = workers[number].Surname,
                            Age = Convert.ToUInt32(ExceptionsInt()),
                            Salary = workers[number].Salary,
                            Projects = workers[number].Projects,
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                    case 4:
                        workers[number] = new Worker
                        {
                            Name = workers[number].Name,
                            Surname = workers[number].Surname,
                            Age = workers[number].Age,
                            Salary = Convert.ToUInt32(ExceptionsInt()),
                            Projects = workers[number].Projects,
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                    case 5:
                        workers[number] = new Worker
                        {
                            Name = workers[number].Name,
                            Surname = workers[number].Surname,
                            Age = workers[number].Age,
                            Salary = workers[number].Salary,
                            Projects = Convert.ToUInt32(ExceptionsInt()),
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                    default:
                        workers[number] = new Worker
                        {
                            Name = ExceptionsString(),
                            Surname = ExceptionsString(),
                            Age = Convert.ToUInt32(ExceptionsInt()),
                            Salary = Convert.ToUInt32(ExceptionsInt()),
                            Projects = Convert.ToUInt32(ExceptionsInt()),
                            Id = workers[number].Id,
                            IdDep = workers[number].IdDep,
                        };
                        break;
                }
                Print("Редоктировать другие данные? д/н?");
                key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'д');

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
                    dep[number] = new Department
                    {
                        NumberDep = dep[number].NumberDep,
                        Data = dep[number].Data,
                        Id = dep[number].Id,
                        NameDep = ExceptionsString(),
                    };
                    break;
                case 2:
                    dep[number] = new Department
                    {
                        NumberDep = dep[number].NumberDep,
                        Data = ExceptionDataTime(),
                        Id = dep[number].Id,
                        NameDep = dep[number].NameDep,
                    };
                    break;
                default:
                    dep[number] = new Department
                    {
                        NameDep = Console.ReadLine(),
                        NumberDep = dep[number].NumberDep,
                        Data = ExceptionDataTime(),
                        Id = dep[number].Id,
                    };
                    break;
            }
        }
        /// <summary>
        /// Удаление сотрудников
        /// </summary>
        public void DismissEmployee()
        {
            int index = Convert.ToInt32(ExceptionsInt());

            int indexEmployee = -1;
            for (int i = 0; i < workers.Count; i++)
            {
                if (index == Convert.ToInt32(workers[i].Id))
                {
                    indexEmployee = i;
                }

            }

            if (indexEmployee >= 0)
            {


                int idDep = Convert.ToInt32(workers[indexEmployee].IdDep);
                int idEmployee = Convert.ToInt32(workers[indexEmployee].Id);


                workers.RemoveAt(indexEmployee);
                if (dep[idDep - 1].Id.Count == 1)
                {
                    dep[idDep - 1].Id.Add(1000001);
                    dep[idDep - 1].Id.Remove(idEmployee);
                }
                else
                {
                    dep[idDep - 1].Id.Remove(idEmployee);
                }
            }
            else
            {
                Print("Неверный ID");
            }


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
                idDep = Convert.ToInt32(workers[index].IdDep);
                idEmployee = Convert.ToInt32(workers[index].Id);

                if (dep[idDep - 1].Id.Contains(idEmployee) == false)
                {
                    Print("в даном департаменте нет сотрудника с таким ID");
                    logic = false;
                }
                else
                {
                    workers[index] = new Worker
                    {
                        Name = workers[index].Name,
                        Surname = workers[index].Surname,
                        Age = workers[index].Age,
                        Salary = workers[index].Salary,
                        Projects = workers[index].Projects,
                        Id = workers[index].Id,
                        IdDep = Convert.ToUInt32(idNewDep),
                    };
                    if (dep[idDep - 1].Id.Count == 1)
                    {
                        dep[idDep - 1].Id.Add(1000001);
                        dep[idDep - 1].Id.Remove(idEmployee);
                    }
                    else
                    {
                        dep[idDep - 1].Id.Remove(idEmployee);
                    }
                    if (dep[idNewDep - 1].Id.Contains(1000001) == true)
                    {
                        dep[idNewDep - 1].Id.Remove(1000001);
                        dep[idNewDep - 1].Id.Add(idEmployee);
                    }
                    else
                    {
                        dep[idNewDep - 1].Id.Add(idEmployee);
                    }
                }
            }
            while (logic == false);
            PrintEmployee();
            PrintDepartment();
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        public void DeleteDep()
        {
            Print("Номер расформировываемого департамента");
            int number = Convert.ToInt32(ExceptionsInt());
            if (dep.Count == 1)
            {
                Print("В данной организации всего один департамент");
            }
            else if (dep.Count == 0)
            {
                Print("В данной организации не сформированы департаменты");
                RecordDep();
            }
            else
            {
                
                int indexDep = -1;
                for (int i = 0; i < dep.Count; i++)
                {
                    if (number == Convert.ToInt32(dep[i].NumberDep))
                    {
                        indexDep = i;
                    }

                }
                if (indexDep >= 0)
                {
                    List<int> idEmployee = new List<int>();
                    foreach (var item in dep[indexDep].Id)
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
                    dep.RemoveAt(indexDep);
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
                dep[number - 1].Id.Add(e);
            }
            if (dep[number - 1].Id[0] == 1000001)
            {
                dep[number - 1].Id.RemoveAt(0);
            }
            List<int> idPeople = new List<int>();
            for (int i = 0; i < idEmployee.Count; i++)
            {
                int id = SearchEmployee(Convert.ToInt32(idEmployee[i]));
                workers[id] = new Worker
                {
                    Name = workers[id].Name,
                    Surname = workers[id].Surname,
                    Age = workers[id].Age,
                    Salary = workers[id].Salary,
                    Projects = workers[id].Projects,
                    Id = workers[id].Id,
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
            for (int i = 0; i < workers.Count; i++)
            {
                if (id == workers[i].Id)
                {
                    return i;
                }
            }
            return -1;
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
                    for (int i = 0; i < dep.Count; i++)
                    {
                        if (dep[i].NumberDep == tempNumberDep)
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
                AddDep(new Department(data[0], DateTime.Parse(data[1]), Convert.ToUInt32(tempNumberDep), initializerList));

                Print($"Депортаменту присвоен номер: {tempNumberDep}"); ;
                Print("добавить еще депортамент? д/н");
                key = Console.ReadKey(true).KeyChar;

            } while (char.ToLower(key) == 'д');
        }
        /// <summary>
        /// поиск сотрудника по ID
        /// </summary>
        /// <param name="i"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public int IdSearch(int i,int d)
        {
            int id;
            id = Convert.ToInt32(dep[i].Id[d]);
            int idSearch = 0;
            for (int q = 0; q < workers.Count; q++)
            {
                if (id == workers[q].Id)
                {
                    idSearch = q;
                }
            }
            return idSearch;
        }
        /// <summary>
        /// сериализация XML
        /// </summary>
        public void XMLRec()
        {
            XElement myCOMPANY = new XElement("COMPANY");

            for (int i = 0; i < dep.Count; i++)
            {
                XElement myDEPARTMENT = new XElement("DEPARTMENT");
                List<string> stringId = new List<string>();
                for (int e = 0; e < dep[i].Id.Count; e++)
                {
                    stringId.Add(Convert.ToString(dep[i].Id[e]));
                }

                string separators = ",";
                string text = string.Join(separators, stringId);            //перобразование масива в строку для хранения в XML

                XElement Dep = new XElement("Name", dep[i].NameDep);
                XElement Data = new XElement("Data", dep[i].Data);
                XElement NumberDep = new XElement("NumberDep", dep[i].NumberDep);
                XElement Id = new XElement("Id", text);

                myDEPARTMENT.Add(Dep);
                myDEPARTMENT.Add(Data);
                myDEPARTMENT.Add(NumberDep);
                myDEPARTMENT.Add(Id);


                int maxId = dep[i].Id[0];
                if (maxId != -1)
                {
                    for (int d = 0; d < dep[i].Id.Count; d++)
                    {
                        int idSearch = IdSearch(i, d);
                        
                        XElement myEMPLOYEE = new XElement("EMPLOYEE");

                        XElement SURNAME = new XElement("SURNAME", workers[idSearch].Surname);
                        XElement NAME = new XElement("NAME", workers[idSearch].Name);
                        XElement AGE = new XElement("AGE", workers[idSearch].Age);
                        XElement PROJECTS = new XElement("PROJECTS", workers[idSearch].Projects);
                        XElement SALARY = new XElement("SALARY", workers[idSearch].Salary);
                        XElement IdEmployee = new XElement("IDEmployee", workers[idSearch].Id);
                        XElement IdDep = new XElement("IdDep", workers[idSearch].IdDep);


                        myEMPLOYEE.Add(SURNAME);
                        myEMPLOYEE.Add(NAME);
                        myEMPLOYEE.Add(AGE);
                        myEMPLOYEE.Add(PROJECTS);
                        myEMPLOYEE.Add(SALARY);
                        myEMPLOYEE.Add(IdEmployee);
                        myEMPLOYEE.Add(IdDep);

                        myDEPARTMENT.Add(myEMPLOYEE);
                    }
                }
                myCOMPANY.Add(myDEPARTMENT);
            }
            Print("Введите имя сохроняемого файла");
            myCOMPANY.Save($"{ExceptionsString()}.xml");
        }
        /// <summary>
        /// десериализация XML
        /// </summary>
        public void XMLRead()
        {
            char key = 'д';
            do
            {
                Print("Введите путь к файлу");
                string path = ExceptionsString();
                bool result = File.Exists(path);
                if (true == result)
                {
                    string xml = File.ReadAllText(path);

                    var dep = XDocument.Parse(xml)
                                       .Descendants("COMPANY")
                                       .Descendants("DEPARTMENT")
                                       .ToList();
                    var employee = XDocument.Parse(xml)
                                         .Descendants("COMPANY")
                                         .Descendants("DEPARTMENT")
                                         .Descendants("EMPLOYEE")
                                         .ToList();
                    int id = 0;
                    foreach (var e in dep)
                    {
                        int tempNumberDep = this.dep.Count;
                        string[] data = new string[2];

                        data[0] = e.Element("Name").Value;
                        data[1] = e.Element("Data").Value;
                        string stringIdEmployee = e.Element("Id").Value;
                        string[] separators = { ",", ".", "!", "?", ";", ":", " " };                                //конвертация строки в масив чисел
                        string[] arrayStringIdEmployee = stringIdEmployee.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        List<int> intIdEmployee = new List<int>();
                        for (int d = 0; d < arrayStringIdEmployee.Length; d++)
                        {
                            intIdEmployee.Add(Convert.ToInt32(arrayStringIdEmployee[d]));                           //запись колекции чисел
                        }
                        List<int> initializerList = new List<int>();
                        initializerList.Add(-1);
                        AddDep(new Department(data[0], DateTime.Parse(data[1]), Convert.ToUInt32(tempNumberDep + 1), initializerList));
                        int idDep = tempNumberDep + 1;

                        if (intIdEmployee[0] != -1)
                        {
                            for (int i = 0; i < intIdEmployee.Count; i++)
                            {
                                string el = employee[i].Element("SURNAME").Value;

                                int index = idDep;
                                //Количество рабочих в базе
                                object idWorker = this.workers.Count;
                                if (idWorker == null)
                                {
                                    this.index = 0;
                                }
                                else
                                {
                                    this.index = Convert.ToInt32(idWorker);
                                }

                                string[] arrayEmployee = new string[5];

                                arrayEmployee[0] = employee[i + id].Element("SURNAME").Value;

                                arrayEmployee[1] = employee[i + id].Element("NAME").Value;

                                arrayEmployee[2] = employee[i + id].Element("AGE").Value;

                                arrayEmployee[3] = employee[i + id].Element("PROJECTS").Value;

                                arrayEmployee[4] = employee[i + id].Element("SALARY").Value;

                                AddWorker(new Worker(arrayEmployee[1], arrayEmployee[0], Convert.ToUInt32(arrayEmployee[2]), Convert.ToUInt32(arrayEmployee[4]), Convert.ToUInt32(arrayEmployee[3]), Convert.ToUInt32(this.index), Convert.ToUInt32(index)));

                                List<int> idList = new List<int>();
                                if (this.dep[index - 1].Id[0] == -1)
                                {
                                    this.dep[index - 1].Id[0] = this.index;
                                }
                                else
                                {
                                    this.dep[index - 1].AddId(this.index);
                                }
                                int countId = this.dep[index - 1].Id.Count;
                                idList.Add(this.index + 1);
                            }
                        }
                        id += intIdEmployee.Count;
                    }
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
        /// серилизация JSON
        /// </summary>
        public void JSONRec()
        {
            JObject company = new JObject();

            JArray departments = new JArray();
            JArray workers = new JArray();

            company["ok"] = true;

            for (int i = 0; i < dep.Count; i++)
            {
                int maxId = dep[i].Id[0];
                JObject department = new JObject();
                department["NameDep"] = dep[i].NameDep;
                department["Data"] = dep[i].Data;
                
                if (maxId != -1)
                {
                    string separators = ",";
                    string text = string.Join(separators, dep[i].Id);
                    department["id_Workers"] = text;
                }
                else
                {
                    department["id_Workers"] = "-";
                }
                
                department["id_Dep"] = dep[i].NumberDep;
                departments.Add(department);
                workers.Clear();
                if (maxId != -1)
                {
                    for (int d = 0; d < dep[i].Id.Count; d++)
                    {
                        int idSearch = IdSearch(i, d);
                        JObject worker = new JObject
                        {
                            ["Name"] = this.workers[idSearch].Name,
                            ["Surname"] = this.workers[idSearch].Surname,
                            ["Age"] = this.workers[idSearch].Age,
                            ["Projeck"] = this.workers[idSearch].Projects,
                            ["Salari"] = this.workers[idSearch].Salary,
                            ["IdDep"] = this.workers[idSearch].IdDep,
                            ["IdEmployee"] = this.workers[idSearch].Id,
                        };
                        workers.Add(worker);
                        department["workers"] = workers;
                    }
                }
            }
            company["company"] = departments;

            string json = company.ToString();

            Print("Введите имя сохроняемого файла");
            File.WriteAllText($"{ExceptionsString()}.json", json);
        }
        /// <summary>
        /// десериализация json
        /// </summary>
        public void JSONRead()
        {
            char key = 'д';
            do
            {
                Print("Введите путь к файлу");
                string path = ExceptionsString();
                bool result = File.Exists(path);

                if (true == result)
                {
                    string json = File.ReadAllText(path);

                    var dep = JObject.Parse(json)["company"].ToArray();

                    int id = 0;
                    foreach (var e in dep)
                    {
                        int tempNumberDep = this.dep.Count;
                        string[] data = new string[2];

                        data[0] = e["NameDep"].ToString();
                        data[1] = e["Data"].ToString();
                        string stringIdEmployee = e["id_Workers"].ToString();
                        string[] separators = { ",", ".", "!", "?", ";", ":", " " };                                //конвертация строки в масив чисел
                        string[] arrayStringIdEmployee = stringIdEmployee.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        List<int> intIdEmployee = new List<int>();
                        for (int d = 0; d < arrayStringIdEmployee.Length; d++)
                        {
                            intIdEmployee.Add(Convert.ToInt32(arrayStringIdEmployee[d]));                           //запись колекции чисел
                        }
                        List<int> initializerList = new List<int>();
                        initializerList.Add(-1);
                        AddDep(new Department(data[0], DateTime.Parse(data[1]), Convert.ToUInt32(tempNumberDep + 1), initializerList));
                        int idDep = tempNumberDep + 1;

                        string worker = Convert.ToString(dep[id]);
                        var employee = JObject.Parse(worker)["workers"].ToArray();

                        if (intIdEmployee[0] != -1)
                        {
                            foreach (var item in employee)
                            {
                                int index = idDep;
                                //Количество рабочих в базе
                                object idWorker = this.workers.Count;
                                if (idWorker == null)
                                {
                                    this.index = 0;
                                }
                                else
                                {
                                    this.index = Convert.ToInt32(idWorker);
                                }

                                string[] arrayEmployee = new string[5];

                                arrayEmployee[0] = item["Surname"].ToString();

                                arrayEmployee[1] = item["Name"].ToString();

                                arrayEmployee[2] = item["Age"].ToString();

                                arrayEmployee[3] = item["Projeck"].ToString();

                                arrayEmployee[4] = item["Salari"].ToString();

                                AddWorker(new Worker(arrayEmployee[1], arrayEmployee[0], Convert.ToUInt32(arrayEmployee[2]), Convert.ToUInt32(arrayEmployee[4]), Convert.ToUInt32(arrayEmployee[3]), Convert.ToUInt32(this.index), Convert.ToUInt32(index)));

                                List<int> idList = new List<int>();
                                if (this.dep[index - 1].Id[0] == -1)
                                {
                                    this.dep[index - 1].Id[0] = this.index;
                                }
                                else
                                {
                                    this.dep[index - 1].AddId(this.index);
                                }
                                int countId = this.dep[index - 1].Id.Count;
                                idList.Add(this.index + 1);
                            }
                        }
                        id++;
                    }
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
            Worker[] employee = new Worker[workers.Count];
            var sortTable = new Worker[1];
            for (int i = 0; i < workers.Count; i++)
            {
                employee[i] = workers[i];
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
            Console.WriteLine(EmployeeTitle());
            for (int i = 0; i < sortTable.Length; i++)
            {
                Console.WriteLine($"{sortTable[i].Surname,30} {sortTable[i].Name,30} {sortTable[i].Salary,5} {sortTable[i].Age,10} {sortTable[i].Projects,15} {sortTable[i].Id,12} {sortTable[i].IdDep,5}");
                Console.WriteLine();
            }
        }
    }
}
