module Calculator.RpnCalculator

let evalRpnExpr (stack : string[]) =
    let solve items current =
        match (current, items) with
        | "+", y::x::t -> (x + y)::t
        | "-", y::x::t -> (x - y)::t
        | "*", y::x::t -> (x * y)::t
        | "/", y::x::t -> (x / y)::t
        | _ -> (float current)::items
    let result = Seq.fold solve [] stack
    result.Head