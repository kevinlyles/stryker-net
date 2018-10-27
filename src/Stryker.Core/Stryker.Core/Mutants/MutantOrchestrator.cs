using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.Logging;
using Stryker.Core.Logging;
using Stryker.Core.Mutants.MutationHandlers;
using Stryker.Core.Mutators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stryker.Core.Mutants
{
	/// <summary>
	/// Mutates abstract syntax trees using mutators and places all mutations inside the abstract syntax tree.
	/// Orchestrator: to arrange or manipulate, especially by means of clever or thorough planning or maneuvering.
	/// </summary>
	public class MutantOrchestrator : IMutantOrchestrator
	{
		private ICollection<Mutant> _mutants { get; set; }
		private int _mutantCount { get; set; } = 0;
		private IEnumerable<IMutator> _mutators { get; set; }
		private ILogger _logger { get; set; }

		/// <param name="mutators">The mutators that should be active during the mutation process</param>
		/// <param name="mutantFactory">An instance of the mutantFactory, use the same for every file to keep the mutationcount increment</param>
		public MutantOrchestrator(IEnumerable<IMutator> mutators)
		{
			_mutators = mutators;
			_mutants = new Collection<Mutant>();
			_logger = ApplicationLogging.LoggerFactory.CreateLogger<MutantOrchestrator>();
		}

		/// <summary>
		/// Gets the stored mutants and resets the mutant list to an empty collection
		/// </summary>
		/// <returns>Mutants</returns>
		public IEnumerable<Mutant> GetLatestMutantBatch()
		{
			var tempMutants = _mutants;
			_mutants = new Collection<Mutant>();
			return tempMutants;
		}

		/// <summary>
		/// Recursively mutates a single SyntaxNode
		/// </summary>
		/// <param name="currentNode">The current root node</param>
		/// <returns>Mutated node</returns>
		public SyntaxNode Mutate(SyntaxNode currentNode)
		{
			if (currentNode is ExpressionStatementSyntax expressionStatement)
			{
				return MutateOnlyWithIfStatements(expressionStatement);
			}

			SyntaxNode ast = currentNode;
			var nodesToReplace = new Dictionary<SyntaxNode, SyntaxNode>();

			foreach (SyntaxNode childNode in currentNode.ChildNodes())
			{
				var mutatedNode = Mutate(childNode);
				if (!childNode.IsEquivalentTo(mutatedNode))
				{
					nodesToReplace.Add(childNode, mutatedNode);
				}
			}
			ast = ast.ReplaceNodes(nodesToReplace.Keys, (node, ignored) => nodesToReplace[node]);

			if (ast is StatementSyntax statement)
			{
				StatementSyntax statementAst = statement;
				foreach (var mutant in FindMutants(statement))
				{
					_mutants.Add(mutant);
					statementAst = MutationIfPlacer.InsertMutation(statementAst, ApplyMutant(statementAst, mutant), mutant.Id);
				}
				ast = statementAst;
			}
			else if (ast is ExpressionSyntax expression)
			{
				ExpressionSyntax expressionAst = expression;
				foreach (var mutant in FindMutants(expression))
				{
					_mutants.Add(mutant);
					expressionAst = MutationTernaryPlacer.InsertMutation(expressionAst, ApplyMutant(expressionAst, mutant), mutant.Id);
				}
				ast = expressionAst;
			}

			return ast;
		}

		private SyntaxNode MutateOnlyWithIfStatements(ExpressionStatementSyntax currentNode)
		{
			StatementSyntax ast = currentNode;
			var nodesToReplace = new Dictionary<SyntaxNode, List<Mutant>>();

			foreach (SyntaxNode childNode in currentNode.ChildNodes())
			{
				nodesToReplace.Add(childNode, FindAllMutants(childNode).ToList());
			}
			foreach (var node in nodesToReplace.Keys)
			{
				foreach (var mutant in nodesToReplace[node])
				{
					ast = MutationIfPlacer.InsertMutation(ast, ApplyMutant(currentNode, mutant), mutant.Id);
				}
			}

			return ast;
		}

		private IEnumerable<Mutant> FindAllMutants(SyntaxNode current)
		{
			foreach (var childNode in current.ChildNodes())
			{
				foreach (var mutant in FindAllMutants(childNode))
				{
					yield return mutant;
				}
			}
			foreach (var mutator in _mutators)
			{
				foreach (var mutation in ApplyMutator(current, mutator))
				{
					yield return mutation;
				}
			}
		}

		private IEnumerable<Mutant> FindMutants(SyntaxNode current)
		{
			foreach (var mutator in _mutators)
			{
				foreach (var mutation in ApplyMutator(current, mutator))
				{
					yield return mutation;
				}
			}
		}

		/// <summary>
		/// Mutates one single SyntaxNode using a mutator
		/// </summary>
		private IEnumerable<Mutant> ApplyMutator(SyntaxNode syntaxNode, IMutator mutator)
		{
			foreach (var mutation in mutator.Mutate(syntaxNode))
			{
				_logger.LogDebug("Mutant {0} created {1} -> {2} using {3}", _mutantCount, mutation.OriginalNode, mutation.ReplacementNode, mutator.GetType());
				yield return new Mutant()
				{
					Id = _mutantCount++,
					Mutation = mutation,
					ResultStatus = MutantStatus.NotRun
				};
			}
		}

		private T ApplyMutant<T>(T statement, Mutant mutant) where T : SyntaxNode
		{
			return statement.ReplaceNode(mutant.Mutation.OriginalNode, mutant.Mutation.ReplacementNode);
		}
	}
}
