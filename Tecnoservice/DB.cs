using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tecnoservice
{
    public class DB
    {
        private TecnoserviceEntities _context;

        public DB()
        {
            _context = TecnoserviceEntities.GetContext();
        }

        public TecnoserviceEntities GetContext()
        {
            return _context;
        }

        public int getLastID()
        {
            // Получение последнего идентификатора из таблицы Request
            var lastId = _context.Request.OrderByDescending(r => r.ID).FirstOrDefault()?.ID;

            // Если lastId равен null, получу 0, иначе значение lastId
            return lastId ?? 0;
        }

        public void addToTable(Request request)
        {
            _context.Request.Add(request);
            _context.SaveChanges();
        }
    }

      
}
