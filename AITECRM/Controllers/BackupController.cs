using System;
using System.IO;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Hosting;

namespace AITECRM.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BackupController : Controller
    {
        // GET: Backup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImporterBD()
        {
            return View();
        }
        public ActionResult SuccesImporter()
        {
            ViewBag.m1 = "Operation d'importation reussie";
            return View();
        }
        public ActionResult SuccesExporter()
        {
            ViewBag.m1 = "Operation d'exportation reussie";
            return View();
        }
        public ActionResult ErreurExporter()
        {
            ViewBag.m1 = "L'operation d'exportation n'a pas aboutie";
            ViewBag.m2 = "Veuillez verifier la connexion a la Base de donnee";
            return View();
        }
        public ActionResult ErreurImporter()
        {
            ViewBag.m1 = "L'operation d'importation n'a pas aboutie";
            ViewBag.m2 = "Veuillez verifier la connexion a la Base de donnee";
            return View();
        }

        public ActionResult ExporterBDAction()
        {

            Directory.CreateDirectory(Server.MapPath("~/App_Data/SavedDatabase"));

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string filenameconstruct = Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Hour) +
                                    Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) +
                                    Convert.ToString(DateTime.Now.Year);
            string directoryname = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) +
                                    Convert.ToString(DateTime.Now.Year);
            string destfinal = Path.Combine(Server.MapPath("~/App_Data/SavedDatabase/"), directoryname);
            Directory.CreateDirectory(destfinal);
            string destpath = Path.Combine(destfinal, filenameconstruct + "AITECRM2.bak");
            //backup the database test in the c:\backup folder
            SqlCommand cmd = new SqlCommand(@"BACKUP DATABASE [AITECRM2] TO " +
            @"DISK = '" + destpath + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("SuccesExporter");
        }

        public ActionResult ExporterBDAuto()
        {
            Directory.CreateDirectory(Path.GetFullPath(HostingEnvironment.MapPath("~/App_Data/SavedDatabase")));

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            string filenameconstruct = Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) +
                                    Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) +
                                    Convert.ToString(DateTime.Now.Year);
            string directoryname = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) +
                                    Convert.ToString(DateTime.Now.Year);
            string destfinal = Path.Combine(Path.GetFullPath(HostingEnvironment.
                MapPath(@"~/App_Data/SavedDatabase/")), directoryname);
            Directory.CreateDirectory(destfinal);
            string destpath = Path.Combine(destfinal, filenameconstruct + "AITECRM2.bak");
            //backup the database test in the c:\backup folder
            SqlCommand cmd = new SqlCommand(@"BACKUP DATABASE [AITECRM2] TO " +
            @"DISK = '" + destpath + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return Json(0, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult ImporterBDAction()
        {
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {

                string fileName = Path.GetFileName(file.FileName);
                string backupfile = Path.Combine(Server.MapPath("~/App_Data/import"), fileName);
                file.SaveAs(backupfile);
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["t1"]
                    .ConnectionString);
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
                   .ConnectionString);

                //string sqlStart1 = string.Format
                //    ("ALTER DATABASE ["+ con1.Database.ToString() +"] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ");
                //con1.Open();
                //SqlCommand b1 = new SqlCommand(sqlStart1, con1);
                //b1.ExecuteNonQuery();

                //string sqlstart2 = "USE MASTER RESTORE DATABASE ["+ con1.Database.ToString() +"] FROM DISK='" + backupfile + "'WITH REPLACE;";
                //SqlCommand b2 = new SqlCommand(sqlstart2, con1);
                //b2.ExecuteNonQuery();

                //string sqlstart3 = string.Format("ALTER DATABASE [" + con1.Database.ToString() + "] SET MULTI_USER");
                //SqlCommand b3 = new SqlCommand(sqlstart3, con1);
                //b3.ExecuteNonQuery();
                //con.Close();
                con.State.ToString();
                con1.Open();
                con1.State.ToString();
                string str = "USE master";
                string str1 = "ALTER DATABASE " + con1.Database.ToString() + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                string str2 = "RESTORE DATABASE " + con1.Database.ToString() + " FROM DISK = '" + backupfile + "' WITH REPLACE ";
                string str3 = "ALTER DATABASE [" + con1.Database.ToString() + "] SET MULTI_USER";

                SqlCommand b1 = new SqlCommand(str, con1);
                SqlCommand b2 = new SqlCommand(str1, con1);
                SqlCommand b3 = new SqlCommand(str2, con1);
                SqlCommand b4 = new SqlCommand(str3, con1);



                b1.ExecuteNonQuery();
                b2.ExecuteNonQuery();
                b3.ExecuteNonQuery();
                b4.ExecuteNonQuery();

                con1.Close();


                //con.Open();

                BackupController.ViderTablesDestination();
                BackupController.RemplirTablesDestination(con1, con);
                return RedirectToAction("SuccesImporter");
            }

            return RedirectToAction("SuccesImporter");

        }

        public static void ViderTablesDestination()
        {

            SqlConnection destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
               .ConnectionString);
            destination.Open();
            using (destination)
            {
                SqlCommand del1 = new SqlCommand("DELETE FROM ContactGroups", destination);
                SqlCommand del2 = new SqlCommand("DELETE FROM Contacts", destination);
                SqlCommand del3 = new SqlCommand("DELETE FROM Projets", destination);
                SqlCommand del4 = new SqlCommand("DELETE FROM Entreprises", destination);
                SqlCommand del5 = new SqlCommand("DELETE FROM Taches", destination);
                SqlCommand del6 = new SqlCommand("DELETE FROM Achats", destination);
                SqlCommand del7 = new SqlCommand("DELETE FROM EtatContacts", destination);
                SqlCommand del8 = new SqlCommand("DELETE FROM CategorieActiviteParentes", destination);
                SqlCommand del9 = new SqlCommand("DELETE FROM CategorieActivites", destination);
                SqlCommand del10 = new SqlCommand("DELETE FROM Activites", destination);
                SqlCommand del11 = new SqlCommand("DELETE FROM CategorieArticleParentes", destination);
                SqlCommand del12 = new SqlCommand("DELETE FROM CategorieArticles", destination);
                SqlCommand del13 = new SqlCommand("DELETE FROM Articles", destination);
                SqlCommand del14 = new SqlCommand("DELETE FROM VenteArticles", destination);

                del1.ExecuteNonQuery();
                del2.ExecuteNonQuery();
                del3.ExecuteNonQuery();
                del4.ExecuteNonQuery();
                del5.ExecuteNonQuery();
                del6.ExecuteNonQuery();
                del7.ExecuteNonQuery();
                del8.ExecuteNonQuery();
                del9.ExecuteNonQuery();
                del10.ExecuteNonQuery();
                del11.ExecuteNonQuery();
                del12.ExecuteNonQuery();
                del13.ExecuteNonQuery();
                del14.ExecuteNonQuery();

                destination.Close();

            }
        }

        public static void RemplirTablesDestination(SqlConnection source, SqlConnection destination)
        {


            using (source)
            {
                SqlCommand cmd = new SqlCommand("Select * from Entreprises", source);
                source.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Entreprises";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                        destination.Close();
                    }
                }
                
                    cmd = new SqlCommand("Select * from Contacts", source);
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                        {
                            using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                            {
                                bc.DestinationTableName = "Contacts";
                                destination.Open();
                                bc.WriteToServer(rdr);
                            }
                        }
                    }

                cmd = new SqlCommand("Select * from ContactGroups", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "ContactGroups";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from Projets", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Projets";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from Taches", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Taches";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from EtatContacts", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "EtatContacts";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from Achats", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Achats";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from CategorieActiviteParentes", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "CategorieActiviteParentes";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from CategorieActivites", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "CategorieActivites";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from Activites", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Activites";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from CategorieArticleParentes", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "CategorieArticleParentes";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from CategorieArticles", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "CategorieArticles";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from Articles", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "Articles";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                cmd = new SqlCommand("Select * from VenteArticles", source);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    destination = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"]
              .ConnectionString);
                    using (destination)
                    {
                        using (SqlBulkCopy bc = new SqlBulkCopy(destination))
                        {
                            bc.DestinationTableName = "VenteArticles";
                            destination.Open();
                            bc.WriteToServer(rdr);
                        }
                    }
                }

                source.Close();
                
                }

            }


        }
    }