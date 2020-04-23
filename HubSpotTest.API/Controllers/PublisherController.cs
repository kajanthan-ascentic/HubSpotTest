using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Resonance;
using Resonance.Models;

namespace HubSpotTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        const int WORKER_COUNT = 10;
        public ILogger<PublisherController> Logger { get; }
        public IEventPublisherAsync EventPublisherAsync { get; }

        public PublisherController(ILogger<PublisherController> logger, IEventPublisherAsync eventPublisherAsync) 
        {
            Logger = logger;
            EventPublisherAsync = eventPublisherAsync;
        }

        // GET: api/Publisher
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // Make sure the topic exists
            var topic1 = EventPublisherAsync.GetTopicByNameAsync("Demo Topic 1").GetAwaiter().GetResult() ?? EventPublisherAsync.AddOrUpdateTopicAsync(new Topic { Name = "Demo Topic 1", Log = false }).GetAwaiter().GetResult();

            // Generate data WHILE also consuming (just like the real world)
            if (true)
            {
                var sw = new Stopwatch();
                sw.Start();

                var arrLen = 500;
                var nrs = new List<int>(arrLen);
                for (int i = 0; i < arrLen; i++) { nrs.Add(i); };

                nrs.AsParallel().ForAll((i) =>
                    Task.Run(async () => // Threadpool task to wait for async parts in inner task
                    {
                        Console.WriteLine($"Run {i:D4} - Start  [{Thread.CurrentThread.ManagedThreadId}]");
                        for (int fk = 1; fk <= 10; fk++) // 1000 different functional keys, 4 TopicEvents per fk
                        {
                            await Task.Delay(1).ConfigureAwait(false);
                            var fkAsString = i.ToString();
                            try
                            {
                                await EventPublisherAsync.PublishAsync(topic1.Name, functionalKey: fkAsString, payload: "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789"); // 100 bytes
                            }
                            catch (Exception) { } // Ignore repo-exceptions (probably deadlocks)
                        }
                        Console.WriteLine($"Run {i:D4} - Finish [{Thread.CurrentThread.ManagedThreadId}]");
                    }
                    ).GetAwaiter().GetResult() // Block until all async work is done (ForAll does not await on Task as result-type)
                );

                sw.Stop();
                Console.WriteLine($"Total time for publishing: {sw.Elapsed.TotalSeconds} sec");
            }

            return new string[] { "value1", "value2" };
        }

        // GET: api/Publisher/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Publisher
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Publisher/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
