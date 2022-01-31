using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace Curso.Models
{
    public class UsuarioLogin
    { 
        [Key]
        public int Int_IdUsuario     { get; set; }
        [Required]
        public string Txt_Usuario    { get; set; }   
        public int Int_IdGerente     { get; set; }
        public int Int_IdNivel       { get; set; }
        public string Txt_Nivel      { get; set; }
        public string Txt_Password   { get; set; }    
        public int Int_IdPlaza       { get; set; }
        public string Txt_Plaza      { get; set; }
        public string Txt_Email      { get; set; }
        public string Txt_Gerente    { get; set; }
        public string Fec_Inicio     { get; set; }
        public string Fec_Fin        { get; set; }
        public int Bol_Activo       { get; set; }
         public string Txt_Activo { get; set; }
        //public string Fec_Fechadia { get; set; }

        //PROPIEDAD DE NAVEGACIÓN
      

            //SqlConnection con = new SqlConnection(@"Data Source=01PROGLP-CMEDER\SQLAUXPROG;Initial Catalog=Cursos;Integrated Security=True");
            SqlConnection con = new SqlConnection(@"Server=tcp:systrackuat.database.windows.net,1433;Initial Catalog=SysFullPass;Persist Security Info=False;User ID=AdministradorCEC;Password=Pankis@99;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;");

            public bool Login()
            {


                con.Open();

                SqlDataAdapter obj = new SqlDataAdapter(" select * from Tb_UsuariosCurso " +
                                                                   " where  Fec_Fin >= getdate()-1 and Txt_Usuario = '" + Txt_Usuario+"' and Txt_Password = '"+Txt_Password+"' " , con);
                
                DataSet a = new DataSet();
                obj.Fill(a);

                if(a.Tables[0].Rows.Count > 0)
                {
                    Int_IdUsuario = Convert.ToInt32(a.Tables[0].Rows[0]["Int_IdUsuario"]);
                    Txt_Usuario = a.Tables[0].Rows[0]["Txt_Usuario"].ToString();
                    Txt_Password = a.Tables[0].Rows[0]["Txt_Password"].ToString();             
                    Txt_Email = a.Tables[0].Rows[0]["Txt_Email"].ToString();
                    Int_IdPlaza =   Convert.ToInt32(a.Tables[0].Rows[0]["Int_IdPlaza"].ToString());
                    Int_IdNivel =   Convert.ToInt32(a.Tables[0].Rows[0]["Int_IdNivel"].ToString());
                    Int_IdGerente = Convert.ToInt32(a.Tables[0].Rows[0]["Int_IdGerente"].ToString());
                     Txt_Gerente = a.Tables[0].Rows[0]["Txt_Gerente"].ToString();          
                    Fec_Inicio = "" + string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(a.Tables[0].Rows[0]["Fec_Inicio"].ToString()));
                    Fec_Fin = "" + string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(a.Tables[0].Rows[0]["Fec_Fin"].ToString()));
                    Bol_Activo = Convert.ToInt32(a.Tables[0].Rows[0]["Bol_Activo"].ToString());
                //Fec_Fechadia = "" + string.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(a.Tables[0].Rows[0]["fechadia"].ToString()));
                //try
                //{
                //var dat1 = DateTime.UtcNow;
                //DateTime dat1;//Fecha de Hoy
                //dat1 = Convert.ToDateTime(Fec_Fechadia);

                //DateTime date;//Fecha de Hoy
                //  date = Convert.ToDateTime(Fec_Fin);
                //int result = DateTime.Compare(date, dat1);
                //  if (result < 0)
                //  {
                //      SqlDataAdapter caja = new SqlDataAdapter("update Tb_UsuariosCurso set Bol_Activo = " + 3 + " where Int_IdUsuario = " + Int_IdUsuario + "", con);
                //      DataSet b = new DataSet();
                //      caja.Fill(b);   // SELECTOR DE TIEMPO DE EXPIRACIÓN, CAMBIA STATUS
                //      b.Reset();
                //      return false;
                //  }
                //  else
                //  {
                      return true;
                //  }
                //}
                //catch(Exception er)
                //{
                //    MessageBox.Show(er.ToString());
                //    return false;
                //}
                //con.Close();
               
                }
                else
                {
                SqlDataAdapter dta = new SqlDataAdapter("update Tb_UsuariosCurso set Bol_Activo = " + 3 + " where Int_IdUsuario = " + Int_IdUsuario + "", con);
                     DataSet b = new DataSet();
                        dta .Fill(b);   // SELECTOR DE TIEMPO DE EXPIRACIÓN, CAMBIA STATUS
                      b.Reset();

                con.Close();
                    return false;
                }
            }

          
        }

        }
 
