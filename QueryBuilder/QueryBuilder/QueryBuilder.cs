using System;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class QueryBuilder : IDisposable
	{
		private SqliteConnection connection;


		public QueryBuilder()
		{

		}

		/// <summary>
		/// Opens the database connection
		/// </summary>
		/// <param name="dbPath"></param>
		public QueryBuilder(string dbPath)
		{
			connection = new SqliteConnection(dbPath);
			connection.Open();
		}

		/// <summary>
		/// Reads all content from a database
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public List<T> ReadAll<T>() where T: IClassModels, new()
		{
			var dataString = "";
			var command = connection.CreateCommand();

			command.CommandText = $"SELECT * FROM {typeof(T).Name}";

			var reader = command.ExecuteReader();

			var list = new List<T>();

			T data;

			while(reader.Read())
			{
				data = new T();
				for(int i = 0; i < reader.FieldCount; i++)
				{
					var propertyType = typeof(T).GetProperty(reader.GetName(i)).PropertyType;
					var propertyName = typeof(T).GetProperty(reader.GetName(i));

					if(propertyType == typeof(int))
					{
                        propertyName.SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }
					else
					{
						propertyName.SetValue(data, reader.GetValue(i));
					}
					
                }
                list.Add(data);
            }
			return list;
		}


		public void Create<T>(T obj) where T : IClassModels, new()
		{
			var command = connection.CreateCommand();
			command.CommandText = $"INSERT INTO {typeof(T).Name}";
		}
		/// <summary>
		/// Reads a specific object from the database
		/// given an Id
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Id"></param>
		/// <returns></returns>
        public T Read<T>(int Id) where T : IClassModels, new()
        {
			var command = connection.CreateCommand();
			command.CommandText = $"SELECT * FROM {typeof(T).Name} WHERE Id = $id";

			command.Parameters.AddWithValue("$id", Id);
			T data = new T();
            var reader = command.ExecuteReader();

			while(reader.Read())
			{
				//data = new T();
				for(int i = 0; i < reader.FieldCount; i++)
				{
					var propertyType = typeof(T).GetProperty(reader.GetName(i)).PropertyType;
					var propertyName = typeof(T).GetProperty(reader.GetName(i));

                    if (propertyType == typeof(int))
                    {
                        propertyName.SetValue(data, Convert.ToInt32(reader.GetValue(i)));
                    }
                    else
                    {
                        propertyName.SetValue(data, reader.GetValue(i));			
                    }
                }
            }
			return data;
            
        }

		public void Delete<T>() where T : IClassModels, new()
		{
			var command = connection.CreateCommand();
			command.CommandText = $"DELETE FROM {typeof(T).Name}";

			command.CommandType = System.Data.CommandType.Text;
			command.ExecuteNonQuery();
		}

        public void Dispose()
		{
			connection.Close();
		}
	}
}

