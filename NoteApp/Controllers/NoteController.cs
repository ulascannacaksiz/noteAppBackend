using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var values = _noteService.GetNoteByUserId(id, user.Id);
            return Ok(values);
        }
        //[HttpPut]
        //[Route("[controller]/[action]/{id}")]
        //public IActionResult Notes()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Notes([FromBody] NoteAddViewModel p)
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
            return Ok();
        }
    }
}
