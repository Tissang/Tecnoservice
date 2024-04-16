using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazanDemo
{
    public class Database
    {
        public int SignIn(string login, string password)
        {
            // Валидация данных.
            if (login.Length == 0 && password.Length == 0)
            {
                throw new FormatException("Заполните поля логина и пароля данными!");
            }
            if (login.Length == 0)
            {
                throw new FormatException("Заполните поле логина данными!");
            }
            if (password.Length == 0)
            {
                throw new FormatException("Заполните поле пароля данными!");
            }

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение пользователей из базы данных по логину.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Login = @Login", connection);
            cmd.Parameters.AddWithValue("@Login", login);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение пользователей.
            if (reader.Read())
            {
                if (reader[4].ToString() != password)
                {
                    throw new FormatException("Неверный пароль!");
                }
                return int.Parse(reader[1].ToString());
            }
            else
            {
                throw new FormatException("Пользователя с таким логином не существует!");
            }
        }

        public List<Status> GetStatuses()
        {
            List<Status> statuses = new List<Status>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение статусов из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Status]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение статусов.
            while (reader.Read())
            {
                Status status = new Status()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                statuses.Add(status);
            }
            return statuses;
        }

        public Status GetStatusById(int statusId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение статуса по его уникальному идентификатору из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Status] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", statusId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение статуса.
            if (reader.Read())
            {
                Status status = new Status()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                return status;
            }
            else
            {
                throw new FormatException("Данного статуса не существует!");
            }
        }

        public List<Equipment> GetEquipments()
        {
            List<Equipment> equipments = new List<Equipment>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение оборудования из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение оборудования.
            while (reader.Read())
            {
                Equipment equipment = new Equipment()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                equipments.Add(equipment);
            }
            return equipments;
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение оборудования по его уникальному идентификатору из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Equipment] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", equipmentId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение оборудования.
            if (reader.Read())
            {
                Equipment equipment = new Equipment()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                return equipment;
            }
            else
            {
                throw new FormatException("Данного оборудования не существует!");
            }
        }

        public List<Contractor> GetContractors()
        {
            List<Contractor> contractors = new List<Contractor>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение исполнителей из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE RoleId = 2", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение исполнителей.
            while (reader.Read())
            {
                Contractor contractor = new Contractor()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Fio = reader[2].ToString()
                };
                contractors.Add(contractor);
            }
            return contractors;
        }

        public Contractor GetContractorByLogin(string login)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение исполнителя по его логину из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE Login = @Login", connection);
            cmd.Parameters.AddWithValue("@Login", login);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение исполнителя.
            if (reader.Read())
            {
                Contractor contractor = new Contractor()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Fio = reader[2].ToString()
                };
                return contractor;
            }
            else
            {
                throw new FormatException("Данного исполнителя не существует!");
            }
        }

        public List<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение видов работ из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Job]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение видов работ.
            while (reader.Read())
            {
                Job job = new Job()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Name = reader[1].ToString()
                };
                jobs.Add(job);
            }
            return jobs;
        }

        public List<Job> GetJobsByRequestId(int requestId)
        {
            List<Job> jobs = new List<Job>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение видов работ по уникальному идентификатору заявки из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Job] JOIN [RequestJob] ON Job.Id = RequestJob.JobId WHERE RequestId = @RequestId", connection);
            cmd.Parameters.AddWithValue("@RequestId", requestId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение видов работ.
            while (reader.Read())
            {
                Job job = new Job()
                {
                    Id = int.Parse(reader[2].ToString()),
                    RequestId = int.Parse(reader[3].ToString()),
                    Status = GetStatusById(int.Parse(reader[6].ToString())),
                    Name = reader[1].ToString()
                };
                jobs.Add(job);
            }
            return jobs;
        }

        public List<Job> GetJobsByContractorId(int contractorId)
        {
            List<Job> jobs = new List<Job>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение видов работ по уникальному идентификатору исполнителя из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Job] JOIN [RequestJob] ON Job.Id = RequestJob.JobId WHERE ContractorId = @ContractorId", connection);
            cmd.Parameters.AddWithValue("@ContractorId", contractorId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение видов работ.
            while (reader.Read())
            {
                Job job = new Job()
                {
                    Id = int.Parse(reader[2].ToString()),
                    RequestId = int.Parse(reader[3].ToString()),
                    Status = GetStatusById(int.Parse(reader[6].ToString())),
                    Name = reader[1].ToString()
                };
                jobs.Add(job);
            }
            return jobs;
        }

        public void AddJob(int requestId, int jobId, int contractorId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на добаление нового вида работ к заявке в базу данных.
            SqlCommand cmd = new SqlCommand("INSERT INTO [RequestJob] (RequestId, JobId, ContractorId, StatusId) VALUES (@RequestId, @JobId, @ContractorId, 1)", connection);
            cmd.Parameters.AddWithValue("@RequestId", requestId);
            cmd.Parameters.AddWithValue("@JobId", jobId);
            cmd.Parameters.AddWithValue("@ContractorId", contractorId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteJob(int jobId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на удаление вида работ у заявки из базы данных.
            SqlCommand cmd = new SqlCommand("DELETE FROM [RequestJob] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", jobId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void SaveJob(int requestId, string comment, int jobId, int statusId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на изменение вида работ у заявки в базе данных.
            SqlCommand cmd = new SqlCommand("UPDATE [RequestJob] SET StatusId = @StatusId WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", jobId);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            connection.Open();
            cmd.ExecuteNonQuery();

            // Запрос на изменение комментария у заявки в базе данных.
            cmd = new SqlCommand("UPDATE [Request] SET Comment = @Comment WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", requestId);
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.ExecuteNonQuery();
        }

        public List<Request> GetRequests()
        {
            List<Request> requests = new List<Request>();

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение заявок из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Request]", connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение заявок.
            while (reader.Read())
            {
                DateTime? endDate;
                if (reader[10].ToString().Length == 0)
                {
                    endDate = null;
                }
                else
                {
                    endDate = DateTime.Parse(reader[10].ToString());
                }

                Request request = new Request()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Equipment = GetEquipmentById(int.Parse(reader[1].ToString())),
                    Status = GetStatusById(int.Parse(reader[2].ToString())),
                    Fio = reader[3].ToString(),
                    Phone = reader[4].ToString(),
                    Email = reader[5].ToString(),
                    SerialNumber = reader[6].ToString(),
                    Description = reader[7].ToString(),
                    Comment = reader[8].ToString(),
                    StartDate = DateTime.Parse(reader[9].ToString()),
                    EndDate = endDate
                };
                requests.Add(request);
            }
            return requests;
        }

        public Request GetRequestById(int requestId)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на получение заявки по её уникальному идентификатору из базы данных.
            SqlCommand cmd = new SqlCommand("SELECT * FROM [Request] WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@Id", requestId);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            // Получение заявки.
            if (reader.Read())
            {
                DateTime? endDate;
                if (reader[10].ToString().Length == 0)
                {
                    endDate = null;
                }
                else
                {
                    endDate = DateTime.Parse(reader[10].ToString());
                }

                Request request = new Request()
                {
                    Id = int.Parse(reader[0].ToString()),
                    Equipment = GetEquipmentById(int.Parse(reader[1].ToString())),
                    Status = GetStatusById(int.Parse(reader[2].ToString())),
                    Fio = reader[3].ToString(),
                    Phone = reader[4].ToString(),
                    Email = reader[5].ToString(),
                    SerialNumber = reader[6].ToString(),
                    Description = reader[7].ToString(),
                    Comment = reader[8].ToString(),
                    StartDate = DateTime.Parse(reader[9].ToString()),
                    EndDate = endDate
                };
                return request;
            }
            else
            {
                throw new FormatException("Данной заявки не существует!");
            }
        }

        public void AddRequest(
            int equipmentId,
            string fio,
            string phone,
            string email,
            string serialNumber,
            string description,
            string comment
        )
        {
            // Валидация данных.
            if (fio.Length == 0 || phone.Length == 0 || email.Length == 0 || serialNumber.Length == 0 || description.Length == 0 || comment.Length == 0)
            {
                throw new FormatException("заполните все поля данными!");
            }

            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на добаление новой заявки в базу данных.
            SqlCommand cmd = new SqlCommand("INSERT INTO [Request] (EquipmentId, StatusId, Fio, Phone, Email, SerialNumber, Description, Comment, StartDate) VALUES (@EquipmentId, 1, @Fio, @Phone, @Email, @SerialNumber, @Description, @Comment, @StartDate)", connection);
            cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
            cmd.Parameters.AddWithValue("@Fio", fio);
            cmd.Parameters.AddWithValue("@Phone", phone);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@SerialNumber", serialNumber);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void SaveRequest(int requestId, int statusId, string description, string comment, DateTime? endDate)
        {
            // Строка подключения к базе данных.
            SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Demo15-04;Integrated Security=True");

            // Запрос на изменение заявки в базе данных.
            SqlCommand cmd = new SqlCommand("UPDATE [Request] SET StatusId = @StatusId, Description = @Description, Comment = @Comment, EndDate = @EndDate WHERE Id = @Id", connection);
            cmd.Parameters.AddWithValue("@StatusId", statusId);
            cmd.Parameters.AddWithValue("@Description", description);
            cmd.Parameters.AddWithValue("@Comment", comment);
            if (endDate == null)
            {
                cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EndDate", endDate);
            }
            cmd.Parameters.AddWithValue("@Id", requestId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
