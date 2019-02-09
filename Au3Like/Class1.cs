using System;
using System . Collections . Generic;
using System . Linq;
using System . Text;
using System . Windows . Forms;
using System . IO;                // File 관련 추가

using System . CodeDom;
using System . CodeDom . Compiler;
using Microsoft . CSharp;
using System . Reflection;
using System . Diagnostics;
using System . Net;                       // 레지스트리 작업 때문엥... webclient

using System . Runtime . InteropServices;  //DllImport


namespace Au3Like
    {
    public class AutoClosingMessageBox
        {
        System . Threading . Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox ( string text , string caption , int timeout )
            {
            _caption = caption;
            _timeoutTimer = new System . Threading . Timer ( OnTimerElapsed ,
                null , timeout , System . Threading . Timeout . Infinite );
            using ( _timeoutTimer )
                MessageBox . Show ( text , caption );
            }
        public static void Show ( string text , string caption , int timeout )
            {
            new AutoClosingMessageBox ( text , caption , timeout );
            }
        void OnTimerElapsed ( object state )
            {
            IntPtr mbWnd = FindWindow ( "#32770" , _caption ); // lpClassName is #32770 for MessageBox
            if ( mbWnd != IntPtr . Zero )
                SendMessage ( mbWnd , WM_CLOSE , IntPtr . Zero , IntPtr . Zero );
            _timeoutTimer . Dispose ( );
            }
        const int WM_CLOSE = 0x0010;
        [System . Runtime . InteropServices . DllImport ( "user32.dll" , SetLastError = true )]
        static extern IntPtr FindWindow ( string lpClassName , string lpWindowName );
        [System . Runtime . InteropServices . DllImport ( "user32.dll" , CharSet = System . Runtime . InteropServices . CharSet . Auto )]
        static extern IntPtr SendMessage ( IntPtr hWnd , UInt32 Msg , IntPtr wParam , IntPtr lParam );
        }



    public class Win32API
        {
        //https://xarfox.tistory.com/45
        public const Int32 WM_COPYDATA = 0x004A;

        public struct COPYDATASTRUCT
            {
            public IntPtr dwData;
            public UInt32 cbData;
            [MarshalAs ( UnmanagedType . LPStr )]
            public string lpData;
            }
        [DllImport ( "user32.dll" , CharSet = CharSet . Auto )]
        public static extern IntPtr SendMessage ( IntPtr hWnd , UInt32 Msg , IntPtr wParam , ref Win32API . COPYDATASTRUCT lParam );

        [DllImport ( "user32.dll" , CharSet = CharSet . Auto )]
        public static extern IntPtr SendMessage ( IntPtr hWnd , UInt32 Msg , IntPtr wParam , StringBuilder lParam );

        [DllImport ( "user32.dll" )]
        public static extern IntPtr SendMessage ( IntPtr hWnd , UInt32 Msg , IntPtr wParam , [MarshalAs ( UnmanagedType . LPStr )] string lParam );

        [DllImport ( "user32.dll" , EntryPoint = "SendMessageW" )]
        public static extern IntPtr SendMessageW ( IntPtr hWnd , UInt32 Msg , IntPtr wParam , [MarshalAs ( UnmanagedType . LPWStr )] string lParam );

        [DllImport ( "user32.dll" )]
        public static extern IntPtr SendMessage ( IntPtr hWnd , UInt32 Msg , Int32 wParam , Int32 lParam );

        [DllImport ( "user32.dll" , CharSet = CharSet . Auto , SetLastError = false )]
        public static extern IntPtr SendMessage ( HandleRef hWnd , UInt32 Msg , IntPtr wParam , IntPtr lParam );

        [DllImport ( "user32.dll" , CharSet = CharSet . Auto )]
        public static extern IntPtr FindWindow ( string strClassName , string strWindowName );
        }


    public static class au3
        {

        /*
                [DllImport ( "user32.dll" , SetLastError = true )]
                static extern IntPtr FindWindowEx ( IntPtr hwndParent , IntPtr hwndChildAfter , string lpszClass , string lpszWindow );
                [DllImport ( "user32.dll" , SetLastError = true )]
                public static extern IntPtr FindWindowEx ( IntPtr parentHandle , IntPtr childAfter , string className , string windowTitle );
                [DllImport("user32.dll", CharSet = CharSet.Auto)]
                public static extern IntPtr FindWindow(string strClassName, string strWindowName);
                [DllImport("user32.dll")]
                public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
               
                public const int WM_SYSCOMMAND = 0x0112;
                public const int SC_CLOSE = 0xF060;
        */




        public static Win32API . COPYDATASTRUCT data;


        // 폼에 sendmessage
        public static void SendMessage ( string FormText , string sMSG )
            {
            /*
             int hWnd = (int)FindWindow(FormText, sMSG);
             if( hWnd > 0 )
             {
               SendMessage(hWnd, WM_SYSCOMMAND, SC_CLOSE, 0);
             }
             */
            //https://xarfox.tistory.com/45
            String message = sMSG;// this . txtMessage . Text;
            String processName = FormText;// this . txtProcessName . Text;
            data = new Win32API . COPYDATASTRUCT ( );
            data . dwData = ( IntPtr )( 1024 + 604 );
            data . cbData = ( uint )message . Length * sizeof ( char );
            data . lpData = message;
            IntPtr handle = Win32API . FindWindow ( null , processName );
            if ( handle . ToInt32 ( ) > 0 )
                Win32API . SendMessage ( handle , Win32API . WM_COPYDATA , IntPtr . Zero , ref data );

            }

        public static string GetMyIP ( )
            {
            // 호스트 이름으로 IP를 구한다 
            IPHostEntry ipEntry = Dns . GetHostEntry ( Dns . GetHostName ( ) );
            IPAddress[] addr = ipEntry . AddressList;
            string ip = string . Empty;
            for ( int i = 0 ; i < addr . Length ; i++ )
                {
                //Console.WriteLine("IP Address {0}: {1} ", i, addr[i].ToString());
                ip += addr[i] . ToString ( ) + " ";

                }
            return ip;

            }

        public static string GlobalIP ( )
            {
            // 외부아이피 알려주는 사이트에서 문자열 가져오기 
            String WanIP = new WebClient ( ) . DownloadString ( "https://livow.familyds.com/ip.php" );
            //Console.WriteLine("IP Address : {0} ", WanIP);
            return WanIP;

            }



        // 프로그램의 시작 폴더경로 반환
        public static string ScriptDir = Application . StartupPath;

        // 윈도우 System32 폴더경로 반환
        public static string SystemDir = Environment . SystemDirectory;

        // 윈도우 사용자 바탕화면 폴더경로 반환
        public static string DesktopDir = Environment . GetFolderPath ( Environment . SpecialFolder . DesktopDirectory );

        // 윈도우 사용자 내문서 폴더경로 반환
        public static string MyDocumentsDir = Environment . GetFolderPath ( Environment . SpecialFolder . MyDocuments );

        // 윈도우 폴더경로 반환
        public static string WindowsDir = Environment . GetFolderPath ( Environment . SpecialFolder . Windows );

        // 임시 폴더 반환
        public static string TempDir = System . IO . Path . GetTempPath ( );

        // 프로그램 경로 반환
        public static string ProgramDir = Environment . GetEnvironmentVariable ( "ProgramFiles" );


        // 바탕화면 해상도 넓이 반환
        //public static int DeskTopWidth = Screen.PrimaryScreen.Bounds.Width;

        /*

        // 바탕화면 해상도 높이 반환
        public static int DeskTopHeight = Screen . PrimaryScreen . Bounds . Height;
        
        // 바탕화면 해상도 넓이 반환 [듀얼,가상포함]
        public static int DeskTopWidthV = System.Windows.Forms.SystemInformation.VirtualScreen.Width;

        // 바탕화면 해상도 높이 반환 [듀얼,가상포함]
        public static int DeskTopHeightV = System.Windows.Forms.SystemInformation.VirtualScreen.Height;
        */



        // 폴더 비움 UDF
        private static void clearFolder ( string path )
            {
            DirectoryInfo dir = new DirectoryInfo ( path );

            foreach ( FileInfo fi in dir . GetFiles ( ) )
                {
                fi . Delete ( );
                }

            foreach ( DirectoryInfo di in dir . GetDirectories ( ) )
                {
                clearFolder ( di . FullName );
                di . Delete ( );
                }
            }

        //문자열클립보드에서 읽기
        public static string ClipGet ( )
            {
            string clip = Clipboard . GetText ( );
            return clip;
            }

        //문자열 클립보드로                        
        public static void ClipPut ( string clip )
            {
            Clipboard . SetText ( clip );
            }

        // 숫자에 3자리마다 , 추가해서 반환하기 
        public static string Comma ( int comma )
            {
            string str = string . Format ( "{0:#,###}" , comma ) . PadLeft ( 7 );
            return str;
            }

        //폴더복사       
        public static bool DirCopy ( string spath , string dpath )
            {
            DirectoryCopy ( @spath , @dpath , true );
            return true;
            }

        //폴더생성
        public static bool DirCreate ( string path )
            {
            if ( Directory . Exists ( @path ) )
                {
                return false;
                }
            Directory . CreateDirectory ( @path );
            return true;

            }

        // 폴더 크기 반환 Byte단위
        public static string DirGetSize ( string path )
            {
            // 1.
            // Get array of all file names.
            string[] a = Directory . GetFiles ( path , "*.*" );

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach ( string name in a )
                {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo ( name );
                b += info . Length;
                }
            // 4.
            // Return total size
            return Convert . ToString ( b );
            }

        // 폴더 이동
        public static bool DirMove ( string spath , string dpath )
            {
            Directory . Move ( @spath , @dpath );
            return true;
            }

        // 폴더 삭제
        public static bool DirRemove ( string path )
            {
            clearFolder ( path );   // 폴더내부 비움
            Directory . Delete ( @path );  // 폴더자체 삭제 [내용물있다면 Delete오류]
            return true;
            }

        // 폵더 복사 UDF
        private static void DirectoryCopy ( string sourceDirName , string destDirName , bool copySubDirs )
            {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo ( sourceDirName );

            if ( !dir . Exists )
                {
                throw new DirectoryNotFoundException (
                    "Source directory does not exist or could not be found: "
                    + sourceDirName );
                }

            DirectoryInfo[] dirs = dir . GetDirectories ( );
            // If the destination directory doesn't exist, create it.
            if ( !Directory . Exists ( destDirName ) )
                {
                Directory . CreateDirectory ( destDirName );
                }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir . GetFiles ( );
            foreach ( FileInfo file in files )
                {
                string temppath = Path . Combine ( destDirName , file . Name );
                file . CopyTo ( temppath , false );
                }

            // If copying subdirectories, copy them and their contents to new location.
            if ( copySubDirs )
                {
                foreach ( DirectoryInfo subdir in dirs )
                    {
                    string temppath = Path . Combine ( destDirName , subdir . Name );
                    DirectoryCopy ( subdir . FullName , temppath , copySubDirs );
                    }
                }
            }

        // 프로그램 종료
        public static void Exit ( )
            {
            Environment . Exit ( 0 );
            }

        //파일복사
        public static void FileCopy ( string sFile , string dFile , bool over = true )
            {
            File . Copy ( sFile , dFile , over );
            }

        //파일 바로가기 생성
        public static void FileCreateShortcut ( string sFile )
            {

            }

        // 파일삭제
        public static void FileDelete ( string fName )
            {
            if ( File . Exists ( fName ) )
                {
                File . Delete ( fName );
                }
            }

        // 원격지 서버 파일 다운로드
        public static void FileDownload ( string url , string local )
            {
            WebClient webClient = new WebClient ( );
            webClient . DownloadFile ( url , @local );
            }


        // 원격지 서버에 로컬 파일 업로드  FTP 이용 
        public static void FileUpload ( string sFile , string server , string ftpID , string ftpPW )
            {
            string oName = FileGetFileName ( sFile );
            FtpWebRequest requestFTPUploader = ( FtpWebRequest )WebRequest . Create ( server + "/" + oName );

            requestFTPUploader . Credentials = new NetworkCredential ( ftpID , ftpPW );
            requestFTPUploader . Method = WebRequestMethods . Ftp . UploadFile;

            FileInfo fileInfo = new FileInfo ( @sFile );
            FileStream fileStream = fileInfo . OpenRead ( );

            int bufferLength = 2048;
            byte[] buffer = new byte[bufferLength];

            Stream uploadStream = requestFTPUploader . GetRequestStream ( );
            int contentLength = fileStream . Read ( buffer , 0 , bufferLength );

            while ( contentLength != 0 )
                {
                uploadStream . Write ( buffer , 0 , contentLength );
                contentLength = fileStream . Read ( buffer , 0 , bufferLength );
                }

            uploadStream . Close ( );
            fileStream . Close ( );
            requestFTPUploader = null;

            // Console . ReadLine ( );
            }





        //  원격지 서버에서 파일 업데이트 처리
        //  원격파일명, 로컬업데이트할 파일명,원격파일버젼명[ver.txt]
        //  urlFile  "http://111.111.111.111/update/test.exe" 
        //  verFile "http://111.111.111.111/update/zver.txt"
        //  selfFile ScriptDir + "/test.exe" 
        public static void FileUpdate ( string urlFile , string selfFile , string verFile )
            {
            WebClient web = new WebClient ( );
            System . IO . Stream stream = web . OpenRead ( verFile );
            using ( System . IO . StreamReader reader = new System . IO . StreamReader ( stream ) )
                {
                string zver = reader . ReadToEnd ( );
                string VersionNumber = FileGetVersion ( selfFile );
                if ( zver != VersionNumber )
                    {
                    MsgBox ( "새버젼" , "요청하신 프로그램이 업데이트됩니당!!" , 3000 );
                    string onName = StringSplit ( @selfFile , "." , 1 );
                    string onExt = StringSplit ( @selfFile , "." , 2 );
                    // test.exe 면 test2.exe로 이름변경처리
                    System . IO . File . Move ( @selfFile , @onName + "2." + onExt );
                    //  MessageBox . Show ( "rename ok" );
                    //Download ( );
                    FileDownload ( urlFile , selfFile );

                    }

                }
            }

        // 파일의 존재 여부 반환
        public static bool FileExists ( string fName )
            {
            if ( File . Exists ( fName ) )
                {
                return true;

                }
            return false;
            }

        // 파일속성반환
        public static string FileGetAttrib ( string fName )
            {
            FileAttributes attributes = File . GetAttributes ( fName );
            return Convert . ToString ( attributes );
            }

        // 파일크기반환
        public static string FileGetSize ( string fName )
            {
            long length = new System . IO . FileInfo ( fName ) . Length;
            return Convert . ToString ( length );
            }

        // 파일 시간 반환 C:생성 W:기록 A:접근
        public static string FileGetTime ( string fName , string fc )
            {
            FileInfo fi = new FileInfo ( fName );
            DateTime created , Lastmodified , LastAccessed;
            if ( fc == "C" )
                {
                created = fi . CreationTime;
                return created . ToString ( );
                }
            if ( fc == "W" )
                {
                Lastmodified = fi . LastWriteTime;
                return Lastmodified . ToString ( );
                }
            if ( fc == "A" )
                {
                LastAccessed = fi . LastAccessTime;
                return LastAccessed . ToString ( );
                }
            return "not";
            }

        //주어진 파일명에서 경로만 잘라 반환
        public static string FileGetDir ( string fName )
            {
            FileInfo fi = new FileInfo ( fName );
            return fi . DirectoryName;
            }
        //주어진 전체 경로+파일명에서 파일명만 잘라 반환
        public static string FileGetFileName ( string fName )
            {
            string dir = FileGetDir ( fName );                     //경로+파일명에서 경로만 추출
            string oName = StringReplace ( fName , dir , "" );// 파일명 
            oName = StringReplace ( oName , "\\" , "" );
            return oName;
            }



        //파일 버젼 반환
        public static string FileGetVersion ( string fName )
            {
            //http://whiteat.com/WhiteAT_Csharp/49477

            FileVersionInfo myFI = FileVersionInfo . GetVersionInfo ( @fName );
            return myFI . FileVersion . ToString ( );
            }

        //파일 설치 C# 은 함수나 메서드 단위로 처리 불가능 ㅠ.ㅠ
        public static void FileInstall ( string sPath , string dPath )
            {
            /*
            System . Reflection . Assembly assem =
             System . Reflection . Assembly . LoadFrom ( @sPath );
            System . IO . Stream fs = assem . GetManifestResourceStream ( sPath);
            var rr = new System . Resources . ResourceReader ( fs ); 
            */

            //https://slaner.tistory.com/tag/CodeDom
            //https://code-examples.net/ko/q/18e9ce6
            //https://code-examples.net/ko/q/c6d962

            //ReadResourceFile ( sPath );
            //CompilerParameters cp = new CompilerParameters ( );
            //CSharpCodeProvider provider = new CSharpCodeProvider ( );
            //if ( provider . Supports ( GeneratorSupport . Resources ) )
            //    cp . EmbeddedResources . Add ( "MySql.Data.dll" );

            //    EmbeddedAssembly . Load ( sPath , dPath  );
            //https://winkey.tistory.com/2
            //https://xinics.tistory.com/89
            //https://www.codeproject.com/Articles/528178/Load-DLL-From-Embedded-Resource?msg=4474268#xx4474268xx

            }

        // 파일이동
        public static void FileMove ( string sFile , string dFile )
            {
            File . Move ( sFile , dFile );
            }

        // 특정 파일에 한줄씩 Append + 줄바꿈
        public static void FileWriteLine ( string sFile , string text )
            {
            using ( StreamWriter w = File . AppendText ( @sFile ) )
                {
                w . Write ( text + "\r\n" );
                }
            }

        // 특정 파일에 날짜+시간 기록후 텍스트 Append + 줄바꿈
        public static void FileWriteLine ( string sFile , string text , bool log )
            {
            using ( StreamWriter w = File . AppendText ( @sFile ) )
                {
                string nlog = Now ( );
                w . Write ( "[" + nlog + "] " + text + "\r\n" );
                }
            }

        // 특정파일의 특정 Line을 수정 저장
        public static void FileWriteLine ( string sFile , string text , int line )
            {
            string[] lines = System . IO . File . ReadAllLines ( @sFile );
            lines[line - 1] = text;
            System . IO . File . WriteAllLines ( @sFile , lines );
            }

        public static string ReadResourceFile ( string filename )
            {
            var thisAssembly = Assembly . GetExecutingAssembly ( );
            using ( var stream = thisAssembly . GetManifestResourceStream ( filename ) )
                {
                using ( var reader = new StreamReader ( stream ) )
                    {
                    return reader . ReadToEnd ( );
                    }
                }
            }

        // 단순 출력형 메세지 박스 문자열
        public static void MsgBox ( string fMSG )
            {
            MessageBox . Show ( fMSG );
            }

        // 질문 출력형 메세지박스[Y/N]  타이틀,문자열 반환 true/false
        public static bool MsgBox ( string Title , string fMSG )
            {
            bool select = false;
            if ( MessageBox . Show ( fMSG , Title , MessageBoxButtons . YesNo ) == DialogResult . Yes )
                {
                select = true;
                }
            return select;

            }

        // 타이틀,본문,시간제한후 자동소멸 메세지박스
        public static void MsgBox ( string Title , string Msg , int time )
            {
            AutoClosingMessageBox . Show ( Msg , Title , time );
            }

        // yyyy-mm-dd  현재 년월일
        public static string NowDate ( )
            {
            string sdate = DateTime . Now . ToString ( "yyyy-MM-dd" );
            return sdate;
            }

        // hh:mm:ss       현재 시간분초
        public static string NowTime ( )
            {
            string stime = DateTime . Now . ToString ( "HH:mm:ss" );
            return stime;
            }

        // yyyy-mm-dd hh:mm:ss  년월일 시간분초 모두
        public static string Now ( )
            {
            string sdate = DateTime . Now . ToString ( "yyyy-MM-dd" );
            string stime = DateTime . Now . ToString ( "HH:mm:ss" );
            return sdate + " " + stime;
            }

        //폴더인지 파일인지
        public static bool IsFolder ( string sPath )
            {
            FileAttributes attr = File . GetAttributes ( @sPath );
            bool val;
            if ( ( attr & FileAttributes . Directory ) == FileAttributes . Directory )
                val = true;
            else
                val = false;
            return val;
            }

        // 문자열에 전체가 숫자인지 여부 문자열발견[false] 미발견[true]
        public static bool IsNumber ( char[] Munja )
            {
            bool check = true;
            for ( int i = 0 ; i < Munja . Length ; i++ )
                {
                if ( char . IsNumber ( Munja[i] ) == false )
                    {
                    check = false;
                    return check;
                    }

                }
            return check;

            }

        // 정해진 숫자중에 랜덤 수 발생
        public static int Random ( int start , int end )
            {
            Random r = new Random ( ); // 랜덤 인스턴스 생성
            int iNum = r . Next ( start , end );  // 0 에서 99 사이에서 랜덤수 생성
            return iNum;
            }

        // 특정 외부 프로그램 실행
        public static void Run ( string sFile )
            {
            Process . Start ( @sFile );
            }

        // os버젼 찾기
        public static string GetOSVersion ( )
            {
            //http://whiteat.com/index.php?mid=WhiteAT_Csharp&page=5&document_srl=59066
            OperatingSystem os = Environment . OSVersion;
            Version v = os . Version;
            string vma = v . Major . ToString ( );
            string vmi = v . Minor . ToString ( );
            return vma + " " + vmi;
            }

        // 특정키 전송
        public static void Send ( string key )
            {
            SendKeys . Send ( "{key}" );
            }


        // 숫자를 문자열로 변환
        public static string String ( int num )
            {
            return Convert . ToString ( num );
            }

        // 문자를 숫자로 변환
        public static int Number ( string Munja )
            {
            return Convert . ToInt32 ( Munja );
            }

        // 평문암호화   / 암호화 key는 8자리
        public static string StringEncrypt ( string cryptString , string key )
            {
            WATCrypt scrypt = new WATCrypt ( key );
            string vcrypt = scrypt . Encrypt ( cryptString );
            return vcrypt;
            }

        //암호화를 평문으로  / 암호화 key는 8자리
        public static string StringDecrypt ( string cryptString , string key )
            {
            WATCrypt scrypt = new WATCrypt ( key );
            string vcrypt = scrypt . Decrypt ( cryptString );
            return vcrypt;
            }

        // 문자열 비교
        public static int StringCompare ( string Munja , string cp )
            {
            return Munja . CompareTo ( cp );
            }

        //문자열분리 배열반환
        public static string[] StringSplit ( string Munja , string spl )
            {
            string[] sMunja;
            sMunja = Munja . Split ( new string[] { spl } , StringSplitOptions . None );
            return sMunja;
            }

        //문자열분리 요소한개만 반환
        public static string StringSplit ( string Munja , string spl , int length = 0 )
            {
            string[] sMunja;
            string sM;
            sMunja = Munja . Split ( new string[] { spl } , StringSplitOptions . None );
            sM = sMunja[length - 1];
            return sM;
            }

        // 문자열이있는가  검색
        public static bool StringInStr ( string Munja , string ser )
            {
            int val = 0;
            val = Munja . IndexOf ( ser );
            if ( val > 0 )
                {
                return true;
                }
            return false;
            }

        // 문자열소문자로
        public static string StringLower ( string Munja )
            {
            return Munja . ToLower ( );
            }

        // 문자열대문자로
        public static string StringUpper ( string Munja )
            {
            return Munja . ToUpper ( );
            }

        // 문자열왼쪽에서자르기  
        public static string StringLeft ( string Munja , int length )
            {
            return Munja . Remove ( length );
            }

        // 문자열 오른쪽에서 자르기
        public static string StringRight ( string Munja , int length )
            {
            if ( length <= Munja . Length )
                {
                return Munja . Substring ( Munja . Length - length );
                }
            return Munja;
            }

        // 문자열중간에서 갯수만큼자르기
        public static string StringMid ( string Munja , int f , int s )
            {
            return Munja . Remove ( f , s );
            }

        // 문자열 중간에 삽입
        public static string StringInsert ( string Munja , int ct , string inMunja )
            {
            return Munja . Insert ( ct , inMunja );
            }

        // 문자열 갯수
        public static int StringLen ( string Munja )
            {
            return Munja . Length;
            }

        // 문자열 교체
        public static string StringReplace ( string Munja , string sMun , string cMun )
            {
            return Munja . Replace ( sMun , cMun );
            }

        // 문자열 왼쪽 혹은 오른쪽에 공백문자 갯수만큼 넣기 mcase 1:Left , mcase 2:right
        public static string StringReplaceS ( string Munja , int Length , int mcase )
            {
            if ( mcase == 1 )
                {
                Munja = Munja . PadLeft ( Length ); //[   1] 왼쪽에3개 공백
                }
            if ( mcase == 2 )
                {
                Munja = Munja . PadRight ( Length ); //[1   ]  오른쪽에 공백3개
                }
            return Munja;
            }

        // 문자열 왼쪽 혹은 오른쪽에 특정문자 갯수만큼 넣기 mcase 1:Left , mcase 2:right
        public static string StringReplaceS ( string Munja , int Length , int mcase , char cMun )
            {
            if ( mcase == 1 )
                {
                Munja = Munja . PadLeft ( Length , cMun ); // 0001
                }
            if ( mcase == 2 )
                {
                Munja = Munja . PadRight ( Length , cMun ); // 1000
                }
            return Munja;
            }

        // 문자열의 양쪽공백제거
        public static string StringStripWS ( string Munja )
            {
            return Munja . Trim ( );
            }

        // 문자열의 앞쪽공백제거
        public static string StringStripS ( string Munja )
            {
            return Munja . TrimStart ( );
            }

        // 문자열의 뒤쪽공백제거
        public static string StringStripE ( string Munja )
            {
            return Munja . TrimEnd ( );
            }

        public static void ProcessClose ( string pName )
            {
            Process[] processList = Process . GetProcessesByName ( pName );
            if ( processList . Length > 0 )
                {
                processList[0] . Kill ( );
                }
            }

        // 이하 ABC 순서 없이 추가 할 예정

        }

    }

// https://docs.microsoft.com/ko-kr/dotnet/api/system.environment.specialfolder?view=netframework-4.7.2

//Assembly assembly;
//Stream _imageStream;
//StreamReader _textStreamReader;
//assembly 처리를 위하여

// 문자열관련 참고
// http://blog.naver.com/PostView.nhn?blogId=netrance&logNo=110095091966

// 암호화 참고
// https://zinzza.tistory.com/20 , http://whiteat.com/index.php?mid=WhiteAT_Csharp&page=5&document_srl=57152