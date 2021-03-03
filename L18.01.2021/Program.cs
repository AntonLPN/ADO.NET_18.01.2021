using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace L18._01._2021
{
    /// <summary>
    /// Класс для работы с базой library
    /// </summary>
    static class DBLibrary
    {


        private static SqlConnection sqlConection=new SqlConnection();
        private static string ConnectionString= @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Library; Integrated Security=SSPI";
        private static  SqlDataReader reader;

        /// <summary>
        /// Метод вставки данных в таблицу авторов
        /// </summary>
        /// <param name="nameTable"></param>
        /// <param name="ConectionString"></param>
        public static void InsertAuthors(string FirstName, string LastName)
        {

            sqlConection.ConnectionString = ConnectionString;
            try
            {
                sqlConection.Open();
                SqlCommand command = new SqlCommand();
             
                //форматирована строка для облегчения вноса данных
                FormattableString insert = $@"insert into Authors (FirstName, LastName) values ('{FirstName}','{LastName}')";

                command.Connection = sqlConection;
                command.CommandText=insert.ToString() ;
                command.ExecuteNonQuery();             
            }
            finally
            {

                if (sqlConection!=null)
                {
                    sqlConection.Close();
                    Console.WriteLine("Conection for insert close");
                }
               
            }

        }
        /// <summary>
        /// Метод вывода таблицы авторов
        /// </summary>
        public static void SelectALLAuthors()
        {
            sqlConection.ConnectionString = ConnectionString;          
            try
            {
                sqlConection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConection;
                command.CommandText = @"select *from Authors";
                reader = command.ExecuteReader();

                //проверка на наличие строк
                if (reader.HasRows)
                {
                    Console.WriteLine("id FirstName  LastName");
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetValue(0) + "  " + reader.GetValue(1) + "         " + reader.GetValue(2));
                    }

                }

            }
            finally
            {
                try
                {
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }              
                if (sqlConection!=null)
                {
                    sqlConection.Close();
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //добавляем в библиотеку людей
            //DBLibrary.InsertAuthors("Bob", "Dilan");
            //DBLibrary.InsertAuthors("Tom", "Hanks");
            //DBLibrary.InsertAuthors("Till", "Lindeman");
            //DBLibrary.InsertAuthors("Test", "Insert");
            //------------------------------------------------------------------------------------

            //Выводим таблицу авторов
            DBLibrary.SelectALLAuthors();


        }
    }
}
