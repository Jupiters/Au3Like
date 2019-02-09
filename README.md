# Au3Like
C# Library



because C# is not often used,  

I lost essential grammers or methods I need. 

so I create a function  in c # with a 

one-line function like AutoItScript.




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

ScriptDir           = Application . StartupPath;

// 윈도우 System32 폴더경로 반환

SystemDir         = Environment . SystemDirectory;


// 윈도우 사용자 바탕화면 폴더경로 반환

DesktopDir        = Environment . GetFolderPath ( Environment . SpecialFolder . DesktopDirectory );


// 윈도우 사용자 내문서 폴더경로 반환

MyDocumentsDir = Environment . GetFolderPath ( Environment . SpecialFolder . MyDocuments );

// 윈도우 폴더경로 반환

WindowsDir       = Environment . GetFolderPath ( Environment . SpecialFolder . Windows );

// 임시 폴더 반환

TempDir           = System . IO . Path . GetTempPath ( );

// 프로그램 경로 반환
ProgramDir       = Environment.GetEnvironmentVariable("ProgramFiles");

//문자열클립보드에서 읽기   ClipGet ( )

//문자열 클립보드로        ClipPut ( string clip )

// 숫자에 3자리마다 , 추가해서 반환하기 Comma ( int comma )

//폴더복사       DirCopy ( string spath , string dpath )

//폴더생성       DirCreate ( string path )

// 폴더 크기 반환 Byte단위  DirGetSize ( string path )

// 폴더 이동    DirMove ( string spath , string dpath )

// 폴더 삭제    public static bool     DirRemove(string path) 

...........

............


// 프로세스 종료 ProcessClose ( string pName )            
