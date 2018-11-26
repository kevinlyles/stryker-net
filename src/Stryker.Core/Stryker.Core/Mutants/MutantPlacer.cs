﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Stryker.Core.Mutants
{
    public static class MutantPlacer
    {
        public static IfStatementSyntax PlaceWithIfStatement(StatementSyntax original, StatementSyntax mutated, int mutantId)
        {
            // place the mutated statement inside the if statement
            return GetIfStatement(original, mutated, mutantId)
                // Mark this node as a MutationIf node. Store the MutantId in the annotation to retrace the mutant later
                .WithAdditionalAnnotations(new SyntaxAnnotation("MutationIf", mutantId.ToString()));
        }

        private static IfStatementSyntax GetIfStatement(StatementSyntax original, StatementSyntax mutated, int mutantId)
        {
            return SyntaxFactory.IfStatement(
                condition: GetBinaryExpression(mutantId),
                statement: SyntaxFactory.Block(mutated),
                @else: SyntaxFactory.ElseClause(SyntaxFactory.Block(original)));
        }

        public static SyntaxNode RemoveByIfStatement(SyntaxNode node)
        {
            if (node is IfStatementSyntax ifStatement)
            {
                // return original statement
                return ifStatement.Else.Statement;
            }
            else
            {
                return null;
            }
        }

        public static StatementSyntax PlaceWithWrappedIfStatement(StatementSyntax original, StatementSyntax mutated, int mutantId)
        {
            if (original.Parent?.Kind() == SyntaxKind.Block)
            {
                return PlaceWithIfStatement(original, mutated, mutantId);
            }
            else
            {
                return SyntaxFactory.Block(GetIfStatement(original, mutated, mutantId))
                    .WithAdditionalAnnotations(new SyntaxAnnotation("MutationBlock", mutantId.ToString()));
            }
        }

        public static SyntaxNode RemoveByWrappedIfStatement(SyntaxNode node)
        {
            if (node is BlockSyntax block)
            {
                return RemoveByIfStatement(block.ChildNodes().FirstOrDefault());
            }
            else
            {
                return null;
            }
        }

        public static ConditionalExpressionSyntax PlaceWithConditionalExpression(ExpressionSyntax original, ExpressionSyntax mutated, int mutantId)
        {
            // place the mutated statement inside the if statement
            return SyntaxFactory.ConditionalExpression(
                condition: GetBinaryExpression(mutantId),
                whenTrue: mutated,
                whenFalse: original)
                // Mark this node as a MutationConditional node. Store the MutantId in the annotation to retrace the mutant later
                .WithAdditionalAnnotations(new SyntaxAnnotation("MutationConditional", mutantId.ToString()));
        }

        public static SyntaxNode RemoveByConditionalExpression(SyntaxNode node)
        {
            if (node is ConditionalExpressionSyntax conditional)
            {
                // return original expression
                return conditional.WhenFalse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Builds a syntax for the expression to check if a mutation is active
        /// Example for mutantId 1: System.Environment.GetEnvironmentVariable("ActiveMutation") == "1"
        /// </summary>
        /// <param name="mutantId"></param>
        /// <returns></returns>
        private static ExpressionSyntax GetBinaryExpression(int mutantId)
        {
            return SyntaxFactory.BinaryExpression(SyntaxKind.EqualsExpression,
                SyntaxFactory.InvocationExpression(
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("System"),
                            SyntaxFactory.IdentifierName("Environment")
                        ),
                        SyntaxFactory.IdentifierName("GetEnvironmentVariable")),
                    SyntaxFactory.ArgumentList(SyntaxFactory.SeparatedList(
                        new List<ArgumentSyntax>() {
                        SyntaxFactory.Argument(SyntaxFactory.ExpressionStatement(
                            SyntaxFactory.LiteralExpression(
                                SyntaxKind.StringLiteralExpression,
                                SyntaxFactory.Literal("ActiveMutation"))).Expression)
                        }
                    ))
                ),
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(mutantId.ToString())));
        }
    }
}
