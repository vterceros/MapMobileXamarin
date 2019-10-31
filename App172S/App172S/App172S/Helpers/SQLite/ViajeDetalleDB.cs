using App172S.Interfaces;
using App172S.Models.Negocio;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace App172S.Helpers.SQLite
{
    public class ViajeDetalleDB
    {
        static object locker = new object();

        SQLiteConnection db;

        public ViajeDetalleDB()
        {
            db = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            db.CreateTable<del_UsuarioViajeDetalle>();
        }

        public List<del_UsuarioViajeDetalle> GetAll()
        {
            lock (locker)
            {
                return (from i in db.Table<del_UsuarioViajeDetalle>() select i).ToList();
            }
        }

        public del_UsuarioViajeDetalle GetItem(int id)
        {
            lock (locker)
            {
                return db.Table<del_UsuarioViajeDetalle>().FirstOrDefault(x => x.iUsuarioViajeDetalle_id == id);
            }
        }

        public IEnumerable<del_UsuarioViajeDetalle> BuscarArticulos(string busqueda)
        {
            lock (locker)
            {
                //Se coloca todo en minuscula
                busqueda = busqueda.ToLower();
                busqueda = busqueda.Replace(' ', '%');
                return (db.Query<del_UsuarioViajeDetalle>(
                    "SELECT * FROM ViajeDetalleDB WHERE artId LIKE '%"
                    + busqueda + "%' OR artNombre LIKE '%" + busqueda + "%'"
                    + " ORDER BY artNombre")).ToList();
            }
        }

        public int SaveItem(del_UsuarioViajeDetalle item)
        {
            lock (locker)
            {            
                return db.Insert(item);
            }
        }

        public int SaveAll(List<del_UsuarioViajeDetalle> list)
        {
            lock (locker)
            {
                return db.InsertAll(list);
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return db.Delete<del_UsuarioViajeDetalle>(id);
            }
        }

        public void DropTable()
        {
            lock (locker)
            {
                // drop the table
                db.DropTable<del_UsuarioViajeDetalle>();
                // create the tables
                db.CreateTable<del_UsuarioViajeDetalle>();
            }
        }
    }
}
