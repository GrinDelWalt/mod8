using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod8
{
    
    class Menu
    {
        public void Print(string a)
        {
            Console.WriteLine(a);
        }
        public void MenuCompany()
        {
            Repository rep = new Repository();
            char key = 'д';
            Print("считать данные с XML или JSON? д/н");
            key = Console.ReadKey(true).KeyChar;
            if (char.ToLower(key) == 'д')
            {
                Print("Десерелизовать XML - 1");
                Print("Десерелизовать JSON - 2");
                int index = Convert.ToInt32(rep.ExceptionsInt());
                switch (index)
                {
                    case 1:
                        rep.XMLRead();
                        break;
                    case 2:
                        rep.JSONRead();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Print("Создание первого департамента");
                rep.RecordDep();
                Print("Добавления сотрудников");
                rep.RecordEmployee();
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

                int index = Convert.ToInt32(rep.ExceptionsInt());
                switch (index)
                {
                    case 1:
                        rep.RecordDep();
                        break;
                    case 2:
                        Print("Существующие департаменты");
                        rep.PrintDepartment();
                        rep.RecordEmployee();
                        break;
                    case 3:
                        rep.PrintEmployee();
                        Print("ID увольняемого сотрудника");
                        rep.DismissEmployee();
                        break;
                    case 4:
                        rep.PrintEmployee();
                        Print("Выберите ID редактируемого сотрудника:");
                        rep.EditEmployee(Convert.ToInt32(Console.Read()));
                        break;
                    case 5:
                        rep.XMLRec();
                        break;
                    case 6:
                        rep.JSONRec();
                        break;
                    case 7:
                        rep.XMLRead();
                        break;
                    case 8:
                        rep.JSONRead();
                        break;
                    case 9:
                        rep.PrintDepartment();
                        rep.PrintEmployee();
                        break;
                    case 10:
                        Print("ID департамента:");
                        rep.EditDep(Convert.ToInt32(Console.Read()));
                        break;
                    case 11:
                        rep.DeleteDep();
                        break;
                    case 12:
                        rep.TransferEmployee();
                        break;
                    case 13:
                        rep.SortWorker();
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
