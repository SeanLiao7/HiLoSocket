# HiLoSocket
HiLoSocket 是一個以 TCP / IP 為傳輸基底的溝通介面 library，其 Socket 溝通是以 msdn 非同步範例加以改寫與擴充。

msdn 非同步 Socket 範例：

非同步用戶端

https://msdn.microsoft.com/zh-tw/library/bew39x2a(v=vs.110).aspx

非同步伺服器端

https://msdn.microsoft.com/zh-tw/library/fx6588te(v=vs.110).aspx

[![Build Status](https://dev.azure.com/seanliao/HiLoSocket/_apis/build/status/SeanLiao7.HiLoSocket?branchName=master)](https://dev.azure.com/seanliao/HiLoSocket/_build/latest?definitionId=6&branchName=master)
[![codecov](https://codecov.io/gh/SeanLiao7/HiLoSocket/branch/master/graph/badge.svg)](https://codecov.io/gh/SeanLiao7/HiLoSocket)

-------------------------------------------------------------------------------------------------------------
主要功能：
1. 支援依照需求個別初始化用戶端與伺服器端。
2. 內建一發一收的交握機制（利用事件通知）。
3. 支援用戶端傳送逾時機制（當未收到伺服器端回傳時）。
3. 支援自定義 logger 寫入日誌。
5. 支援傳遞自定義物件模型。
6. 支援多種物件模型序列化方式。
7. 支援資料壓縮。

-------------------------------------------------------------------------------------------------------------
使用方式：
1. 利用 ClientBuilder 建立客戶端

        var logger = new ConsoleLogger( );
        var client = ClientBuilder<SocketCommandModel>.CreateNew( )
            .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8001 ) )
            .SetRemoteIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
            .SetFormatterType( FormatterType.BinaryFormatter )
            .SetCompressType( CompressType.Default )
            .SetTimeoutTime( 2000 )
            .SetLogger( logger )
            .Build( );

        ClientBuilder<SocketCommandModel>.CreateNew( ) → 以 SocketCommandModel 為傳輸資料模型建立客戶端
        SetLocalIpEndPoint → 設定本地 IP 位置
        SetRemoteIpEndPoint → 設定伺服器端 IP 位置
        SetFormatterType → 設定序列化方式
        SetCompressType → 設定資料壓縮方式
        SetTimeoutTime → 設定傳輸逾時時間
        SetLogger → 設定 logger
        Build → 依照設定建立客戶端物件

    1-1. 客戶端傳送資料模型

        client.Send( new SocketCommandModel
        {
            CommandName = "SomeCommand",
            Id = Guid.NewGuid( ),
            Results = "true,true,true,true,false,true,true,true,true,true,true,true,false,true,true,true",
            Time = DateTime.Now
        } );

    1-2. 客戶端連結事件（當接收到伺服器回傳資料模型後觸發）

        client.OnCommandModelReceived += Client_OnAckCommandReceived;
        
2. 利用 ServerBuilder 建立伺服器端

        var logger = new ConsoleLogger( );
        var server = ServerBuilder<SocketCommandModel>.CreateNew( )
            .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
            .SetFormatterType( FormatterType.BinaryFormatter )
            .SetCompressType( CompressType.Default )
            .SetLogger( logger )
            .Build( );

        ServerBuilder<SocketCommandModel>.CreateNew( ) → 以 SocketCommandModel 為傳輸資料模型建立伺服器端
        SetLocalIpEndPoint → 設定本地 IP 位置
        SetFormatterType → 設定序列化方式
        SetCompressType → 設定資料壓縮方式
        SetLogger → 設定 logger
        Build → 依照設定建立伺服器端物件

    2-1. 伺服器端開始監聽

        server.StartListening( );

    2-2. 伺服器端停止監聽

        server.StopListening( );

    2-3. 伺服器連結事件 ( 當接收到用戶端傳送的資料物件後觸發 )

        server.OnCommandModelReceived += Server_OnSocketCommandRecevied;
        
3. 序列化支援種類
        
        BinaryFormatter
        JsonFormatter
        MessagePackFormatter
        ProtobufFormatter
        
4. 壓縮資料種類

        Default ( 不壓縮 )
        GZip
        Deflate

5. 逾時處理

        當客戶端傳送資料至伺服器端後，超過設定的逾時時間且未收到伺服器端回傳結果時；Send 方法將拋出例外。
        
-------------------------------------------------------------------------------------------------------------
MIT License

Copyright (c) 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
