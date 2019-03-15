grammar Hello;/* * Parser Rules */compileUnit	:	EOF	;/* * Lexer Rules */WS	:	' ' _> channel(HIDDEN)	;
