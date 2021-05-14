using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace mod8
{
    class Department
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="NameDep"></param>
        /// <param name="Data"></param>
        /// <param name="NumberDep"></param>
        public Department(string NameDep, DateTime Data, uint NumberDep, List<uint> Id)
        {
            this.NumberDep = NumberDep;
            this.NameDep = NameDep;
            this.Data = Data;
            this.Id = Id;
            
        }

        /// <summary>
        /// Номер депортамента
        /// </summary>
        public uint NumberDep { get; set; }

        /// <summary>
        /// Название депортамента
        /// </summary>
        public string NameDep { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// ID сотрудников относящихся к данному депортаменту
        /// </summary>
        public List<uint> Id { get; set; }

        public void AddId(uint id)
        {
            this.Id.Add(id);
        }

        public void PrintList()
        {
            int count = this.Id.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(Id[i]);
            }
        }
        
        /// <summary>
        /// Печать данных
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"{this.NameDep,30} { this.NumberDep,30} {this.Data,35}";
        }
       

        public List<Worker> workers;

        uint index;

        public Department()
        {
            this.workers = new List<Worker>();
            int id = workers.Count;
            this.index = workers[id - 1].Id;
        }
        
       
        
        
        


       


    }
}
