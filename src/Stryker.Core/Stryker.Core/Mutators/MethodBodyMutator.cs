using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Stryker.Core.Mutants;
using Microsoft.CodeAnalysis;
using System.Linq;
using System;

namespace Stryker.Core.Mutators
{
    public class MethodBodyMutator : Mutator<MethodDeclarationSyntax>, IMutator
    {
        public override IEnumerable<Mutation> ApplyMutations(MethodDeclarationSyntax node)
        {
            yield return new Mutation()
            {
                DisplayName = "Method body mutation",
                OriginalNode = node,
                ReplacementNode = GetReplacement(node),
                Type = nameof(MethodBodyMutator),
            };
        }

        private static MethodDeclarationSyntax GetReplacement(MethodDeclarationSyntax node)
        {
            BlockSyntax replacementBody = null;
            if (node.Body != null)
            {
                IEnumerable<ParameterSyntax> outParameters = node.ParameterList.Parameters.Where(parameter => parameter.ChildTokens().Any(token => token.Kind() == SyntaxKind.OutKeyword));
                IEnumerable<AssignmentExpressionSyntax> setOutParameters = outParameters.Select(parameter => SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName(parameter.Identifier), SyntaxFactory.DefaultExpression(parameter.Type)));
                ReturnStatementSyntax returnStatement = null;
                if (node.ReturnType.Kind() != SyntaxKind.PredefinedType || node.ReturnType.GetFirstToken().Kind() != SyntaxKind.VoidKeyword)
                {
                    //Without the leading trivia, there's no space between return and default
                    returnStatement = SyntaxFactory.ReturnStatement(SyntaxFactory.DefaultExpression(node.ReturnType).WithLeadingTrivia(SyntaxFactory.Whitespace(" ")));
                }
                replacementBody = SyntaxFactory.Block().AddStatements(setOutParameters.Select(expression => SyntaxFactory.ExpressionStatement(expression)).ToArray()).AddStatements(returnStatement);
            }
            ArrowExpressionClauseSyntax replacementExpressionBody = null;
            if (node.ExpressionBody != null)
            {
                replacementExpressionBody = SyntaxFactory.ArrowExpressionClause(SyntaxFactory.DefaultExpression(node.ReturnType));
            }
            return SyntaxFactory.MethodDeclaration(node.AttributeLists, node.Modifiers, node.ReturnType, node.ExplicitInterfaceSpecifier, node.Identifier, node.TypeParameterList, node.ParameterList, node.ConstraintClauses, replacementBody, replacementExpressionBody, node.SemicolonToken);
        }
    }
}