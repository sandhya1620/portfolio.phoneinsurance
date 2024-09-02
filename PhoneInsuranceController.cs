csharp
using PhoneInsurance.Models;
using System.Linq;
using System.Web.Mvc;

namespace PhoneInsurance.Controllers
{
    public class PhoneInsuranceController : Controller
    {
        private PhoneInsuranceContext db = new PhoneInsuranceContext();

        // GET: PhoneInsurance
        public ActionResult Index()
        {
            return View(db.PhoneInsuranceQuotes.ToList());
        }

        // GET: PhoneInsurance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhoneInsurance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhoneBrand,PhoneModel,PurchaseDate,Condition")] PhoneInsuranceQuote phoneInsuranceQuote)
        {
            if (ModelState.IsValid)
            {
                // Calculate the quote amount based on phone details
                phoneInsuranceQuote.QuoteAmount = CalculateQuote(phoneInsuranceQuote);
                
                db.PhoneInsuranceQuotes.Add(phoneInsuranceQuote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phoneInsuranceQuote);
        }

        private decimal CalculateQuote(PhoneInsuranceQuote quote)
        {
            decimal baseAmount = 100m; // Base amount for insurance
            if (quote.Condition == "New")
                baseAmount += 50m;
            else if (quote.Condition == "Like New")
                baseAmount += 30m;
            else if (quote.Condition == "Used")
                baseAmount += 10m;

            // You can add more complex calculations here based on brand, model, etc.

            return baseAmount;
        }

        // GET: PhoneInsurance/Details/5
        public ActionResult Details(int id)
        {
            PhoneInsuranceQuote quote = db.PhoneInsuranceQuotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // GET: PhoneInsurance/Delete/5
        public ActionResult Delete(int id)
        {
            PhoneInsuranceQuote quote = db.PhoneInsuranceQuotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        // POST: PhoneInsurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhoneInsuranceQuote quote = db.PhoneInsuranceQuotes.Find(id);
            db.PhoneInsuranceQuotes.Remove(quote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


### *Step 5: Create Views*

#### *Create.cshtml*

In the Views/PhoneInsurance folder, create a Create.cshtml file.

html
@model PhoneInsurance.Models.PhoneInsuranceQuote

@{
    ViewBag.Title = "Create Phone Insurance Quote";
}

<h2>Create Phone Insurance Quote</h2>

@using (Html.BeginForm()) 
{
    @Html.AntiForgeryToken()
    
    <div class="form-horizontal">
        <h4>PhoneInsuranceQuote</h4>
        <hr />
        @Html.ValidationSummary(true, "", new { @class = "text-danger" })
        <div class="form-group">
            @Html.LabelFor(model => model.PhoneBrand, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.PhoneBrand, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.PhoneBrand, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.PhoneModel, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.PhoneModel, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.PhoneModel, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.PurchaseDate, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.PurchaseDate, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.PurchaseDate, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            @Html.LabelFor(model => model.Condition, htmlAttributes: new { @class = "control-label col-md-2" })
            <div class="col-md-10">
                @Html.EditorFor(model => model.Condition, new { htmlAttributes = new { @class = "form-control" } })
                @Html.ValidationMessageFor(model => model.Condition, "", new { @class = "text-danger" })
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <input type="submit" value="Create" class="btn btn-default" />
            </div>
        </div>
    </div>
}

<div>
    @Html.ActionLink("Back to List", "Index")
</div>


#### *Index.cshtml*

In the Views/PhoneInsurance folder, create an Index.cshtml file.

html
@model IEnumerable<PhoneInsurance.Models.PhoneInsuranceQuote>

@{
    ViewBag.Title = "Phone Insurance Quotes";
}

<h2>Phone Insurance Quotes</h2>

<p>
    @Html.ActionLink("Create New Quote", "Create")
</p>
<table class="table">
    <tr>
        <th>Phone Brand</th>
        <th>Phone Model</th>
        <th>Purchase Date</th>
        <th>Condition</th>
        <th>Quote Amount</th>
        <th></th>
    </tr>

@foreach (var item in Model) {
    <tr>
        <td>@Html.DisplayFor(modelItem => item.PhoneBrand)</td>
        <td>@Html.DisplayFor(modelItem => item.PhoneModel)</td>
        <td>@Html.DisplayFor(modelItem => item.PurchaseDate)</td>
        <td>@Html.DisplayFor(modelItem => item.Condition)</td>
        <td>@Html.DisplayFor(modelItem => item.QuoteAmount)</td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { id = item.QuoteId }) |
            @Html.ActionLink("Details", "Details", new { id = item.QuoteId }) |
            @Html.ActionLink("Delete", "Delete", new { id = item.QuoteId })
        </td>
    </tr>
}

</table>

