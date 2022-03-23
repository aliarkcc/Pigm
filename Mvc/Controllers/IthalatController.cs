using ClosedXML.Excel;
using DataAccess.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace Mvc.Controllers
{
    public class IthalatController : Controller
    {
        private readonly HttpClient _httpClient;
        private string url = "http://localhost:54694/api/";

        public IthalatController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(string currentFilter, string searching, int? page)
        {
            if (searching != null)
            {
                page = 1;
            }
            else
            {
                searching = currentFilter;
            }
            ViewBag.CurrentFilter = searching;

            var followList = await _httpClient.GetFromJsonAsync<ApiListResponseModel<FollowList>>(url + "FollowLists/GetList");
            followList.data = followList.data.Where(x => x.ImportExportId == 2).ToList();
            if (!string.IsNullOrEmpty(searching))
            {
                followList.data = followList.data.Where(x => x.InvoiceNo.Contains(searching)).ToList();
            }
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            return View(followList.data.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(FollowList followList)
        {
            followList.ImportExportId = 2;
            followList.CompanyId = 1;
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url + "FollowLists/Add", followList);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Ithalat");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponseModel<FollowList>>(url + "FollowLists/GetById/" + id);
            if (result.success)
            {
                return View(result.data);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> Update(FollowList followList)
        {
            followList.ImportExportId = 2;
            followList.CompanyId = 1;
            HttpResponseMessage responseMessage = await _httpClient.PutAsJsonAsync(url + "FollowLists/Update", followList);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Ithalat");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ApiResponseModel<FollowList>>(url + "FollowLists/GetById/" + id);
            if (result.success)
            {
                return View(result.data);
            }
            return BadRequest();
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(FollowList followList)
        {
            var isDelete = await _httpClient.DeleteAsync(url + "FollowLists/Delete/" + followList.Id);
            if (isDelete.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Ithalat");
            }
            return BadRequest();
        }
        //[HttpGet]
        //public IActionResult ExportExcel()
        //{
        //    using (var workBook = new XLWorkbook())
        //    {
        //        var workSheet = workBook.Worksheets.Add("Ihracat");
        //        workSheet.Cell(1, 1).Value = "Fatura Numarası";
        //        workSheet.Cell(1, 2).Value = "Segment";
        //        workSheet.Cell(1, 3).Value = "Seri Numarası";
        //        workSheet.Cell(1, 4).Value = "Kuyu";
        //        workSheet.Cell(1, 5).Value = "Pigm Numarası";
        //        workSheet.Cell(1, 6).Value = "Dilekçe Numarası";
        //        workSheet.Cell(1, 7).Value = "Kullanılmış mı";
        //        workSheet.Cell(1, 8).Value = "Ön Dizin Durumu";
        //        workSheet.Cell(1, 9).Value = "Açıklama";
        //        workSheet.Cell(1, 10).Value = "Başvuru Tarihi";
        //        workSheet.Cell(1, 11).Value = "Tps Numarası";
        //        workSheet.Cell(1, 12).Value = "Belge Numarası";
        //        workSheet.Cell(1, 13).Value = "Kabul Tarihi";
        //        int blogRowCount = 2;
        //        foreach (var item in _followListService.GetAll().Data)
        //        {
        //            workSheet.Cell(blogRowCount, 1).Value = item.InvoiceNo;
        //            workSheet.Cell(blogRowCount, 2).Value = item.Segment;
        //            workSheet.Cell(blogRowCount, 3).Value = item.SerialNo;
        //            workSheet.Cell(blogRowCount, 4).Value = item.Well;
        //            workSheet.Cell(blogRowCount, 5).Value = item.PigmNo;
        //            workSheet.Cell(blogRowCount, 6).Value = item.PetitionNo;
        //            workSheet.Cell(blogRowCount, 7).Value = item.Used;
        //            workSheet.Cell(blogRowCount, 8).Value = item.FrontIndexStatus;
        //            workSheet.Cell(blogRowCount, 9).Value = item.Explanation;
        //            workSheet.Cell(blogRowCount, 10).Value = item.RecourseDate;
        //            workSheet.Cell(blogRowCount, 11).Value = item.TpsNo;
        //            workSheet.Cell(blogRowCount, 12).Value = item.DocumentNo;
        //            workSheet.Cell(blogRowCount, 13).Value = item.RatificationDate;
        //            blogRowCount++;
        //        }
        //        using (var stream =new MemoryStream())
        //        {
        //            workBook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ihracat.xlsx");
        //        }
        //    }

        //}
        [HttpGet]
        public FileResult ExportExcel()
        {
            using (Contexts db = new Contexts())
            {
                DataTable dt = new DataTable("Grid");
                dt.Columns.AddRange(new DataColumn[13]
                    {
                    new DataColumn("Fatura Numarası"),
                    new DataColumn("Segment"),
                    new DataColumn("Seri Numarası"),
                    new DataColumn("Kuyu"),
                    new DataColumn("Pigm Numarası"),
                    new DataColumn("Dilekçe Numarası"),
                    new DataColumn("Kullanılmış mı"),
                    new DataColumn("Ön Dizin Durumu"),
                    new DataColumn("Açıklama"),
                    new DataColumn("Başvuru Tarihi"),
                    new DataColumn("Tps Numarası"),
                    new DataColumn("Belge Numarası"),
                    new DataColumn("Kabul Tarihi")
                    });
                var emps = from fl in db.FollowLists.ToList() select fl;
                foreach (var item in emps)
                {
                    dt.Rows.Add(item.InvoiceNo, item.Segment, item.SerialNo, item.Well, item.PigmNo, item.PetitionNo, item.Used, item.FrontIndexStatus, item.Explanation, item.RecourseDate, item.TpsNo, item.DocumentNo, item.RatificationDate);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dt);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ihracat.xlsx");
                    }
                }
            }
        }
    }
}
