using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FiveGold.Classes;
using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;

namespace FiveGold.Controllers
{
    public class TemaConhecimentoController : Controller
    {
        public TemaConhecimentoBusiness temaConhecimentoBusiness = new TemaConhecimentoBusiness();

        // GET: TemaConhecimento
        public ActionResult Index()
        {
            return View(temaConhecimentoBusiness.Listar());
        }

        // GET: TemaConhecimento/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TemaConhecimento temaConhecimento)
        {
            if (ModelState.IsValid)
            {
                temaConhecimento = temaConhecimentoBusiness.Inserir(temaConhecimento);
                
                if (temaConhecimento.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Imagens/ImagensTemaConhecimento";
                    var file = string.Format("{0}.jpg", temaConhecimento.IDTema);

                    var response = FilesHelper.UploadPhoto(temaConhecimento.LogoFile, folder, file);
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        temaConhecimento.Logo = pic;
                        temaConhecimentoBusiness.Alterar(temaConhecimento);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(temaConhecimento);
        }

        // GET: TemaConhecimento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TemaConhecimento temaConhecimento = temaConhecimentoBusiness.Buscar(id);

            if (temaConhecimento == null)
            {
                return HttpNotFound();
            }
            return View(temaConhecimento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TemaConhecimento temaConhecimento)
        {
            if (ModelState.IsValid)
            {
                if (temaConhecimento.LogoFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Imagens/ImagensTemaConhecimento";
                    var file = string.Format("{0}.jpg", temaConhecimento.IDTema);

                    var response = FilesHelper.UploadPhoto(temaConhecimento.LogoFile, folder, string.Format("{0}.jpg", temaConhecimento.IDTema));
                    if (response)
                    {
                        pic = string.Format("{0}/{1}", folder, file);
                        temaConhecimento.Logo = pic;
                    }
                }

                temaConhecimentoBusiness.Alterar(temaConhecimento);
                return RedirectToAction("Index");
            }
            return View(temaConhecimento);
        }

        // GET: TemaConhecimento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TemaConhecimento temaConhecimento = temaConhecimentoBusiness.Buscar(id);
            if (temaConhecimento == null)
            {
                return HttpNotFound();
            }
            return View(temaConhecimento);
        }

        // POST: TemaConhecimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TemaConhecimento temaConhecimento = temaConhecimentoBusiness.Buscar(id);
            temaConhecimentoBusiness.Remove(temaConhecimento);
            return RedirectToAction("Index");
        }
    }
}
