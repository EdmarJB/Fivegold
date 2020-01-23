using FiveGold.Mapeamento.Business;
using FiveGold.Models.Business;
using FiveGold.Models.Mapeamento;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{

    public class ConhecimentoController : Controller
    {
        private ConhecimentoBusiness conhecimentoBusiness = new ConhecimentoBusiness();
        TemaConhecimentoBusiness temaConhecimentoBusiness = new TemaConhecimentoBusiness();

        // GET: Conhecimento
        public ActionResult Index(int? v)
        {
            if (v == 1)
            {
                ViewBag.msgValidacao = "Obrigado por compartilhar seu conhecimento, seu Post sera validado em breve!";
            }
            return View(conhecimentoBusiness.Listar());
        }

        public ActionResult IndexValidacao()
        {
            return View(conhecimentoBusiness.ListarValidacoesPendente());
        }

        public JsonResult ValidarPost(int IDPost)
        {
            try
            {
                conhecimentoBusiness.ValidarPost(IDPost);
                return Json("validou");
            }
            catch (Exception)
            {

                return Json("naoValidou");
            }

        }

        // GET: Conhecimento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.comentarios = conhecimentoBusiness.BuscarComentarios(id);

            var conhecimento = conhecimentoBusiness.Buscar(id);
            //ViewBag.Imagens = temaConhecimentoBusiness.Listar();
            if (conhecimento == null)
            {
                return HttpNotFound();
            }
            return View(conhecimento);
        }

        [Authorize]
        // GET: Conhecimento/Create
        public ActionResult Create()
        {
            var temaCon = temaConhecimentoBusiness.Listar();

            temaCon.Add(new TemaConhecimento
            {
                IDTema = 0,
                Nome = "[ Selecione ]"
            });
            temaCon = temaCon.OrderBy(d => d.Nome).ToList();

            ViewBag.IDTema = new SelectList(temaCon, "IDTema", "Nome");
            return View();
        }

        // POST: Conhecimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Conhecimento conhecimento)
        {
            conhecimento.UserName = User.Identity.Name;
            conhecimento.NomeCompleto = Usuario.Get.NomeCompleto;
            conhecimento.DataPost = DateTime.Now;
            conhecimento.validacao = false;

            if (ModelState.IsValid)
            {
                conhecimentoBusiness.Inserir(conhecimento);
                return RedirectToAction("Index", "Conhecimento", new { v = 1 });
            }
            var temaCon = temaConhecimentoBusiness.Listar();
            temaCon.Add(new TemaConhecimento
            {
                IDTema = 0,
                Nome = "[ Selecione ]"
            });
            temaCon = temaCon.OrderBy(d => d.Nome).ToList();

            ViewBag.IDTema = new SelectList(temaCon, "IDTema", "Nome");
            return View(conhecimento);
        }

        // GET: Conhecimento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conhecimento = conhecimentoBusiness.Buscar(id);
            if (conhecimento == null)
            {
                return HttpNotFound();
            }
            var temaCon = temaConhecimentoBusiness.Listar();
            temaCon.Add(new TemaConhecimento
            {
                IDTema = 0,
                Nome = "[ Selecione ]"
            });
            temaCon = temaCon.OrderBy(d => d.Nome).ToList();

            ViewBag.IDTema = new SelectList(temaCon, "IDTema", "Nome", conhecimento.IDTema);
            return View(conhecimento);
        }

        // POST: Conhecimento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Conhecimento conhecimento)
        {
            if (ModelState.IsValid)
            {
                conhecimentoBusiness.AlterarConfirmacao(conhecimento);
                return RedirectToAction("../Conhecimento/IndexValidacao");
            }
            var temaCon = temaConhecimentoBusiness.Listar();
            temaCon.Add(new TemaConhecimento
            {
                IDTema = 0,
                Nome = "[ Selecione ]"
            });
            temaCon = temaCon.OrderBy(d => d.Nome).ToList();

            ViewBag.IDTema = new SelectList(temaCon, "IDTema", "Nome", conhecimento.IDTema);
            return View(conhecimento);
        }

        // GET: Conhecimento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var conhecimento = conhecimentoBusiness.Apagar(id);
            if (conhecimento == null)
            {
                return HttpNotFound();
            }
            return View(conhecimento);
        }

        public ActionResult AddTemaConhecimento()
        {
            return View();
        }

        // POST: Conhecimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conhecimentoBusiness.ApagarConfirmacao(id);
            return RedirectToAction("../Conhecimento/IndexValidacao");
        }
    }
}