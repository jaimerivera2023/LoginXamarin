using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace LoginApp.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection _connection;

        public DataBase() { }

        public DataBase(string path) 
        { 
            _connection = new SQLiteAsyncConnection(path); 
            // Creacion de objetos de base de datos
            _connection.CreateTableAsync<model.Contactos>().Wait();
        }

        // Crear metodos CREATE - READ - UPDATE - DELETE

        public Task<int> AddContact(model.Contactos contactos)
        {
            if (contactos.Id == 0)
            {
                return _connection.InsertAsync(contactos);
            }
            else
            {
                return _connection.UpdateAsync(contactos);
            }
        } 

        public Task<List<model.Contactos>> GetAllPersonas() 
        {
            return _connection.Table<model.Contactos>().ToListAsync();
        }

        public Task<model.Contactos> GetPersona(int pid) 
        {
            return _connection.Table<model.Contactos>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        public Task<int> DeletePersona(model.Contactos personas) 
        {
            return _connection.DeleteAsync(personas);
        }

    }
}
