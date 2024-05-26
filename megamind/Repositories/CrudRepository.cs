using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using megamind.Models;
using Npgsql;
using NpgsqlTypes;

namespace megamind.Repositories
{
    public class CrudRepository
    {
        private readonly NpgsqlConnection _conn;
        public CrudRepository(NpgsqlConnection conn)
        {
            _conn = conn;
        }

        public List<tblcrud> GetAll()
        {
            List<tblcrud> cruds = new List<tblcrud>();
            try
            {
                _conn.Open();
                using var command = new NpgsqlCommand("select * from t_person",_conn);

                using var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    tblcrud crud = new tblcrud()
                    {
                        c_id = Convert.ToInt32(reader["c_id"]),
                        c_name = reader["c_name"].ToString(),
                        c_email = reader["c_email"].ToString(),
                        c_phone = reader["c_phone"].ToString(),
                        c_address = reader["c_address"].ToString(),
                        c_sid = Convert.ToInt32(reader["c_sid"]),
                        c_cid = Convert.ToInt32(reader["c_cid"]),
                        c_isagree = Convert.ToBoolean(reader["c_isagree"]) 
                    };
                    cruds.Add(crud);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return cruds;
        }

        public void Insert(tblcrud crud)
        {
            try
            {
                _conn.Open();
                using var command = new NpgsqlCommand("insert_t_person",_conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_c_name",crud.c_name);
                command.Parameters.AddWithValue("p_c_email",crud.c_email);
                command.Parameters.AddWithValue("p_c_phone",crud.c_phone);
                command.Parameters.AddWithValue("p_c_address", NpgsqlDbType.Varchar, crud.c_address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p_c_sid",crud.c_sid);
                command.Parameters.AddWithValue("p_c_cid",crud.c_cid);
                command.Parameters.AddWithValue("p_c_isagree",crud.c_isagree);

                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Update(tblcrud crud)
        {
            try
            {
                _conn.Open();
                using var command = new NpgsqlCommand("update_t_person",_conn);
                
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("p_c_id",crud.c_id);
                command.Parameters.AddWithValue("p_c_name",crud.c_name);
                command.Parameters.AddWithValue("p_c_email",crud.c_email);
                command.Parameters.AddWithValue("p_c_phone",crud.c_phone);
                command.Parameters.AddWithValue("p_c_address", NpgsqlDbType.Varchar, crud.c_address ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("p_c_sid",crud.c_sid);
                command.Parameters.AddWithValue("p_c_cid",crud.c_cid);
                command.Parameters.AddWithValue("p_c_isagree",crud.c_isagree);

                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                _conn.Open();
                using var command = new NpgsqlCommand("delete_t_person",_conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("p_c_id",id);

                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }

        public List<tblstate>GetallState()
        {
            List<tblstate> states = new List<tblstate>();
            try
            {
                
                _conn.Open();
                using var command = new NpgsqlCommand("select * from t_state ",_conn);
                using var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    tblstate state = new tblstate()
                    {
                        c_sid = Convert.ToInt32(reader["c_sid"]),
                        c_sname = reader["c_sname"].ToString()
                    };
                    states.Add(state);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return states;
        }

        public List<tblcity> GetAllCity(int id)
        {
            List<tblcity> citys = new List<tblcity>();
            try
            {
                _conn.Open();
                using var command = new NpgsqlCommand("SELECT c_cid, c_cname, c_sid FROM public.t_city where c_sid = @id;",_conn);
                command.Parameters.AddWithValue("@id",id);
                using var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    tblcity city = new tblcity()
                    {
                        c_cid = Convert.ToInt32(reader["c_cid"]),
                        c_cname = reader["c_cname"].ToString()
                    };
                    citys.Add(city);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return citys;
        }
    }
}