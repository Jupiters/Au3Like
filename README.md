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

항목이 좀 많습니다...

............


// 프로세스 종료 ProcessClose ( string pName )            




단순하게 AU3  함수 기능과 C#의 메서드 기능을 비교하려고 시작했습니다.

[김삿갓님] 의 아이디어 제공으로 모듈화 해봅니다.

DLL에 정적인 클래스를 만들어 함수화  / 모듈화를 시작합니다

자주 사용하는 기능을 독립시켜서 본문에서 불러다 쓸수있게 하려 합니다.


DLL은 생성시 클래스 라이브러리 라는 도구로 만듭니다.
![20](https://user-images.githubusercontent.com/2012078/52516976-fb932b80-2c76-11e9-9af7-805f79fe2f75.jpg)

오토잇의 함수명을 그대로 구현할것이므로 프로젝트 / 네임스페이스 이름을 Au3Like로 설정하겠습니다.

생성되는 파일명은 [컴파일이후]  Au3Like.dll 이 됩니다.

![21](https://user-images.githubusercontent.com/2012078/52516987-2b423380-2c77-11e9-944b-39836b096ac0.jpg)

내부 클래스명은 au3 로 정했습니다.

그리고 오토잇의 매크로 를 흉내내서
ScriptDir   / SystemDir  / DesktopDir / TempDir 을 변수로 처리했습니다.

![22](https://user-images.githubusercontent.com/2012078/52516988-2ed5ba80-2c77-11e9-9d6c-38fb016d76c9.jpg)

오토잇의 File 관련 함수 몇개도 샘플로 만들어 보았습니다.



![23](https://user-images.githubusercontent.com/2012078/52516989-3301d800-2c77-11e9-8582-099b769fed0e.jpg)

오토잇의 폴더 관련 Dir 함수도 몇개 만들어 봅니다.

Dll 을 컴파일 합니다. 최종 출력물의 위치는 Debug 폴더에 있을 겁니다.


![24](https://user-images.githubusercontent.com/2012078/52516991-372df580-2c77-11e9-85c9-8077393bd817.jpg)

Au3Like.dll 이 6kb 크기로 생성 되었습니다.

본 프로젝트에서  저 파일을 참조로 불러다 쓰면 됩니다.


![25](https://user-images.githubusercontent.com/2012078/52516994-39904f80-2c77-11e9-9862-3552d5d82f96.jpg)

참조 클릭하여 [찾아보기] 로 방금 만든 경로에 들어가서 Au3Like.dll 을 선택후 확인을 누르면
좌측의 참조에 Au3Like  라이브러리가 추가된것을 확인 할수 있습니다.



![26](https://user-images.githubusercontent.com/2012078/52516998-3c8b4000-2c77-11e9-98a3-0ca7e969f6ed.jpg)


정적인 라이브러리 이니 using으로 추가해도 해줍니다.
using Au3Like ; 를 추가 해줍니다.
이전에 사용한
using System . Windows . Forms;
using System . IO;                // File 관련 추가
참조는 필요없게 됬습니다.

저 참조는 이미 dll에서 사전 참조가 되었기에 본문에선 추가할 필요가 없습니다.

이제 함수 호출하듯 불러다 쓰면 되는데

Dll 의 클래스 명이 au3 이었습니다.

au3.FileCopy  ( 원본 파일 , 대상 파일 ) ;  이렇게 한줄로 처리 할수 있게 됩니다.

![27](https://user-images.githubusercontent.com/2012078/52517002-401ec700-2c77-11e9-9248-88692af70d25.jpg)


MsgBox 도 흉내 냈습니다. 클래스명,함수명 이렇게 불러 사용하면 됩니다.


Dll / test  프로젝트 파일로 첨부 합니다.

이제 추가된 클래스 / 함수 명이 입력시 적용됩니다.


![28](https://user-images.githubusercontent.com/2012078/52517003-42812100-2c77-11e9-8fb3-666a5637a0a1.jpg)





![29](https://user-images.githubusercontent.com/2012078/52517004-457c1180-2c77-11e9-9469-303eb7b6de81.jpg)

