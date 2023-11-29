using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Curso_Tracker.Models;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace Curso_Tracker.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath) 
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Empleado>().Wait();
            db.CreateTableAsync<Cursos>().Wait();
            db.CreateTableAsync<Relacion>().Wait();

        }


        //-------------------------------------
        public Task<int> SaveEmpleadoAsync(Empleado emp)
        {
           
            if (emp.Id != 0)
            {

                return db.UpdateAsync(emp);
            }
            else
            {
                return db.InsertAsync(emp);
            }

        }


        public Task<List<Empleado>> GetEmpleadosAsync()
        {
            return db.Table<Empleado>().ToListAsync();
        }

        public Task<Empleado> GetEmpleadoById(int id)
        {
            return db.Table<Empleado>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        //-------------------------------------
        public Task<int> SaveEmpleadoAsync(Cursos curso)
        {
            

            if (curso.Id != 0)
            {

                return db.UpdateAsync(curso);
            }
            else
            {
                return db.InsertAsync(curso);
            }
        }


        public Task<List<Cursos>> GetCursoAsync()
        {
            return db.Table<Cursos>().ToListAsync();
        }

        public Task<Cursos> GetCursoById(int id)
        {
            return db.Table<Cursos>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }


        //-------------------------------------
        public Task<int> SaveRelacionAsync(Relacion relacion)
        {

            if(relacion.Id != 0)
            {
                
                return db.UpdateAsync(relacion);
            }
            else
            {
                return db.InsertAsync(relacion);
            }
        }
        public Task<int> DeleteRelacionAsync(int id)
        {
            return db.DeleteAsync<Relacion>(id);
        }


        public Task<List<Relacion>> GetRelacionAsync()
        {
            return db.Table<Relacion>().ToListAsync();
        }

        public Task<Relacion> GetRelacionById(int id)
        {
            return db.Table<Relacion>().Where(d => d.Id == id).FirstOrDefaultAsync();
        }


    }
}
