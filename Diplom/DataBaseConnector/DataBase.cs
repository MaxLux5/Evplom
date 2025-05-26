using Dapper;
using MySqlConnector;
using System.Windows;

namespace Diplom.DataBaseConnector
{
    /// <summary>
    /// Singleton-класс для обращения в базу данных.
    /// </summary>
    public class DataBase
    {
        private static DataBase _instance;
        private const string _connectionString = "Server=localhost; Username=root; Password=24681357; Database=Warehouse";


        private DataBase() { }


        public static DataBase GetInstance()
        {
            return _instance ??= new DataBase();
        }

        /// <summary>
        /// Проверяет есть ли пользователь в базе данных.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Возвращает true, если пользователь есть в базе данных, иначе возвращает false.</returns>
        public bool Login(string login, string password)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    var queryString =
                    $"""
                    select count(*) from auths
                    where auths.login = '{login}'
                    and auths.password = '{password}'
                    """;

                    var result = connection.ExecuteScalar<int>(queryString);
                    if (result > 0) return true;
                    else return CallErrorMessage();
                }
                catch (Exception)
                {
                    return CallErrorMessage();
                }
            }

            bool CallErrorMessage()
            {
                MessageBox.Show("Такого пользователя не существует.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}
