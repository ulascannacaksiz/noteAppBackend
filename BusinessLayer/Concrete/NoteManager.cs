using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NoteManager : INoteService
    {
        private  INoteDal _noteDal;

        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        public List<Note> GetAllNotesByUser(int userId)
        {
            return _noteDal.GetListFilter(x=>x.AppUserId ==userId);
        }

        public Note GetNoteByUserId(int id, int userId)
        {
           return _noteDal.GetNoteByUserId(id, userId);
        }

        public void TAdd(Note t)
        {
            _noteDal.Insert(t);
        }

        public void TDelete(Note t)
        {
            throw new NotImplementedException();
        }

        public Note TGetById(int id)
        {
            return _noteDal.GetById(id);
        }

        public List<Note> TGetList()
        {
            throw new NotImplementedException();
            
            //return _noteDal.GetList();
        }

        public void TUpdate(Note t)
        {
            _noteDal.Update(t);
        }
    }
}
