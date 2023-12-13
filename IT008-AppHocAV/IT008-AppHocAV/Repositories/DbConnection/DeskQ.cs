﻿using IT008_AppHocAV.Models;
using IT008_AppHocAV.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace IT008_AppHocAV.Repositories.DbConnection
{
    public class DeskQ
    {

        private readonly SqlConnection _sqlConnection;

        public DeskQ(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public int CreateDesk(Models.ListFlashCard desk)
        {
            try
            {
                string query =
                    " INSERT INTO desk (user_id ,title, description, quantity, updated_at, created_at ) " +
                    " output inserted.id " +
                    " VALUES " +
                    " (@user_id, @title, @description , @quantity , GETDATE(), GETDATE()) ";
            
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@user_id", desk.UserId);
                    command.Parameters.AddWithValue("@title", desk.Title);
                    command.Parameters.AddWithValue("@description", desk.Description);
                    command.Parameters.AddWithValue("@quantity", desk.Quantity);

                     _sqlConnection.Open();
                    int modified = (int)command.ExecuteScalar();
                    if (_sqlConnection.State == System.Data.ConnectionState.Open)
                        _sqlConnection.Close();
                    return modified;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            finally
            {
                _sqlConnection.Close();
            }
        }
        public void UpdateDeskContent(int id ,int quantity, string title, string description)
        {
            try
            {
                string query = "UPDATE desk"+
                                " SET title = @title , description = @description , quantity = @quantity , updated_at= GETDATE()"+
                                "WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@description",description);
                    command.Parameters.AddWithValue("@quantity",quantity);
                    _sqlConnection.Open();
                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
        public ListFlashCard SelectDeskById ( int id)
        {
            try
            {

                string query = " SELECT *" +
                               " FROM [desk] " +
                               " WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    _sqlConnection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {



                            Models.ListFlashCard desk = new Models.ListFlashCard(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("user_id")),
                                reader.GetString(reader.GetOrdinal("title")),

                                reader.GetString(reader.GetOrdinal("description")),

                                reader.GetInt32(reader.GetOrdinal("quantity")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at")));
                            
                          

                            return desk;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return null;
        }
        public bool DeleteDeskById(int id)
        {
            try
            {
                string query = "DELETE [desk] WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    _sqlConnection.Open();
                    command.ExecuteScalar();
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }


    }
}
