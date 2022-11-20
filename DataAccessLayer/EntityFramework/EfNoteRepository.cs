using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfNoteRepository : GenericRepository<Note>, INoteDal
    {
        public Note GetNoteByUserId(int id, int userId)
        {
            using (var context = new Context())
            {
                //return context.Notes.Where(x=>x.NoteId == id).Where(y=>y.AppUserId==id).FirstOrDefault();
                return context.Notes.Where(x => x.NoteId == id && x.AppUserId== userId).FirstOrDefault();
            }
        }
    }
}
