module Calculator.Parser.TokenParser

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

type TokenParserState(acc: string option,items : Token list) =
    member x.acc = acc 
    member x.items = items 

let private append_token (state: TokenParserState) (token: Token) =
        let items = state.items
        match state.acc with 
            | None -> TokenParserState(None,(token:: items))    
            | Some(value) -> TokenParserState(None,token::Token(TokenID.DIGIT,value) :: items)

let private is_digit (x: char) =  (int8 x) >= 48y && (int8 x) <= 57y || (x = '.')
let private digit_token (state: TokenParserState) (c: char) =
        if  (is_digit c)  then
            let items = state.items
            match state.acc with 
                | None -> TokenParserState(Some(c.ToString()),items)
                | Some(acc)-> TokenParserState(Some(acc + c.ToString()),items)
         else 
            state


let private token (state: TokenParserState) (current: char ) =
    match current with
        | '+' -> append_token state (Token(TokenID.PLUS,""))
        | '-' -> append_token state (Token(TokenID.MINUS,""))
        | '*' -> append_token state (Token(TokenID.TIMES,"")) 
        | '/' -> append_token state (Token(TokenID.DIV,"")) 
        | '(' -> append_token state (Token(TokenID.OPEN_PAREN,"")) 
        | ')' -> append_token state (Token(TokenID.CLOSE_PAREN,"")) 
        | _ -> digit_token state current


let parse (str:string) =
    let initState = TokenParserState(None,[])
    let finalState = Seq.fold token initState str
    let items = finalState.items
    match finalState.acc with 
        | None -> Seq.toArray(Seq.rev(items))
        | Some(value) -> Seq.toArray(Seq.rev(Token(TokenID.DIGIT,value) :: items))

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