using System;
using System.Reflection;
using System.Text;
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



		public void Create<T>(T obj) where T : IClassModels, new()
		{
			var command = connection.CreateCommand();
			PropertyInfo[] properties = typeof(T).GetProperties();
			List<string> values = new List<string>();
			List<string> names = new List<string>();

			// loop through the properties of each object
			foreach(PropertyInfo property in properties)
			{
				if(property.PropertyType == typeof(string))
				{
					values.Add("\"" + property.GetValue(obj) + "\"");
				}
				else
				{
					values.Add(property.GetValue(obj).ToString());
				}
				
				names.Add(property.Name);
			}

			StringBuilder sbValues = new StringBuilder();
            StringBuilder sbNames = new StringBuilder();

			for(int i = 0; i < values.Count; i++)
			{
				// do not add a comma after the last value
				if (i == values.Count - 1)
				{
                    sbValues.Append($"{values[i]}");
                    sbNames.Append($"{names[i]}");
				}
				else
				{
                    sbValues.Append($"{values[i]}, ");
                    sbNames.Append($"{names[i]}, ");
                }
			}

			command.CommandText = $"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues})";

			//Console.WriteLine($"INSERT INTO {typeof(T).Name} ({sbNames}) VALUES ({sbValues});");
			command.ExecuteNonQuery();
			Console.WriteLine("Record has been added.");
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


		public void Create(Author a)
		{
			//var command = connection.CreateCommand();
			var commandText = @"INSERT INTO Author
								VALUES($Id, $FirstName, $SurName)";
			//command.CommandText = $"INSERT INTO {typeof(T).Name}";

			using (var command = new SqliteCommand(commandText, connection))
			{
				command.Parameters.AddWithValue("$Id", a.Id).SqliteType = SqliteType.Integer;
				command.Parameters.AddWithValue("$FirstName", a.FirstName).SqliteType = SqliteType.Text;
				command.Parameters.AddWithValue("$SurName", a.SurName).SqliteType = SqliteType.Text;
				command.ExecuteNonQuery();
				Console.WriteLine("Record has been added!");
            }
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

		/// <summary>
		/// Deletes a table from the database
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void Delete<T>(T obj) where T : IClassModels, new()
		{
			var command = connection.CreateCommand();
			command.CommandText = $"DELETE FROM {typeof(T).Name} WHERE Id = {obj.Id}";

			command.CommandType = System.Data.CommandType.Text;
			command.ExecuteNonQuery();
			Console.WriteLine("Entry deleted.");
		}

		public void Update<T>(T obj) where T : IClassModels, new()
		{
			var command = connection.CreateCommand();
			PropertyInfo[] properties = typeof(T).GetProperties();
			List<string> values = new List<string>();
			List<string> names = new List<string>();

			// loop through the properties of each object
			foreach (PropertyInfo property in properties)
			{
				if (property.PropertyType == typeof(string))
				{
					values.Add("\"" + property.GetValue(obj) + "\"");
				}
				else
				{
					values.Add(property.GetValue(obj).ToString());
				}

				names.Add(property.Name);
			}

			StringBuilder sbValues = new StringBuilder();
			StringBuilder sbNames = new StringBuilder();

			for (int i = 0; i < values.Count; i++)
			{
				// do not add a comma after the last value
				if (i == values.Count - 1)
				{
					sbValues.Append($"{values[i]}");
					sbNames.Append($"{names[i]}");
				}
				else
				{
					sbValues.Append($"{values[i]}, ");
					sbNames.Append($"{names[i]}, ");
				}
			}
			
            command.CommandText = $"UPDATE {typeof(T).Name} SET ({sbNames}) = ({sbValues}) WHERE Id = {obj.Id}";
			command.ExecuteNonQuery();
		}

		/// <summary>
		/// Updates a record from the database
		/// </summary>
		/// <param name="a"></param>
		/// <param name="Id"></param>
		/// 
		public void Update<T>(Author a, int Id) where T : IClassModels, new()
		{
			var commandText = @"UPDATE Author
								SET Id = $Id,
								FirstName = $FirstName,
								SurName = $SurName
								WHERE Id = $Id";

            using (var command = new SqliteCommand(commandText, connection))
            {
                command.Parameters.AddWithValue("$Id", a.Id).SqliteType = SqliteType.Integer;
                command.Parameters.AddWithValue("$FirstName", a.FirstName).SqliteType = SqliteType.Text;
                command.Parameters.AddWithValue("$SurName", a.SurName).SqliteType = SqliteType.Text;
                command.ExecuteNonQuery();
                Console.WriteLine("Record has been updated!");
            }

        }

        public void Dispose()
		{
			connection.Close();
		}
	}
}

