using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Stryker.Core;
using Stryker.Core.Initialisation;
using Stryker.Core.Logging;
using Stryker.Core.Mutants;
using Stryker.Core.MutationTest;
using Stryker.Core.Mutators;
using Stryker.Core.Options;
using Stryker.Core.Reporters;

namespace TestApplication
{
    class Test
    {
        private IReporter _reporter;
        private InitialisationProcess _initialisationProcess;
        private MutationTestInput _input;
        private MutationTestProcess _mutationTestProcess;

        public StrykerRunResult RunMutationTest(StrykerOptions options)
        {
            // start stopwatch
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // setup logging
            ApplicationLogging.ConfigureLogger(options.LogOptions);
            var logger = ApplicationLogging.LoggerFactory.CreateLogger<StrykerRunner>();
            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "272")
            {
                try
                {
                    // initialize
                    _reporter = ReporterFactory.Create(options);
                    _initialisationProcess = _initialisationProcess ?? new InitialisationProcess();
                    _input = _initialisationProcess.Initialize(options);

                    _mutationTestProcess = _mutationTestProcess ?? new MutationTestProcess(
                        mutationTestInput: _input,
                        mutators: new List<IMutator>()
                            {
                            // the default list of mutators
                            //new BinaryExpressionMutator(),
                            new BlockMutator(),
                            new BooleanMutator(),
                            new AssignmentStatementMutator(),
                            new PrefixUnaryMutator(),
                            new PostfixUnaryMutator(),
                            new LinqMutator(),
                            },
                        reporter: _reporter,
                        mutationTestExecutor: new MutationTestExecutor(_input.TestRunner, _input.TimeoutMS));

                    // mutate
                    _mutationTestProcess.Mutate();

                    // test mutations and return results
                    return _mutationTestProcess.Test(options);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred during the mutation test run ");
                    throw;
                }
                finally
                { }
            }
            else
            {
                if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "271")
                {
                    try
                    {
                        // initialize
                        _reporter = ReporterFactory.Create(options);
                        _initialisationProcess = _initialisationProcess ?? new InitialisationProcess();
                        _input = _initialisationProcess.Initialize(options);

                        _mutationTestProcess = _mutationTestProcess ?? new MutationTestProcess(
                            mutationTestInput: _input,
                            mutators: new List<IMutator>()
                                {
                            // the default list of mutators
                            //new BinaryExpressionMutator(),
                            new BlockMutator(),
                                /*new BooleanMutator(),
                                new AssignmentStatementMutator(),
                                new PrefixUnaryMutator(),
                                new PostfixUnaryMutator(),
                                new LinqMutator(),*/
                                },
                            reporter: _reporter,
                            mutationTestExecutor: new MutationTestExecutor(_input.TestRunner, _input.TimeoutMS));

                        // mutate
                        _mutationTestProcess.Mutate();

                        // test mutations and return results
                        return _mutationTestProcess.Test(options);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred during the mutation test run ");
                        throw;
                    }
                    finally
                    {
                        //return default(StrykerRunResult);
                    }
                }
                else
                {
                    if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "270")
                    {
                        try
                        {
                            // initialize
                            _reporter = ReporterFactory.Create(options);
                            _initialisationProcess = _initialisationProcess ?? new InitialisationProcess();
                            _input = _initialisationProcess.Initialize(options);

                            _mutationTestProcess = _mutationTestProcess ?? new MutationTestProcess(
                                mutationTestInput: _input,
                                mutators: new List<IMutator>()
                                    {
                            // the default list of mutators
                            //new BinaryExpressionMutator(),
                            new BlockMutator(),
                                    /*new BooleanMutator(),
                                    new AssignmentStatementMutator(),
                                    new PrefixUnaryMutator(),
                                    new PostfixUnaryMutator(),
                                    new LinqMutator(),*/
                                    },
                                reporter: _reporter,
                                mutationTestExecutor: new MutationTestExecutor(_input.TestRunner, _input.TimeoutMS));

                            // mutate
                            _mutationTestProcess.Mutate();

                            // test mutations and return results
                            return _mutationTestProcess.Test(options);
                        }
                        catch (Exception ex)
                        { }
                        finally
                        {
                            // log duration
                            stopwatch.Stop();
                            logger.LogInformation("Time Elapsed {0}", stopwatch.Elapsed);
                        }
                    }
                    else
                    {
                        if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "269")
                        {
                            try
                            {
                                // initialize
                                _reporter = ReporterFactory.Create(options);
                                _initialisationProcess = _initialisationProcess ?? new InitialisationProcess();
                                _input = _initialisationProcess.Initialize(options);

                                _mutationTestProcess = _mutationTestProcess ?? new MutationTestProcess(
                                    mutationTestInput: _input,
                                    mutators: new List<IMutator>()
                                        {
                            // the default list of mutators
                            //new BinaryExpressionMutator(),
                            new BlockMutator(),
                                        /*new BooleanMutator(),
                                        new AssignmentStatementMutator(),
                                        new PrefixUnaryMutator(),
                                        new PostfixUnaryMutator(),
                                        new LinqMutator(),*/
                                        },
                                    reporter: _reporter,
                                    mutationTestExecutor: new MutationTestExecutor(_input.TestRunner, _input.TimeoutMS));

                                // mutate
                                _mutationTestProcess.Mutate();

                                // test mutations and return results
                                return _mutationTestProcess.Test(options);
                            }
                            catch (Exception ex)
                            {
                                return default(StrykerRunResult);
                            }
                            finally
                            {
                                // log duration
                                stopwatch.Stop();
                                logger.LogInformation("Time Elapsed {0}", stopwatch.Elapsed);
                            }
                        }
                        else
                        {
                            if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "268")
                            {
                                try
                                {
                                    return default(StrykerRunResult);
                                }
                                catch (Exception ex)
                                {
                                    logger.LogError(ex, "An error occurred during the mutation test run ");
                                    throw;
                                }
                                finally
                                {
                                    // log duration
                                    stopwatch.Stop();
                                    logger.LogInformation("Time Elapsed {0}", stopwatch.Elapsed);
                                }
                            }
                            else
                            {
                                try
                                {
                                    // initialize
                                    _reporter = ReporterFactory.Create(options);
                                    _initialisationProcess = _initialisationProcess ?? new InitialisationProcess();
                                    _input = _initialisationProcess.Initialize(options);

                                    _mutationTestProcess = _mutationTestProcess ?? new MutationTestProcess(
                                        mutationTestInput: _input,
                                        mutators: new List<IMutator>()
                                            {
                                                // the default list of mutators
                                                //new BinaryExpressionMutator(),
                                                new BlockMutator(),
                                                new BooleanMutator(),
                                                new AssignmentStatementMutator(),
                                                new PrefixUnaryMutator(),
                                                new PostfixUnaryMutator(),
                                                new LinqMutator(),
                                            },
                                        reporter: _reporter,
                                        mutationTestExecutor: new MutationTestExecutor(_input.TestRunner, _input.TimeoutMS));

                                    // mutate
                                    _mutationTestProcess.Mutate();

                                    // test mutations and return results
                                    return _mutationTestProcess.Test(options);
                                }
                                catch (Exception ex)
                                {
                                    logger.LogError(ex, "An error occurred during the mutation test run ");
                                    throw;
                                }
                                finally
                                {
                                    // log duration
                                    stopwatch.Stop();
                                    logger.LogInformation("Time Elapsed {0}", stopwatch.Elapsed);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
