using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ProjectTrainingToiecs.Common.Enum;
using ProjectTrainingToiecs.Models;

namespace ProjectTrainingToiecs.Controllers
{
    public class TestDetailController : Controller
    {
        private readonly DbTrainingToiecsContext _context;
        private readonly int[] types = { (int)ETypeDtetailQuesion.TalkShort, (int)ETypeDtetailQuesion.QAndA, (int)ETypeDtetailQuesion.Conversation,
        (int)ETypeDtetailQuesion.CompleteParagraph,(int)ETypeDtetailQuesion.ReadingComprehen
        };
        public TestDetailController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var lst = (from i in _context.TestDetails
                       join c in _context.Documents
                       on i.DocumentId equals c.Id
                       where i.RecordStatusId == (int)ERecordStatus.Actived
                       select new TestDetail()
                       {
                           Id = i.Id,
                           AnswerA = i.AnswerA,
                           AnswerB = i.AnswerB,
                           AnswerC = i.AnswerC,
                           AnswerD = i.AnswerD,
                           CorrectAnswer = i.CorrectAnswer,
                           NameLesson = c.Title,
                           Description = i.Description,
                           Title = i.Title,
                           LinkImage = i.LinkImage,
                           Audio = i.Audio
                       }).ToList();
            
            var order = 0;
            lst.ForEach(x =>
            {
                x.Order = order + 1;
                x.Description = !string.IsNullOrEmpty(x.Description) ? x.Description.Replace("\n", " ") : x.Description;
                x.LinkImage = String.IsNullOrEmpty(x.LinkImage) ? "/upload_files/ImageNone.jpg" : x.LinkImage;
                order++;
            });
            ViewBag.lst = lst;
            return View();
        }
        public IActionResult Create(int? id = 0)
        {
            var documents = _context.Documents.ToList();
            var model = new TestDetail();
            if (id > 0)
            {
                model = _context.TestDetails.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = model;
            ViewBag.document = documents;
            return View();
        }
        public JsonResult CreateTestDetail(TestDetail model)
        {
            //check order
            if(model.ItemOrder == 0 && model.Id == 0 && !types.Contains(model.TypeDetail))
            {
                model.ItemOrder = _context.TestDetails.Any(x=>x.UnitId == model.UnitId && x.RecordStatusId == (int)ERecordStatus.Actived) ?
                    _context.TestDetails.Where(x => x.UnitId == model.UnitId && x.RecordStatusId == (int)ERecordStatus.Actived).Max(x => x.ItemOrder) + 1 : 1;
            }
            if (model.Id > 0)
            {
                var quesionOld = _context.TestDetails.FirstOrDefault(x => x.Id == model.Id);
                quesionOld.Scription = model.Scription;
                quesionOld.LinkImage = model.LinkImage;
                quesionOld.Audio = model.Audio;
                quesionOld.Description = model.Description;
                quesionOld.CorrectAnswer = model.CorrectAnswer;
                quesionOld.AnswerA = model.AnswerA;
                quesionOld.AnswerB = model.AnswerB;
                quesionOld.AnswerC = model.AnswerC;
                quesionOld.AnswerD = model.AnswerD;
                quesionOld.Question = model.Question;
                _context.Update(quesionOld);
                _context.SaveChanges();
            }
            else
            {
                _context.Add(model);
                _context.SaveChanges();
                if (types.Contains(model.TypeDetail))
                {
                    var order = 1;
                    foreach(var i in model.QuestionsDetail)
                    {
                        i.IdQS = model.Id;
                        i.ItemOrder = order;
                        _context.Add(i);
                        _context.SaveChanges();
                        order++;
                    }
                }
            }
            
            return Json(new { data = model.Id });
        }
        [HttpPost]
        public async Task<ActionResult> UploadFiles(IList<IFormFile> files)
        {
            string fileName = null;

            foreach (IFormFile source in files)
            {
                // Get original file name to get the extension from it.
                string orgFileName = string.Empty;

                // Create a new file name to avoid existing files on the server with the same names.
                fileName = DateTime.Now.ToFileTime() + Path.GetExtension(orgFileName);

                string fullPath = string.Format("wwwroot/upload_files/{0}_{1}",fileName,source.FileName);

                // Create the directory.
                Directory.CreateDirectory(Directory.GetParent(fullPath).FullName);

                // Save the file to the server.
                await using FileStream output = System.IO.File.Create(fullPath);
                await source.CopyToAsync(output);
                fileName = fullPath.Replace("wwwroot","");
            }

            var response = new { FileName =  fileName};

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult> UploadAudio(IList<IFormFile> audios)
        {
            string fileName = null;

            foreach (IFormFile source in audios)
            {
                // Get original file name to get the extension from it.
                string orgFileName = string.Empty;

                // Create a new file name to avoid existing files on the server with the same names.
                fileName = DateTime.Now.ToFileTime() + Path.GetExtension(orgFileName);

                string fullPath = string.Format("wwwroot/upload_audio/{0}_{1}", fileName, source.FileName);

                // Create the directory.
                Directory.CreateDirectory(Directory.GetParent(fullPath).FullName);

                // Save the file to the server.
                await using FileStream output = System.IO.File.Create(fullPath);
                await source.CopyToAsync(output);
                var url = this.Request.Host;
                fileName = fullPath.Replace("wwwroot", String.Format("https://{0}",url));
            }

            var response = new { FileName = fileName };

            return Ok(response);
        }
        public JsonResult DeleteTest(int id)
        {
            var val = false;
            var user = _context.TestDetails.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.RecordStatusId = (int)ERecordStatus.Deleted;
                _context.Update(user);
                _context.SaveChanges();
                val = true;
            }
            return Json(new { Data = val });
        }
        public JsonResult DeleteTestDetail(int id)
        {
            var val = false;
            var user = _context.Questions.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.RecordStatusId = (int)ERecordStatus.Deleted;
                _context.Update(user);
                _context.SaveChanges();
                val = true;
            }
            return Json(new { Data = val });
        }
        public JsonResult UpdateInfoQuesion(Question model)
        {
            if(model.Id > 0)
            {
                var quesionOld= _context.Questions.FirstOrDefault(x => x.Id == model.Id);
                quesionOld.CorrectAnswer = model.CorrectAnswer;
                quesionOld.QS = model.QS;
                quesionOld.QSA = model.QSA;
                quesionOld.QSB = model.QSB;
                quesionOld.QSC = model.QSC;
                quesionOld.QSD = model.QSD;
                quesionOld.Description = model.Description;
                _context.Update(quesionOld);
            }
            else
            {
                _context.Add(model);
            }
            _context.SaveChanges();
            return Json(new { Data = model.Id > 0 });
        }
    }
}
