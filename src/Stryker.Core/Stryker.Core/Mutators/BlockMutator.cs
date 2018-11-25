using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Stryker.Core.Mutants;
using Microsoft.CodeAnalysis;
using System.Linq;

namespace Stryker.Core.Mutators
{
    public class BlockMutator : Mutator<BlockSyntax>, IMutator
    {
        public override IEnumerable<Mutation> ApplyMutations(BlockSyntax node)
        {
            foreach (BlockSyntax replacement in GetReplacements(node))
            {
                yield return new Mutation()
                {
                    OriginalNode = node,
                    ReplacementNode = replacement,
                    DisplayName = "Block mutation",
                    Type = nameof(BlockMutator),
                };
            }
        }

        private static IEnumerable<BlockSyntax> GetReplacements(BlockSyntax node)
        {
            MethodDeclarationSyntax containingMethod = GetContainingMethod(node);
            if (containingMethod == null)
            {
                yield break;
            }

            DefaultExpressionSyntax returnValue = null;
            if (!MethodReturnsVoid(containingMethod))
            {
                returnValue = SyntaxFactory.DefaultExpression(containingMethod.ReturnType).WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
            }
            yield return SyntaxFactory.Block(SyntaxFactory.ReturnStatement(returnValue).WithLeadingTrivia(SyntaxFactory.EndOfLine("\r\n")).WithTrailingTrivia(SyntaxFactory.EndOfLine("\r\n")));

            if (!MustHaveReturn(node, containingMethod))
            {
                yield return SyntaxFactory.Block();
            }
        }

        private static MethodDeclarationSyntax GetContainingMethod(BlockSyntax node)
        {
            SyntaxNode parent = node.Parent;
            while (parent != null)
            {
                if (parent is MethodDeclarationSyntax methodDeclaration)
                {
                    return methodDeclaration;
                }
                parent = parent.Parent;
            }
            return null;
        }

        private static bool MethodReturnsVoid(MethodDeclarationSyntax methodDeclaration)
        {
            if (methodDeclaration.ReturnType.Kind() != SyntaxKind.PredefinedType)
            {
                return false;
            }
            return methodDeclaration.ReturnType.GetFirstToken().Kind() == SyntaxKind.VoidKeyword;
        }

        private static bool MustHaveReturn(BlockSyntax node, MethodDeclarationSyntax methodDeclaration)
        {
            if (MethodReturnsVoid(methodDeclaration) || !HasReturn(node))
            {
                return false;
            }

            SyntaxNode parent = node.Parent;
            switch (parent.Kind())
            {
                case SyntaxKind.TryStatement:
                    return !OtherReturnExists(methodDeclaration, parent);
                case SyntaxKind.IfStatement:
                case SyntaxKind.ElseClause:
                    while (true)
                    {
                        if (parent?.Parent == null)
                        {
                            return true;
                        }
                        if (parent.Parent.Kind() != SyntaxKind.IfStatement && parent.Parent.Kind() != SyntaxKind.ElseClause)
                        {
                            break;
                        }
                        parent = parent.Parent;
                    }
                    return !OtherReturnExists(methodDeclaration, parent);
                case SyntaxKind.SwitchSection:
                case SyntaxKind.CatchClause:
                case SyntaxKind.FinallyClause:
                    return !OtherReturnExists(methodDeclaration, parent.Parent);
                default:
                    return !OtherReturnExists(methodDeclaration, node);
            }
        }

        private static bool HasReturn(SyntaxNode node)
        {
            return node.DescendantNodes().Any(child => child.Kind() == SyntaxKind.ReturnStatement || child.Kind() == SyntaxKind.ThrowStatement);
        }

        // This is probably imperfect (the other return might not be guaranteed to happen).
        // I'm not sure how to best address that without the ability to check and see if a change will compile.
        // Maybe we should have a more robust rollback that will add (and compile) things one at a time
        // if everything together fails, and skip any that introduce compilation errors.
        private static bool OtherReturnExists(MethodDeclarationSyntax methodDeclaration, SyntaxNode nodeToSkip)
        {
            return methodDeclaration.DescendantNodes(node => node != nodeToSkip).OfType<ReturnStatementSyntax>().Any();
        }
    }
}
