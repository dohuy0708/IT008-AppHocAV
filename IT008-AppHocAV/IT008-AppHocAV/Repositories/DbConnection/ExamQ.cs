﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using IT008_AppHocAV.Models;
using IT008_AppHocAV.Util;
using PexelsDotNetSDK.Models;

namespace IT008_AppHocAV.Repositories.DbConnection
{
    public class ExamQ
    {
        private readonly SqlConnection _sqlConnection;

        public ExamQ(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }
        public List<Question> GetRandomQuestion()
        {   
            List<Question> result = new List<Question>();
            Random random = new Random();
            HashSet<int> questionIds = new HashSet<int>();
            string queslist = "(";
            int a = random.Next(1, 81);
            questionIds.Add(a);
            queslist += a.ToString();
            while (questionIds.Count < 20) 
            {
                int newId = random.Next(1, 81);
                if (!questionIds.Contains(newId)) 
                {
                    questionIds.Add(newId); 
                    queslist += "," + newId;
                }
            }
            queslist += ")";
            try
            {
                string query = "Select * from question where id in"+queslist;
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    _sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Question question = new Models.Question(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("content")),
                                reader.GetString(reader.GetOrdinal("answer_a")),
                                reader.GetString(reader.GetOrdinal("answer_b")),
                                reader.GetString(reader.GetOrdinal("answer_c")),
                                reader.GetString(reader.GetOrdinal("answer_d")),
                                reader.GetString(reader.GetOrdinal("correct_answer")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")),
                                reader.GetDateTime(reader.GetOrdinal("updated_at"))
                            );
                            result.Add(question);
                        }
                    }

                }
                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;

            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        public List<Models.Exam> SelectListExamByUserId(int userId)
        {
            List<Models.Exam> result = new List<Models.Exam>();
            try
            {
                string query = " SELECT id, level,score,created_at" +
                               " FROM [test] " +
                               " WHERE user_id = " + userId + " ORDER BY created_at desc";
                using (SqlCommand command = new SqlCommand(query, _sqlConnection))
                {
                    _sqlConnection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Exam Exam = new Models.Exam(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetByte(reader.GetOrdinal("level")),
                                reader.GetByte(reader.GetOrdinal("score")),
                                reader.GetDateTime(reader.GetOrdinal("created_at")).ToString());
                            result.Add(Exam);
                        }
                    }

                }
                return result;
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
        }

    }
}