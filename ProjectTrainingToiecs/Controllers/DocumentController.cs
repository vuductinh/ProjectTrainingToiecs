using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using NuGet.Protocol;
using ProjectTrainingToiecs.Common.Enum;
using ProjectTrainingToiecs.Models;
using ProjectTrainingToiecs.Service.Service.Impl;
using ProjectTrainingToiecs.Service.Service;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ProjectTrainingToiecs.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DbTrainingToiecsContext _context;
        private readonly int[] types = { (int)ETypeDtetailQuesion.TalkShort , (int)ETypeDtetailQuesion.QAndA , (int)ETypeDtetailQuesion.Conversation,
            (int)ETypeDtetailQuesion.CompleteParagraph, (int)ETypeDtetailQuesion.ReadingComprehen };
        public DocumentController(DbTrainingToiecsContext context)
        {
            _context = context;
        }
        public IActionResult Index(int idUnit)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "User")
            {
                return RedirectToAction("ClientIndex", "Document", new { idUnit = idUnit });
            }
            var unit = _context.Units.Any(x => x.Id == idUnit) ? _context.Units.FirstOrDefault(x => x.Id == idUnit) : new Units();
            var course = _context.Course.Any(x => x.Id == unit.CourseId) ? _context.Course.FirstOrDefault(x => x.Id == unit.CourseId) : new Course();
            
            ViewBag.idUnit = unit.Id;
            ViewBag.link = string.Format("<i class=\"fa fa-home\" aria-hidden=\"true\"></i><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> Course " +
                "<i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> {0} " +
                "<i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> <b>{1}</b>", course.Name, unit.Name);
            ViewBag.count = unit.Count;
            return View();
        }
        public IActionResult ClientIndex(int idUnit)
        {
            if(idUnit == 0)
            {
                return RedirectToAction("TestFinalIndex", "Document");
            }
            var unit = _context.Units.Any(x => x.Id == idUnit) ? _context.Units.FirstOrDefault(x => x.Id == idUnit) : new Units();
            var course = _context.Course.FirstOrDefault(x => x.Id == unit.CourseId);

            ViewBag.idUnit = unit.Id;
            ViewBag.link = string.Format("<i class=\"fa fa-home\" aria-hidden=\"true\"></i><i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i><span onclick=\"onDircourse("+unit.CourseId+")\"> Course</span> " +
                "<i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> {0} " +
                "<i class=\"fa fa-chevron-right\" aria-hidden=\"true\"></i> <b>{1}</b>", course.Name, unit.Name);
            ViewBag.count = unit.Count;
            return View();
        }
        public JsonResult GetDocument(FilterModel filter)
        {
            var url = string.Format("{0}/{1}", HttpContext.Request.GetDisplayUrl(), filter.UnitId);
            var correct = 0;
            var countQs = 0;
            SaveAuditLog(url);
            var lst = _context.TestDetails.Where(x => x.RecordStatusId == (int)ERecordStatus.Actived 
            && x.UnitId == filter.UnitId && x.Type == filter.TypeQuesion
            && x.TypeDetail == filter.TypeQuesionDetail).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch) && lst.Any())
            {
                if(types.Contains(filter.TypeQuesionDetail))
                {
                    lst = lst.Where(x => x.Scription.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
                }
                else
                {
                    if(filter.TypeQuesionDetail == (int)ETypeDtetailQuesion.CompleteSentence ||
                        filter.TypeQuesionDetail == (int)ETypeDtetailQuesion.ReadingComprehen ||
                        filter.TypeQuesionDetail == (int)ETypeDtetailQuesion.CompleteParagraph)
                    {
                        lst = lst.Where(x => x.Scription.ToLower().Contains(filter.TextSearch.ToLower())
                              || x.Question.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
                    }
                    else
                    {
                        lst = lst.Where(x => x.Description.ToLower().Contains(filter.TextSearch.ToLower())
                            || x.Scription.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
                    }
                }
                
            }
            var role = HttpContext.Session.GetString("Role");
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var markeds = new Dictionary<int, StatusStudyModel>();
            var markedsDetail = new Dictionary<int, StatusStudyModel>();
            var testIds = lst.Select(x => x.Id);

            if(role == "User" && testIds.Any() &&  !types.Contains(filter.TypeQuesionDetail))
            {
                markeds = _context.StatusStudies.Where(x => x.UserId == userId && testIds.Contains(x.IdTest)).
                    ToDictionary(x => x.IdTest, x => new StatusStudyModel() { Option = x.Option, Result = x.Result });
                correct = markeds.Where(x => x.Value.Result).Count();
                
            }
            var order = 0;
            lst.ForEach(x =>
            {
                if(role == "User" && markeds.Any())
                {
                    x.Marked = markeds.ContainsKey(x.Id);
                    x.Option = markeds.ContainsKey(x.Id) ? markeds[x.Id].Option : "";
                }
                x.QuestionsDetail = new List<Question>();
                if (types.Contains(filter.TypeQuesionDetail))
                {
                    x.QuestionsDetail = _context.Questions.Where(xx=>xx.IdQS == x.Id && xx.RecordStatusId == (int)ERecordStatus.Actived).ToList();
                    var idDetail = x.QuestionsDetail.Select(xx => xx.Id);
                    if (role == "User" && idDetail.Any())
                    {
                        markedsDetail = _context.StatusStudies.Where(xx => xx.UserId == userId && idDetail.Contains(xx.IdTest)).
                            ToDictionary(m => m.IdTest, m => new StatusStudyModel() { Option = m.Option, Result = m.Result });
                        if (markedsDetail.Any())
                        {
                            x.Marked = true;
                            x.QuestionsDetail.ForEach(xx =>
                            {
                                xx.Marked = markedsDetail.ContainsKey(xx.Id);
                                xx.Option = markedsDetail.ContainsKey(xx.Id) ? markedsDetail[xx.Id].Option : "";
                            });
                        }

                        countQs = countQs + x.QuestionsDetail.Count();
                    }
                    var itemOrder = 1;
                    if (x.QuestionsDetail.Any())
                    {
                        x.QuestionsDetail.ForEach(x =>
                        {
                            x.ItemOrder = itemOrder;
                            itemOrder++;
                        });
                    }
                }
                x.Order = order + 1;
                order++;
            });
            
            return Json(new {data = lst, correct = correct , countQs = countQs > 0 ? countQs : lst.Count()});
        }
        public IActionResult TestFinalIndex()
        {
            return View();
        }
        public IActionResult Create(int? id = 0)
        {
            var lessons = _context.Lesson.ToList();
            var model = new ProjectTrainingToiecs.Models.Document();
            if(id > 0)
            {
                model = _context.Documents.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.model = model;
            ViewBag.lesson = lessons;
            return View();
        }
        public JsonResult CreateDocument(ProjectTrainingToiecs.Models.Document model)
        {
            
            if(model.Id > 0)
            {
                _context.Update(model);
            }
            else
            {
                _context.Add(model);
            }
            _context.SaveChanges();
            return Json(new { data = model.Id });
        }
        public JsonResult DeleteDocument(int id)
        {
            var val = false;
            var user = _context.Documents.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                user.RecordStatusId = (int)ERecordStatus.Deleted;
                _context.Update(user);
                _context.SaveChanges();
                val = true;
            }
            return Json(new { Data = val });
        }
        public JsonResult GetDataTestFinal()
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var courseId = _context.Users.FirstOrDefault(x => x.Id == userId).TypeCourse;
            var lstTest = new List<TestDetail>();
            var unitIds = _context.Units.Where(x => x.CourseId == courseId 
                        && x.RecordStatusId == (int)ERecordStatus.Actived).Select(x => x.Id);
            var part1 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
            && x.TypeDetail == (int)ETypeDtetailQuesion.ImageDescription && x.RecordStatusId == (int)ERecordStatus.Actived).
            OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(6).ToList();
            lstTest.AddRange(part1);
            var part2 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
            && x.TypeDetail == (int)ETypeDtetailQuesion.QAndA && x.RecordStatusId == (int)ERecordStatus.Actived).
            OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(1).ToList();

            part2.ForEach(x =>
            {
                x.QuestionsDetail = _context.Questions.Where(xx => xx.RecordStatusId == (int)ERecordStatus.Actived &&
                xx.IdQS == x.Id).ToList();
            });
            lstTest.AddRange(part2);
            var part3 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
            && x.TypeDetail == (int)ETypeDtetailQuesion.Conversation && x.RecordStatusId == (int)ERecordStatus.Actived).
            OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(5).ToList();

            part3.ForEach(x =>
            {
                x.QuestionsDetail = _context.Questions.Where(xx => xx.RecordStatusId == (int)ERecordStatus.Actived &&
                xx.IdQS == x.Id).ToList();
            });
            lstTest.AddRange(part3);
            var part4 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
           && x.TypeDetail == (int)ETypeDtetailQuesion.TalkShort && x.RecordStatusId == (int)ERecordStatus.Actived).
           OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(5).ToList();

            part4.ForEach(x =>
            {
                x.QuestionsDetail = _context.Questions.Where(xx => xx.RecordStatusId == (int)ERecordStatus.Actived &&
                xx.IdQS == x.Id).ToList();
            });
            lstTest.AddRange(part4);
            var part5 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
            && x.TypeDetail == (int)ETypeDtetailQuesion.CompleteSentence && x.RecordStatusId == (int)ERecordStatus.Actived).
            OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(30).ToList();
            lstTest.AddRange(part5);
            var part6 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
          && x.TypeDetail == (int)ETypeDtetailQuesion.CompleteParagraph && x.RecordStatusId == (int)ERecordStatus.Actived).
          OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(4).ToList();

            part6.ForEach(x =>
            {
                x.QuestionsDetail = _context.Questions.Where(xx => xx.RecordStatusId == (int)ERecordStatus.Actived &&
                xx.IdQS == x.Id).ToList();
            });
            lstTest.AddRange(part6);
            var part7 = _context.TestDetails.Where(x => unitIds.Contains(x.UnitId)
          && x.TypeDetail == (int)ETypeDtetailQuesion.ReadingComprehen && x.RecordStatusId == (int)ERecordStatus.Actived).
          OrderBy(t => Guid.NewGuid()).AsNoTracking().Take(4).ToList();

            part7.ForEach(x =>
            {
                x.QuestionsDetail = _context.Questions.Where(xx => xx.RecordStatusId == (int)ERecordStatus.Actived &&
                xx.IdQS == x.Id).ToList();
            });
            lstTest.AddRange(part7);
            var order = 0;
            lstTest.ForEach(x =>
            {
                x.ItemOrder = order + 1;
                if (x.QuestionsDetail != null)
                {
                    x.QuestionsDetail.ForEach(xx =>
                    {
                        xx.ItemOrder = order + 1;
                        order++;
                    });
                }
                else
                {
                    x.ItemOrder = order + 1;
                    order++;
                }
                
            });
            return Json( new { data = lstTest, count = lstTest.Any() ? lstTest.Max(x=>x.ItemOrder) : 0 });
        }
        private void SaveAuditLog(string url)
        {
            ICommonService service = new AppCommonService();
            //get Ip
            var ip = service.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet);
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var audit = new AuditLog()
            {
                UserId = userId,
                AdressAccess = ip,
                PageAccess = url,
                CreatedAt = DateTime.Now
            };
            _context.Add(audit);
            _context.SaveChanges();
        }
    }
}
