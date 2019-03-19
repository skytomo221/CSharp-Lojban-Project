# CSharp Lojban Project

CSharp Lojban Project C #에서 Lojban을 처리하기 위해 만들어진 라이브러리입니다.  
불행히도 아직 개발 중입니다.

## Other Language

만약 한국어를 읽을 수없는 경우에는 다음의 문서를보세요.  
[로지반🌐](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/README.md)
[영어🇬🇧](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.en.md)
[한국🇰🇷](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ko.md)
[일본어🇯🇵](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.ja.md)
[중국🇨🇳](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/doc/README.zh.md)

이 문서의 원문은 일본어이며 모순이 발생한 경우는 일본어가 우선됩니다.  
또한 오역이 존재하는 경우 issues을 세우십시오.

그러나 라이센스는 영어가 우선됩니다.

## Library

[Pegasus](http://otac0n.com/Pegasus/)
[(Source repository)](https://github.com/otac0n/Pegasus)  
Pegasus는 MSBuild 및 Visual Studio와 통합되는 .NET 용 PEG (구문 분석 식 문법) 파서 생성기입니다.  
로반의 문법은 PEG로 쓰여져 있기 때문에, 그 파서를 생성 할 때 사용됩니다.

[Newtonsoft.Json](https://www.newtonsoft.com/json)
[(Source repository)](https://github.com/JamesNK/Newtonsoft.Json)  
Json.NET은 .NET 용으로 유명한 고성능 JSON 프레임 워크입니다.  
주로 로반 분석 결과를 JSON으로 변환 할 때 사용됩니다.

[LojbanGrammer.peg](https://gist.github.com/otac0n/63d8fae45c551c4e8d41c83c53afc17e#file-lojbangrammar-peg)  
Pegasus의 제작자 인 [John Gietzen](https://gist.github.com/otac0n) 씨가 만든 peg 파일입니다.  
현재는 이것을 사용하고 있습니다.  
그러나 다른 로반 파서와 비교하면 다른 결과가 나와 버리므로, 수정이 필요합니다.

## License

라이센스는 MIT 라이센스입니다.  
자세한 내용은 [여기](https://github.com/skytomo221/CSharp-Lojban-Project/blob/master/LICENSE)에서。
