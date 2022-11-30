using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    [EnableCors("AllowOrigin")]
    public class NoteController : Controller
    {
        INoteService _noteService;
        private readonly UserManager<AppUser> _userManager;

        public NoteController(INoteService noteService, UserManager<AppUser> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Notes()
        {
            //User.Identity.Name
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var values = _noteService.TGetList();
            var values = _noteService.GetAllNotesByUser(user.Id);
            return Ok(values);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Notes(int id)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var values = _noteService.GetNoteByUserId(id, user.Id);
                return Ok(values);
            }catch (Exception ex)
            {
                return BadRequest("Not bulunamadı");
            }

        }
        [HttpPut]
       // [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> Notes (NoteUpdateViewModel p)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var notes = _noteService.GetNoteByUserId(p.Id, user.Id);
                notes.NoteTitle = p.Title;
                notes.NoteDescription = p.Description;
                notes.EditedDate = DateTime.Now;
                notes.Status = true;
                _noteService.TUpdate(notes);
                return Ok("Not başarılı bir şekilde güncellendi");
            }catch (Exception ex)
            {
                return BadRequest("Not güncellenirken hata oluştu");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Notes([FromBody] NoteAddViewModel p)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                Note notes = new Note();
                notes.AppUserId = user.Id;
                notes.NoteTitle = p.Title;
                notes.NoteDescription = p.Description;
                notes.CreateDate = DateTime.Now;
                notes.EditedDate = DateTime.Now;
                notes.Status = true;
                _noteService.TAdd(notes);
                return Ok("Not başarılı bir şekilde eklendi");
            }catch (Exception ex)
            {
                  return BadRequest("Not not eklenirken hata oluştu");
            }

        }
    }
}
