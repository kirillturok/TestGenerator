﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestGeneratorLib;

namespace MainPart
{
    class Pipeline
    {
        public Task Generate(IEnumerable<string> files, string pathToGenerated)
        {
            var execOptions = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = files.Count() };
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };
            var downloadStringBlock = new TransformBlock<string, string>
            (
                async path =>
                {
                    using (var reader = new StreamReader(path))
                    {
                        Console.WriteLine(path);
                        return await reader.ReadToEndAsync();
                    }
                },
                execOptions
            );
            var generateTestsBlock = new TransformManyBlock<string, KeyValuePair<string, string>>
            (
                async sourceCode =>
                {
                    var fileInfo = await Task.Run(() => CodeAnalyzer.GetFileInfo(sourceCode));
                    return await Task.Run(() => TestsGenerator.GenerateTests(fileInfo));
                },
                execOptions
            );
            var writeFileBlock = new ActionBlock<KeyValuePair<string, string>>
            (
                async fileNameCodePair =>
                {
                    using (var writer = new StreamWriter(pathToGenerated + '\\' + fileNameCodePair.Key + ".cs"))
                    {
                        Console.WriteLine(fileNameCodePair.Key + "\n" + fileNameCodePair.Key);
                        await writer.WriteAsync(fileNameCodePair.Value);
                    }
                },
                execOptions
            );
            downloadStringBlock.LinkTo(generateTestsBlock, linkOptions);
            generateTestsBlock.LinkTo(writeFileBlock, linkOptions);
            
            //if doesnt work
            //check Post argument
            //v gthb
            foreach (var file in files)
            {
                downloadStringBlock.Post(file);
                Console.WriteLine(file);
            }

            downloadStringBlock.Complete();
            return writeFileBlock.Completion;
        }
    }
}
