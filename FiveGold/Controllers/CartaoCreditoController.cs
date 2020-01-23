using FiveGold.Models;
using FiveGold.Models.Mapeamento;
using System.Net;
using System.Web.Mvc;

namespace FiveGold.Controllers
{
    public class CartaoCreditoController : Controller
    {
         private CartaoCreditoBusiness cartaoCreditoBusiness = new CartaoCreditoBusiness();

        // GET: CartaoCredito
        public ActionResult Index()
        {
            return View(cartaoCreditoBusiness.Listar());
        }

        // GET: CartaoCredito/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cartaoCredito = cartaoCreditoBusiness.Buscar(id);
            if (cartaoCredito == null)
            {
                return HttpNotFound();
            }
            return PartialView(cartaoCredito);
        }

        // GET: CartaoCredito/Create
        public ActionResult Create()
        {
            ViewBag.IDBandeira = cartaoCreditoBusiness.ListarBandeira();
            ViewBag.IDConta = cartaoCreditoBusiness.ListarConta(User.Identity.Name);
            return PartialView();
        }

        // POST: CartaoCredito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartaoCredito cartaoCredito)
        {
            cartaoCredito.UserId = User.Identity.Name;

            if (ModelState.IsValid)
            {
                cartaoCreditoBusiness.Inserir(cartaoCredito);
                return RedirectToAction("../Dashboard/Cartao");
            }

            ViewBag.IDBandeira = cartaoCreditoBusiness.ListarBandeiraPost(cartaoCredito);
            //ViewBag.IDConta = cartaoCreditoBusiness.ListarContaPost(cartaoCredito);
            return PartialView(cartaoCredito);
        }

        // GET: CartaoCredito/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cartaoCredito = cartaoCreditoBusiness.Alterar(id);
            if (cartaoCredito == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBandeira = cartaoCreditoBusiness.ListarBandeiraPost(cartaoCredito);
            //ViewBag.IDConta = cartaoCreditoBusiness.ListarContaPost(cartaoCredito);
            return PartialView(cartaoCredito);
        }

        // POST: CartaoCredito/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartaoCredito cartaoCredito)
        {
            if (ModelState.IsValid)
            {
                cartaoCreditoBusiness.AlterarConfirmacao(cartaoCredito);
                return RedirectToAction("../Dashboard/Cartao");
            }
            ViewBag.IDBandeira = cartaoCreditoBusiness.ListarBandeiraPost(cartaoCredito);
            //ViewBag.IDConta = cartaoCreditoBusiness.ListarContaPost(cartaoCredito);
            return View(cartaoCredito);
        }

        // GET: CartaoCredito/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cartaoCredito = cartaoCreditoBusiness.Apagar(id);
            if (cartaoCredito == null)
            {
                return HttpNotFound();
            }
            return PartialView(cartaoCredito);
        }

        // POST: CartaoCredito/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cartaoCreditoBusiness.ApagarConfirmacao(id);
            return RedirectToAction("../Dashboard/Cartao");
        }
    }
}