using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Stryker.Core.Mutants.MutationHandlers
{
	public class MutationTernaryPlacer
	{
		public static ExpressionSyntax InsertMutation(ExpressionSyntax original, ExpressionSyntax mutated, int mutantId)
		{
			// place the mutated statement inside the if statement
			return SyntaxFactory.ParenthesizedExpression(
				SyntaxFactory.ConditionalExpression(
					condition: SyntaxFactory.BinaryExpression(SyntaxKind.EqualsExpression,
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
						SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(mutantId.ToString()))),
					whenTrue: SyntaxFactory.ParenthesizedExpression(mutated),
					whenFalse: SyntaxFactory.ParenthesizedExpression(original)
				)
			)
			// Mark this node as a MutationTernary node. Store the MutantId in the annotation to retrace the mutant later
			.WithAdditionalAnnotations(new SyntaxAnnotation("MutationTernary", mutantId.ToString()));
		}

		public static SyntaxNode RemoveMutation(SyntaxNode node)
		{
			if (node.HasAnnotation(new SyntaxAnnotation("MutationTernary")))
			{
				ConditionalExpressionSyntax conditional = (node as ParenthesizedExpressionSyntax).Expression as ConditionalExpressionSyntax;
				return conditional.WhenFalse;
			}
			else
			{
				return null;
			}
		}
	}
}
