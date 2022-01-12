using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamBrianMolina.Models;

namespace XamBrianMolina.Data
{
    public class DBContext
    {
        private readonly SQLiteAsyncConnection connection;
        /// <summary>
        /// Se solicita como parametro el Path de la Base de datos y luego se procde a crear la  tabla segun el modelo especificado
        /// </summary>
        /// <param name="DBPath"></param>
        public DBContext(string DBPath)
        {
            connection = new SQLiteAsyncConnection(DBPath);
            connection.CreateTableAsync<taskModel>().Wait();
        }
        /// <summary>
        /// Se genera el metodo para obtener todos los items de la tabla en una lista generica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await connection.Table<T>().ToListAsync();
        }


        /// <summary>
        /// Se genera el metodo para filtrar  los items de la tabla en una lista generica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<List<T>> FilterItemsAsync<T>(string table, string condition) where T : new()
        {
            return await connection.QueryAsync<T>($"SELECT * FROM {table} WHERE {condition}");
        }
        /// <summary>
        /// Se genra el metodo para obtener un item segun su ID
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetItemAsync<T>(string id) where T : new()
        {
            return await connection.FindAsync<T>(id);
        }
        /// <summary>
        /// Se genera el metodo para guardar el item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="isInsert"></param>
        /// <returns></returns>
        public async Task<int> SaveItemAsync<T>(T item, bool isInsert) where T : new()
        {
            return (isInsert)
                ? await connection.InsertAsync(item)
                : await connection.UpdateAsync(item);
        }
        /// <summary>
        /// Metodo para eleiminar el item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return await connection.DeleteAsync(item);
        }
    }
}
