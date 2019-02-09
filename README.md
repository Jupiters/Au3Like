# Au3Like
C# Library

C# 작업을 자주하지는 않습니다.
가끔 하지만 필요한 메서드나 클래스를 찾아
문법도 잊어버릴때가 많고 해서 
AutoItScript 와 같이 한줄로 된 함수로 c#의 기능 메서드를 생성합니다.

작업 환경은 Visual Studio 2010 에서 이루어 졌습니다.
컴파일된 Au3Like.dll  파일은 
다른 기타 프로젝트에서 참조 와 using Au3Like; 를 추가함으로써 
이 라이브러리를 바로 사용할수 있게 됩니다.

프로젝트 추가는 https://cafe.naver.com/autoitscript/12699  페이지를 참고 하시기 바랍니다.

현재 추가된 매크로 와 메서드의 목록입니다.

// 프로그램의 시작 폴더경로 반환
public static string   ScriptDir           = Application . StartupPath;

// 윈도우 System32 폴더경로 반환
public static string   SystemDir         = Environment . SystemDirectory;


// 윈도우 사용자 바탕화면 폴더경로 반환
public static string   DesktopDir        = Environment . GetFolderPath ( Environment . SpecialFolder . DesktopDirectory );


// 윈도우 사용자 내문서 폴더경로 반환
public static string   MyDocumentsDir = Environment . GetFolderPath ( Environment . SpecialFolder . MyDocuments );




// 윈도우 폴더경로 반환
public static string   WindowsDir       = Environment . GetFolderPath ( Environment . SpecialFolder . Windows );
// 임시 폴더 반환
public static string   TempDir           = System . IO . Path . GetTempPath ( );
// 프로그램 경로 반환
public static string   ProgramDir       = Environment.GetEnvironmentVariable("ProgramFiles");
//문자열클립보드에서 읽기
public static string   ClipGet ( )
//문자열 클립보드로                        
public static void     ClipPut ( string clip )
// 숫자에 3자리마다 , 추가해서 반환하기 
public static string Comma ( int comma )
//폴더복사       
public static bool     DirCopy ( string spath , string dpath )
//폴더생성
public static bool     DirCreate ( string path )
// 폴더 크기 반환 Byte단위
public static string   DirGetSize ( string path )
// 폴더 이동
public static bool     DirMove ( string spath , string dpath )
// 폴더 삭제
public static bool     DirRemove(string path) 
// 폵더 복사 UDF
private static void    DirectoryCopy ( string sourceDirName , string destDirName , bool copySubDirs )
// 프로그램 종료
public static void     Exit ( )
//파일복사
public static void     FileCopy ( string sFile , string dFile , bool over = true )
//파일 바로가기 생성
public static void    FileCreateShortcut(string sFile)
// 파일삭제
public static void     FileDelete ( string fName )
// 원격지 서버 파일 다운로드
public static void     FileDownload ( string url , string local )
//  원격지 서버에서 파일 업데이트 처리
public static void     FileUpdate ( string urlFile , string selfFile ,string verFile)
// 파일의 존재 여부 반환
public static bool     FileExists ( string fName )
// 파일속성반환
public static string   FileGetAttrib ( string fName )
// 파일크기반환
public static string   FileGetSize ( string fName )
// 파일 시간 반환 C:생성 W:기록 A:접근
public static string   FileGetTime ( string fName , string fc )
//주어진 파일명에서 경로만 잘라 반환
public static string   FileGetDir ( string fName  ) 
//파일 버젼 반환
public static string   FileGetVersion ( string fName ) 
//파일 설치 C# 은 함수나 메서드 단위로 처리 불가능 ㅠ.ㅠ
public static void     FileInstall ( string sPath , string dPath )
// 파일이동
public static void     FileMove ( string sFile , string dFile )
// 단순 출력형 메세지 박스 문자열
public static void     MsgBox ( string fMSG )
// 질문 출력형 메세지박스[Y/N]  타이틀,문자열 반환 true/false
public static bool     MsgBox ( string Title ,string fMSG )
// 타이틀,본문,시간제한후 자동소멸 메세지박스
public static void     MsgBox ( string Title , string Msg , int time )
// yyyy-mm-dd  현재 년월일
public static string   NowDate ( ) 
// hh:mm:ss       현재 시간분초
public static string   NowTime ( )
// yyyy-mm-dd hh:mm:ss  년월일 시간분초 모두
public static string   Now ( )
//폴더인지 파일인지
public static bool     IsFolder (string sPath )
// 문자열에 전체가 숫자인지 여부 문자열발견[false] 미발견[true]
public static bool     IsNumber ( char[] Munja )
// 정해진 숫자중에 랜덤 수 발생
public static int       Random ( int start , int end )
// 특정 외부 프로그램 실행
public static void     Run(string sFile)
// os버젼 찾기
public static string   GetOSVersion ( )
// 특정키 전송
public static void     Send ( string key )
// 숫자를 문자열로 변환
public static string   String ( int num )
// 문자를 숫자로 변환
public static int       Number (string Munja )
// 평문암호화   / 암호화 key는 8자리
public static string   StringEncrypt ( string cryptString,string key)
//암호화를 평문으로  / 암호화 key는 8자리
public static string   StringDecrypt ( string cryptString , string key )
// 문자열 비교
public static int       StringCompare ( string Munja , string cp )
//문자열분리 배열반환
public static string[] StringSplit ( string Munja , string spl )
//문자열분리 요소한개만 반환
public static string   StringSplit ( string Munja , string spl , int length = 0 )
// 문자열이있는가  검색
public static bool     StringInStr ( string Munja , string ser )
// 문자열소문자로
public static string   StringLower ( string Munja )
// 문자열대문자로
public static string   StringUpper ( string Munja )
// 문자열왼쪽에서자르기  
public static string   StringLeft ( string Munja , int length )
// 문자열 오른쪽에서 자르기
public static string   StringRight(string Munja, int length)
// 문자열중간에서 갯수만큼자르기
public static string   StringMid ( string Munja , int f, int s )
// 문자열 중간에 삽입
public static string   StringInsert(string Munja, int ct ,string inMunja)
// 문자열 갯수
public static int       StringLen ( string Munja )
// 문자열 교체
public static string   StringReplace ( string Munja , string sMun,string cMun )
// 문자열 왼쪽 혹은 오른쪽에 공백문자 갯수만큼 넣기 mcase 1:Left , mcase 2:right
public static string   StringReplaceS ( string Munja , int Length,int mcase )
// 문자열 왼쪽 혹은 오른쪽에 특정문자 갯수만큼 넣기 mcase 1:Left , mcase 2:right
public static string   StringReplaceS ( string Munja , int Length , int mcase,char cMun )
// 문자열의 양쪽공백제거
public static string   StringStripWS ( string Munja ) 
// 문자열의 앞쪽공백제거
public static string   StringStripS ( string Munja ) 
// 문자열의 뒤쪽공백제거
public static string   StringStripE ( string Munja ) 
// 프로세스 종료
public static void ProcessClose ( string pName )            
