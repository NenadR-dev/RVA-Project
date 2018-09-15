using GSPPackage;
using GSPPackage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSPServer.App_Service
{
    public class GSPService : IGSPService
    {
        private static GSPDataAccess db = GSPDataAccess.Connect();
        private static MyUser _currentLoggedUser = null;
        private static readonly Dictionary<string, MyUser> _loggedUsers = new Dictionary<string, MyUser>();

        public bool addNewEntity(string data)
        {
            lock (db)
            {
                try
                {
                    string[] parts = data.Split(';');
                    if (parts.Contains("Autobus"))
                    {
                        db.dbSetAutobus.Add(new Autobus()
                        {
                            Oznaka = parts[0],
                        });
                        db.SaveChanges();
                        return true;
                    }
                    if (parts.Contains("Sofer"))
                    {
                        db.dbSetVozac.Add(new Vozac()
                        {
                            Oznaka = parts[0],
                            Ime = parts[1],
                            Prezime = parts[2],
                        });
                        db.SaveChanges();
                        return true;
                    }
                    if(parts.Contains("User"))
                    {
                        db.dbSetUser.Add(new MyUser()
                        {
                            FirstName = parts[2],
                            LastName = parts[3],
                            Username = parts[0],
                            Password = parts[1]
                        });
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        public List<Linija> GetLinijaList()
        {
            lock(db)
            {
                return db.dbSetLinija.ToList();
            }
        }

        public MyUser GetEntity(string key)
        {
            if (_loggedUsers.Keys.Contains(key))
            {
                return _loggedUsers[key];
            }
            else
            {
                return null;
            }
        }

        public void Logout(string key)
        {
            if (_loggedUsers.Keys.Contains(key))
            {
                _loggedUsers.Remove(key);
            }
        }

        public string Login(string user,string pass)
        {
            if(VerifyAdmin(user,pass))
            {
                if (!_loggedUsers.ContainsKey("admin"))
                {
                    _loggedUsers.Add("admin", _currentLoggedUser);
                    return "Success;Admin";
                }
                else
                {
                    return "Error";
                }
            }
            else if(VerifyUser(user,pass))
            {
                if (!_loggedUsers.ContainsKey("User" + _currentLoggedUser.ID))
                {
                    _loggedUsers.Add("User" + _currentLoggedUser.ID, _currentLoggedUser);
                    return "Success;User" + _currentLoggedUser.ID;
                }
                else
                {
                    return "Error";
                }
            }
            else
            {
                return "Error";
            }
        }

        private bool VerifyUser(string user, string pass)
        {
            lock(db)
            {
                if(db.dbSetUser.ToList().Exists(x=> x.Username == user && x.Password == pass))
                {
                    _currentLoggedUser = db.dbSetUser.ToList().Find(x => x.Username == user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool modifyEntity(string[] data)
        {
            lock (db)
            {
                if(db.dbSetUser.ToList().Exists(x=> x.Username == _currentLoggedUser.Username))
                {
                    MyUser user = db.dbSetUser.ToList().Find(x => x.Username == _currentLoggedUser.Username);
                    user.Username = data[0];
                    user.Password = data[1];
                    user.FirstName = data[2];
                    user.LastName = data[3];
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool removeEntity(string user)
        {
            lock (db)
            {
                if(db.dbSetUser.ToList().Exists(x=> x.Username == user))
                {
                    db.dbSetUser.ToList().Remove(db.dbSetUser.ToList().Find(x => x.Username == user));
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool VerifyAdmin(string user,string pass)
        {

            if (string.Equals(user, "admin") && string.Equals(pass, "admin"))
            {
                _currentLoggedUser = GSPServerService.users[0];
                return true;
            }
            return false;
        }

        public List<Autobus> GetAutobusList()
        {
            lock (db)
            {
                return GSPDataAccess.Connect().dbSetAutobus.ToList();
            }
        }
        
        public List<Vozac> GetSoferList()
        {
            lock (db)
            {
                return GSPDataAccess.Connect().dbSetVozac.ToList();
            }
        }

    }
}
