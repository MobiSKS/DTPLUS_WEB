

using ClosedXML.Excel;
using ClosedXML.Extensions;
using HPCL.Common.Helper;
using HPCL.Common.Models.CommonEntity;
using HPCL.Common.Models.ViewModel.ApplicationFormDataEntry;
using HPCL.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HPCL_Web.Controllers
{
    [TypeFilter(typeof(SessionExpireActionFilter))]
    public class ApplicationFormDataEntryController : Controller
    {
        private readonly IApplicationFormDataEntryService _applicationFormDataEntryService;
        private readonly ICommonActionService _commonActionService;
        public ApplicationFormDataEntryController(IApplicationFormDataEntryService applicationFormDataEntryService, ICommonActionService commonActionService)
        {
            _applicationFormDataEntryService = applicationFormDataEntryService;
            _commonActionService = commonActionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddAddOnCards()
        {
            var model = await _applicationFormDataEntryService.AddAddOnCards();

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetFormName(string customerId)
        {
            var searchResult = await _applicationFormDataEntryService.GetCustomerName(customerId);
            return Json(new { searchResult = searchResult });
        }

        [HttpPost]
        public async Task<JsonResult> CheckAddOnForm(string formNumber)
        {
            var searchResult = await _applicationFormDataEntryService.CheckAddOnForm(formNumber);
            return Json(new { searchResult = searchResult });
        }
        [HttpPost]
        public async Task<IActionResult> GetAddOnCardsPartialView([FromBody] List<ObjCardDetail> objCardDetails)
        {
            var modals = await _applicationFormDataEntryService.GetAddOnCardsPartialView(objCardDetails);
            return PartialView("~/Views/ApplicationFormDataEntry/_AddAddOnCardsTbl.cshtml", modals);
        }
        [HttpPost]
        public async Task<IActionResult> CustomerAddCardVehicleTbl([FromBody] List<ObjCardDetail> objCardDetails)
        {
            var modals = await _applicationFormDataEntryService.CustomerAddCardVehicleTbl(objCardDetails);
            return PartialView("~/Views/ApplicationFormDataEntry/_AddAddOnCardVehicleDetailsTable.cshtml", modals);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddOnCards(AddAddOnCard addAddOnCard)
        {
            var Model = await _applicationFormDataEntryService.AddAddOnCards(addAddOnCard);

            if (Model.StatusCode == 1000)
            {
                return RedirectToAction("SuccessAddOnCardRedirect", new { Message = Model.Reason });
            }
           
            return View(Model);
        }
        public async Task<IActionResult> SuccessAddOnCardRedirect(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CheckCardIdentifierAlreadyUsed(string cardIdentifier, string customerID)
        {
            var Model = await _applicationFormDataEntryService.CheckCardIdentifierAlreadyUsed(cardIdentifier, customerID);

            return Json(Model);
        }
        private ClosedXML.Excel.XLWorkbook GenerateClosedXMLWorkbook(string CustomerType, string CardOrCardLess, List<VehicleTypeModel> sortedtList)
        {
            var wb = new ClosedXML.Excel.XLWorkbook();
            var ws = wb.AddWorksheet("Sheet1");

            if (CustomerType == "902" || CustomerType == "917")//Corporate,Non Fleet
            {
                ws.Cell("A1").Value = "Card Identifier";
                IXLCell c = ws.Cell(1, 1);
                c.Style.Font.Bold = true;

                if (CardOrCardLess == "Cardless")
                {
                    ws.Cell("B1").Value = "Mobile Number";
                    IXLCell c1 = ws.Cell(1, 2);
                    c1.Style.Font.Bold = true;
                }

                int i = 999;
                foreach (VehicleTypeModel item in sortedtList)
                {
                    i = i + 1;
                    ws.Cell("Z" + i.ToString()).Value = item.VehicleTypeName;
                }

                ws.Range("C2:C1000").SetDataValidation().List(ws.Range("z1000:z" + i.ToString()), true);
            }
            else if (CustomerType == "905" || CustomerType == "909")//Attached,Generic
            {
                ws.Cell("A1").Value = "Card Identifier";
                ws.Cell("B1").Value = "Vehicle Number";
                ws.Cell("C1").Value = "Vehicle Type";
                ws.Cell("D1").Value = "Vehicle Make";
                ws.Cell("E1").Value = "Year of Registration";

                IXLCell c = ws.Cell(1, 1);
                c.Style.Font.Bold = true;
                IXLCell c1 = ws.Cell(1, 2);
                c1.Style.Font.Bold = true;
                IXLCell c2 = ws.Cell(1, 3);
                c2.Style.Font.Bold = true;
                IXLCell c3 = ws.Cell(1, 4);
                c3.Style.Font.Bold = true;
                IXLCell c4 = ws.Cell(1, 5);
                c4.Style.Font.Bold = true;

                if (CardOrCardLess == "Cardless")
                {
                    ws.Cell("F1").Value = "Mobile Number";
                    IXLCell c5 = ws.Cell(1, 6);
                    c5.Style.Font.Bold = true;
                }

                int i = 999;
                foreach (VehicleTypeModel item in sortedtList)
                {
                    i = i + 1;
                    ws.Cell("Z" + i.ToString()).Value = item.VehicleTypeName;
                }

                ws.Range("C2:C1000").SetDataValidation().List(ws.Range("z1000:z" + i.ToString()), true);
            }
            else if (CustomerType == "901" || CustomerType == "906")//Fleet,Credit
            {
                ws.Cell("A1").Value = "Vehicle Number";
                ws.Cell("B1").Value = "Vehicle Type";
                ws.Cell("C1").Value = "Vehicle Make";
                ws.Cell("D1").Value = "Year of Registration";

                IXLCell c = ws.Cell(1, 1);
                c.Style.Font.Bold = true;
                IXLCell c1 = ws.Cell(1, 2);
                c1.Style.Font.Bold = true;
                IXLCell c2 = ws.Cell(1, 3);
                c2.Style.Font.Bold = true;
                IXLCell c3 = ws.Cell(1, 4);
                c3.Style.Font.Bold = true;

                if (CardOrCardLess == "Cardless")
                {
                    ws.Cell("E1").Value = "Mobile Number";
                    IXLCell c4 = ws.Cell(1, 5);
                    c4.Style.Font.Bold = true;
                }

                int i = 999;
                foreach (VehicleTypeModel item in sortedtList)
                {
                    i = i + 1;
                    ws.Cell("Z" + i.ToString()).Value = item.VehicleTypeName;
                }

                ws.Range("B2:B1000").SetDataValidation().List(ws.Range("z1000:z" + i.ToString()), true);
            }

            return wb;
        }

        public async Task<ActionResult> Download(string CustomerType, string CardOrCardLess)
        {
            List<VehicleTypeModel> sortedtList = new List<VehicleTypeModel>();
            sortedtList = await _commonActionService.GetVehicleTypeDropdown();

            using (var wb = GenerateClosedXMLWorkbook(CustomerType, CardOrCardLess, sortedtList))
            {
                // Add ClosedXML.Extensions in your using declarations
                // or specify the content type:
                return wb.Deliver("NewCardFormGeneral.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

    }
}