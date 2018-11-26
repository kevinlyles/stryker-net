using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Stryker.Core.Mutants;
using Stryker.Core.Mutators;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Stryker.Core.UnitTest.Mutators
{
    public class BlockMutatorTests
    {
        private string _resourcesDirectory { get; set; }

        public BlockMutatorTests()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyLocation = Path.GetDirectoryName(assembly.Location);
            _resourcesDirectory = Path.Combine(assemblyLocation, "Mutators", "TestResources");
        }

        [Theory]
        [InlineData("IfElseBothMustReturn.cs")]
        [InlineData("IfNoReturnCouldReturn.cs")]
        [InlineData("IfNoReturnVoid.cs")]
        [InlineData("IfReturnCouldReturn.cs")]
        [InlineData("IfReturnVoid.cs")]
        [InlineData("Loop.cs")]
        [InlineData("Switch.cs")]
        [InlineData("TryCatchFinally.cs")]
        public void Mutator_TestResourcesInputShouldBecomeOutput(string filename)
        {
            string source = File.ReadAllText(Path.Combine(_resourcesDirectory, "In", filename));
            string expected = File.ReadAllText(Path.Combine(_resourcesDirectory, "Out", filename));

            IMutator[] mutators = {
                new BlockMutator(),
            };
            var orchestrator = new MutantOrchestrator(mutators);
            var actualNode = orchestrator.Mutate(CSharpSyntaxTree.ParseText(source).GetRoot());
            var expectedNode = CSharpSyntaxTree.ParseText(expected).GetRoot();
            //Without the ToString, some of the tests "fail", even though they're identical
            actualNode.ToString().ShouldBeSemantically(expectedNode.ToString());
        }
    }
}
