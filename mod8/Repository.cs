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
        public int index;
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
        public void XMLRec(string path)
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
            
            myCOMPANY.Save($"{path}.xml");
        }
        /// <summary>
        /// десериализация XML
        /// </summary>
        public void XMLRead(string path)
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
        /// <summary>
        /// серилизация JSON
        /// </summary>
        public void JSONRec(string path)
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

            File.WriteAllText($"{path}.json", json);
        }
        /// <summary>
        /// десериализация json
        /// </summary>
        public void JSONRead(string path)
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
        public int SearchEmployeeRreeId()
        {
            bool result = false;
            List<int> idWork = new List<int>();

            for (int i = 0; i < workers.Count; i++)
            {
                idWork.Add(Convert.ToInt32(workers[i].Id));
            }
            for (int i = 0; i < index; i++)
            {
                for (int d = 0; d < idWork.Count; d++)
                {
                    if (i == idWork[d])
                    {
                        result = true;
                        break;
                    }
                }
                if (result == false)
                {
                    return i;
                }
                result = false;
            }
            return index;
        }
        
    }
}
