using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSPServer.App_Service;
using GSPPackage.Models;

namespace GSPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerStartUp();
            InitializeData();
            Console.ReadKey(true);
        }

        private static void InitializeData()
        {
            GSPDataAccess db = GSPDataAccess.Connect();

            lock (db)
            {
                if (db.dbSetVozac.ToList().Count == 0 && db.dbSetLinija.ToList().Count == 0 && db.dbSetAutobus.ToList().Count == 0)
                {
                    db.dbSetAutobus.Add(new Autobus()
                    {
                        Oznaka = "B190"
                    });

                    db.dbSetAutobus.Add(new Autobus()
                    {
                        Oznaka = "B160"
                    });

                    db.dbSetAutobus.Add(new Autobus()
                    {
                        Oznaka = "B140"
                    });

                    db.dbSetAutobus.Add(new Autobus()
                    {
                        Oznaka = "B110"
                    });

                    db.dbSetVozac.Add(new Vozac()
                    {
                        Ime = "Pera",
                        Prezime = "Peric",
                        Oznaka = "VZ120"
                    });

                    db.dbSetVozac.Add(new Vozac()
                    {
                        Ime = "Zika",
                        Prezime = "Zikic",
                        Oznaka = "VZ202"
                    });

                    db.dbSetVozac.Add(new Vozac()
                    {
                        Ime = "Mile",
                        Prezime = "Ajkula",
                        Oznaka = "VZ390"
                    });

                    db.dbSetLinija.Add(new Linija()
                    {
                        Oznaka = "L123",
                        Vozaci = new List<Vozac>()
                        {
                            new Vozac()
                            {
                                Ime = "Sima",
                                Prezime = "Simic",
                                Oznaka = "OV131"
                            },
                            new Vozac()
                            {
                                Ime = "Jovan",
                                Prezime = "Jovanovic",
                                Oznaka = "Zmaj"
                            }
                        },
                        M_Autobus = new List<Autobus>()
                        {
                            new Autobus()
                            {
                                Oznaka = "A121"
                            },
                            new Autobus()
                            {
                                Oznaka = "A144"
                            }
                        }
                    });

                    db.SaveChanges();
                }
            }
        }

        private static void ServerStartUp()
        {
            try
            {
                GSPServerService server = new GSPServerService();
                server.Open();
            }   
            catch(Exception e)
            {
                Console.WriteLine("Error occured:\n" + e.Message);
            }
        }
    }
}
