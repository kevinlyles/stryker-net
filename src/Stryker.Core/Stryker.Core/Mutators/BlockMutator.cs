using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Stryker.Core.Mutants;
using System.Collections.Generic;
using System.Linq;

namespace Stryker.Core.Mutators
{
    /// <summary>
    /// A simple mutator that changes AddExpressions to SubtractExpressions, useful for UnitTesting
    /// </summary>
    public class BlockMutator : Mutator<BlockSyntax>, IMutator
    {
        public override IEnumerable<Mutation> ApplyMutations(BlockSyntax node)
        {
            if (node.IsKind(SyntaxKind.Block) && node.Statements.Any())
            {
                yield return new Mutation()
                {
                    OriginalNode = node,
                    ReplacementNode = SyntaxFactory.Block(),
                    DisplayName = "Block mutation",
                    Type = "BlockMutator"
                };
            }
        }

    }
}

