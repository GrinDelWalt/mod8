using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace mod8
{
    class Menu
    {
        Repository rep = new Repository();

        Functionality func = new Functionality();
        public void Print(string a)
        {
            Console.WriteLine(a);
        }
        public void MenuCompany()
        {
            
            char key = 'д';
            Print("считать данные с XML или JSON? д/н");
            key = Console.ReadKey(true).KeyChar;
            if (char.ToLower(key) == 'д')
            {
                Print("Десерелизовать XML - 1");
                Print("Десерелизовать JSON - 2");
                int index = Convert.ToInt32(func.ExceptionsInt());
                switch (index)
                {
                    case 1:
                        func.XMLReadControl();
                        break;
                    case 2:
                        func.JSONReadControl();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Print("Создание первого департамента");
                func.RecordDep();
                Print("Добавления сотрудников");
                func.RecordEmployee();
            }

            do
            {
                Print("Действия в отделе кадров производства");
                Print("");
                Print("Добавить департамент - 1");
                Print("Добавить сотрудника - 2");
                Print("Уволить сотрудника - 3");
                Print("Редактировать данные сотрудника - 4");
                Print("Редактировать департамент - 10");
                Print("Расформировать департамент - 11");
                Print("Перевезти сотрудника - 12");
                Print("Сортировка сотруднирков - 13");
                Print("");
                Print("Работа с XML и JSON");
                Print("");
                Print("Серелизовать XML - 5");
                Print("Серелизовать JSON - 6");
                Print("Десерелизовать XML - 7");
                Print("Десерелизовать JSON - 8");
                Print("Печать - 9");

                int index = Convert.ToInt32(func.ExceptionsInt());
                switch (index)
                {
                    case 1:
                        func.RecordDep();
                        break;
                    case 2:
                        Print("Существующие департаменты");
                        func.PrintDepartment();
                        func.RecordEmployee();
                        break;
                    case 3:
                        func.PrintEmployee();
                        Print("ID увольняемого сотрудника");
                        func.DismissEmployee();
                        break;
                    case 4:
                        func.PrintEmployee();
                        Print("Выберите ID редактируемого сотрудника:");
                        func.EditEmployee(Convert.ToInt32(Console.Read()));
                        break;
                    case 5:
                        Print("Введите имя сохроняемого файла");

                        rep.XMLRec(func.ExceptionsString());
                        break;
                    case 6:
                        Print("Введите имя сохроняемого файла");
                        rep.JSONRec(func.ExceptionsString());
                        break;

                    case 7:
                        func.XMLReadControl();
                        break;

                    case 8:
                        func.JSONReadControl();
                        break;
                    case 9:
                        func.PrintDepartment();
                        func.PrintEmployee();
                        break;
                    case 10:
                        Print("ID департамента:");
                        func.EditDep(Convert.ToInt32(Console.Read()));
                        break;
                    case 11:
                        func.DeleteDep();
                        break;
                    case 12:
                        func.TransferEmployee();
                        break;
                    case 13:
                        func.SortWorker();
                        break;
                    default:
                        break;
                }

                Print("Открыть меню? д/н");
                key = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(key) == 'д');
        }
    }
}
