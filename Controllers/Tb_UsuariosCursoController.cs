using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Curso.Models;

namespace Curso.Controllers
{
    public class Tb_UsuariosCursoController : Controller
    {
        private SysDesarrolloHEntities db = new SysDesarrolloHEntities();

        // GET: Tb_UsuariosCurso
        public ActionResult Index()
        {
            return View(db.Tb_UsuariosCurso.ToList());
        }

        // GET: Tb_UsuariosCurso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_UsuariosCurso tb_UsuariosCurso = db.Tb_UsuariosCurso.Find(id);
            if (tb_UsuariosCurso == null)
            {
                return HttpNotFound();
            }
            return View(tb_UsuariosCurso);
        }

        // GET: Tb_UsuariosCurso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_UsuariosCurso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Int_IdUsuario,Txt_Usuario,Txt_Password,Int_IdGerente,Txt_Gerente,Txt_Email,Int_IdPlaza,Int_IdNivel,Fec_Inicio,Fec_Fin,Bol_Activo")] Tb_UsuariosCurso tb_UsuariosCurso)
        {
            if (ModelState.IsValid)
            {
                db.Tb_UsuariosCurso.Add(tb_UsuariosCurso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_UsuariosCurso);
        }

        // GET: Tb_UsuariosCurso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_UsuariosCurso tb_UsuariosCurso = db.Tb_UsuariosCurso.Find(id);
            if (tb_UsuariosCurso == null)
            {
                return HttpNotFound();
            }
            return View(tb_UsuariosCurso);
        }

        // POST: Tb_UsuariosCurso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Int_IdUsuario,Txt_Usuario,Txt_Password,Int_IdGerente,Txt_Gerente,Txt_Email,Int_IdPlaza,Int_IdNivel,Fec_Inicio,Fec_Fin,Bol_Activo")] Tb_UsuariosCurso tb_UsuariosCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_UsuariosCurso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_UsuariosCurso);
        }

        // GET: Tb_UsuariosCurso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_UsuariosCurso tb_UsuariosCurso = db.Tb_UsuariosCurso.Find(id);
            if (tb_UsuariosCurso == null)
            {
                return HttpNotFound();
            }
            return View(tb_UsuariosCurso);
        }

        // POST: Tb_UsuariosCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tb_UsuariosCurso tb_UsuariosCurso = db.Tb_UsuariosCurso.Find(id);
            db.Tb_UsuariosCurso.Remove(tb_UsuariosCurso);
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
