using System;
using System.Collections.Generic;
using System.Text;

namespace StrykerNet.UnitTest.Mutants.TestResources
{
	class TestClass
	{
		void TestMethod()
		{
			int i = 0;
			if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "10")
			{
				if (i + 8 == 8)
				{
					i = i + 1;
					if (i + 8 == 9)
					{
						i = i + 1;
					};
				}
				else
				{
					i = i + 3;
					if (i == i + i - 8)
					{
						i = i - 1;
					};
				}
			}
			else
			{
				if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "9")
				{
					if (i + 8 == 8)
					{
						i = i + 1;
						if (i + 8 == 9)
						{
							i = i + 1;
						};
					}
					else
					{
						i = i + 3;
						if (i == i - i - 8)
						{
							i = i + 1;
						};
					}
				}
				else
				{
					if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "8")
					{
						if (i + 8 == 8)
						{
							i = i + 1;
							if (i + 8 == 9)
							{
								i = i + 1;
							};
						}
						else
						{
							i = i + 3;
							if (i == i + i + 8)
							{
								i = i + 1;
							};
						}
					}
					else
					{
						if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "7")
						{
							if (i + 8 == 8)
							{
								i = i + 1;
								if (i + 8 == 9)
								{
									i = i + 1;
								};
							}
							else
							{
								i = i + 3;
								if (i != i + i - 8)
								{
									i = i + 1;
								};
							}
						}
						else
						{
							if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "6")
							{
								if (i + 8 == 8)
								{
									i = i + 1;
									if (i + 8 == 9)
									{
										i = i + 1;
									};
								}
								else
								{
									i = i - 3;
									if (i == i + i - 8)
									{
										i = i + 1;
									};
								}
							}
							else
							{
								if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "5")
								{
									if (i + 8 == 8)
									{
										i = i + 1;
										if (i + 8 == 9)
										{
											i = i - 1;
										};
									}
									else
									{
										i = i + 3;
										if (i == i + i - 8)
										{
											i = i + 1;
										};
									}
								}
								else
								{
									if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "4")
									{
										if (i + 8 == 8)
										{
											i = i + 1;
											if (i - 8 == 9)
											{
												i = i + 1;
											};
										}
										else
										{
											i = i + 3;
											if (i == i + i - 8)
											{
												i = i + 1;
											};
										}
									}
									else
									{
										if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "3")
										{
											if (i + 8 == 8)
											{
												i = i + 1;
												if (i + 8 != 9)
												{
													i = i + 1;
												};
											}
											else
											{
												i = i + 3;
												if (i == i + i - 8)
												{
													i = i + 1;
												};
											}
										}
										else
										{
											if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "2")
											{
												if (i + 8 == 8)
												{
													i = i - 1;
													if (i + 8 == 9)
													{
														i = i + 1;
													};
												}
												else
												{
													i = i + 3;
													if (i == i + i - 8)
													{
														i = i + 1;
													};
												}
											}
											else
											{
												if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "1")
												{
													if (i - 8 == 8)
													{
														i = i + 1;
														if (i + 8 == 9)
														{
															i = i + 1;
														};
													}
													else
													{
														i = i + 3;
														if (i == i + i - 8)
														{
															i = i + 1;
														};
													}
												}
												else
												{
													if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "0")
													{
														if (i + 8 != 8)
														{
															i = i + 1;
															if (i + 8 == 9)
															{
																i = i + 1;
															};
														}
														else
														{
															i = i + 3;
															if (i == i + i - 8)
															{
																i = i + 1;
															};
														}
													}
													else
													{
														if (i + 8 == 8)
														{
															i = i + 1;
															if (i + 8 == 9)
															{
																i = i + 1;
															};
														}
														else
														{
															i = i + 3;
															if (i == i + i - 8)
															{
																i = i + 1;
															};
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (System.Environment.GetEnvironmentVariable("ActiveMutation") == "11")
			{
				i = i - i;
			}
			else
			{
				i = i + i;
			}
		}
	}
}