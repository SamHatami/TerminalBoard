For now we will be using reflection (which uses JIT compiler) to run the extensions
It might come to the conclusion that Elementa startup times might be slow to much OR that the performance of running the extensions 
will be to "slow".

Even if none of that would be the case, I'm guessing that a pre-compiled version of the extensions will be needed for reasons I can't assemble right now. 

Basically reading the entire graph, and re-making it into a ICompiledExtension which can be used instead of the JIT compiled / graphical version of the extension

