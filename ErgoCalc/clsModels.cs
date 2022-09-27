/*
https://stackoverflow.com/questions/5107694/how-do-i-add-a-reference-to-an-unmanaged-c-project-called-by-a-c-sharp-project 
I would follow Slaks' second answer...
[...] you can add a link to the unmanaged DLL as a file in the C# project, and set Build Action to None and Copy to Output Directory to Copy If Newer.
... followed by my comment, to differentiate between Debug and Release builds (even if is a little bit "hackish", since it requires you to manually edit the C# project file)
open your C# project's csproj file with a text editor and search for all "YourNativeCppProject.dll" occurrences (without the ".dll" subfix, so if you added pdb files as a link too,
you'll find more than one occurrence), and change the Include path using macros, for example: Include="$(SolutionDir)$(ConfigurationName)\YourNativeCppProject.dll
PS: if you look at the properties (F4), VS shows you the Debug's path even if you switch to the Release configuration, but if you compile, you'll see that the dll copied to output is the release version
 
I would suspect you're trying to add reference to a file which is only possible to do with managed assemblies and some COM files.
Here's what you should do:
1. Compile your solution.
2. Right click on the managed project and select "Add/Existing Item". Do not use "Add Reference".
3. Navigate to your compiled native DLL and select it (adjust file types as needed).
4. Click on the down arrow in the "Add" split button and select "Add As Link" (which is what I meant by "adding as reference" - sorry I have not used VS 2008 in a while).
5. Right click on that freshly added file and select "Properties".
6. Make sure "Build Action" is "Content" and "Copy To Output Directory" is set to "Copy always" or "Copy if newer".
7. Right click on the managed project and select "Project Dependencies".
8. Check your native DLL in the list which would appear.
You now should be all set.
  
 */


namespace ErgoCalc
{

    namespace Models
    {
        // Para acceder a todos los servicios Interop
        using System.Runtime.InteropServices;

        

    }   // namespace DLL

}   // namespace ErgoCalc
