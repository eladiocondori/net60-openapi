using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Example;

namespace MinApi.Domain
{
    public class ExampleCommands : ControllerBase, ICommandsController
    {
        public async Task<ActionResult<Example>> CreateExampleAsync(ExampleCreate body, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (body.Something == "Very important")
            {
                return Problem(statusCode: (int) HttpStatusCode.Conflict);
            }

            var result = new Example
            {
                ExampleId = 2,
                Else = body.Else,
                Something = body.Something
            };
            return await Task.FromResult(Ok(result));
        }
    }
}