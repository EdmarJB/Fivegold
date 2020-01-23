using FiveGold.Mapeamento.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    [Authorize]
    public class ComentarioConhecimentoController : Controller
    {
        private ComentarioConhecimentoBusiness comentarioConhecimentoBusiness = new ComentarioConhecimentoBusiness();

        // GET: ComentarioBlog
        public ActionResult Index()
        {
            return View(comentarioConhecimentoBusiness.Listar());
        }

        // GET: ComentarioBlog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comentarioConhecimento = comentarioConhecimentoBusiness.Buscar(id);
            if (comentarioConhecimento == null)
            {
                return HttpNotFound();
            }
            return View(comentarioConhecimento);
        }

        // GET: ComentarioBlog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComentarioBlog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ComentarioConhecimento comentarioConhecimento)
        {
            comentarioConhecimento.DataComentario = DateTime.Now;
            //comentarioConhecimento.UserName = User.Identity.Name;
            comentarioConhecimento.NomeCompleto = Usuario.Get.NomeCompleto;

            if (ModelState.IsValid)
            {
                comentarioConhecimentoBusiness.Inserir(comentarioConhecimento);
                return RedirectToAction("../Conhecimento/Details/" + comentarioConhecimento.IDConhecimento);
                //return View();
            }

            //return View(comentarioConhecimento);
            return RedirectToAction("../Conhecimento/Details/" + comentarioConhecimento.IDConhecimento);
        }

        public ActionResult CriarComentario()
        {
            return PartialView();
        }

        // GET: ComentarioBlog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comentarioConhecimento = comentarioConhecimentoBusiness.Alterar(id);
            if (comentarioConhecimento == null)
            {
                return HttpNotFound();
            }
            return View(comentarioConhecimento);
        }

        // POST: ComentarioBlog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ComentarioConhecimento comentarioConhecimento)
        {
            if (ModelState.IsValid)
            {
                comentarioConhecimentoBusiness.AlterarConfirmacao(comentarioConhecimento);
                return RedirectToAction("Index");
            }
            return View(comentarioConhecimento);
        }

        // GET: ComentarioBlog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comentarioConhecimento = comentarioConhecimentoBusiness.Apagar(id);
            if (comentarioConhecimento == null)
            {
                return HttpNotFound();
            }
            return View(comentarioConhecimento);
        }

        // POST: ComentarioBlog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comentarioConhecimentoBusiness.ApagarConfirmacao(id);
            return RedirectToAction("Index");
        }
    }
}