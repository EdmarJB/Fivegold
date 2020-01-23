using FiveGold.Classes;
using FiveGold.Models;
using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class BandeiraController : Controller
    {
        private BandeiraBusiness bandeiraBusiness = new BandeiraBusiness();

        // GET: Bandeiras
        //[Authorize(Roles = "View")]
        public ActionResult Index()
        {
            return View(bandeiraBusiness.Listar());
        }

        // GET: Bandeiras/Details/5
        //[Authorize(Roles = "View")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bandeira bandeira = bandeiraBusiness.Buscar(id);
            if (bandeira == null)
            {
                return HttpNotFound();
            }
            return View(bandeira);
        }

        // GET: Bandeiras/Create
        //[Authorize(Roles = "Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bandeiras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Bandeira bandeira)
        {
            if (ModelState.IsValid)
            {
                bandeira = bandeiraBusiness.Inserir(bandeira);

                if (bandeira.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Imagens/LogoBandeira";
                    var file = string.Format("{0}.jpg", bandeira.IDBandeira);

                    var response = FilesHelper.UploadPhoto(bandeira.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        bandeira.Logo = pic;
                        bandeiraBusiness.Alterar(bandeira);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(bandeira);
        }

        // GET: Bandeiras/Edit/5
        //[Authorize(Roles = "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bandeira bandeira = bandeiraBusiness.Buscar(id);
            if (bandeira == null)
            {
                return HttpNotFound();
            }
            return View(bandeira);
        }

        // POST: Bandeiras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Bandeira bandeira)
        {
            if (ModelState.IsValid)
            {
                if (bandeira.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Imagens/LogoBandeira";
                    var file = string.Format("{0}.jpg", bandeira.IDBandeira);

                    var response = FilesHelper.UploadPhoto(bandeira.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        bandeira.Logo = pic;

                    }
                }
                bandeira = bandeiraBusiness.Alterar(bandeira);
                return RedirectToAction("Index");
            }
            return View(bandeira);
        }

        // GET: Bandeiras/Delete/5
        //[Authorize(Roles = "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bandeira bandeira = bandeiraBusiness.Buscar(id);
            if (bandeira == null)
            {
                return HttpNotFound();
            }
            return View(bandeira);
        }

        // POST: Bandeiras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bandeiraBusiness.Apagar(id);
            return RedirectToAction("Index");
        }
    }
}
