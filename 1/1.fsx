open System
open System.IO

let input = File.ReadAllLines "input.txt"

let getNumberFromLine1 (s: string) =
    let numbers =
        s
        |> Seq.choose (fun c ->
            match System.Int32.TryParse(c.ToString()) with
            | true, i -> Some i
            | _ -> None)

    10 * (Seq.head numbers) + (Seq.last numbers)

printfn $"Result Part 1: {input |> Array.map getNumberFromLine1 |> Array.sum}"

open System.Text.RegularExpressions

let digitParser s =
    [ for m in Regex.Matches(s, @"\d") -> (m.Value |> System.Int32.Parse, m.Index) ]

let numbers =
    [ ("one", 1)
      ("two", 2)
      ("three", 3)
      ("four", 4)
      ("five", 5)
      ("six", 6)
      ("seven", 7)
      ("eight", 8)
      ("nine", 9) ]

let allParsers =
    [ digitParser
      for (word, number) in numbers do
          (fun s -> [ for m in Regex.Matches(s, word) -> (number, m.Index) ]) ]

let getNumbersFromLine2 (line: string) =
    [ for parser in allParsers do
          yield! parser line ]
    |> List.sortBy snd
    |> List.map fst

let result2 =
    input
    |> Array.map getNumbersFromLine2
    |> Array.map (fun is -> 10 * (List.head is) + (List.last is))
    |> Array.sum

printfn $"Result Part 1: {result2}"
