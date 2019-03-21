// Learn more about F# at http://fsharp.org

open System
open Calculator.RpnCalculator
open Calculator.Parser

[<EntryPoint>]
let main argv =
    [ "4 2 5 * + 1 3 2 * + /"; "5 4 6 + /"; "10 4 3 + 2 * -"; "2 3 +";
    "90 34 12 33 55 66 + * - + -"; "90 3 -" ]
    |> List.map (fun expr -> expr, evalRpnExpr(expr.Split(' ')))
    |> List.iter (fun (expr, result) -> printfn "(%s) = %A" expr result)

    " 1 + 2 * 3 + 4.6 " 
    |> TokenParser.parse 
    |> TokenParser.print 
    0