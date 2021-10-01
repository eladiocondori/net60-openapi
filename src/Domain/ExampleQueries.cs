using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenApi.Example;

namespace MinApi.Domain
{
    public class ExampleQueries : ControllerBase, IQueriesController
    {
        public async Task<ActionResult<ArrayOfExamples>> GetExamplesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new ArrayOfExamples
            {
                Items = new List<Example>
                {
                    new() {ExampleId = 12345678, Else = "Not important", Something = "Very important"}
                }
            };

            return await Task.FromResult(Ok(result));
        }

        public async Task<ActionResult<Example>> GetExampleByIdAsync(int exampleId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new Example {ExampleId = 12345678, Else = "Not important", Something = "Very important"};
            return await Task.FromResult(Ok(result));
        }
    }
}