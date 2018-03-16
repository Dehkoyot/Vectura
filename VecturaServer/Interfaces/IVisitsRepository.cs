using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VecturaServer.Interfaces
{
        public interface IVisitsRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T Get(int id);

            IEnumerable<T> Find(Func<T, bool> predicate);

            void Create(T item);
            void Update(T item);
            void Delete(int id);
        }
}
