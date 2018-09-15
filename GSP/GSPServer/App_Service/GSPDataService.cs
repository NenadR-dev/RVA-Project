using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSPPackage;
using GSPPackage.Enums;
using GSPPackage.Models;
using GSPServer.App_Service.Commands;
using GSPServer.App_Service.Commands.CommandTypes;
namespace GSPServer.App_Service
{
    public class GSPDataService : IGSPDataService
    {
        private static GSPDataAccess db = GSPDataAccess.Connect();
        private static int currentCommand = 0;
        public static List<Command> commands = new List<Command>();
        private static CommandExecutor Executor = new CommandExecutor();

        public bool ComputeCommandBus(Autobus Data, string command)
        {
            return CreateAndExecuteAction(Data, DataType.Bus, command);
        }

        public bool ComputeCommandDriver(Vozac Data, string command)
        {
            return CreateAndExecuteAction(Data, DataType.Driver, command);
        }

        public bool ComputeCommandLine(Linija Data, string command)
        {
            return CreateAndExecuteAction(Data, DataType.Line, command);
        }

        public bool ComputeCommandUser(MyUser Data, string command)
        {
            return CreateAndExecuteAction(Data, DataType.User, command);
        }

        private bool CreateAndExecuteAction(object Data, DataType type, string command)
        {
            try
            {
                Command com = null;
                switch (command)
                {
                    case "ADD":
                        {
                            com = new AddDataCommand(Executor, Data, type);
                            break;
                        }
                    case "DELETE":
                        {
                            com = new DeleteDataCommand(Executor, Data, type);
                            break;
                        }
                    case "UPDATE":
                        {
                            com = new UpdateDataCommand(Executor, Data, type);
                            break;
                        }
                    case "REVERT":
                        {
                            com = new RevertUpdateCommand(Executor, Data, type);
                            break;
                        }
                }
                com.Execute();
                if (commands.Count > currentCommand)
                {
                    commands.RemoveRange(currentCommand, commands.Count - currentCommand);
                }

                commands.Add(com);
                currentCommand++;

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Redo()
        {
            if (currentCommand + 1 <= commands.Count)
            {
                Command command = commands[currentCommand++];
                command.Execute();
                return true;
            }
            else
            {
                Console.WriteLine("Redo exceeds limit");
                return false;
            }
        }

        public bool Undo()
        {
            if (currentCommand > 0)
            {
                Command command = commands[--currentCommand];
                command.UnExecute();
                return true;
            }
            else
            {
                Console.WriteLine("Undo exceeds limit");
                return false;
            }
        }




        //public bool DeleteBus(Autobus data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            db.dbSetAutobus.ToList().Remove(data);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return false;
        //        }
        //    }
        //}

        //public bool DeleteDriver(Vozac data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            db.dbSetVozac.ToList().Remove(data);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch(Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return false;
        //        }
        //    }
        //}

        //public bool DeleteLine(Linija data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            db.dbSetLinija.ToList().Remove(data);
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch(Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //            return false;
        //        }
        //    }
        //}

        //public bool UpdateDriver(Vozac data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            Vozac v = db.dbSetVozac.ToList().Find(x => x.ID == data.ID);

        //            v.Ime = data.Ime;
        //            v.Prezime = data.Prezime;
        //            v.LinijaID = data.LinijaID;
        //            v.Oznaka = data.Oznaka;
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool UpdateLine(Linija data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            Linija l = db.dbSetLinija.ToList().Find(x => x.ID == data.ID);
        //            l.Oznaka = data.Oznaka;
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }
        //}

        //public bool UpdateBus(Autobus data)
        //{
        //    lock (db)
        //    {
        //        try
        //        {
        //            Autobus a = db.dbSetAutobus.ToList().Find(x => x.ID == data.ID);
        //            a.LinijaID = data.LinijaID;
        //            a.Oznaka = data.Oznaka;
        //            db.SaveChanges();
        //            return true;
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
