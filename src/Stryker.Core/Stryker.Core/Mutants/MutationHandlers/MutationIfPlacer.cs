using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Stryker.Core.Mutants.MutationHandlers
{
	/// <summary>
	/// This is the base handler. If no other handler could handle the mutation it will be placed in an if statement.
	/// </summary>
	public class MutationIfPlacer
	{
		public static StatementSyntax InsertMutation(StatementSyntax original, StatementSyntax mutated, int mutantId)
		{
			// place the mutated statement inside the if statement
			return SyntaxFactory.IfStatement(
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
				statement: SyntaxFactory.Block(mutated),
				@else: SyntaxFactory.ElseClause(SyntaxFactory.Block(original)))
				// Mark this node as a MutationIf node. Store the MutantId in the annotation to retrace the mutant later
				.WithAdditionalAnnotations(new SyntaxAnnotation("MutationIf", mutantId.ToString()));
		}

		public static StatementSyntax RemoveMutation(SyntaxNode node)
		{
			if (node is IfStatementSyntax ifStatement)
			{
				return (ifStatement.Else.Statement as BlockSyntax).Statements[0];
			}
			else
			{
				return null;
			}
		}
	}
}
