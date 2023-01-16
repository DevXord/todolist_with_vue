using MySql.Data.MySqlClient;
using System.Data;
 
namespace ToDoAPI.Models
{
    public class TasksModel
    {

        public class TaskToDo
        {
          public int ID_Task { get; set; }
          public string TaskText { get; set; }
          public DateTime? DataEnd { get; set; }
          public DateTime? DataCreate { get; set; }
          public bool IsDone { get; set; }
        }


        public static List<TaskToDo> GetTasks()
        {
            List<TaskToDo> tasks = new List<TaskToDo>();
            string query = string.Format(@"SELECT 
								 ID_Task 
                                ,Task
                                ,DataEnd  
                                ,IsDone 
                                FROM tasks
                                order by IsDone desc ,DataEnd asc;");
            using (MySqlConnection connection = new MySqlConnection(ToDoAPI.Database.Datebase.GetDateBaseAddress()))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                TaskToDo x = new TaskToDo();
                                x.ID_Task = reader.GetInt32(0);
                                x.TaskText = reader.IsDBNull(1) || reader.GetString(1) == "" ? string.Empty : reader.GetString(1);
                                x.DataEnd = reader.IsDBNull(2) ? null : reader.GetDateTime(2);
                                x.IsDone = reader.IsDBNull(3) || reader.GetByte(3) == 0 ? false  : true;
                                tasks.Add(x);

                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }

                        }

                    }

                }

            }
            return tasks;

        }

        public static void DeleteTask(int? taskID)
        {
            string query = @"DELETE FROM `todolistvue`.`tasks`
                            WHERE ID_Task = @ID_Task;";


            using (MySqlConnection connection = new MySqlConnection(ToDoAPI.Database.Datebase.GetDateBaseAddress()))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ID_Task", MySqlDbType.Int32).Value = taskID;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }

                }

            }

        }

        public static void UpdateTask(TaskToDo task)
        {
            string query = @"UPDATE `todolistvue`.`tasks`
                            SET
                            `Task` = @Task,
                            `DataEnd` = @EndDate,
                            `IsDone`  = @IsDone
                            WHERE `ID_Task` = @ID_Task;";

         





            using (MySqlConnection connection = new MySqlConnection(ToDoAPI.Database.Datebase.GetDateBaseAddress()))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@ID_Task", MySqlDbType.Int32).Value = task.ID_Task;
                    cmd.Parameters.Add("@Task", MySqlDbType.String).Value = task.TaskText;
                    cmd.Parameters.Add("@EndDate", MySqlDbType.DateTime).Value = task.DataEnd;
                    cmd.Parameters.Add("@IsDone", MySqlDbType.Byte).Value = task.IsDone == true ? 1 : 0;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }

                }

            }

        }

        public static void CreateNewTask(TaskToDo task)
        {
            string query = @"INSERT INTO `todolistvue`.`tasks`
                                    (
                                   `Task`,
                                    `DataEnd`,
                                    `DataCreate`,
                                    `IsDone`)
                                    VALUES
                                    (
                                    @Task,
                                    @DataEnd,
                                    @DataCreate,
                                    0);";

          


            using (MySqlConnection connection = new MySqlConnection(ToDoAPI.Database.Datebase.GetDateBaseAddress()))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Task", MySqlDbType.String).Value = task.TaskText;
                    cmd.Parameters.Add("@DataCreate", MySqlDbType.DateTime).Value = task.DataCreate; 
                    cmd.Parameters.Add("@DataEnd", MySqlDbType.DateTime).Value = task.DataEnd;
                    try
                    {

                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {

                        throw e;
                    }

                }

            }

        }
    }
}
