using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod8
{
    public struct Worker
    {
        #region Конструкторы

       
        public Worker(string Name, string Surname, uint Age, uint Salary, uint Projects, uint Id, uint IdDep)
        {
            this.surname = Surname;
            this.name = Name;
            this.age = Age;
            this.salary = Salary;
            this.projects = Projects;
            this.id = Id;
            this.idDep = IdDep;
        }
        #endregion

        #region Свойства
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get { return this.surname; } set { this.surname = value; } }

        /// <summary>
        /// Колличество проктво у сотрудника
        /// </summary>
        public uint Projects { get { return this.projects; } set { this.projects = value; } }

        /// <summary>
        /// ЗП
        /// </summary>
        public uint Salary { get { return this.salary; } set { this.salary = value; } }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }
       
        /// <summary>
        /// Возраст
        /// </summary>
        public uint Age { get { return this.age; } set { this.age = value; } }

        /// <summary>
        /// Id сотридника
        /// </summary>
        /// <returns></returns>
        public uint Id { get { return this.id; } set { this.id = value; } }
        
        /// <summary>
        /// Id департамента в котором находится сотрудник
        /// </summary>
        public uint IdDep { get { return this.idDep; } set { this.idDep = value; } }
      
        #endregion

        public string Print()
        {
            return $"{this.name,30} {this.surname,30} {this.salary,5} {this.age,10} {this.projects,15} {this.id, 12} {this.IdDep,6}";
        }
        #region Поля

        //Фамилия
        private string surname;

        //Колличество проектов
        private uint projects;

        //ЗП
        private uint salary;

        //Имя
        private string name;

        //Возраст
        private uint age;

        //Id сотрудника
        private uint id;

        //Id
        private uint idDep;

       
        #endregion
    }
}
/// Создать прототип информационной системы, в которой есть возможност работать со структурой организации
/// В структуре присутствуют департаменты и сотрудники
/// Каждый департамент может содержать не более 1_000_000 сотрудников.
/// У каждого департамента есть поля: наименование, дата создания,
/// количество сотрудников числящихся в нём 
/// (можно добавить свои пожелания)
/// 
/// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
/// уникальный номер, размер оплаты труда, количество закрепленным за ним.
///
/// В данной информаиционной системе должна быть возможность 
/// - импорта и экспорта всей информации в xml и json
/// Добавление, удаление, редактирование сотрудников и департаментов
/// 
/// * Реализовать возможность упорядочивания сотрудников в рамках одно департамента 
/// по нескольким полям, например возрасту и оплате труда
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
/// 