using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using VecturaServer.Enteties;
using VecturaServer.Interfaces;
using VecturaServer.Models;

namespace VecturaServer.Repositories
{

        public class VisitRepository : IVisitsRepository<VisitModel>
        {
            private VisitsContext _context;
            private int _nextId;

            public VisitRepository(VisitsContext context)
            {
                _context = context;
                _nextId = _context.Visits.Count();
            }

            public IEnumerable<VisitModel> GetAll()
            {
                return _context.Visits;
            }

            public VisitModel Get(int id)
            {
                return _context.Visits.Find(id);
            }

            public IEnumerable<VisitModel> Find(Func<VisitModel, bool> predicate)
            {
                return _context.Visits.Where(predicate).ToList();
            }

            public void Create(VisitModel Visit)
            {
                _context.Visits.Add(Visit);
                _context.SaveChanges();
            }

            public void Update(VisitModel Visit)
            {
                _context.Entry(Visit).State = EntityState.Modified;
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
               VisitModel Visit = _context.Visits.Find(id);
                if (Visit != null)
                    _context.Visits.Remove(Visit);
                _context.SaveChanges();
            }
        }   
}