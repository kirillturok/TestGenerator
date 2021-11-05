using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace MainPart
{
    class Pipeline
    {
        public void Generate(IEnumerable<string> files, string pathToGenerated)
        {
            var execOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = files.Count() };
            var LinkOptions = new DataflowLinkOptions { PropagateCompletion = true };

        }
    }
}
