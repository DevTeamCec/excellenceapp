using System;
using System.Net;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Curso.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;
using System.Web.Services.Description;
using Google.Protobuf.Collections;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Web.UI.WebControls;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
using System.Windows.Forms;
using System.Net.Mail;

namespace Curso.Controllers 
{
    [OutputCache( Duration = 1)]
    public class HomeController : Controller
    {
        private SysDesarrolloHEntities db = new SysDesarrolloHEntities();

        private SysDesarrolloHEntities objSysDesarrolloHEntities;
        SqlConnection con = new SqlConnection("data source=tcp:systrackuat.database.windows.net,1433;initial catalog=SysFullPass;persist security info=False;user id=AdministradorCEC;password=Pankis@99;multipleactiveresultsets=False;encrypt=True;trustservercertificate=False;App=EntityFramework&quot;");
        //Se obtiene usuario

        public ActionResult Login(string mensaje)
        {
            ViewBag.msg = mensaje;
            return View();
        }

        //Verificación del usuario si está activo o no
      
        [HttpPost]
        public ActionResult Login(UsuarioLogin datos)
        {  if (ModelState.IsValid)
            {   if (datos.Login() == true)
                {   Session["Txt_Usuario"] = datos.Txt_Usuario;
                    Session["Int_IdUsuario"] = datos.Int_IdUsuario;
                    Session["Txt_Password"] = datos.Txt_Password;
                    Session["Int_IdGerente"] = datos.Int_IdGerente;
                    Session["Txt_Gerente"] = datos.Txt_Gerente;
                    Session["Fec_Inicio"] = datos.Fec_Inicio;
                    Session["Fec_Fin"] = datos.Fec_Fin;
                    Session["Int_IdPlaza"] = datos.Int_IdPlaza;
                    Session["Txt_Email"] = datos.Txt_Email;
                    Session["Int_IdNivel"] = datos.Int_IdNivel;
                    Session["Bol_Activo"] = datos.Bol_Activo;
                   // Session["TotalDias"] = datos.totDias;

                    SysDesarrolloHEntities db = new SysDesarrolloHEntities();

                    var userv = db.Tb_UsuariosCurso.FirstOrDefault(e => e.Txt_Usuario == datos.Txt_Usuario && e.Txt_Password == datos.Txt_Password);
                    
                   
                    if (Session["Bol_Activo"].ToString() == "2" ){
                        if (userv != null)
                        {
                            return RedirectToAction("Index", new { mensaje = "2" });
                        }
                        else
                        {
                            return RedirectToAction("Login", new { mensaje = "2" });
                        }
                    }
                    else if (Session["Bol_Activo"].ToString() == "1" || Session["Bol_Activo"].ToString() == "3") {
                        SqlConnection con = new SqlConnection("data source=tcp:systrackuat.database.windows.net,1433;initial catalog=SysFullPass;persist security info=False;user id=AdministradorCEC;password=Pankis@99;multipleactiveresultsets=False;connect timeout=30;encrypt=True;trustservercertificate=False;App=EntityFramework&quot;");
                        con.Open();

                        SqlCommand cmd = new SqlCommand("update Tb_UsuariosCurso set Bol_Activo = " + 2 + " where Int_IdUsuario = " + Session["Int_IdUsuario"] + "", con);
                        cmd.ExecuteNonQuery();
                        return RedirectToAction("Index", new { mensaje = "1" });
                    }
                    else
                    {
                        return RedirectToAction("Login", new { mensaje = "2" });
                    }
                    
                }
                else
                { 
                   return RedirectToAction("Login", new { mensaje = "2" });
                }
            }
            else{return RedirectToAction("Login", new { mensaje = "2" });}
           
        }

        //Se termina la sesión, se regresa a Login

      
        public ActionResult Logout()
        {          
            HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoStore();
            HttpCookie c = new HttpCookie("Login");
            c.Expires = DateTime.Now.AddDays(-1);
            Session["Txt_Usuario"] = null;
            Session.RemoveAll();
            Session.Abandon();

          
            Session.Clear();
       
          
            FormsAuthentication.SignOut();
         
            return RedirectToAction("Login");
        }

        //Convertidor de los objetos en la BD
        public void moduloActivo()
        {
            List<Activo> lst = null;
            using (Models.SysDesarrolloHEntities dr = new Models.SysDesarrolloHEntities())
            {
                lst = (
                    from d in dr.Ct_ActivoCursos
                    select new Activo
                    {
                        Bol_Activo = d.Bol_Activo,
                        Txt_Activo = d.Txt_Activo
                    }).ToList();

                List<SelectListItem> activo = lst.ConvertAll(df =>
                {
                    return new SelectListItem()
                    {
                        Value = df.Bol_Activo.ToString(),
                        Text = df.Txt_Activo.ToString(),
                        Selected = false
                    };
                });
                ViewBag.activo = activo;
            }
        }

        //Convertidor de los objetos en la BD
        public void moduloNivel()
        {
            List<Nivel> lst = null;

            using (Models.SysDesarrolloHEntities dr = new Models.SysDesarrolloHEntities())
            {
                lst = (
                    from d in dr.Ct_NivelCursos
                    select new Nivel
                    {
                        Int_IdNivel = d.Int_IdNivel,
                        Txt_Nivel = d.Txt_Nivel
                    }).ToList();

                List<SelectListItem> nivel = lst.ConvertAll(df =>
                {
                    return new SelectListItem()
                    {
                        Value = df.Int_IdNivel.ToString(),
                        Text = df.Txt_Nivel.ToString(),
                        Selected = false
                    };
                });
                ViewBag.nivel = nivel;
            }
        }

        //Convertidor de los objetos en la BD
        public void moduloPlaza()
        {
            List<Plaza> lst = null;

            using (Models.SysDesarrolloHEntities dr = new Models.SysDesarrolloHEntities())
            {
                lst = (
                from d in dr.Ct_Plaza
                select new Plaza
                {
                    Int_IdPlaza = d.Int_IdPlaza,
                    Txt_Plaza = d.Txt_Plaza
                }).ToList();

                List<SelectListItem> plaza = lst.ConvertAll(df =>
                {
                    return new SelectListItem()
                    {
                        Value = df.Int_IdPlaza.ToString(),
                        Text = df.Txt_Plaza.ToString(),
                        Selected = false
                    };
                });
                ViewBag.plaza = plaza;
            }
        }



        public ActionResult Registro(string mensaje, string idusuario, string dias)
        {
            if (Session["Txt_Usuario"] != null)
            {
                moduloNivel();
                moduloActivo();
                moduloPlaza();

                ViewBag.msn = mensaje;
                ViewBag.id = idusuario;
                ViewBag.dias = dias;

                return View();
            }
            else
            {

                return RedirectToAction("Login");

             }
           

        }
        
       
        [HttpPost]
        public ActionResult Registro(Tb_UsuariosCurso obj)
        {
              if (Session["Txt_Usuario"] != null)
                 {
               
                    SysDesarrolloHEntities db = new SysDesarrolloHEntities();
                    var user = db.Tb_UsuariosCurso.FirstOrDefault(e => e.Txt_Usuario == obj.Txt_Usuario || e.Txt_Email == obj.Txt_Email);
                    if (user == null)
                    {
                        if (ModelState.IsValid)
                        {
                        try
                        {
                            db.Tb_UsuariosCurso.Add(obj);

                            db.SaveChanges();
                            string iduser = Convert.ToString(obj.Int_IdUsuario);

                            DateTime a = Convert.ToDateTime(obj.Fec_Fin);
                            DateTime b = Convert.ToDateTime(obj.Fec_Inicio);

                            TimeSpan totalDias = a - b;
                            string days = Convert.ToString(totalDias.Days);
                            return RedirectToAction("Registro", new { mensaje = "2", idusuario = iduser, dias = days });
                        }
                        catch(Exception er)
                        {
                            MessageBox.Show(er.Message);
                            return RedirectToAction("Registro", new { mensaje = "1" });
                        }

                        }
                        else
                        {
                            return RedirectToAction("Registro", new { mensaje = "1" });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Registro", new { mensaje = "3" });
                    }
               
                
               
            }
            else
            {
                return RedirectToAction("Login");
            }
           
         


        }

        [HttpGet]
        public JsonResult buscar(int userid) {
            
            List<usuarioslista> lista;

            using (SysDesarrolloHEntities dr = new SysDesarrolloHEntities())
            {
                lista = (from c in dr.Tb_UsuariosCurso
                         where c.Int_IdUsuario == userid 
                         select new usuarioslista
                         {
                             Int_IdUsuario = c.Int_IdUsuario,
                             Txt_Usuario = c.Txt_Usuario,
                             Int_IdNivel = c.Int_IdNivel,
                             Txt_Password = c.Txt_Password,
                             Int_IdGerente = c.Int_IdGerente,
                             Txt_Gerente = c.Txt_Gerente,
                             Int_IdPlaza = c.Int_IdPlaza,
                             Txt_Email = c.Txt_Email,
                             Fec_Inicio = c.Fec_Inicio.ToString(),
                             Fec_Fin = c.Fec_Fin.ToString(),
                             Bol_Activo = c.Bol_Activo


                         }).ToList();
            
            }

                return Json(lista,JsonRequestBehavior.AllowGet);
        }

        public HomeController()
        {
            objSysDesarrolloHEntities = new SysDesarrolloHEntities();
        }

        public ActionResult Index()
        {

            if (Session["Txt_Usuario"] != null)
            {
                Video1();
                Video2();
                Video3();
                Video4();
                Video5();
                Video6();
                Video7();
                Video8();
                Video9();
                Video10();
                Video11();
                Video12();
                Video13();
                return View();
            }
            else
            {
           
                return RedirectToAction("Login");
                
                
            }

            
           
           
        }

        [HttpGet]
        public JsonResult cast( string content, string name)
        {

            const string accountName = "strategias";
            const string keyValue = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials = new StorageCredentials(accountName, keyValue);
            var account = new CloudStorageAccount(credentials, true);

            var container = account.CreateCloudBlobClient().GetContainerReference(content);
            var blob = container.GetBlockBlobReference(name);

            var shareAccessSig = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed = string.Format("{0}{1}", blob.Uri, shareAccessSig);

          

            return Json(urlToBePlayed, JsonRequestBehavior.AllowGet);
        }

        #region Videos
        public void Video1()
        {
            const string accountName = "strategias";
            const string keyValue = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials = new StorageCredentials(accountName, keyValue);
            var account = new CloudStorageAccount(credentials, true);

            var container = account.CreateCloudBlobClient().GetContainerReference("asset-92471228-6f8e-4f1a-9d77-cd6c76517616");
            var blob = container.GetBlockBlobReference("1-introduccion.mp4");

            var shareAccessSig = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed = string.Format("{0}{1}", blob.Uri, shareAccessSig);

            ViewData["Video1"] = urlToBePlayed;
        }         
        public void Video2()
        {
            const string accountName2 = "strategias";
            const string keyValue2 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials2 = new StorageCredentials(accountName2, keyValue2);
            var account2 = new CloudStorageAccount(credentials2, true);

            var container2 = account2.CreateCloudBlobClient().GetContainerReference("asset-5d7cf754-a4df-4b37-970e-4a0b1c79c9e3");
            var blob2 = container2.GetBlockBlobReference("2-Admin-efectivo.mp4");

            var shareAccessSig2 = blob2.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed2 = string.Format("{0}{1}", blob2.Uri, shareAccessSig2);

            ViewData["Video2"] = urlToBePlayed2;
        }
        public void Video3()
        {
            const string accountName3 = "strategias";
            const string keyValue3 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials3 = new StorageCredentials(accountName3, keyValue3);
            var account3 = new CloudStorageAccount(credentials3, true);

            var container3 = account3.CreateCloudBlobClient().GetContainerReference("asset-665d865e-dcb0-4a60-940b-c4f41c2b1861");
            var blob3 = container3.GetBlockBlobReference("3-Retornos-financieros.mp4");

            var shareAccessSig3 = blob3.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed3 = string.Format("{0}{1}", blob3.Uri, shareAccessSig3);

            ViewData["Video3"] = urlToBePlayed3;
        } 
        public void Video4()
        {
            const string accountName4 = "strategias";
            const string keyValue4 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials4 = new StorageCredentials(accountName4, keyValue4);
            var account4 = new CloudStorageAccount(credentials4, true);

            var container4 = account4.CreateCloudBlobClient().GetContainerReference("asset-fa979251-9c1d-45f8-b5bf-a076aaae008d");
            var blob4 = container4.GetBlockBlobReference("4-Transfer-Inter.mp4");

            var shareAccessSig4 = blob4.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed4 = string.Format("{0}{1}", blob4.Uri, shareAccessSig4);

            ViewData["Video4"] = urlToBePlayed4;
        } 
        public void Video5()
        {
            const string accountName5 = "strategias";
            const string keyValue5 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials5 = new StorageCredentials(accountName5, keyValue5);
            var account5 = new CloudStorageAccount(credentials5, true);

            var container5 = account5.CreateCloudBlobClient().GetContainerReference("asset-41dcf9c0-5787-4cde-8048-6de9c830b06c");
            var blob5 = container5.GetBlockBlobReference("5-Proyectos360.mp4");

            var shareAccessSig5 = blob5.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed5 = string.Format("{0}{1}", blob5.Uri, shareAccessSig5);

            ViewData["Video5"] = urlToBePlayed5;
        } 
        public void Video6()
        {
            const string accountName6 = "strategias";
            const string keyValue6 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials6 = new StorageCredentials(accountName6, keyValue6);
            var account6 = new CloudStorageAccount(credentials6, true);

            var container6 = account6.CreateCloudBlobClient().GetContainerReference("asset-beeb1304-8941-4e72-aaff-f31e1abd8a16");
            var blob6 = container6.GetBlockBlobReference("6-TC-monedero-electronico.mp4");

            var shareAccessSig6 = blob6.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed6 = string.Format("{0}{1}", blob6.Uri, shareAccessSig6);

            ViewData["Video6"] = urlToBePlayed6;
        } 
        public void Video7()
        {
            const string accountName7 = "strategias";
            const string keyValue7 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials7 = new StorageCredentials(accountName7, keyValue7);
            var account7 = new CloudStorageAccount(credentials7, true);

            var container7 = account7.CreateCloudBlobClient().GetContainerReference("asset-3a7228a1-2ce8-424f-81fc-05a228599e0c");
            var blob7 = container7.GetBlockBlobReference("7-TT-TPV.mp4");

            var shareAccessSig7 = blob7.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed7 = string.Format("{0}{1}", blob7.Uri, shareAccessSig7);

            ViewData["Video7"] = urlToBePlayed7;
        }
        public void Video8() {
            const string accountName8 = "strategias";
            const string keyValue8 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials8 = new StorageCredentials(accountName8, keyValue8);
            var account8 = new CloudStorageAccount(credentials8, true);

            var container8 = account8.CreateCloudBlobClient().GetContainerReference("asset-3de897c7-89dc-4df4-b362-e7792c5445aa");
            var blob8 = container8.GetBlockBlobReference("8-Admin-Nomina.mp4");

            var shareAccessSig8 = blob8.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed8 = string.Format("{0}{1}", blob8.Uri, shareAccessSig8);

            ViewData["Video8"] = urlToBePlayed8;
        }
        public void Video9() {
            const string accountName9 = "strategias";
            const string keyValue9 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials9 = new StorageCredentials(accountName9, keyValue9);
            var account9 = new CloudStorageAccount(credentials9, true);

            var container9 = account9.CreateCloudBlobClient().GetContainerReference("asset-a062f95a-9307-4f94-8623-6992675b2a56");
                                                                                  
            var blob9 = container9.GetBlockBlobReference("9-Plan-privado-pensiones.mp4");

            var shareAccessSig9 = blob9.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed9 = string.Format("{0}{1}", blob9.Uri, shareAccessSig9);

            ViewData["Video9"] = urlToBePlayed9;
        }
        public void Video10() {
            const string accountName10 = "strategias";
            const string keyValue10 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials10 = new StorageCredentials(accountName10, keyValue10);
            var account10 = new CloudStorageAccount(credentials10, true);

            var container10 = account10.CreateCloudBlobClient().GetContainerReference("asset-9c9603a9-de1a-4781-9fa8-bcf32e3620b9");
            var blob10 = container10.GetBlockBlobReference("10-Sindicatos.mp4");

            var shareAccessSig10 = blob10.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed10 = string.Format("{0}{1}", blob10.Uri, shareAccessSig10);

            ViewData["Video10"] = urlToBePlayed10;
        }
        public void Video11() {
            const string accountName11 = "strategias";
            const string keyValue11 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials11 = new StorageCredentials(accountName11, keyValue11);
            var account11 = new CloudStorageAccount(credentials11, true);

            var container11 = account11.CreateCloudBlobClient().GetContainerReference("asset-4bebd110-6f4a-4cc3-8aca-4a6cf27fd581");
            var blob11 = container11.GetBlockBlobReference("11-Nomina-Construccion.mp4");

            var shareAccessSig11 = blob11.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed11 = string.Format("{0}{1}", blob11.Uri, shareAccessSig11);

            ViewData["Video11"] = urlToBePlayed11;
        }
        public void Video12()
        {
            const string accountName12 = "strategias";
            const string keyValue12 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials12 = new StorageCredentials(accountName12, keyValue12);
            var account12 = new CloudStorageAccount(credentials12, true);

            var container12 = account12.CreateCloudBlobClient().GetContainerReference("asset-0542a436-0fc9-4da1-bd52-73449fbc54da");
            var blob12 = container12.GetBlockBlobReference("12-PC-Soluciones-Financieras.mp4");

            var shareAccessSig12 = blob12.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed12 = string.Format("{0}{1}", blob12.Uri, shareAccessSig12);

            ViewData["Video12"] = urlToBePlayed12;
        }
        public void Video13()
        {
            const string accountName13 = "strategias";
            const string keyValue13 = "1Wlto6bL8q+HRAWrH3fArieOlVA2ceHBd2sPB2aIFj0UohcVKeIBjfAc7x1gqXZjZ+viv6Sj11W7Br6h6jbb2g==";

            var credentials13 = new StorageCredentials(accountName13, keyValue13);
            var account13 = new CloudStorageAccount(credentials13, true);

            var container13 = account13.CreateCloudBlobClient().GetContainerReference("asset-77c38ddb-f89e-4a5d-be5a-b30ff00b43b0");
            var blob13 = container13.GetBlockBlobReference("13-7-Pasos-Socio-Comercial.mp4");

            var shareAccessSig11 = blob13.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(1)
            });

            var urlToBePlayed13 = string.Format("{0}{1}", blob13.Uri, shareAccessSig11);

            ViewData["Video13"] = urlToBePlayed13;
        }
        #endregion

        public ActionResult VTigerSection()
        {
            if(Session["Txt_Usuario"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public JsonResult GetAllEmployees()
        {
            return Json(objSysDesarrolloHEntities.Tb_UsuariosCurso.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string mail, string name, string subject, string message)
        {
            if (Session["Txt_Usuario"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        string mailemisor = "soporteti@masterexchange.com.mx";
                        string mailreceptor = "cmederos@strategias.mx";
                        string contraseña = "sistemas2021";
                        string mailName = "Nombre de quien solicitó: " + name;
                        string mailAd = "Correo de quién mandó la solicitud: " + mail;

                        MailMessage msng = new MailMessage(mailemisor, mailreceptor, subject, mailName + "/n" + mailAd + "/n" + message);
                        msng.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                        smtpClient.Send(msng);
                        smtpClient.Dispose();
                        ViewBag.Success = "Correo Enviado";
                        return View();
                    }

                }
                catch (Exception er)
                {
                    ViewBag.Error = "Some Error";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }



        public ActionResult SetLanguage(string language)
        {
            var cookie = new HttpCookie("_cultureinformation");
            cookie.Value = language;
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(cookie);
            return RedirectToAction("Index");
        }

        //public ActionResult Update()
        //{
        //    SysDesarrolloHEntities db = new SysDesarrolloHEntities();

        //    return View();
        //}

    public ActionResult GridMvcEmpleados()
        {
            if (Session["Txt_Usuario"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-mx");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-mx");
                List<UsuarioLogin> usuarios = new List<UsuarioLogin>();


                using (SysDesarrolloHEntities db = new SysDesarrolloHEntities())
                {
                    usuarios = (from c in db.Tb_UsuariosCurso
                                join d in db.Ct_Plaza on c.Int_IdPlaza equals d.Int_IdPlaza
                                join e in db.Ct_NivelCursos on c.Int_IdNivel equals e.Int_IdNivel
                                join f in db.Ct_ActivoCursos on c.Bol_Activo equals f.Bol_Activo
                                where e.Int_IdNivel == 1
                                select new UsuarioLogin
                                {
                                    Int_IdUsuario = c.Int_IdUsuario,
                                    Txt_Usuario = c.Txt_Usuario.ToString(),
                                    Int_IdNivel = e.Int_IdNivel,
                                    Txt_Nivel = e.Txt_Nivel.ToString(),
                                    Txt_Password = c.Txt_Password.ToString(),
                                    Int_IdPlaza = d.Int_IdPlaza,
                                    Txt_Plaza = d.Txt_Plaza.ToString(),
                                    Txt_Email = c.Txt_Email.ToString(),
                                    Int_IdGerente = c.Int_IdGerente,
                                    Txt_Gerente = c.Txt_Gerente.ToString(),
                                    Fec_Inicio = c.Fec_Inicio.ToString(),
                                    Fec_Fin = c.Fec_Fin.ToString(),
                                    Bol_Activo = f.Bol_Activo,
                                    Txt_Activo = f.Txt_Activo.ToString()

                                }).ToList();

                }

                return View(usuarios);
              
            }
            else
            {

                return RedirectToAction("Login");


            }
           
        }


        public ActionResult GERENT()
        {
            if (Session["Txt_Usuario"] != null)
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-mx");
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-mx");

                List<UsuarioLogin> gerentes = new List<UsuarioLogin>();

                using (SysDesarrolloHEntities db = new SysDesarrolloHEntities())
                {
                    gerentes = (from c in db.Tb_UsuariosCurso
                                join d in db.Ct_Plaza on c.Int_IdPlaza equals d.Int_IdPlaza
                                join e in db.Ct_NivelCursos on c.Int_IdNivel equals e.Int_IdNivel
                                join f in db.Ct_ActivoCursos on c.Bol_Activo equals f.Bol_Activo
                                where e.Int_IdNivel == 2
                                select new UsuarioLogin
                                {
                                    Int_IdUsuario = c.Int_IdUsuario,
                                    Txt_Usuario = c.Txt_Usuario.ToString(),
                                    Int_IdNivel = e.Int_IdNivel,
                                    Txt_Nivel = e.Txt_Nivel.ToString(),
                                    Txt_Password = c.Txt_Password.ToString(),
                                    Int_IdPlaza = d.Int_IdPlaza,
                                    Txt_Plaza = d.Txt_Plaza.ToString(),
                                    Txt_Email = c.Txt_Email.ToString(),
                                    Int_IdGerente = c.Int_IdGerente,
                                    Txt_Gerente = c.Txt_Gerente.ToString(),
                                    Fec_Inicio = c.Fec_Inicio.ToString(),
                                    Fec_Fin = c.Fec_Fin.ToString(),
                                    Bol_Activo = f.Bol_Activo,
                                    Txt_Activo = f.Txt_Activo.ToString()

                                }).ToList();

                }

                return View(gerentes);
            }
            else
            {
                return RedirectToAction("Login");
            }
              

        }



        public ActionResult editar(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult editar([Bind(Include = "Int_IdUsuario,Txt_Usuario,Txt_Password,Int_IdGerente,Txt_Gerente,Txt_Email,Int_IdPlaza,Int_IdNivel,Fec_Inicio,Fec_Fin,Bol_Activo")] Tb_UsuariosCurso tb_UsuariosCurso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_UsuariosCurso).State = EntityState.Modified;
                Console.WriteLine(tb_UsuariosCurso);
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_UsuariosCurso);
        }
        //Refactorizar el código, hay mucho qué hacer

    }



}
