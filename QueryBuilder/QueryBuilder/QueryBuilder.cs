using System;
using Microsoft.Data.Sqlite;
using QueryBuilder.Models;

namespace QueryBuilder
{
	public class QueryBuilder : IDisposable
	{
		private SqliteConnection connection;

		public QueryBuilder(string dbPath)
		{
			connection = new SqliteConnection(dbPath);
			connection.Open();
		}

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

		

		public void Dispose()
		{
			connection.Close();
		}
	}
}

