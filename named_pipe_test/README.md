# Test named pipes

## Test on MacOS:

Create named pipes server by dotnet core.
Test client by dotnet core and Unity3D.

Platform: MacOS.

### Server
```shell
cd ./server/

dotnot run

# output:
Pipe server init. Pipe name: clouddeskpipeTest
```

### Client dotnet

```shell
cd ./client_dotnet/

dotnot run

# output:
Unhandled exception. System.PlatformNotSupportedException: Access to remote named pipes is not supported on this platform.
```

### Client unity3d
1. Open unity3d project `./client_unity3d/`
2. In editor, open scene `./client_unity3d/Assets/Scenes/Client.unity`.
3. Ensure that there is the gameobject named 'Client' in hierarchy column, and ensure that the script NamedPipeClient.cs attaches to it.
4. Click Play.
5. On game playing window, find input field. Input test string and click on SendData button.

In Console panel, get error message:
```
Error on creating named pipe: error code -1
UnityEngine.Debug:Log(Object)
NamedPipeClient:SendData(String) (at Assets/Scenes/NamedPipeClient.cs:50)
NamedPipeClient:OnGUI() (at Assets/Scenes/NamedPipeClient.cs:30)
```

### Reference:
[unity3D 命名管道 进程通信](https://blog.csdn.net/nicepainkiller/article/details/53642833): 貌似在window下可以成功实现命令管道通讯。

[Unity3d Mono Compatibility](https://docs.unity3d.com/410/Documentation/ScriptReference/MonoCompatibility.html):
Unity3D基于Mono实现DotNet。

[unity-named-pipes](https://github.com/Lachee/unity-named-pipes): 
A native named pipe wrapper for Unity3D. Have not test on MacOS.
