using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface INoteService:IGenericService<Note>
    {
        Note GetNoteByUserId(int id, int userId);
        List<Note> GetAllNotesByUser(int userId);
    }
}
