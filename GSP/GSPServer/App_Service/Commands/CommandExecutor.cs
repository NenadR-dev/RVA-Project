using GSPPackage.Enums;
using GSPPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service.Commands
{
    public class CommandExecutor
    {
        private static GSPDataAccess db = GSPDataAccess.Connect();

        public void AddData(object Data, DataType type)
        {
            lock (db)
            {
                if (type == DataType.Bus)
                {
                    Autobus bus = (Autobus)Data;
                    db.dbSetAutobus.Add(bus);
                    db.SaveChanges();
                }
                else if(type == DataType.Driver)
                {
                    db.dbSetVozac.Add((Vozac)Data);
                    db.SaveChanges();
                }
                else if(type == DataType.Line)
                {
                    Linija l = (Linija)Data;
                    db.dbSetLinija.Add((Linija)Data);
                    db.SaveChanges();
                }
                else if(type == DataType.User)
                {
                    db.dbSetUser.Add((MyUser)Data);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error occured at adding Data into Database");
                }
            }
            
        }

        public void DeleteData(object Data, DataType type)
        {
            lock (db)
            {
                if (type == DataType.Bus)
                {
                    Autobus b = db.dbSetAutobus.ToList().Find(x => x.ID == ((Autobus)Data).ID);
                    db.dbSetAutobus.Remove(b);
                    db.SaveChanges();
                }
                else if (type == DataType.Driver)
                {
                    Vozac v = db.dbSetVozac.ToList().Find(x => x.ID == ((Vozac)Data).ID);
                    db.dbSetVozac.Remove(v);
                    db.SaveChanges();
                }
                else if (type == DataType.Line)
                {
                    Linija l = db.dbSetLinija.ToList().Find(x => x.ID == ((Linija)Data).ID);
                    db.dbSetLinija.Remove(l);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error occured at adding Data into Database");
                }
            }
        }

        public void UpdateData(object Data, DataType type)
        {
            lock (db)
            {
                if (type == DataType.Bus)
                {
                    db.dbSetAutobus.ToList().Find(x => x.ID == ((Autobus)Data).ID).Oznaka = ((Autobus)Data).Oznaka;
                    db.SaveChanges();
                }
                else if (type == DataType.Driver)
                {
                    db.dbSetVozac.ToList().Find(x => x.ID == ((Vozac)Data).ID).Oznaka = ((Vozac)Data).Oznaka;
                    db.dbSetVozac.ToList().Find(x => x.ID == ((Vozac)Data).ID).Ime = ((Vozac)Data).Ime;
                    db.dbSetVozac.ToList().Find(x => x.ID == ((Vozac)Data).ID).Prezime = ((Vozac)Data).Prezime;
                    db.SaveChanges();
                }
                else if (type == DataType.Line)
                {
                    db.dbSetLinija.ToList().Find(x => x.ID == ((Linija)Data).ID).Oznaka = ((Linija)Data).Oznaka;
                    db.dbSetLinija.ToList().Find(x => x.ID == ((Linija)Data).ID).M_Autobus = ((Linija)Data).M_Autobus;
                    db.dbSetLinija.ToList().Find(x => x.ID == ((Linija)Data).ID).Vozaci = ((Linija)Data).Vozaci;

                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Error occured at adding Data into Database");
                }
            }
        }
    }
}
