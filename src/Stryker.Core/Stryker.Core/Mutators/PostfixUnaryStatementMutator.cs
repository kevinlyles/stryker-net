using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Stryker.Core.Mutants;
using Stryker.Core.Mutators;

namespace Stryker.Core.Mutators
{
	public class PostfixUnaryStatementMutator : Mutator<ExpressionStatementSyntax>, IMutator
	{
		private static readonly Dictionary<SyntaxKind, SyntaxKind> UnaryWithOpposite = new Dictionary<SyntaxKind, SyntaxKind>
		{
			{SyntaxKind.PostIncrementExpression, SyntaxKind.PostDecrementExpression},
			{SyntaxKind.PostDecrementExpression, SyntaxKind.PostIncrementExpression},
		};

		public override IEnumerable<Mutation> ApplyMutations(ExpressionStatementSyntax node)
		{
			if (node.Expression is PostfixUnaryExpressionSyntax expression)
			{
				var unaryKind = expression.Kind();
				if (UnaryWithOpposite.TryGetValue(unaryKind, out var oppositeKind))
				{
					yield return new Mutation
					{
						OriginalNode = node,
						ReplacementNode = SyntaxFactory.ExpressionStatement(SyntaxFactory.PostfixUnaryExpression(oppositeKind, expression.Operand)),
						DisplayName = $"{unaryKind} to {oppositeKind} mutation",
						Type = nameof(PostfixUnaryMutator)
					};
				}
			}
		}
	}
}