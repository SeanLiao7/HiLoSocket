# HiLoSocket
HiLoSocket 是一個以 TCP / IP 為傳輸基底的溝通介面 library，其 Socket 溝通是以 msdn 非同步範例加以改寫與擴充。

msdn 非同步 Socket 範例：

非同步用戶端

https://msdn.microsoft.com/zh-tw/library/bew39x2a(v=vs.110).aspx

非同步伺服器端

https://msdn.microsoft.com/zh-tw/library/fx6588te(v=vs.110).aspx

-------------------------------------------------------------------------------------------------------------
主要功能：
1. 支援依照需求個別初始化用戶端與伺服器端。
2. 支援一發一收的交握機制（利用事件）。
3. 支援用戶端傳送逾時機制（當未收到伺服器端回傳時）。
3. 支援自定義 logger 寫入日誌。
5. 支援傳遞自定義物件模型。
6. 支援多種物件模型格式化方式。
7. 支援資料壓縮。

-------------------------------------------------------------------------------------------------------------
使用方式：
1. 利用 ClientBuilder 建立客戶端。

        var logger = new ConsoleLogger( );
        var client = ClientBuilder<SocketCommandModel>.CreateNew( )
            .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8001 ) )
            .SetRemoteIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
            .SetFormatterType( FormatterType.BinaryFormatter )
            .SetCompressType( FormatterType.Default )
            .SetTimeoutTime( 2000 )
            .SetLogger( logger )
            .Build( );

        ClientBuilder<SocketCommandModel>.CreateNew( ) → 以 SocketCommandModel 為傳輸資料模型建立客戶端
        SetLocalIpEndPoint → 設定本地 IP 位置
        SetRemoteIpEndPoint → 設定伺服器端 IP 位置
        SetFormatterType → 設定格式化方式
        SetCompressType → 設定資料壓縮方式
        SetTimeoutTime → 設定傳輸逾時時間
        SetLogger → 設定 logger
        Build → 依照設定建立客戶端物件
        
2. 利用 ServerBuilder 建立伺服器端。

        var logger = new ConsoleLogger( );
        var server = ServerBuilder<SocketCommandModel>.CreateNew( )
            .SetLocalIpEndPoint( new IPEndPoint( IPAddress.Parse( "127.0.0.1" ), 8000 ) )
            .SetFormatterType( FormatterType.BinaryFormatter )
            .SetCompressType( FormatterType.Default )
            .SetLogger( logger )
            .Build( );

        ServerBuilder<SocketCommandModel>.CreateNew( ) → 以 SocketCommandModel 為傳輸資料模型建立伺服器端
        SetLocalIpEndPoint → 設定本地 IP 位置
        SetFormatterType → 設定格式化方式
        SetCompressType → 設定資料壓縮方式
        SetLogger → 設定 logger
        Build → 依照設定建立伺服器端物件
