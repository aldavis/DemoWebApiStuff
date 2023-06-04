using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Dapper;

namespace DemoStuff.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var connectionString =
                "Data Source=localhost;Initial Catalog=Einstein.Workflow;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            var list = new List<TaskList>();

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command = new SqlCommand("select * from WorkItem", connection);

                    var reader = command.ExecuteReader();

                    //while (reader.Read())
                    //{

                    //    var workitem2 = new TaskList
                    //    {
                    //        CreatedBy = "bill",
                    //        Id = 8
                    //    };

                    //    list.Add(workItem);

                    //    Console.WriteLine("boom");

                    //    Console.WriteLine(reader["WorkflowId"].ToString());
                    //}

                    connection.Close();
                }
                catch (Exception e)
                {
                        Console.WriteLine(e);
                        throw;
                }
            }

            return Ok("good");
        }

        [HttpGet("dapper")]
        public async Task<IActionResult> GetDapper()
        {
            var connectionString =
                "Data Source=localhost;Initial Catalog=Einstein.Workflow;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            await using (var connection = new SqlConnection(connectionString))
            {
                var results = await connection.QueryAsync<TaskList>("select * from workflow");

                foreach (var result in results)
                {
                    Console.WriteLine(result.CreatedBy);
                }
            }

            return Ok("good");
        }

        [HttpPost]
        public IActionResult Post()
        {
            var connectionString =
                "Data Source=localhost;Initial Catalog=Einstein.Workflow;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command =
                        new SqlCommand("insert into workflow(WorkflowTemplateId,DateCreated,CreatedBy)values(1,getdate(),'Bingo')",connection);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Ok("good");
        }

        [HttpPut]
        public IActionResult Put()
        {
            var connectionString =
                "Data Source=localhost;Initial Catalog=Einstein.Workflow;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";



            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var command =
                        new SqlCommand("update workflow set createdby = 'Fred' where Id = 33", connection);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return Ok("good");
        }

        [HttpGet("buildlist")]
        public TaskList BuildList()
        {

            var list = new TaskList
            {
                CreatedBy = "alex",
                Id = 5,
                Status = "open",
                Tasks = new List<ToDoItem>
                {
                    new ToDoItem{Id = 1,Queue = new TaskQueue{QueueId = 1,QueueName = "publix"},Name = "get milk"},
                    new ToDoItem{Id = 1,Queue = new TaskQueue{QueueId = 1,QueueName = "publix"},Name = "get bread"},
                    new ToDoItem{Id = 1,Queue = new TaskQueue{QueueId = 1,QueueName = "publix"},Name = "get ice"},
                }
            };

            foreach (var doItem in list.Tasks)
            {
                Console.WriteLine(doItem.Name);
                Console.WriteLine(doItem.Queue.QueueName);
            }



            return list;
        }
    }
}