using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.Extensions.Logging;
using Stryker.Core.Logging;
using Stryker.Core.Mutants.MutationHandlers;
using Stryker.Core.Mutators;
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
            if (currentNode is StatementSyntax statement && !(statement is BlockSyntax))
            {
                StatementSyntax ast = statement;
                foreach (var mutant in FindMutants(statement))
                {
                    _mutants.Add(mutant);
                    ast = MutationIfPlacer.InsertMutation(ast, ApplyMutant(statement, mutant), mutant.Id);
                }
                return ast;
            }
            else if (currentNode is ExpressionSyntax expression)
            {
                ExpressionSyntax ast = expression;
                foreach (var mutant in FindMutants(expression))
                {
                    _mutants.Add(mutant);
                    ast = MutationTernaryPlacer.InsertMutation(ast, ApplyMutant(expression, mutant), mutant.Id);
                }
                return ast;
            }
            else
            {
                // No statement found yet, search deeper in the tree for statements to mutate
                var editor = new SyntaxEditor(currentNode, new AdhocWorkspace());
                foreach (SyntaxNode childNode in currentNode.ChildNodes())
                {
                    var mutatedNode = Mutate(childNode);
                    if (!childNode.IsEquivalentTo(mutatedNode))
                    {
                        editor.ReplaceNode(childNode, mutatedNode);
                    }
                }
                return editor.GetChangedRoot();
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
            foreach (var mutant in current.ChildNodes().SelectMany(FindMutants))
            {
                yield return mutant;
            }
        }

        /// <summary>
        /// Mutates one single SyntaxNode using a mutator
        /// </summary>
        private IEnumerable<Mutant> ApplyMutator(SyntaxNode syntaxNode, IMutator mutator)
        {
            var mutations = mutator.Mutate(syntaxNode);
            foreach (var mutation in mutations)
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
            var editor = new SyntaxEditor(statement, new AdhocWorkspace());
            editor.ReplaceNode(mutant.Mutation.OriginalNode, mutant.Mutation.ReplacementNode);
            return editor.GetChangedRoot() as T;
        }

        /// <summary>
        /// Places an IfStatementSyntax node around the mutated node and places the original node in the ElseClause block.
        /// </summary>
        private StatementSyntax MutantIf(StatementSyntax original, StatementSyntax mutated, int mutantId)
        {
            return MutationIfPlacer.InsertMutation(original, mutated, mutantId);
        }
    }
}
