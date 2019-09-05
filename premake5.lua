if os.ishost "windows" then

    -- Windows
    newaction
    {
        trigger     = "solution",
        description = "Open the game-estates solution",
        execute = function ()
            os.execute "start src\\All.sln"
        end
    }

    newaction
    {
        trigger     = "bundleBuild",
        description = "Bundle build folder with dlls",
        execute = function ()
            os.rmdir "_bundle"
            userprofile = os.getenv("USERPROFILE")
            os.execute ( "mkdir _bundle & pushd _bundle \z
&& copy \""..userprofile.."\\.nuget\\packages\\mathnet.numerics\\4.7.0\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\zstdnet\\1.3.3\\build\\x64\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\zstdnet\\1.3.3\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\Newtonsoft.Json\\12.0.2\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.caching.memory\\3.0.0-preview5.19210.2\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.caching.abstractions\\3.0.0-preview5.19210.2\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.options\\2.2.0\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.primitives\\2.2.0\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.dependencyinjection.abstractions\\2.2.0\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\microsoft.extensions.logging.abstractions\\2.2.0\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\system.memory\\4.5.2\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\system.runtime.compilerservices.unsafe\\4.5.2\\lib\\netstandard2.0\\*.dll\" . \z
&& copy \""..userprofile.."\\.nuget\\packages\\system.buffers\\4.5.0\\lib\\netstandard2.0\\*.dll\" . \z
&& popd" )
os.execute "copy _bundle\\libzstd.dll lib\\UnityBundle\\"
        end
    }

    newaction
    {
        trigger     = "bundle",
        description = "Bundle dotnet dlls",
        execute = function ()
            -- os.execute "premake5 bundleBuild"
            os.execute "pushd _bundle \z
&& ..\\tools\\ILRepack.exe /out:\"..\\lib\\UnityBundle\\UnityBundle.dll\" \z
\"Newtonsoft.Json.dll\" \z
\"Microsoft.Extensions.Caching.Abstractions.dll\" \z
\"Microsoft.Extensions.Caching.Memory.dll\" \z
\"Microsoft.Extensions.DependencyInjection.Abstractions.dll\" \z
\"Microsoft.Extensions.Logging.Abstractions.dll\" \z
\"Microsoft.Extensions.Options.dll\" \z
\"Microsoft.Extensions.Primitives.dll\" \z
\"System.Buffers.dll\" \z
\"System.Memory.dll\" \z
\"System.Runtime.CompilerServices.Unsafe.dll\" \z
\"ZstdNet.dll\" \z
\"MathNet.Numerics.dll\" \z
&& popd"
        end
    }


else

     -- MacOSX and Linux.
    
     newaction
     {
         trigger     = "solution",
         description = "Open the game-estates solution",
         execute = function ()
         end
     }
 
     newaction
     {
         trigger     = "loc",
         description = "Count lines of code",
         execute = function ()
             os.execute "wc -l *.cs"
         end
     }
     
end

newaction
{
    trigger     = "clean",
    description = "Clean all build files and output",
    execute = function ()
        files_to_delete = 
        {
            "*.make",
            "*.zip",
            "*.tar.gz",
            "*.db",
            "*.opendb"
        }
        directories_to_delete = 
        {
            "_bundle",
            "obj",
            "ipch",
            "bin",
            "nupkgs",
            ".vs",
            "Debug",
            "Release",
            "release"
        }
        for i,v in ipairs( directories_to_delete ) do
          os.rmdir( v )
        end
        if not os.ishost "windows" then
            os.execute "find . -name .DS_Store -delete"
            for i,v in ipairs( files_to_delete ) do
              os.execute( "rm -f " .. v )
            end
        else
            for i,v in ipairs( files_to_delete ) do
              os.execute( "del /F /Q  " .. v )
            end
        end

    end
}
