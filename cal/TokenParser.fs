﻿module Calculator.Parser.TokenParser

open System
// 文法
// expr -> expr + term | expr - term | term
// term -> term * factor | term / factor | factor
// factor -> digital | (expr)

type TokenID =
    | PLUS = 0
    | MINUS = 1
    | TIMES = 2
    | DIV = 3
    | OPEN_PAREN = 4
    | CLOSE_PAREN = 5
    | DIGIT = 6

type Token(kind: TokenID,value: string) =
    member x.kind = kind
    member x.value = value


let private token (items: Token list) (current:string) =
    match current with
        | "+" -> Token(TokenID.PLUS,"") :: items
        | "-" -> Token(TokenID.MINUS,"") :: items
        | "*" -> Token(TokenID.TIMES,"") :: items
        | "/" -> Token(TokenID.DIV,"") :: items
        | "(" -> Token(TokenID.OPEN_PAREN,"") :: items 
        | ")" -> Token(TokenID.CLOSE_PAREN,"") :: items
        | _ -> Token(TokenID.DIGIT,current) :: items

let parse (str:string) =
    let strs = str.Split(' ')
    Seq.toArray(Seq.rev(Seq.fold token [] strs))

let print (tokens: Token[]) = 
    let show (token: Token) =
        match token.kind with 
            | TokenID.PLUS -> printf "PLUS "
            | TokenID.MINUS -> printf "MINUS "
            | TokenID.TIMES -> printf "TINES "
            | TokenID.DIV -> printf "DIV "
            | TokenID.OPEN_PAREN -> printf "OPEN_PAREN "
            | TokenID.CLOSE_PAREN -> printf "CLOSE_PAREN "
            | TokenID.DIGIT -> printf "(DIGIT %s) " token.value
            | _ -> printf " ERROR: unkown token"
    Seq.iter show tokens